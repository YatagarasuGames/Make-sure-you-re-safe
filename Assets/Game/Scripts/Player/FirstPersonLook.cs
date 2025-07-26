using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] Transform character;
    [SerializeField] public float sensitivity = 2;
    [SerializeField] public float smoothing = 1.5f;

    private Vector2 velocity;
    private Vector2 frameVelocity;

    [SerializeField] private Image _blink;
    [SerializeField] private float _blinkDuration;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Unblink();
        SettingsMenu.OnSensitivityChanged += ChangeSensitivity;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.5f);
    }

    void Update()
    {
        RotateCamera();
    }

    private void ChangeSensitivity(float newValue) => sensitivity = newValue;

    private void RotateCamera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine(false));
    }
    public void Unblink()
    {
        StartCoroutine(BlinkCoroutine(true));
    }

    private IEnumerator BlinkCoroutine(bool openEyes)
    {
        float startAlpha = _blink.color.a;
        float targetAlpha = openEyes ? 0f : 1f;
        float elapsedTime = 0f;

        while (elapsedTime < _blinkDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _blinkDuration);

            Color newColor = _blink.color;
            newColor.a = newAlpha;
            _blink.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Color finalColor = _blink.color;
        finalColor.a = targetAlpha;
        _blink.color = finalColor;
    }

    private void OnDestroy()
    {
        SettingsMenu.OnSensitivityChanged -= ChangeSensitivity;
    }
}
