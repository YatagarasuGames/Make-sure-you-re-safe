using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsGameObject;
    [Inject] private PlayerStateController _stateController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Continue();
    }

    public void Continue()
    {
        _settingsGameObject.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        _stateController.ChangeState(PlayerStateType.Game);
    }
    public void OpenSettings()
    {
        gameObject.SetActive(false);
        _settingsGameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
