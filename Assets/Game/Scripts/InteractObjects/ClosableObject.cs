using UnityEngine;

public abstract class ClosableObject : MonoBehaviour, IInteract
{
    public bool IsClosed { get; protected set; } = false;
    public bool CanInteract { get;  set; } = false;

    public virtual void Interact()
    {
        if (!CanInteract) return;
        Close();
    }

    protected virtual void Close() { }
}
