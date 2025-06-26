using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class InGameUI :MonoBehaviour
{


    private float timeforrequirekey = 5.0f;

    private GameObject InventoryUI;
    private GameObject DiaryUI;
    private GameObject TextBookUI;
    private GameObject BasicUI;
    private GameObject PictureBookUI;
    private GameObject UseUI;
    private GameObject AlertUI;

    private bool requirekeyon = false;

    private GameObject[] inven = new GameObject[30];
    private string[] invencontentname = new string[30];
    private int invencount = 0;

    private GameObject contentfolder;
    private GameObject InCodePrefab;
    private IEnumerator speechCoroutine;
    private GameObject PlayerSpeechUI;
    private string[] txts = new string[5];
    private GameObject FadeoutUI;

    static public event Action Textsoundplay;
    static public event Action Keysoundplay;

    Action FadeAction;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        var all = transform.root.GetComponentsInChildren<Transform>(true);
        GameObject Find(string name) => all.FirstOrDefault(t => t.name == name)?.gameObject;

        InventoryUI = Find("InventoryUI");
        DiaryUI = Find("DiaryUI");
        PictureBookUI = Find("PictureBookUI");
        TextBookUI = Find("TextBookUI");
        BasicUI = Find("BasicUI");
        UseUI = Find("UseUI");
        AlertUI = Find("AlertUI");
        PlayerSpeechUI = Find("PlayerSpeechUI");
        FadeoutUI = Find("FadeoutUI");
        contentfolder = Find("Contentfolder");


        InventoryUI.SetActive(false);
        DiaryUI.SetActive(false); // ���۽� ��Ȱ��ȭ
        PictureBookUI.SetActive(false);
        TextBookUI.SetActive(false);
        BasicUI.SetActive(false);
        UseUI.SetActive(false);
        AlertUI.SetActive(false);
        PlayerSpeechUI.SetActive(false);
        FadeoutUI.SetActive(true); //���̵��� �ƿ��� ���İ����� ����, �׻� ��������


        speechCoroutine = StartspeechCoroutine();
        assignUIEvents();
    }

    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title") return;
        if (SceneManager.GetActiveScene().name == "GameOver") return;



        if (Input.GetKeyDown(KeyCode.Tab) && !InventoryUI.activeSelf) //�κ��丮
        {
            closeAll();
            InventoryUI.SetActive(true);
            UseUI.SetActive(false);
            BasicUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !DiaryUI.activeSelf) // ���̾
        {
            closeAll();
            //DiaryUI.GetComponentInParent<AudioSource>().Play(); //TODO JYW �Ҹ� ����� SoundManager����
            DiaryUI.SetActive(true); //Q���̾ üũ�� ���� Ȱ��ȭ
            DiaryUI.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ManagerObject.TriggerEvent.NowMission;
            UseUI.SetActive(false);
            BasicUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //TODO JYW ����Ҹ� �߰��� ��
            closeAll();
            UseUI.SetActive(true);
            BasicUI.SetActive(true);

        }





        timeforrequirekey += Time.deltaTime;
        if (timeforrequirekey < 5.0f)
        {
            requirekeyon = true;
        }
        else
        {
            requirekeyon = false;
        }

        
        FadeAction?.Invoke();
    }


    private void SetAlpha(float alpha)
    {
        var image = FadeoutUI.transform.GetChild(0).GetComponent<Image>();
        Color color = image.color;
        color.a = Mathf.Clamp01(alpha);
        image.color = color;
    }



    private void FadeIn()
    {
        var image = FadeoutUI.transform.GetChild(0).GetComponent<Image>();
        Color color = image.color;
        color.a += 0.001f;
        if (color.a >= 1f)
        {
            color.a = 1f;
            FadeAction -= FadeIn;
        }
        image.color = color;
    }

    private void FadeOut()
    {
        var image = FadeoutUI.transform.GetChild(0).GetComponent<Image>();
        Color color = image.color;
        color.a -= 0.001f;
        if (color.a <= 0f)
        {
            color.a = 0f;
            FadeAction -= FadeOut;
        }
        image.color = color;
    }

    private void requirekeyAlert()
    {
        AlertUI.SetActive(true);
        AlertUI.transform.GetChild(0).GetComponent<Text>().text = "���谡 �ʿ��մϴ�.";
    }
    private void endrequirekeyAlert()
    {
        AlertUI.SetActive(false);
        AlertUI.transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void closeAll()
    {
        DiaryUI.SetActive(false);
        PictureBookUI.SetActive(false);
        TextBookUI.SetActive(false);
        InventoryUI.SetActive(false);
    }


    public void intoinven(GameObject gotit)
    {
        //������ ȹ�� �� �ڵ����� ����Ǵ� �̺�Ʈ�� TriggerEventManager���� ��������Ʈ�� ó�� �ϵ��� �����Ͽ���.
        inven[invencount] = gotit;
        invennamesubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.AddComponent<Image>();
        InCodePrefab = GameObject.Instantiate(itemininventoryprefab); //�κ��丮�� ��ư(������)����
        InCodePrefab.transform.parent = contentfolder.transform; //������ ��ȯ �� content �Ʒ��� ����
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

    public void stopspeech()
    {
        ManagerObject.Instance.StopCoroutine(speechCoroutine); //�ڷ�ƾ �������� �ʰ� ������ִ� �ڷ�ƾ ��ž
        Debug.Log("�ڷ�ƾ����");
    }

    public void startspeech(List<string> lines)
    {
        for (int i = 0; i < txts.Length; i++)
        {
            txts[i] = i < lines.Count ? lines[i] : string.Empty;
        }

        speechCoroutine = StartspeechCoroutine(); // ���ڿ� ���� �ڷ�ƾ�� �غ�
        ManagerObject.Instance.StartCoroutine(speechCoroutine);
    }


    public IEnumerator StartspeechCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerSpeechUI.transform.GetChild(0).GetComponent<Text>().text= txts[i];
            yield return new WaitForSeconds(4.0f);
        }
        PlayerSpeechUI.transform.GetChild(0).GetComponent<Text>().text = ""; //������ ����
    }


    void assignUIEvents()
    {
        PlayerInteractor.UseUIOn += (str) => {
            UseUI.transform.GetChild(0).GetComponent<Text>().text = str;
            UseUI.SetActive(true);
        };
        PlayerInteractor.UseItem += (Type, go) =>
        {
            if(Type == PlayerInteractor.ItemType.PictureBook)
            {
                Textsoundplay();
                PictureBookUI.transform.GetChild(0).GetComponent<Image>().sprite = go.GetComponent<Image>().sprite; //��ũ���� ��������Ʈ�� ����ĳ��Ʈ�� ���� hit�� image������Ʈ�� ��������Ʈ�� �ٲ�
                PictureBookUI.SetActive(true); // Ȱ��ȭ
                UseUI.SetActive(false);
                BasicUI.SetActive(false);
            }
            else if (Type == PlayerInteractor.ItemType.Textbook)
            {
                Textsoundplay();
                TextBookUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = go.GetComponent<Text>().text; // �ش� ������Ʈ�� text ������Ʈ�� ���� ������ ������
                TextBookUI.SetActive(true); // Ȱ��ȭ
                UseUI.SetActive(false);
                BasicUI.SetActive(false);

            }
            else if(Type == PlayerInteractor.ItemType.Door)
            {

                if (findinven(go.GetComponent<Door>().KeyName)) // �ش� ���� door class�� KeyName�� �κ��丮�� �ִ��� Ȯ��
                {
                    go.transform.Rotate(0, 90, 0); //Y 90�� ȸ��
                    go.GetComponent<Collider>().enabled = false;
                    Keysoundplay();
                }
                else
                {
                    requirekeyAlert();
                    Invoke("endrequirekeyAlert", 3f);
                }
            }
            else if (Type == PlayerInteractor.ItemType.Key)
            {
                go.SetActive(false);
                intoinven(go);
                Keysoundplay();
            }
            else if(Type == PlayerInteractor.ItemType.MovingWall)
            {
                go.GetComponent<MovingWall>().moveon = true;
            }

        };

        PlayerInteractor.UseUIOff += () =>
        {
            UseUI.transform.GetChild(0).GetComponent<Text>().text = "";
            UseUI.SetActive(false);
        };

        TriggerEventManager.Speech += lines =>
        {
            stopspeech();
            startspeech(lines); // params string[] �� �迭�� �ǰ� ���� ���ڵ� ��
        };

        TriggerEventManager.FadeIn += () =>
        {
            SetAlpha(0f);
            FadeAction += FadeIn;
        };
        
        TriggerEventManager.FadeOut += () =>
        {
            SetAlpha(1f);
            FadeAction += FadeOut;
        };
        
        TitleUI.InGameUIOn += () =>
        {
            BasicUI.SetActive(true);
            PlayerSpeechUI.SetActive(true);
        };

        GameOver.InGameUIOff += () =>
        {
            BasicUI.SetActive(false);
            PlayerSpeechUI.SetActive(false);
        };

    }
}
