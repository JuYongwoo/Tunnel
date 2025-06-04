using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event0300 : MonoBehaviour
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
        PlayerSpeechScreen.Instance.startspeech("휴.. 겨우 따돌렸군", "이제 이 미로만 빠져나가면..", "돌아가자마자 바로 경찰에 신고하겠어", "", ""); //시작 시 플레이어 혼잣말
        DiaryBody.GetComponent<Text>().text = "날 따라오는 녀석을 겨우 따돌렸다.\n이제 이 미로같은 곳만 빠져나가면 이곳과 작별이다."; //시작 시 일지 업데이트

        PlayerPrefs.SetInt("chap", 3);
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
            Destroy(this);
        }
    }

}
