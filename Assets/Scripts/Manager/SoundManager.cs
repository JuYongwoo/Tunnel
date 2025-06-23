using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager
{

    AudioClip papersound;
    AudioClip doorsound;
    AudioClip BGM;
    AudioClip stepsound;

    public void OnAwake()
    {

        ManagerObject.Instance.gameObject.AddComponent<AudioSource>(); //오디오 재생을 위해 오디오소스 add
        papersound = Resources.Load<AudioClip>("papersound");
        doorsound = Resources.Load<AudioClip>("doorsound");
        BGM = Resources.Load<AudioClip>("BGM");
        stepsound = Resources.Load<AudioClip>("stepsound");
    }
    public void OnStart()
    {
    }

        public void OnUpdate()
    {
    }


        // 외부에서 BGM 재생 제어하고 싶을 때 사용할 수 있음
    public void PlayMusic(AudioClip newClip)
    {
        ManagerObject.Instance.gameObject.GetComponent<AudioSource>().clip = newClip;
        ManagerObject.Instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        ManagerObject.Instance.gameObject.GetComponent<AudioSource>()?.Stop();
        ManagerObject.Instance.gameObject.GetComponent<AudioSource>().clip = null;
    }

}
