using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreamerUI : MonoBehaviour
{
    [SerializeField] private AudioSource _screamerSound;
    [SerializeField] private Image _screamer;
    [SerializeField] private float _blinkingDuration = 5f;
    [SerializeField] private float _gameResultAppearTime = 2f;

    [SerializeField] private GameObject _gameResult;
    [SerializeField] private Image _bg;

    private void OnEnable()
    {
        StartCoroutine(DoScreamerBlink());
    }

    private IEnumerator DoScreamerBlink()
    {
        _screamerSound.Play();
        float blinkingTimer = 0f;

        while (blinkingTimer < _blinkingDuration)
        {
            blinkingTimer += Time.deltaTime;
            float alpha = Random.Range(0.1f, 1f);
            _screamer.color = new Color(_screamer.color.r, _screamer.color.g, _screamer.color.b, alpha);
            yield return null;
        }
        StartCoroutine(ShowGameResult());
    }

    private IEnumerator ShowGameResult()
    {
        float timer = 0f;
        while (timer < _gameResultAppearTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, timer / _gameResultAppearTime);
            _screamer.color = new Color(_screamer.color.r, _screamer.color.g, _screamer.color.b, alpha);
            _bg.color = new Color(_bg.color.r, _bg.color.g, _bg.color.b, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Menu");
    }
}
