using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

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
        DiaryUI.SetActive(false); // 시작시 비활성화
        PictureBookUI.SetActive(false);
        TextBookUI.SetActive(false);
        BasicUI.SetActive(false);
        UseUI.SetActive(false);
        AlertUI.SetActive(false);
        PlayerSpeechUI.SetActive(false);
        FadeoutUI.SetActive(true); //페이드인 아웃은 알파값으로 조절, 항상 켜진상태


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


        UseUI.SetActive(!PictureBookUI.activeSelf); //그림책이 보여지고 있으면 획득표시 없애기
        UseUI.SetActive(!TextBookUI.activeSelf); //글책이 보여지고 있으면 획득표시 없애기
        BasicUI.SetActive(!PictureBookUI.activeSelf && !DiaryUI.activeSelf && !InventoryUI.activeSelf); //책이미지, 다이어리, 인벤토리 모두 안열려있을 시 기본UI활성화
        AlertUI.SetActive(requirekeyon);



        if (Input.GetKeyDown(KeyCode.Tab) && !InventoryUI.activeSelf) //인벤토리
        {
            closeAll();
            InventoryUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !DiaryUI.activeSelf) // 다이어리
        {
            closeAll();
            //DiaryUI.GetComponentInParent<AudioSource>().Play(); //TODO JYW 소리 재생은 SoundManager에서
            DiaryUI.SetActive(true); //Q다이어리 체크에 따라 활성화
            DiaryUI.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ManagerObject.TriggerEvent.NowMission;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //TODO JYW 딸깍소리 추가할 것
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
        //아이템 획득 시 자동으로 실행되는 이벤트는 TriggerEventManager에서 델리게이트로 처리 하도록 설정하였음.
        inven[invencount] = gotit;
        invennamesubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.AddComponent<Image>();
        InCodePrefab = GameObject.Instantiate(itemininventoryprefab); //인벤토리에 버튼(아이템)생성
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

    public void stopspeech()
    {
        ManagerObject.Instance.StopCoroutine(speechCoroutine); //코루틴 갱신하지 않고 선언되있던 코루틴 스탑
        Debug.Log("코루틴멈춤");
    }

    public void startspeech(params string[] lines)
    {
        for (int i = 0; i < txts.Length; i++)
        {
            txts[i] = i < lines.Length ? lines[i] : string.Empty;
        }

        speechCoroutine = StartspeechCoroutine(); // 문자열 받은 코루틴을 준비
        ManagerObject.Instance.StartCoroutine(speechCoroutine);
    }


    public IEnumerator StartspeechCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerSpeechUI.transform.GetChild(0).GetComponent<Text>().text= txts[i];
            yield return new WaitForSeconds(4.0f);
        }
        PlayerSpeechUI.transform.GetChild(0).GetComponent<Text>().text = ""; //끝나면 공백
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
                PictureBookUI.transform.GetChild(0).GetComponent<Image>().sprite = go.GetComponent<Image>().sprite; //스크린의 스프라이트를 레이캐스트에 맞은 hit의 image컴포넌트의 스프라이트로 바꿈
                PictureBookUI.SetActive(true); // 활성화
            }
            else if (Type == PlayerInteractor.ItemType.Textbook)
            {
                go.GetComponent<AudioSource>().Play();
                TextBookUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = go.GetComponent<Text>().text; // 해당 오브젝트의 text 컴포넌트에 적힌 내용을 가져옴
                TextBookUI.SetActive(true); // 활성화
            }
            else if(Type == PlayerInteractor.ItemType.Door)
            {

                if (findinven(go.name + "key")) // "door이름+key" 이름이 있는지 찾음
                {
                    go.transform.Rotate(0, 90, 0); //Y 90도 회전
                    go.GetComponent<Collider>().enabled = false; //콜라이더 끄기(부딪X 레이캐스트감지 X 더이상 회전안됨)
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

        TriggerEventManager.Speech += lines =>
        {
            stopspeech();
            startspeech(lines); // params string[] 라서 배열도 되고 여러 인자도 됨
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
