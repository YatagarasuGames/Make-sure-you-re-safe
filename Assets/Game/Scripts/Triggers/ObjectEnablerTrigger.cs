using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnablerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _newState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) _gameObject.SetActive(_newState);
        gameObject.SetActive(false);
    }
}
