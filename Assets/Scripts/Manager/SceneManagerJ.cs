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


        // 현재 씬에서의 Player 오브젝트
        ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); //전환되는 씬마다의 새로운 플레이어를 찾는다.


        //트리거이벤트 초기화
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
