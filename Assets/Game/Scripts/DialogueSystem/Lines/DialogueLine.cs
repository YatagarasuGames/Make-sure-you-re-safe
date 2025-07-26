using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/Line")]
public class DialogueLine : ScriptableObject
{
    [SerializeField] private DialogueSpeaker speaker;
    [SerializeField] private string text;

    public DialogueSpeaker Speaker => speaker;
    public string Text => text;
}
