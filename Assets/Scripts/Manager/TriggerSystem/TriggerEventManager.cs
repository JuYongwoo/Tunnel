using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class TriggerEventManager
{
    public static event Action FadeIn;
    public static event Action FadeOut;
    public static event Action<List<string>> Speech;

    [HideInInspector]
    public string NowMission = "";

    public List<TriggerEvent> triggerevents;
    private Dictionary<string, TriggerEvent> triggereventmap;


    public void OnAwake()
    {
        triggerevents = new List<TriggerEvent>();
        triggereventmap = new Dictionary<string, TriggerEvent>();
    }
       

    public void OnStart() {
       

    }

    public void OnUpdate()
    {
        if (ManagerObject.Scene.ThisScenePlayer == null) return;

        foreach (var TriggerEvent in triggerevents)
        {

            if(TriggerEvent.istriggered && TriggerEvent.eventfollower != "")
            {
                GameObject.Find(TriggerEvent.eventfollower).GetComponent<NavMeshAgent>().destination = ManagerObject.Scene.ThisScenePlayer.transform.position;
            }

        }

    }




    public void ClearAllTriggerEvents()
    {
        triggerevents.Clear();
        triggereventmap.Clear();
    }

    public void LoadCurrentSceneTriggerEvents()
    {

        TriggerEvent[] events = Resources.LoadAll<TriggerEvent>($"TriggerEvents/"+SceneManager.GetActiveScene().name);

        triggerevents.Clear();
        triggerevents.AddRange(events);

    }

    public void mappingAlltriggerEvents()
    {
        foreach (var trigger in triggerevents)
        {
            triggereventmap[trigger.name] = trigger;
            if (trigger.isColliderTriggerActive)
            {
                CreateCollisionTrigger(trigger);
            }
            else if (trigger.isItemTriggerActive)
            {
                CreateItemTrigger(trigger);
            }
        }
    }

    public void followermove()
    {

    }


    void CreateCollisionTrigger(TriggerEvent trigger)
    {
        GameObject go = new GameObject("Trigger_" + trigger.name);
        go.transform.position = trigger.triggerPosition;

        var col = go.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = Vector3.one * trigger.triggerdistance;

        var proxy = go.AddComponent<GameObjectForCollisionTrigger>();
        proxy.TriggerEnterAction = () =>
        {
            if (!trigger.istriggered)
            {
                PlayEvent(trigger.name);
            }
            trigger.istriggered = true;
        };
    }

    void CreateItemTrigger(TriggerEvent trigger)
    {
        Key key = GameObject.Find(trigger.tiggerGetItem).GetComponent<Key>();
        key.DisableAction = () =>
        {
            if (!trigger.istriggered)
            {
                PlayEvent(trigger.name);
            }
            trigger.istriggered = true;
        };

    }


    private class GameObjectForCollisionTrigger : MonoBehaviour
    {
        public Action TriggerEnterAction;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerEnterAction?.Invoke();
            }
        }
    }


    public void PlayEvent(string eventname)
    {
        if (triggereventmap.TryGetValue(eventname, out var data))
        {

            if (triggereventmap[eventname].isDescription) //목표 수정 존재할 경우
            {
                NowMission = triggereventmap[eventname].eventdescription; //목표 업데이트
            
            }
            if (triggereventmap[eventname].isSpeech) // 대화 존재할 경우
            {
                Speech(triggereventmap[eventname].eventSpeeches);
            }

            if (triggereventmap[eventname].isSceneChange) //씬 전환 존재할 경우
            {
                SceneManager.LoadScene(triggereventmap[eventname].eventscenechange);
            }
            if (triggereventmap[eventname].isSound)
            {
                ManagerObject.Sound.PlayMusic(triggereventmap[eventname].sound);
            }
            if (triggereventmap[eventname].isSave)
            {
                PlayerPrefs.SetInt("chap", triggereventmap[eventname].save);

            }
            if (triggereventmap[eventname].isFadeIn)
            {
                FadeIn();

            }
            if (triggereventmap[eventname].isFadeOut)
            {
                FadeOut();
            }


            bool sameFollowObject = false;
            bool samePatrolObject = false;

            if (triggereventmap[eventname].isSpawnObject)
            {
                //GameObject go = GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/" + triggereventmap[eventname].SpawnObjectName));
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/" + triggereventmap[eventname].SpawnPrefabName);
                GameObject go = GameObject.Instantiate(prefab);
                go.name = triggereventmap[eventname].SpawnNaming;
                go.transform.position = triggereventmap[eventname].SpawnObjectPosition;
                if (triggereventmap[eventname].isFollow)
                {
                    ManagerObject.Resource.GetorAddComponent<Following>(go);
                    sameFollowObject = true;
                }
                if (triggereventmap[eventname].isPatrol)
                {
                    Patrolling Patroll = ManagerObject.Resource.GetorAddComponent<Patrolling>(go);
                    Patroll.patrolpositions = triggereventmap[eventname].eventPatrolPositions;
                    samePatrolObject = true;
                }
            }

            if (triggereventmap[eventname].isFollow && !sameFollowObject)
            {
                GameObject go = GameObject.Find(triggereventmap[eventname].eventfollower);
                ManagerObject.Resource.GetorAddComponent<Following>(go);

            }
            if (triggereventmap[eventname].isPatrol && !samePatrolObject)
            {
                GameObject go = GameObject.Find(triggereventmap[eventname].PatrolObjectName);
                Patrolling Patroll = ManagerObject.Resource.GetorAddComponent<Patrolling>(go);
                Patroll.patrolpositions = triggereventmap[eventname].eventPatrolPositions;




            }



        }
        else
        {
            Debug.LogWarning($"No event found for ID: {eventname}");
        }
    }


}
