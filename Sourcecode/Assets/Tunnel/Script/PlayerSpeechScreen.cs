using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class PlayerSpeechScreen : MonoBehaviour
{

    public static PlayerSpeechScreen Instance;

    IEnumerator speechCoroutine;
    public GameObject PlayerSpeech;
    Text speech;
    string[] txts = new string[5];

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;

        speech = PlayerSpeech.GetComponent<Text>();
        speech.text = "";

        for (int i = 0; i < 5; i++)
        {
            txts[i] = ""; //처음엔 문자열 비우기
        }


        PlayerSpeechScreen.Instance.speechCoroutine = StartspeechCoroutine(); //문자열이 비어있는 코루틴을 준비
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void stopspeech()
    {
        StopCoroutine(speechCoroutine); //코루틴 갱신하지 않고 선언되있던 코루틴 스탑
        Debug.Log("코루틴멈춤");
    }

    public void startspeech(string txt1, string txt2, string txt3, string txt4, string txt5)
    {

        //추후에 아래 노가다 코드를 1.보내는 스트링 인자값을 한줄로 해서 쉼표로 구분 후 대입 2. 배열로 받아서 대입 으로 변경
        txts[0] = txt1;
        txts[1] = txt2;
        txts[2] = txt3;
        txts[3] = txt4;
        txts[4] = txt5;

        speechCoroutine = StartspeechCoroutine(); //문자열 받은 코루틴을 준비
        StartCoroutine(speechCoroutine);
        Debug.Log("코루틴시작");

    }

    public IEnumerator StartspeechCoroutine()
    {
        for(int i=0; i < 5; i++)
        {
        speech.text = txts[i];
        yield return new WaitForSeconds(4.0f);
        }
        speech.text = ""; //끝나면 공백
    }
}
