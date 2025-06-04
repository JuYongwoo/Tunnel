using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //public GameObject parent;
    public float rx;
    public float ry;
    public float rotSpeed = 70;
    public Vector3 forshare;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //1. 마우스 입력 값을 이용한다.
        float mx = Input.GetAxis("Mouse X"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 오른 양수
        float my = Input.GetAxis("Mouse Y"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 위 양수

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        //rx 회전 각을 제한
        rx = Mathf.Clamp(rx, -80, 80);

        //transform.parent.gameObject.transform.eulerAngles = new Vector3(-rx, ry, 0);
        transform.eulerAngles = new Vector3(-rx, ry, 0); //X축의 회전은 양수가 증가되면 아래, 음수가 증가되면 위로

    }

}
