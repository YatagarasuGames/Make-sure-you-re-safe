using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ChoiceButtonSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;

    public void OnDestroy() {
        AudioSource.PlayClipAtPoint(_audioClips[Random.Range(0, _audioClips.Count)], GameObject.FindWithTag("Player").transform.position);
    }
}
