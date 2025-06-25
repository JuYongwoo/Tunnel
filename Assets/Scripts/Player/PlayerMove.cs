using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 150;
    CharacterController charCtrl;

    public GameObject moveaudioobject;
    AudioSource moveaudiosource;
    float movetime = 0.5f;
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        moveaudiosource = moveaudioobject.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {

        transform.eulerAngles = new Vector3(0, UnityEngine.Camera.main.GetComponent<Camera>().ry, 0);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            gameObject.tag = "Crouching"; //컨트롤 누를 시 해당 오브젝트(플레이어)의 태그 변경
            return;
        }
        else {
            gameObject.tag = "Player";
        }


        //transform.rotation = Camera.main.transform.rotation;

        float h = Input.GetAxis("Horizontal"); //축 입력받기
        float v = Input.GetAxis("Vertical");
        
        Vector3 dir = Vector3.right * h + Vector3.forward * v; //방향은 입력받은 축의 합
        
        //dir = Camera.main.transform.TransformDirection(dir);
        dir = transform.TransformDirection(dir); //스크립트가 적용된 오브젝트의 앞향을 dir의 앞으로 변경
        //로컬스페이스에서 월드스페이스로 변환 해준다. (트렌스폼 기준으로 결과를 바꾼다.)
        dir = new Vector3(dir.x, 0, dir.z);
        if(dir.magnitude>1.0f || dir.magnitude<-1.0f)
        dir.Normalize();

        if (dir.sqrMagnitude > 0.1f)
            movetime += Time.deltaTime;

        if (movetime > 0.5)
        {
            moveaudiosource.Play();
            movetime = 0;
        }
        // 3. 그 방향으로 이동한다.
        // P = P0 + vt
        //transform.position += dir * speed * Time.deltaTime;
        charCtrl.SimpleMove(dir * speed * Time.deltaTime);


    }


}
