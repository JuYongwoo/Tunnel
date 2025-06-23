using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class InGameUI :MonoBehaviour
{


    public float timeforrequirekey = 5.0f;

    [HideInInspector]
    public GameObject InventoryUI;
    [HideInInspector]
    public GameObject DiaryUI;
    [HideInInspector]
    public GameObject TextBookUI;
    [HideInInspector]
    public GameObject BasicUI;
    [HideInInspector]
    public GameObject PictureBookUI;
    [HideInInspector]
    public GameObject NoticeUI;

    [HideInInspector]
    public GameObject UseUI;
    bool QDiarycheck = false;

    [HideInInspector]
    public GameObject temppicturebook;
    [HideInInspector]
    public GameObject temptextbook;


    [HideInInspector]
    public GameObject AlertUI;
    bool requirekeyon = false;


    public GameObject[] inven = new GameObject[30];
    public string[] invencontentname = new string[30];
    int invencount = 0;

    bool Canvasactive = false;

    public GameObject contentfolder;
    GameObject InCodePrefab;





    IEnumerator speechCoroutine;
    [HideInInspector]
    public GameObject PlayerSpeechUI;
    string[] txts = new string[5];

    [HideInInspector]
    public GameObject FadeoutUI;


    Action updateaction;

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


        UseUI.SetActive(!PictureBookUI.activeSelf); //�׸�å�� �������� ������ ȹ��ǥ�� ���ֱ�
        UseUI.SetActive(!TextBookUI.activeSelf); //��å�� �������� ������ ȹ��ǥ�� ���ֱ�
        BasicUI.SetActive(!PictureBookUI.activeSelf && !DiaryUI.activeSelf && !InventoryUI.activeSelf); //å�̹���, ���̾, �κ��丮 ��� �ȿ������� �� �⺻UIȰ��ȭ
        AlertUI.SetActive(requirekeyon);



        if (Input.GetKeyDown(KeyCode.Tab) && !InventoryUI.activeSelf) //�κ��丮
        {
            closeAll();
            InventoryUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !DiaryUI.activeSelf) // ���̾
        {
            closeAll();
            //DiaryUI.GetComponentInParent<AudioSource>().Play(); //TODO JYW �Ҹ� ����� SoundManager����
            DiaryUI.SetActive(true); //Q���̾ üũ�� ���� Ȱ��ȭ
            DiaryUI.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ManagerObject.TriggerEvent.NowMission;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //TODO JYW ����Ҹ� �߰��� ��
            closeAll();
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

        
        updateaction?.Invoke();
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
            updateaction -= FadeIn;
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
            updateaction -= FadeOut;
        }
        image.color = color;
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

    public void startspeech(string txt1, string txt2, string txt3, string txt4, string txt5)
    {

        //���Ŀ� �Ʒ� �밡�� �ڵ带 1.������ ��Ʈ�� ���ڰ��� ���ٷ� �ؼ� ��ǥ�� ���� �� ���� 2. �迭�� �޾Ƽ� ���� ���� ����
        txts[0] = txt1;
        txts[1] = txt2;
        txts[2] = txt3;
        txts[3] = txt4;
        txts[4] = txt5;

        speechCoroutine = StartspeechCoroutine(); //���ڿ� ���� �ڷ�ƾ�� �غ�
        ManagerObject.Instance.StartCoroutine(speechCoroutine);
        Debug.Log("�ڷ�ƾ����");

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
                go.GetComponent<AudioSource>().Play();
                PictureBookUI.transform.GetChild(0).GetComponent<Image>().sprite = go.GetComponent<Image>().sprite; //��ũ���� ��������Ʈ�� ����ĳ��Ʈ�� ���� hit�� image������Ʈ�� ��������Ʈ�� �ٲ�
                PictureBookUI.SetActive(true); // Ȱ��ȭ
                temppicturebook = go;
            }
            else if (Type == PlayerInteractor.ItemType.Textbook)
            {
                go.GetComponent<AudioSource>().Play();
                TextBookUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = go.GetComponent<Text>().text; // �ش� ������Ʈ�� text ������Ʈ�� ���� ������ ������
                TextBookUI.SetActive(true); // Ȱ��ȭ
                temptextbook = go;
            }
            else if(Type == PlayerInteractor.ItemType.Door)
            {

                if (findinven(go.name + "key")) // "door�̸�+key" �̸��� �ִ��� ã��
                {
                    go.transform.Rotate(0, 90, 0); //Y 90�� ȸ��
                    go.GetComponent<Collider>().enabled = false; //�ݶ��̴� ����(�ε�X ����ĳ��Ʈ���� X ���̻� ȸ���ȵ�)
                    go.GetComponent<AudioSource>().Play();
                }
                else if (!findinven(go.name + "key"))
                {
                    timeforrequirekey = 0.0f;
                }
            }
            else if (Type == PlayerInteractor.ItemType.Key)
            {
                go.SetActive(false);
                intoinven(go);
                go.GetComponentInParent<AudioSource>().Play();
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

        TriggerEventManager.Speech += (str1, str2, str3, str4, str5) =>
        {
            stopspeech();
            startspeech(str1, str2, str3, str4, str5);
        };

        TriggerEventManager.FadeIn += () =>
        {
            SetAlpha(0f);
            updateaction += FadeIn;
        };
        
        TriggerEventManager.FadeOut += () =>
        {
            SetAlpha(1f);
            updateaction += FadeOut;
        };

        Chapter2.InGameUIOn += () =>
        {
            BasicUI.SetActive(true);
            PlayerSpeechUI.SetActive(true);
        };

        
        Chapter1.InGameUIOn += () =>
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
