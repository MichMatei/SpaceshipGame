using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionMenu;
    public GameObject exitMenu;

    public bool optionsMenuOpen = false;
    public bool exitMenuOpen = false;
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionsAcces()
    {
        startMenu.active = false;
        optionMenu.active = true;
        optionsMenuOpen = true;
    }

    public void Back()
    {
        if(optionsMenuOpen)
        {
            optionsMenuOpen = false;
            optionMenu.active = false;
            startMenu.active = true;
        }
        else
        {
            exitMenu.active = false;
            exitMenuOpen = false;
            startMenu.active = true;
        }
    }

    public void ExitAcces()
    {
        startMenu.active = false;
        exitMenu.active = true;
        exitMenuOpen = true;
    }

    public void EndApplication()
    {
        Application.Quit();
    }
}
