using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenResolution : MonoBehaviour
{
    //public GameObject InventoryForLock;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1600, 900, false);

        if (SceneManager.GetActiveScene().name == "Title")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
