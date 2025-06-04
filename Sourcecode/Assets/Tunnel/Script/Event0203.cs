using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Event0203 : MonoBehaviour
{
    public GameObject zombie2;
    bool isspeech = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && isspeech)
        {
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("들켰다 앞만 보고 달려야 해!!","","","","");
            isspeech = false;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zombie2.GetComponent<NavMeshAgent>().destination = collision.gameObject.transform.position;
            zombie2.GetComponent<NavMeshAgent>().speed = 4.0f;
        }
    }
}
