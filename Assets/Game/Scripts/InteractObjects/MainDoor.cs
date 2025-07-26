using UnityEngine;
using Zenject;

public class MainDoor : ClosableObject
{
    [SerializeField] private AudioSource _audioSource;
    [Inject] private DialogueUIBox _dialogueStarter;
    [SerializeField] private Dialogue _canNotInteractDialogue;
    [SerializeField] private Dialogue _alreadyInteractDialogue;
    public override void Interact()
    {
        if (!CanInteract) { _dialogueStarter.StartDialogue(_canNotInteractDialogue); return; }
        Close();
    }
    protected override void Close()
    {
        if (IsClosed) { _dialogueStarter.StartDialogue(_alreadyInteractDialogue); return; }
        base.Close();
        IsClosed = !IsClosed;
        _audioSource.Play();
    }
}
