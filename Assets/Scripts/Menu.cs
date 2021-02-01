using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    public RectTransform MainPanel;

    public RectTransform SettingsPanel;

    void Start()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        MainPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu(RectTransform current)
    {
        current.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
