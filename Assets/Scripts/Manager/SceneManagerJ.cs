using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerJ
{
    [HideInInspector]
    public GameObject ThisScenePlayer;


    public void OnAwake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver") return;


        // ���� �������� Player ������Ʈ
        ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); //��ȯ�Ǵ� �������� ���ο� �÷��̾ ã�´�.


        //Ʈ�����̺�Ʈ �ʱ�ȭ
        ManagerObject.TriggerEvent.ClearAllTriggerEvents();
        ManagerObject.TriggerEvent.LoadCurrentSceneTriggerEvents();
        ManagerObject.TriggerEvent.mappingAlltriggerEvents();

        

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


        ManagerObject.Sound.StopMusic();

    }
}
