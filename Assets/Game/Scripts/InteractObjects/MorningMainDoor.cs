using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MorningMainDoor : MonoBehaviour, IInteract
{
    [SerializeField] private string _nextSceneName;
    [Inject] private FirstPersonLook _playerCamera;
    public void Interact()
    {
        _playerCamera.Blink();
        StartCoroutine(TranslateToNextScene());
    }

    private IEnumerator TranslateToNextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_nextSceneName);
    }
}
