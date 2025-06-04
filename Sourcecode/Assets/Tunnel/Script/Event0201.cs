using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Event0201 : MonoBehaviour
{
    public GameObject DiaryBody;
    public GameObject ifkey;
    public GameObject Zombie2;

    public GameObject[] position123 = new GameObject[3];

    bool canplay = true;
    int patrol = 1;
    float nowtime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Zombie2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().findinven(ifkey.name) && canplay) //ifkey의 이름이 인벤토리에 존재하고 canplay일 때
        {

            Zombie2.SetActive(true); //활성화
            DiaryBody.GetComponent<Text>().text = "괴물을 피해 창고로 이동하자.\nLeftCtrl키를 눌러 앉으면 괴물의 시야에서 보이지 않게 되는 것 같다.";
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("화장실 쪽에서 무슨 소리가 들렸는데","","","","");
            canplay = false; //다시 발동 못하게 false
        }



                nowtime += Time.deltaTime;
                if (nowtime > 10 && Zombie2.activeSelf) {
                        patrol = Random.Range(0, 3); //10초마다 patrol 1~3 랜덤
                        Zombie2.GetComponent<NavMeshAgent>().destination=position123[patrol].transform.position; //랜덤값을 배열 인덱스로
                        nowtime = 0.0f; //다음 실행을 위해 시간초기화
                }
                if(Zombie2.activeSelf)
                Zombie2.GetComponent<Animator>().SetFloat("Speed", Zombie2.GetComponent<NavMeshAgent>().velocity.magnitude);
    }
}