using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    //public static UImanager instance;

    // private void Awake()
    // {
    //     instance = this;
    // }

    [SerializeField] GameObject costumePanel;
    [SerializeField] GameObject LoadingPanel;
    Camera Cam;
    private Vector3 newPosition;
    private Vector3 oldPosition;

    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject QuitButton;
    [SerializeField] GameObject costumeButton;
    [SerializeField] GameObject titletextButton;

    [SerializeField] GameObject costumeText;
    [SerializeField] GameObject backButton;

    [SerializeField] GameObject selectButton;

    private void Start()
    {
        costumePanel.SetActive(false);
        LoadingPanel.SetActive(false);
        Cam = Camera.main;
        newPosition = new Vector3(1000, 1000, 1000);
        oldPosition = new Vector3(0, 0, 0);
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        costumeButton.SetActive(true);
        titletextButton.SetActive(true);   
        costumeText.SetActive(false);
        backButton.SetActive(false);
        selectButton.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void CostumeButton()
    {
        //costumePanel.SetActive(true);
        Cam.transform.position = newPosition;
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        costumeButton.SetActive(false);
        titletextButton.SetActive(false);
        costumeText.SetActive(true);
        backButton.SetActive(true);
        selectButton.SetActive(true);
    }

    public void BackButton()
    {
        //costumePanel.SetActive(false);
        Cam.transform.position = oldPosition;
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        costumeButton.SetActive(true);
        titletextButton.SetActive(true);
        costumeText.SetActive(false);
        backButton.SetActive(false);
        selectButton.SetActive(false);
    }


}
