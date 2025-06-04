using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event0101 : MonoBehaviour
{
    public GameObject DiaryBody;
    public GameObject zombie;
    Text speech;

    bool canplay = true;
    // Start is called before the first frame update
    void Start()
    {
        zombie.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canplay) //닿은 물체의 태그가 Player고 canplay가 true 상태면
        {
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("젠장, 문 앞에서 기다리고 있잖아?", "문을 열지 않고 갈수 있는 방법이 있을까..?", "", "", "");
            DiaryBody.GetComponent<Text>().text = "철창 뒤에서 나를 노리는 녀석의 모습이 보인다.\n안전하게 지나갈 방법을 찾아야 한다.";
            canplay = false; //한번 대사했으면 다시 X

        }

        
    }
}
