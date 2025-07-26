using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScreamerUI3D : MonoBehaviour
{
    [SerializeField] private AudioSource _screamerSound;
    [SerializeField] private Image _screamer;
    [SerializeField] private float _blinkingDuration = 5f;
    [SerializeField] private float _gameResultAppearTime = 2f;

    [SerializeField] private Image _gameResultBG;
    [SerializeField] private TMP_Text _gameResultText;

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
            float alpha = Random.Range(0.1f, 0.4f);
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
            float alpha = Mathf.Lerp(0, 1, timer / _gameResultAppearTime);
            _gameResultBG.color = new Color(_screamer.color.r, _screamer.color.g, _screamer.color.b, alpha);
            _gameResultText.color = new Color(_gameResultText.color.r, _gameResultText.color.g, _gameResultText.color.b, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Menu");
    }
}
