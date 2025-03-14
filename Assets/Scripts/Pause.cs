using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject menuList;

    [SerializeField] private bool menuKeys = true;
    [SerializeField] private AudioSource bgmSound;

    void Start()
    {
        
    }

    void Update()
    {

        if (menuKeys)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                menuList.SetActive(true);
                menuKeys = false;
                Time.timeScale = (0); 
                bgmSound.Pause(); 
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuList.SetActive(false);
            menuKeys = true;
            Time.timeScale = (1); 
            bgmSound.Play(); 
        }

    }

    public void Return ()
    {
        menuList.SetActive(false);
        menuKeys = true;
        Time.timeScale = (1); 
        bgmSound.Play(); 
    }

    public void Restart ()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = (1);
    }

    public void Exit ()
    {
        SceneManager.LoadScene(0);
        Application.Quit();
    }
}
