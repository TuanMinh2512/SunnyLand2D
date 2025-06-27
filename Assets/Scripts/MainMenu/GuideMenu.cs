using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
