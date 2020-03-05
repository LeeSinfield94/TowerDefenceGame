using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel, LevelsPenel;   
    public void ChooseLevel()
    {
        MainMenuPanel.SetActive(false);
        LevelsPenel.SetActive(true);
    }
    public void BackToMenu()
    {
        MainMenuPanel.SetActive(true);
        LevelsPenel.SetActive(false);
    }

    public void OpenStuLevel() { SceneManager.LoadScene("Stu"); }
    public void OpenLeeLevel() { SceneManager.LoadScene("Lee"); }
        
}
