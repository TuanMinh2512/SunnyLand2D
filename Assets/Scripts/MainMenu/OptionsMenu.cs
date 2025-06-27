using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void VolumeSetting()
    {
        Debug.Log("Volume button clicked");
        // Ban co the them logic dieu chinh am thanh sau
    }
}
