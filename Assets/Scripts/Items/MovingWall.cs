using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public bool moveon = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (moveon)
        {
            if (this.transform.position.x > -11)
                this.transform.position -= new Vector3(Time.deltaTime, 0, 0);
            else
                moveon = false;

        }

    }
}
