using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chapter2 : MonoBehaviour
{
    static public event Action InGameUIOn;
    private void Start()
    {
        ManagerObject.TriggerEvent.PlayEvent("Ch2Start");


        InGameUIOn();
    }
}