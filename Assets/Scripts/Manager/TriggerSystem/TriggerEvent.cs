using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Event Data")]
public class TriggerEvent : ScriptableObject
{
    [NonSerialized]
    public bool istriggered = false; 

    public bool isColliderTriggerActive;
    public Vector3 triggerPosition;
    public float triggerdistance;
    public bool isItemTriggerActive;
    public string tiggerGetItem; 

    public bool isDescription;
    public string eventdescription;

    public bool isSpeech;
    public List<string> eventSpeeches = new List<string>();

    public bool isSceneChange;
    public string eventscenechange = "";

    public bool isSound;
    public AudioClip sound = null;

    public bool isSave;
    public int save = -1;

    public bool isFadeOut = false;
    public bool isFadeIn = false;

    public bool isSpawnObject = false;
    public string SpawnPrefabName = "";
    public string SpawnNaming = "";
    public Vector3 SpawnObjectPosition;

    public bool isFollow;
    public string eventfollower = "";

    public bool isPatrol;
    public string PatrolObjectName = "";
    public List<Vector3> eventPatrolPositions = new List<Vector3>();




}
