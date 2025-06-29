using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterNameManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public void ConfirmName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName); // Luu ten
            PlayerPrefs.Save();

            SceneManager.LoadScene("Level1"); // Chuyen sang Level1
        }
        else
        {
            Debug.Log("You haven't entered your name!");
        }
    }
}
