import re
import time
import cv2
import pytesseract
import serial

PORT = "COM3"       # Change to your Arduino's port
CAMERA_NUMBER = 0   # Try 1 if the webcam does not open

TARGET_CODES = ["1011", "1022"]  # Left cube, then right cube

# On Windows, if needed, uncomment this and update the path:
# pytesseract.pytesseract.tesseract_cmd = (
#     r"C:\Program Files\Tesseract-OCR\tesseract.exe"
# )

# These are the two camera areas where the labels are expected.
# Adjust these numbers if your cubes appear in different places.
LEFT_ROI = (0.05, 0.25, 0.48, 0.75)
RIGHT_ROI = (0.52, 0.25, 0.95, 0.75)

arduino = serial.Serial(PORT, 9600, timeout=0.01)
time.sleep(2)  # The Arduino restarts when the serial connection opens

camera = cv2.VideoCapture(CAMERA_NUMBER)
occupied = [False, False]
last_command = ""


def read_four_digits(image):
    """Try to read a four-digit code from one camera crop."""
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    gray = cv2.resize(
        gray, None, fx=3, fy=3, interpolation=cv2.INTER_CUBIC
    )

    _, clean_image = cv2.threshold(
        gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU
    )

    settings = (
        "--oem 3 --psm 7 "
        "-c tessedit_char_whitelist=0123456789"
    )

    text = pytesseract.image_to_string(clean_image, config=settings)
    digits = re.sub(r"\D", "", text)

    if len(digits) == 4:
        return digits

    return ""


def crop_area(frame, roi):
    """Crop a rectangle using proportions of the camera image."""
    height, width = frame.shape[:2]
    x1, y1, x2, y2 = roi

    left = int(x1 * width)
    top = int(y1 * height)
    right = int(x2 * width)
    bottom = int(y2 * height)

    return frame[top:bottom, left:right], (left, top, right, bottom)


while True:
    # Read the Arduino's latest foil sensor message.
    while arduino.in_waiting:
        line = arduino.readline().decode(errors="ignore").strip()

        if line.startswith("P:") and len(line) >= 4:
            occupied = [line[2] == "1", line[3] == "1"]

    success, frame = camera.read()

    if not success:
        print("Could not read from the webcam.")
        break

    left_crop, left_box = crop_area(frame, LEFT_ROI)
    right_crop, right_box = crop_area(frame, RIGHT_ROI)

    left_code = read_four_digits(left_crop)
    right_code = read_four_digits(right_crop)

    # Wait until both foil sensors detect cubes and both codes are read.
    if not all(occupied):
        command = "WAIT"
        message = "Place both cubes in the spaces"
    elif not left_code or not right_code:
        command = "WAIT"
        message = "Camera cannot read both codes yet"
    elif [left_code, right_code] == TARGET_CODES:
        command = "CORRECT"
        message = "Correct!"
    else:
        command = "WRONG"
        message = "Incorrect"

    # Send a command only when the result changes.
    if command != last_command:
        arduino.write((command + "\n").encode())
        last_command = command

    # Show the camera areas and the codes it read.
    cv2.rectangle(frame, left_box[:2], left_box[2:], (255, 0, 0), 2)
    cv2.rectangle(frame, right_box[:2], right_box[2:], (0, 0, 255), 2)

    cv2.putText(
        frame,
        f"Left: {left_code or '?'}  Right: {right_code or '?'}",
        (20, 35),
        cv2.FONT_HERSHEY_SIMPLEX,
        0.7,
        (0, 0, 0),
        2
    )

    cv2.putText(
        frame,
        message,
        (20, 70),
        cv2.FONT_HERSHEY_SIMPLEX,
        0.7,
        (0, 128, 0),
        2
    )

    cv2.imshow("Puzzle camera", frame)

    # Press Q to close the program.
    if cv2.waitKey(1) & 0xFF == ord("q"):
        break

camera.release()
arduino.close()
cv2.destroyAllWindows()
