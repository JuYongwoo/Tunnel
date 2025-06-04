using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    float cool = 0.0f;
    float nowtime = 0.0f;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        nowtime += Time.deltaTime;
        if (Input.GetMouseButton(0) && nowtime>cool)
        {
            Instantiate(Bullet,GameObject.Find("Gun").transform.position,GameObject.Find("Gun").transform.localRotation);

            cool = nowtime+0.1f;
            
        }


    }
}
