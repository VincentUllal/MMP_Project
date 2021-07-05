using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu, optionsMenu;

    public void startGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
