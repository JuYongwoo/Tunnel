using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static event Action InGameUIOff;
    void Start()
    {
        Invoke("LoadMainScene", 1f);

        InGameUIOff();

    }

    void Update()
    {
        
    }
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
