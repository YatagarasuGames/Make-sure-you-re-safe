using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField] private float speed = 1.5f;
    private Vector3 _gravityVelocity = new Vector3(0, -9.81f, 0);
    private bool _isMoving;
    [Inject] private FirstPersonLook _camera;
    private Vector3 _cameraOriginalPosition;
    private CharacterController _characterController;

    [Header("Headbob Params")]
    [SerializeField] private float _walkBobSpeed = 14f;
    [SerializeField] private float _walkBobAmount = 0.05f;
    private float _headbobTimer;
    [SerializeField] private float _headBobSmoothness = 10;

    [SerializeField] private AudioSource _footstepsAudioSource;
    [SerializeField] private List<AudioClip> _footStepSounds;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraOriginalPosition = _camera.transform.localPosition;
    }
    private Vector3 velocity;
    private float gravity = -3.5f;
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _characterController.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime);

        _isMoving = move.magnitude > 0.1f;
        HandleHeadBob();
        HandleFootsteps();
    }

    private void HandleHeadBob()
    {
        if (_isMoving)
        {
            _headbobTimer += Time.deltaTime * _walkBobSpeed;
            Vector3 cameraMoveStep = new Vector3(Mathf.Cos(_headbobTimer * 0.5f) * _walkBobAmount, Mathf.Sin(_headbobTimer) * _walkBobAmount, 0f);
            Vector3 newCameraPosition = _cameraOriginalPosition + cameraMoveStep;
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, newCameraPosition, Time.deltaTime * _headBobSmoothness);
        }
        else
        {
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, _cameraOriginalPosition, Time.deltaTime * _headBobSmoothness);
        }

    }

    private void HandleFootsteps()
    {
        if (_isMoving && !_footstepsAudioSource.isPlaying)
        {
            AudioClip stepSound = _footStepSounds[Random.Range(0, _footStepSounds.Count)];
            _footstepsAudioSource.clip = stepSound;
            _footstepsAudioSource.PlayDelayed(0.2f);
            float time = Random.Range(0.4f, 0.5f);
            Invoke("HandleFootsteps", time);
        }
    }

    private void OnDisable() {
        _footstepsAudioSource.Stop();
    }

}