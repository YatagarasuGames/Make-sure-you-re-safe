using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DialogueNode : ScriptableObject
{
    [SerializeField] private DialogueLine dialogueLine;
    public DialogueLine DialogueLine => dialogueLine;

    public abstract void Accept(IDialogueNodeVisitor visitor);
}
