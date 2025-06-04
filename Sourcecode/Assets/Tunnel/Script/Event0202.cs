using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event0202 : MonoBehaviour
{
    public GameObject Entrancekey;
    bool Eventdid = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Entrancekey.activeSelf && !Eventdid)
        {
            PlayerSpeechScreen.Instance.stopspeech();
            PlayerSpeechScreen.Instance.startspeech("이제 이곳을 빠져나갈 수 있어", "나가게 되면 뒤도 돌아보지않고 달려야 해", "", "", ""); //시작 시 플레이어 혼잣말
            Eventdid = true;
        }
    }
}
