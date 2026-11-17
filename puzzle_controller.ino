const byte slotPins[2] = {2, 3};
const byte greenLed = 8;
const byte redLed = 9;

void setup() {
  Serial.begin(9600);
  Serial.setTimeout(10);

  // The input reads LOW when a cube bridges the foil strips.
  pinMode(slotPins[0], INPUT_PULLUP);
  pinMode(slotPins[1], INPUT_PULLUP);

  pinMode(greenLed, OUTPUT);
  pinMode(redLed, OUTPUT);
}

void loop() {
  // Send whether the left and right spaces are occupied.
  Serial.print("P:");
  Serial.print(digitalRead(slotPins[0]) == LOW ? '1' : '0');
  Serial.println(digitalRead(slotPins[1]) == LOW ? '1' : '0');

  // Receive the result from the laptop.
  if (Serial.available()) {
    String command = Serial.readStringUntil('\n');
    command.trim();

    if (command == "CORRECT") {
      digitalWrite(greenLed, HIGH);
      digitalWrite(redLed, LOW);
    } else if (command == "WRONG") {
      digitalWrite(greenLed, LOW);
      digitalWrite(redLed, HIGH);
    } else if (command == "WAIT") {
      digitalWrite(greenLed, LOW);
      digitalWrite(redLed, LOW);
    }
  }

  delay(100);
}

