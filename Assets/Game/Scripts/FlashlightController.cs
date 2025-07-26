using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light _flashlight;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    _flashlight.enabled = !_flashlight.enabled;
        //    _audioSource.Play();
        //}
    }
}
