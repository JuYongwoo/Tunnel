using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Event0302 : MonoBehaviour
{
    public GameObject ch0302image;
    public GameObject ch0302text;
    float nowtime = 0.0f;
    float nowtime2 = 0.0f;
    bool isfade = false;
    bool speechstart = false;
    bool imagefadeoutdone = false;

    // Start is called before the first frame update
    void Start()
    {
        ch0302text.SetActive(false);
        ch0302image.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        fadein();
        speech();
        textfadein();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            ch0302image.SetActive(true); // 꺼놨던 이미지 키기
            isfade = true;
            speechstart = true;
            GetComponent<BoxCollider>().enabled = false; //다시 콜라이더에 부딪히면 또 실행되는 것을 방지하기 위해 콜라이더 끄기
        }
    }

    void fadein()
    {
        if(isfade)
        {
            nowtime += Time.deltaTime;
            ch0302image.GetComponent<Image>().color = new Color(0, 0, 0, nowtime / 2);


            if (nowtime > 5)
            {
                imagefadeoutdone = true;
                ch0302text.SetActive(true); // 페이드인 끝났으면 텍스트 키기
                isfade = false; //페이드인 끝났으면 더이상 코드가 실행될 필요 없으므로 isfade 조건 false 주기
            }

        }
    }

    void speech()
    {
        if (speechstart)
        {
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("드디어 성공이야 여기서 나갈 수 있어!", "", "", "", "");
            speechstart = false;
        }
            
    }

    void textfadein()
    {
        if (imagefadeoutdone)
        {
            nowtime2 += Time.deltaTime;
            ch0302text.GetComponent<Text>().color = new Color(255, 255, 255, nowtime2 / 2);

            if (nowtime2>8)
                SceneManager.LoadScene("Chapter3NormalClear");

        }
    }
}
