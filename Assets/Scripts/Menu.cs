using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Menu : MonoBehaviour
{

    public RectTransform MainPanel;

    public RectTransform SettingsPanel;
    public RectTransform GifPlane;

    public Animator Gif;

    void Start()
    {
        
    }


    public void StartGame()
    {
        MainPanel.gameObject.SetActive(false);
        GifPlane.gameObject.SetActive(true);
        
        StartCoroutine(PlayGif());
        
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

    IEnumerator PlayGif()
    {
        Gif.Play("StartGif");
        yield return new WaitForSeconds(1f);
        Gif.enabled = false;
        SceneManager.LoadScene("childhood");
    }
}
