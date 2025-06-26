using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : UIButtonBinder<TitleUI.ButtonID>
{

    private GameObject settingsScreen;
    static public event Action InGameUIOn;

    public enum ButtonID
    {
        LoadButton,
        StartButton,
        SettingsButton,
        GameExitButton
    }

    protected override void Awake()
    {
        base.Awake();

        settingsScreen = transform.root.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "SettingsCanvas")?.gameObject;

        foreach(var button in buttonMap.Values)
        {
            AttachHoverEvents(button); //타이틀 네가지 버튼 모두에 호버액션 달아준다.
        }
    }

    protected override Dictionary<ButtonID, UnityAction> GetHandlers()
    {
        return new Dictionary<ButtonID, UnityAction>
        {
            { ButtonID.LoadButton, () => {SceneManager.LoadScene($"Chapter"+PlayerPrefs.GetInt("chap"));
                                            InGameUIOn();} },
            { ButtonID.StartButton, () => { SceneManager.LoadScene("Chapter1");
                                            InGameUIOn();} },
            { ButtonID.SettingsButton, () => { settingsScreen.SetActive(true); } },
            { ButtonID.GameExitButton, () => { Application.Quit(); } }
        };
    }


    private void AttachHoverEvents(Button button)
    {
        var trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        trigger.triggers ??= new List<EventTrigger.Entry>();

        AddTrigger(trigger, EventTriggerType.PointerEnter, () =>
        {
            var text = button.GetComponentInChildren<Text>();
            if (text) text.color = Color.red;
        });

        AddTrigger(trigger, EventTriggerType.PointerExit, () =>
        {
            var text = button.GetComponentInChildren<Text>();
            if (text) text.color = Color.white;
        });
    }

    private void AddTrigger(EventTrigger trigger, EventTriggerType type, UnityAction action)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(_ => action());
        trigger.triggers.Add(entry);
    }

}
