using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryScreen : MonoBehaviour
{
    public GameObject Body;
    Text Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = Body.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DiaryContentChange(string content) // !!!!!!!!!!!!! 평소에 DiaryScreen이 꺼져있어서 이 메소드 이용 불가 But Text는 사용가능(?) 다른 스크립트에서 내용을 바꾸려면 .Text="" 식으로 변수 직접 변경
    {
        Text.text = content;
    }
}
