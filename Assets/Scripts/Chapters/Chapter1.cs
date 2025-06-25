using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chapter1 : MonoBehaviour
{

    private void Start()
    {
        ManagerObject.TriggerEvent.PlayEvent("Chapter1_StartEvent");

    }
}