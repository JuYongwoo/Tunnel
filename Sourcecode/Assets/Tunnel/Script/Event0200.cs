using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event0200 : MonoBehaviour
{
    public GameObject DiaryBody;
    public GameObject StartFadeInOutImage;
    public GameObject StartFadeText;
    public float nowtime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartFadeInOutImage.SetActive(true);
        StartFadeText.SetActive(true);
        PlayerSpeechScreen.Instance.stopspeech();
        PlayerSpeechScreen.Instance.startspeech("젠장 여긴 어디야?", "나갈 수 있는 방법을 찾아봐야겠어","","",""); //시작 시 플레이어 혼잣말
        DiaryBody.GetComponent<Text>().text = "어디인지 모를 산장에 갇혀버렸다.\n나갈 수 있는 방법을 찾아보자."; //시작 시 일지 업데이트

        PlayerPrefs.SetInt("chap", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (nowtime < 5.0f)
        {
            nowtime += Time.deltaTime;
            StartFadeInOutImage.GetComponent<Image>().color = new Color(0, 0, 0, 5 - nowtime);
            StartFadeText.GetComponent<Text>().color = new Color(255, 255, 255, 5 - nowtime);

        }

        if (nowtime > 5.0f)
        {
            StartFadeInOutImage.SetActive(false);
            StartFadeText.SetActive(false);
        }
    }

}
