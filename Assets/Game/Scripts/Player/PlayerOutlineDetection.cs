using UnityEngine;
using Zenject;

public class PlayerOutlineDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _outlinableLayers;
    [Inject] private FirstPersonLook _playerCamera;
    private Outline _outlinableObject = null;
    private void Update()
    {
        DetectOutline();
    }

    private void DetectOutline()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hitObject, 1.5f))
        {
            if (_outlinableLayers.Contains(hitObject.collider.gameObject.layer))
            {
                if (_outlinableObject == null)
                {
                    if (hitObject.collider.gameObject.TryGetComponent(out Outline outline)) { _outlinableObject = outline; outline.enabled = true; }
                    else Debug.LogError("Interactable object do not have Outline component");
                }
            }
            else
            {
                if (_outlinableObject != null) { _outlinableObject.enabled = false; _outlinableObject = null; }
            }
        }
        else if (_outlinableObject != null) { _outlinableObject.enabled = false; _outlinableObject = null; }
    }
}
