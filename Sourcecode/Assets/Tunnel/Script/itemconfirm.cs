using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemconfirm : MonoBehaviour
{
    float timeforrequirekey = 5.0f;

    public GameObject BasicUI;
    public GameObject itemEkey;
    public GameObject DiaryUI;
    public GameObject InventoryUI;
    bool QDiarycheck = false;
    public GameObject TextBookUI;
    public GameObject TextBookText;
    public GameObject PictureBookUI;
    public GameObject PictureBookImageScreen;

    GameObject temppicturebook;
    GameObject temptextbook;

    bool moveon = false;

    public GameObject requirekey;
    bool requirekeyon = false;

    public GameObject PlayerInventory;
    PlayerInventory inven;

    // Start is called before the first frame update
    void Start()
    {
        InventoryUI.SetActive(false);
        DiaryUI.SetActive(false); // 시작시 비활성화
        PictureBookUI.SetActive(false);
        TextBookUI.SetActive(false);
        BasicUI.SetActive(true);
        
        inven = PlayerInventory.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        itemEkey.SetActive(!PictureBookUI.activeSelf); //그림책이 보여지고 있으면 획득표시 없애기
        itemEkey.SetActive(!TextBookUI.activeSelf); //글책이 보여지고 있으면 획득표시 없애기
        BasicUI.SetActive(!PictureBookUI.activeSelf&&!DiaryUI.activeSelf&&!InventoryUI.activeSelf); //책이미지, 다이어리, 인벤토리 모두 안열려있을 시 기본UI활성화
        requirekey.SetActive(requirekeyon);

        if (TextBookUI.activeSelf && Input.GetKeyDown(KeyCode.Escape)) //글책이 보여지고 있을 때 ESC를 누르면
        {
            //TextBookUI.GetComponentInParent<AudioSource>().Play(); //PictureBookUI의 부모오브젝트에 소리를 넣어두었음 그것 재생
            temptextbook.GetComponent<AudioSource>().Play(); //달려있는 오디오소스 실행
            TextBookUI.SetActive(false); //ESC를 누르면 열린 책 닫기
        }

        if (PictureBookUI.activeSelf && Input.GetKeyDown(KeyCode.Escape)) // 그림책
        {
            //PictureBookUI.GetComponentInParent<AudioSource>().Play();
            temppicturebook.GetComponent<AudioSource>().Play();
            PictureBookUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Q누르면
        {
            DiaryUI.GetComponentInParent<AudioSource>().Play();
            QDiarycheck = !QDiarycheck; //Q다이어리체크 전환
            DiaryUI.SetActive(QDiarycheck); //Q다이어리 체크에 따라 활성화
            PictureBookUI.SetActive(false); //나와있던 그림 없애기
            TextBookUI.SetActive(false); //나와있던 책 없애기
        }


        RaycastHit hit; //Raycast 맞는 물체 hit 정의
        //Debug.DrawRay(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), Camera.main.transform.forward*100f, Color.red);
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, 3)) //카메라 포지션에서 카메라 방향으로 5만큼 거리의 레이캐스트 발사해서 hit가 맞으면
        {
            if ((hit.collider.tag == "picturebook"))// hit의 태그가 book이면
            {
                itemEkey.GetComponent<Text>().text = "E 읽기";
                itemEkey.SetActive(true);
            }
            else if ((hit.collider.tag == "textbook"))// hit의 태그가 book이면
            {
                itemEkey.GetComponent<Text>().text = "E 읽기";
                itemEkey.SetActive(true);
            }
            else if (hit.collider.tag == "door")//hit의 태그가 door면
            {
                itemEkey.GetComponent<Text>().text = "E 열기";
                itemEkey.SetActive(true);
            } else if (hit.collider.tag == "key")//hit의 태그가 key면
            {
                itemEkey.GetComponent<Text>().text = "E 획득";
                itemEkey.SetActive(true);
            }
            else if (hit.collider.name == "MovingBrick")//1챕터 움직이는 벽
            {
                itemEkey.GetComponent<Text>().text = "E 밀기";
                itemEkey.SetActive(true);
            }


        else
            {
                itemEkey.GetComponent<Text>().text = "";
                itemEkey.SetActive(false);
            }

            //GameObject hitObj = hit.collider.gameObject; //맞는 물체가 hit였고 hit의 콜라이더의 오브젝트를 hitObj에 넣음
                if (hit.collider.tag == "picturebook" && Input.GetKeyDown(KeyCode.E) && !PictureBookUI.activeSelf) //태그가 book 이고 E누르면
                {
                    hit.collider.GetComponent<AudioSource>().Play();
                    PictureBookImageScreen.GetComponent<Image>().sprite = hit.collider.GetComponent<Image>().sprite; //스크린의 스프라이트를 레이캐스트에 맞은 hit의 image컴포넌트의 스프라이트로 바꿈
                    PictureBookUI.SetActive(true); // 활성화
                    temppicturebook = hit.collider.gameObject;
                }

            if(hit.collider.tag == "textbook" && Input.GetKeyDown(KeyCode.E) && !TextBookUI.activeSelf) //태그가 book 이고 E누르면
                {
                hit.collider.GetComponent<AudioSource>().Play();
                TextBookText.GetComponent<Text>().text = hit.collider.GetComponent<Text>().text; // 해당 오브젝트의 text 컴포넌트에 적힌 내용을 가져옴
                TextBookUI.SetActive(true); // 활성화
                temptextbook = hit.collider.gameObject;
            }
            if (hit.collider.name == "MovingBrick" && Input.GetKeyDown(KeyCode.E)) //1챕터 움직이는 벽
            {
                moveon = true;
            }



            if (hit.collider.tag=="door"&&Input.GetKeyDown(KeyCode.E)) //태그가 door이고 E누르면
                {
                    if (inven.findinven(hit.collider.name + "key")) // "door이름+key" 이름이 있는지 찾음
                    {
                        //hit.collider.transform.eulerAngles = new Vector3(0, 90, 0); //Y 90도로
                        //hit.collider.transform.rotation = Quaternion.Euler(0, 90, 0); //Y 90도로
                        hit.collider.transform.Rotate(0, 90, 0); //Y 90도 회전
                        hit.collider.GetComponent<Collider>().enabled = false; //콜라이더 끄기(부딪X 레이캐스트감지 X 더이상 회전안됨)
                        hit.collider.GetComponent<AudioSource>().Play();
                }
                    else if (!inven.findinven(hit.collider.name + "key"))
                    {
                        timeforrequirekey = 0.0f;
                    }
                }

                if(hit.collider.tag == "key" && Input.GetKeyDown(KeyCode.E)) //태그가 key이고 E누르면
            {
                hit.collider.gameObject.SetActive(false);
                inven.intoinven(hit.collider.gameObject);
                hit.collider.GetComponentInParent<AudioSource>().Play();

            }

        }

        else //레이캐스트에 걸린게 없으면
        {
            itemEkey.SetActive(false); //획득 안뜨게
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

        if(moveon)//1챕터 움직이는 벽
            movemove();
    }
    void movemove()//1챕터 움직이는 벽
    {
        if (GameObject.Find("MovingBrick").transform.position.x > -11)
            GameObject.Find("MovingBrick").transform.position -= new Vector3(Time.deltaTime, 0, 0);
        else
            moveon = false;
    }

}
