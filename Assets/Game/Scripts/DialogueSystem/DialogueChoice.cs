using System;
using UnityEngine;

[Serializable] public class DialogueChoice
{
    [SerializeField] private string choiceName;
    [SerializeField] private DialogueNode choiceNode;

    public string ChoiceName => choiceName;
    public DialogueNode ChoiceNode => choiceNode;
}
