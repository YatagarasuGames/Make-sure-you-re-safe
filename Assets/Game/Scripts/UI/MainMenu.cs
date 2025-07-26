using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartGame() => SceneManager.LoadScene("FirstDay");
    public void Exit() => Application.Quit();
}
