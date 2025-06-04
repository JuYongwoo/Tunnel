using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public GameObject keepbutton;
    // Start is called before the first frame update
    void Start()
    {
        int chap = PlayerPrefs.GetInt("chap");
        if(chap==0)
        keepbutton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameKeep()
    {
        int chap = PlayerPrefs.GetInt("chap");
        switch (chap)
        {
            case 1:
                SceneManager.LoadScene("Chapter1");
                break;
            case 2:
                SceneManager.LoadScene("Chapter2");
                break;
            case 3:
                SceneManager.LoadScene("Chapter3");
                break;

        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
