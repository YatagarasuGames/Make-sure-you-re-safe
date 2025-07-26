using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MonsterJump : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _jumpOffset;
    [SerializeField] private AudioClip _screamerClip;
    private void Start()
    {
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        transform.rotation = Quaternion.Euler(0, 90, 0);
        AudioSource.PlayClipAtPoint(_screamerClip, transform.position);
        transform.DOLocalMove(_player.transform.position + _jumpOffset, 0.2f);
    }
}
