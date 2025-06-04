using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Event0102 : MonoBehaviour
{
    public GameObject DiaryBody;
    NavMeshAgent Nav;
    Animator anim;

    public GameObject zombie;

    Text speech;

    bool canplay = true;
    // Start is called before the first frame update
    void Start()
    {
        zombie.SetActive(false);
        Nav = zombie.GetComponent<NavMeshAgent>();
        anim = zombie.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombie.activeSelf) //좀비가 활성화 되면
        {
            Nav.destination = GameObject.Find("Player").transform.position; //위치 추적
            anim.SetFloat("Speed", Nav.velocity.magnitude); //속도에 따라 애니메이션파라미터값 변경
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canplay) //닿은 물체의 태그가 Player고 canplay가 true 상태면
        {
            zombie.SetActive(true);
            GetComponent<AudioSource>().Play();
            canplay = false; //한번 대사했으면 다시 X
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("누군가 나를 향해 달려오는 것 같다.", "", "", "", "");
            DiaryBody.GetComponent<Text>().text = "도망쳐";
        }

        
    }
}
