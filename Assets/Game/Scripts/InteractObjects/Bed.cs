using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Bed : MonoBehaviour, IInteract
{
    [SerializeField] public bool CanInteract;
    [SerializeField] private List<ClosableObject> _closableObjects;
    [Inject] private FirstPersonLook _playerCamera;
    [SerializeField] private Transform _layViewPoint;
    [SerializeField] private string _nextSceneName;
    private bool _isSleeping = false;

    private Vector3 _cameraOriginal;

    [SerializeField] private AudioSource _audioSource;

    private int _answersGiven = 0;

    [Inject] private DialogueUIBox _dialogueStarter;
    [SerializeField] private Dialogue _canNotInteractDialogue;
    [SerializeField] private Dialogue _nightChoicesDialogue;


    private void OnEnable()
    {
        DialogueChoiceBox.onNextNodeRequest += HandleAnswerGiven;
        DialogueUIBox.onDialogueEnd += HandleDialogueEnd;
    }

    private void HandleAnswerGiven(DialogueNode node) { if (_isSleeping) ++_answersGiven; print("answerGiven"); }

    private void HandleDialogueEnd()
    {
        if (!_isSleeping) return;
        print("dialogueEnd");
        if (_answersGiven == 1)
        {
            _playerCamera.transform.DORotate(Quaternion.identity.eulerAngles, 1).SetLink(_playerCamera.gameObject);
            _playerCamera.transform.DOMove(_cameraOriginal, 1).SetLink(_playerCamera.gameObject);
            _playerCamera.Unblink();
        }
        if (_answersGiven == 2)
        {
            StartCoroutine(ChangeScene());
        }
        _answersGiven = 0;


    }

    private IEnumerator HandleDialogueEndDelayed()
    {
        yield return null;
        
    }

    private IEnumerator ChangeScene()
{
    yield return new WaitForSeconds(0.75f);
    if (_closableObjects.Any(obj => obj.IsClosed == false)) SceneManager.LoadScene("NightDie");
    else SceneManager.LoadScene(_nextSceneName);
}

    public void Interact()
    {
        if (!CanInteract) { _dialogueStarter.StartDialogue(_canNotInteractDialogue); return; }
        LayOnBed();
    }

    private void LayOnBed()
    {
        _audioSource.Play();
        _isSleeping = true;
        _cameraOriginal = _playerCamera.transform.position;
        _playerCamera.transform.DORotate(_layViewPoint.rotation.eulerAngles, 1).SetLink(_playerCamera.gameObject);
        _playerCamera.transform.DOMove(_layViewPoint.position, 1).SetLink(_playerCamera.gameObject);
        _playerCamera.Blink();
        Invoke(nameof(StartDialogue), 1.75f);
    }

    private void StartDialogue()
    {
        _dialogueStarter.StartDialogue(_nightChoicesDialogue);
    }

    private void OnDisable()
    {
        DialogueChoiceBox.onNextNodeRequest -= HandleAnswerGiven;
        DialogueUIBox.onDialogueEnd -= HandleDialogueEnd;
    }
}
