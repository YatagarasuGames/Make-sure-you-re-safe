using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OnInSightTrigger : MonoBehaviour
{
    [Inject] private FirstPersonLook _player;
    private Camera _playerCamera;
    [SerializeField] private AudioSource _audioSource;
    private Renderer objectRenderer;
    private bool _triggered = false;
    [SerializeField] private bool _mustBeDestroyed = true;

    private void OnEnable()
    {
        objectRenderer = GetComponent<Renderer>();
        _playerCamera = _player.GetComponent<Camera>();
    }
    private void Update()
    {
        var cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_playerCamera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, objectRenderer.bounds) && !_triggered && CheckDirectView(_playerCamera))
        {
            _triggered = true;
            _audioSource.Play();
            StartCoroutine(DisableTargetObject());
        }
    }

    private bool CheckDirectView(Camera playerCamera)
    {
        Vector3 normalized = (transform.position - playerCamera.transform.position).normalized;
        float maxDistance = Vector3.Distance(playerCamera.transform.position, transform.position);
        if (Physics.Raycast(playerCamera.transform.position, normalized, out var hitInfo, maxDistance))
        {
            return hitInfo.collider.gameObject == gameObject;
        }
        return false;
    }
    
	private IEnumerator DisableTargetObject()
	{
		yield return new WaitForSeconds(1f);
        if(_mustBeDestroyed) gameObject.SetActive(false);
	}
}
