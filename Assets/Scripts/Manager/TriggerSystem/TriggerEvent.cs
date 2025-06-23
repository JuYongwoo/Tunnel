using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Event Data")]
public class TriggerEvent : ScriptableObject
{
    [NonSerialized]
    public bool istriggered = false; //�����Ϳ��� �����Ǵ� �� �ƴ�

    public bool isColliderTriggerActive;
    public Vector3 triggerPosition;
    public float triggerdistance;
    public bool isItemTriggerActive;
    public string tiggerGetItem; //������ �� �̺�Ʈ �����ϵ��� �� ������ �̸�
    [HideInInspector]


    public bool isDescription;
    [TextArea]
    public string eventdescription;

    public bool isSpeech;
    [TextArea]
    public string eventspeech1 = "";
    [TextArea]
    public string eventspeech2 = "";
    [TextArea]
    public string eventspeech3 = "";
    [TextArea]
    public string eventspeech4 = "";
    [TextArea]
    public string eventspeech5 = "";

    public bool isSceneChange;
    public string eventscenechange = "";

    public bool isFollow;
    public string eventfollower = "";

    public bool isSound;
    public AudioClip sound = null;

    public bool isSave;
    public int save = -1;

    public bool isFadeOut = false;
    public bool isFadeIn = false;

    public bool isSpawnObject = false;
    public string SpawnObjectName = "";
    public Vector3 SpawnObjectPosition;




}
