using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public MouseLook playerVision;
    bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject exitMenu;
    public GameObject TaskMenus;
    public GameObject instructionsMenu;

    public bool trashtaskopen = false;
    public bool electricaltaskopen = false;
    public bool instructionsOpen = false;
    public bool exitMenuOpen = false;

    public GameObject trashInstructions;
    public GameObject electricalInstructions;


    public void PauseOnOff()
    {
        if(gamePaused) //PauseOff
        {
            gamePaused = false;
            playerMovement.controller.enabled = true;
            playerVision.mouseSensitivity = 100f;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else //PauseOn
        {
            gamePaused = true;
            playerMovement.controller.enabled = false;
            playerVision.mouseSensitivity = 0f;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ReturnToMainScene()
    {
        TaskMenus.SetActive(false);
        playerMovement.taskMenusActive = false;
        SceneManager.LoadScene("StartScene");
    }

    public void ExitAcces()
    {
        exitMenu.SetActive(true);
        pauseMenu.SetActive(false);
        exitMenuOpen = true;
    }

    public void InstructionsAcces()
    {
        instructionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        instructionsOpen = true;
    }

    public void Back()
    {
        if(exitMenuOpen)
        {
            exitMenu.SetActive(false);
        }
        else if (instructionsOpen)
        {
            instructionsMenu.SetActive(false);
        } 
        pauseMenu.SetActive(true);
    }

    public void AccessTrashInstructions()
    {
        if(electricaltaskopen)
        {
            electricaltaskopen = false;
            electricalInstructions.SetActive(false); 
        }

        trashtaskopen = true;
        trashInstructions.SetActive(true);
    }

    public void AccessElectricalInstructions()
    {
        if(trashtaskopen)
        {
            trashtaskopen = false;
            trashInstructions.SetActive(false);
        }

        electricaltaskopen = true;
        electricalInstructions.SetActive(true);
    }
}
