using System.ComponentModel;
using UnityEngine;
using Zenject;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [Inject] private DialogueUIBox _dialogueStarter;
    private bool _wasTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_wasTriggered) return;
        if (!other.CompareTag("Player")) return;
        CreateDialogueWindow();
    }

    private void CreateDialogueWindow()
    {
        _wasTriggered = true;
        _dialogueStarter.StartDialogue(_dialogue);
    }
}
