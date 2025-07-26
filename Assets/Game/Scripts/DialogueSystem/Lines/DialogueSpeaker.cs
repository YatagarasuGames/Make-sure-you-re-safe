using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/Speaker")]
public class DialogueSpeaker : ScriptableObject
{
    [SerializeField] private string speakerName;
    public string SpeakerName => speakerName;
}
