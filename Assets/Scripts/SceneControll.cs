using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }

    public void PauseOrResume(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
        Time.timeScale = Time.timeScale == 1 ? 0f : 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}