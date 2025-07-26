using UnityEngine;
using Zenject;

public class ClosableObjectsEnablerTrigger : MonoBehaviour
{
    private ClosableObject[] _closableObjects;
    [Inject] private Bed _bed;
    private void Start()
    {
        _closableObjects = FindObjectsOfType<ClosableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) EnableClosableObjects();
    }

    public void EnableClosableObjects()
    {
        foreach (ClosableObject closableObject in _closableObjects) closableObject.CanInteract = true;
        _bed.CanInteract = true;
    }
}
