using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/DoorKey Data")]
public class DoorKeyData : ScriptableObject
{
    public string DoorName = "";
    public string KeyName = "";

}
