using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chapter2 : MonoBehaviour
{
    private void Start()
    {
        ManagerObject.TriggerEvent.PlayEvent("Ch2Start");
    }
}