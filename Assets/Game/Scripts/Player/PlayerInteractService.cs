using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInteractService : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject _menu;
    [Inject] private FirstPersonLook _playerCamera;
    [Inject] private FirstPersonMovement _playerMovement;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Interact();
        if (Input.GetKeyDown(KeyCode.Escape)) OpenMenu();
    }

    public void Interact()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hitObject, 1.5f))
        {
            if (hitObject.collider.gameObject.TryGetComponent(out IInteract interact)) interact.Interact();
        }
    }

    private void OpenMenu()
    {
        Time.timeScale = 0f;
        enabled = false;
        _playerCamera.enabled = false;
        _playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        _menu.SetActive(true);
    }
}
