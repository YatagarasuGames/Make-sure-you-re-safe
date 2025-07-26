using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectableFlashlight : MonoBehaviour, IInteract
{
    [SerializeField] private AudioClip _flashlightCollectSound;
    [SerializeField] private GameObject _flashlightController;
    [Inject] private FirstPersonLook _playerCamera;
    public void Interact()
    {
        AudioSource.PlayClipAtPoint(_flashlightCollectSound, transform.position);
        Instantiate(_flashlightController, _playerCamera.transform);
        Destroy(gameObject);
    }
}
