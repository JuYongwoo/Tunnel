using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //public GameObject parent;
    public float rx;
    public float ry;
    public float rotSpeed = 70;
    public Vector3 forshare;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        //rx 회전 각을 제한
        rx = Mathf.Clamp(rx, -80, 80);

        //transform.parent.gameObject.transform.eulerAngles = new Vector3(-rx, ry, 0);
        transform.eulerAngles = new Vector3(-rx, ry, 0); //X축의 회전은 양수가 증가되면 아래, 음수가 증가되면 위로



        if (ManagerObject.Scene.ThisScenePlayer == null) return;

        //transform.parent.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);

        if (Input.GetKey(KeyCode.LeftControl)) // 컨트롤키 누르면 앉기, 카메라 위치 0.5 아래로
            transform.position = new Vector3(ManagerObject.Scene.ThisScenePlayer.transform.position.x, ManagerObject.Scene.ThisScenePlayer.transform.position.y - 0.5f, ManagerObject.Scene.ThisScenePlayer.transform.position.z);
        else
            transform.position = new Vector3(ManagerObject.Scene.ThisScenePlayer.transform.position.x, ManagerObject.Scene.ThisScenePlayer.transform.position.y, ManagerObject.Scene.ThisScenePlayer.transform.position.z);


    }

}
