using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSystem/Nodes/Choice Dialogue node")]
public class ChoiceDialogueNode : DialogueNode
{
    [field: SerializeField] private DialogueChoice[] choices;
    public DialogueChoice[] Choices => choices;

    public override void Accept(IDialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
