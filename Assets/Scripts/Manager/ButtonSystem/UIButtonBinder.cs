using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIButtonBinder<T> : MonoBehaviour where T : struct, Enum
{
    protected Dictionary<T, Button> buttonMap;
    protected abstract Dictionary<T, UnityAction> GetHandlers();

    protected virtual void Awake()
    {
        buttonMap = new Dictionary<T, Button>();

        var buttons = GetComponentsInChildren<Button>(true);
        foreach (var btn in buttons)
        {
            if (Enum.TryParse<T>(btn.gameObject.name, out var id))
            {
                buttonMap[id] = btn;
            }
        }

        foreach (var pair in GetHandlers())
        {
            if (buttonMap.TryGetValue(pair.Key, out var button))
                button.onClick.AddListener(pair.Value);
            else
                Debug.LogWarning($"[UIButtonBinder] Button '{pair.Key}' not found");
        }
    }
}
