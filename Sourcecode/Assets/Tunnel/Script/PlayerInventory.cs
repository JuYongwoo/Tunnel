using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject []inven = new GameObject[30];
    public string[] invencontentname = new string[30];
    int invencount = 0;

    public GameObject InventoryUI;
    bool Canvasactive = false;

    public GameObject itemininventoryprefab;
    public GameObject contentfolder;
    GameObject InCodePrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InventoryUI.SetActive(Canvasactive);
        if (Input.GetKeyDown(KeyCode.Tab)){
            Canvasactive = !Canvasactive;
        }
        
    }
    public void intoinven(GameObject gotit)
    {
        inven[invencount] = gotit;
        invennamesubmit();
        invencount += 1;

        InCodePrefab = Instantiate(itemininventoryprefab); //인벤토리에 버튼(아이템)생성
        InCodePrefab.transform.parent = contentfolder.transform; //프리팹 소환 시 content 아래에 생성
        InCodePrefab.GetComponent<Image>().sprite = gotit.GetComponent<Image>().sprite;
    }
    public void invennamesubmit()
    {
        invencontentname[invencount] = inven[invencount].name;
    }
    public bool findinven(string name)
    {
        int pos = Array.IndexOf(invencontentname, name);
        if (pos > -1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
}
