using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SceneChange", 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneChange()
    {

        if (SceneManager.GetActiveScene().name == "Chapter3Hit")
            SceneManager.LoadScene("Title");
        if (SceneManager.GetActiveScene().name=="Chapter2Hit")
            SceneManager.LoadScene("Title");
        if (SceneManager.GetActiveScene().name == "Chapter1Hit")
            SceneManager.LoadScene("Title");
        if (SceneManager.GetActiveScene().name == "Chapter1Clear")
            SceneManager.LoadScene("Chapter2");
        if (SceneManager.GetActiveScene().name == "Chapter3NormalClear")
            Invoke("totitle", 5.0f);

    }
    void totitle()
    {
        SceneManager.LoadScene("Title");
    }
}
