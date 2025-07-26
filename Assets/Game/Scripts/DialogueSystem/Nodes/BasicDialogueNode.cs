using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/Nodes/Basic Dialogue node")]
public class BasicDialogueNode : DialogueNode
{
    [SerializeField] private DialogueNode nextNode;
    public DialogueNode NexnNode => nextNode;

    public override void Accept(IDialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
