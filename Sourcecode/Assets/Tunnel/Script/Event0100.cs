using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event0100 : MonoBehaviour
{
    public GameObject Body;
    public GameObject StartFadeInOutImage;
    public GameObject StartFadeText;
    public float nowtime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        StartFadeInOutImage.SetActive(true);
        StartFadeText.SetActive(true);

        Body.GetComponent<Text>().text = "불의의 사고로 지하터널에 떨어지게 되었다. 이 곳을 조사해보자.";
        PlayerPrefs.SetInt("chap", 1);
        PlayerSpeechScreen.Instance.startspeech("으으...", "여긴 어디지? 분명 집으로 가는 길이었는데", "여길 빠져나갈 방법을 찾아봐야겠어", "", "");
    }

    // Update is called once per frame
    void Update()
    {
        if (nowtime < 5.0f) //5초동안 시간에따라 투명(알파값이 0일시 안보임)
        {
            nowtime += Time.deltaTime;
            StartFadeInOutImage.GetComponent<Image>().color = new Color(0, 0, 0, 5 - nowtime);
            StartFadeText.GetComponent<Text>().color = new Color(255, 255, 255, 5 - nowtime);
        }
        
    }
}
