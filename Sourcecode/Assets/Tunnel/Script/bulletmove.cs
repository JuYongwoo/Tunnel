using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmove : MonoBehaviour
{
    Vector3 forr;
    // Start is called before the first frame update
    void Start()
    {
        forr = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(forr*700 * Time.deltaTime);
    }

}
