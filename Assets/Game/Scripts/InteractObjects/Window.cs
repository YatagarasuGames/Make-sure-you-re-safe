using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Window : ClosableObject
{
    protected bool _isOpening;
    [SerializeField] private Transform _rotator;
    [SerializeField] private AudioSource _audioSource;
    [Inject] private DialogueUIBox _dialogueStarter;
    [SerializeField] private Dialogue _canNotInteractDialogue;

    public override void Interact()
    {
        if (!CanInteract) { _dialogueStarter.StartDialogue(_canNotInteractDialogue); return; }
        Close();
    }
    protected override void Close()
    {
        base.Close();
        if (_isOpening) return;
        _audioSource.Play();
        if (IsClosed)
        {
            _isOpening = true;
            _rotator.transform.DOLocalRotate(new Vector3(0, 60, 0), 1f).SetLink(_rotator.gameObject)
            .OnComplete(() =>
            {
                _isOpening = false;
                IsClosed = false;
            });

        }
        else
        {
            _isOpening = true;
            _rotator.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f).SetLink(_rotator.gameObject)
            .OnComplete(() =>
            {
                _isOpening = false;
                IsClosed = true;
            });

        }
    }
}
