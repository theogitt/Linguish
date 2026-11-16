using UnityEngine;

public class CustomAction : MonoBehaviour
{
    public enum ActionType
    {
        ChatBubble,
        Move
    }

    public ActionType actionType;

    public ChatBubbleParams chatBubbleParams;
    [System.Serializable]
    public class ChatBubbleParams
    {
        public GameObject gameObject;
        public string text;
        public CustomAction onChatBubbleComplete;
    }

    public MoveParams moveParams;
    [System.Serializable]
    public class MoveParams
    {
        public GameObject unitGameObject;
        public Vector3 movePosition;
        public CustomAction onReachedPosition;
    }

    public void ExecuteAction()
    {
        switch (actionType)
        {
            case ActionType.ChatBubble:
                chatBubbleParams.ExecuteAction();
                break;
            case ActionType.Move:
        }
    }
}
