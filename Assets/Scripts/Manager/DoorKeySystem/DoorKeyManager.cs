using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DoorKeyManager
{

    public List<DoorKeyData> doorKeyEvents;
    private Dictionary<string, string> doorKeyMap;


    public void OnAwake()
    {
        doorKeyEvents = new List<DoorKeyData>();
        doorKeyMap = new Dictionary<string, string>();

    }


    public void OnStart()
    {


    }

    public void OnUpdate()
    {

    }
    public void LoadDoorKeyDatas()
    {
        DoorKeyData[] doorkey = Resources.LoadAll<DoorKeyData>($"DoorKeyData/" + SceneManager.GetActiveScene().name);

        doorKeyEvents.Clear();
        doorKeyEvents.AddRange(doorkey);

        foreach (DoorKeyData data in doorKeyEvents)
        {
            doorKeyMap.Add(data.DoorName, data.KeyName);
        }
    }

    public void applyDoorKeydatas()
    {
        Door[] doors = GameObject.FindObjectsOfType<Door>();
        foreach (var door in doors)
        {
            if (doorKeyMap.TryGetValue(door.gameObject.name, out string keyName))
            {
                door.KeyName = keyName;
            }
        }
    }

}
