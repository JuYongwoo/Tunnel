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

        ManagerObject.Instance.gameObject.AddComponent<AudioSource>(); //����� ����� ���� ������ҽ� add
        papersound = Resources.Load<AudioClip>("papersound");
        doorsound = Resources.Load<AudioClip>("doorsound");
        BGM = Resources.Load<AudioClip>("BGM");
        stepsound = Resources.Load<AudioClip>("stepsound");
    }
    public void OnStart()
    {
        InGameUI.Textsoundplay += () => { }; //TODO JYW ���������� audio �����ϰ� clip����ϰ� ���ֱ�
        InGameUI.Keysoundplay += () => { }; //TODO JYW ���������� audio �����ϰ� clip����ϰ� ���ֱ�
    }

        public void OnUpdate()
    {
    }


        // �ܺο��� BGM ��� �����ϰ� ���� �� ����� �� ����
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
