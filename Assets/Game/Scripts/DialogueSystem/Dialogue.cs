using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private DialogueNode firstNode;
    public DialogueNode FirstNode => firstNode;
}
