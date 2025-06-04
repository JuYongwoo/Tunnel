using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{



    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.name == "KeepButton")
            GetComponentInChildren<Text>().color = Color.blue;

        if (this.gameObject.name=="StartButton")
            GetComponentInChildren<Text>().color = Color.red;

        if (this.gameObject.name == "ExitButton")
            GetComponentInChildren<Text>().color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Text>().color = Color.white; //Or however you do your color
    }
}