using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            gameObject.tag = "Crouching"; //컨트롤 누를 시 해당 오브젝트(플레이어)의 태그 변경
            gameObject.GetComponent<PlayerMove>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            gameObject.tag = "Player";
            gameObject.GetComponent<PlayerMove>().enabled = true;
        }

    }
}
