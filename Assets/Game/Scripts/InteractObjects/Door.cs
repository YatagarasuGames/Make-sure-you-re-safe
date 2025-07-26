using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private Transform _rotator;
    private bool _isOpen = false;
    private bool _isOpening = false;
    [SerializeField] private AudioSource _source;

    public void Interact()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (_isOpening) return;
        _source.Play();
        if (_isOpen)
        {
            _isOpening = true;
            _rotator.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f).SetLink(_rotator.gameObject)
            .OnComplete(() =>
            {
                _isOpening = false;
                _isOpen = false;
            });

        }
        else
        {
            _isOpening = true;
            _rotator.transform.DOLocalRotate(new Vector3(0, -90, 0), 1f).SetLink(_rotator.gameObject)
            .OnComplete(() =>
            {
                _isOpening = false;
                _isOpen = true;
            });

        }
    }
}
