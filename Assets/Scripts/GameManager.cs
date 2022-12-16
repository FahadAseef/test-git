using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text ScoreText;
    public float Score=0;
    public float scoreSpeed=5;

    public GameObject escapePanel;
    public GameObject diePanel;
    public AudioSource audioSource;
    //public AudioClip DieAudio;
    //public AudioClip RunTimeAudio;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        escapePanel.SetActive(false);
        diePanel.SetActive(false);
        //audioSource.clip = RunTimeAudio;
        //audioSource.Play();
    }

    public void ResumeFN()
    {
        Time.timeScale = 1;
        escapePanel.SetActive(false);
        audioSource.enabled = true;
    }

    public void MainMenuFN()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitFN()
    {
        Application.Quit();
    }

    public void playAgainFN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Time.timeScale = 0;
        //    escapePanel.SetActive(true);
        //    audioSource.enabled = false;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideOptions();
        }
        if (escapePanel.activeInHierarchy && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void ShowHideOptions()
    {
        if (!escapePanel.activeInHierarchy)
        {
            escapePanel.SetActive(true);
            Time.timeScale = 0;
            audioSource.enabled = false;
        }
        else
        {
            escapePanel.SetActive(false);
            Time.timeScale = 1;
            audioSource.enabled = true;
        }
        
    }

    private void FixedUpdate()
    {
        Score += Time.deltaTime*scoreSpeed;
        ScoreText.text = ((int)Score).ToString();
    }


}
