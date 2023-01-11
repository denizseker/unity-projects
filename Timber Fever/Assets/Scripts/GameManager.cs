using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject cameraMain;
    public GameObject cameraPosLv2;
    public GameObject groundTop;
    public Text moneyText;
    public Button mergeButton;
    public Button addRootButton;
    public List<GameObject> rootsLv1;
    public List<GameObject> rootsLv2;
    public List<GameObject> rootsLv3;
    public List<GameObject> rootsLv4;
    public List<GameObject> rootsLv5;
    public GameObject sawMain;
    private int rotationSpeed = 40;
    public int money;
    public int mergedTreeCountLv2 = 1;
    public int mergedTreeCountLv3 = 0;
    public int mergedTreeCountLv4 = 0;
    public int mergedTreeCountLv5 = 0;
    public bool canMerge = false;
    public bool moveCam = false;

    private bool level2isActive = false;
    private bool level3isActive = false;
    private bool level4isActive = false;
    private bool level5isActive = false;




    private void Start()
    {
        //Oyun baþlangýcýnda ilk root aktif ediliyor
        money = 200;
        moneyText.text = "Money :"+money+"$";
        rootsLv1[0].GetComponent<root>().ActivateRoot();
    }

    
    public void addNewRoot(List<GameObject> _root)
    {
        //kaç adet root var ise dönecek loop
        for (int i = 0; i < _root.Count; i++)
        {
            //Para kontrol
            if (money >= 5)
            {
                //Root aktif deðilse
                if (!_root[i].GetComponent<root>().isRootActive)
                {
                    //Para eklenip root aktif hale geliyor
                    money -= 5;
                    moneyText.text = "Money : " + money + "$";
                    _root[i].GetComponent<root>().ActivateRoot();
                    break;
                }
            }
            else
            {
                Debug.Log("Para yetersiz");
            }
        }
    }

    //Butona týklandýðýnda lv1 e root ekleniyor
    public void rootButtonClick()
    {
        addNewRoot(rootsLv1);
    }

    //Merge yapma
    public void Merge()
    {
        if (canMerge)
        {
            canMerge = false;
            rootsLv1[0].GetComponent<root>().CloseRoot();
            rootsLv1[1].GetComponent<root>().CloseRoot();
            rootsLv1[2].GetComponent<root>().CloseRoot();
            rootsLv1[3].GetComponent<root>().CloseRoot();
            mergedTreeCountLv2 += 1;
            mergeButton.GetComponent<Button>().interactable = false;

            //level2 daha önce aktif deðil ise aktif ediliyor ve ilk aðaç ekleniyor
            if(!level2isActive)
            {
                //boruyu uzatýyoruz
                sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 50);
                //chain ana objesinin içerisindeki 2. child olan testereyi aktif ediyoruz
                sawMain.transform.GetChild(1).gameObject.SetActive(true);
                moveCam = true;
                //cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, cameraPosLv2.transform.position, 25 * Time.deltaTime);
                Debug.Log("Merge edildi");
                level2isActive = true;
                groundTop.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                rootsLv2[0].GetComponent<root>().ActivateRoot();

            }
            //level2 aktif ise merge edilen aðaç ekleniyor.
            else
            {
                addNewRoot(rootsLv2);

            }


        }
    }



    private void Update()
    {
        

        if(money >= 5)
        {
            addRootButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            addRootButton.GetComponent<Button>().interactable = false;
        }
        //Tüm rootlar aktif ve merge butonu aktif deðilse kontrol edip, merge olabilir durumu
        if(rootsLv1[0].GetComponent<root>().isRootActive && rootsLv1[1].GetComponent<root>().isRootActive && rootsLv1[2].GetComponent<root>().isRootActive && rootsLv1[3].GetComponent<root>().isRootActive && !canMerge)
        {
            canMerge = true;
            mergeButton.GetComponent<Button>().interactable = true;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Kamera hareketi
        if (moveCam)
        {
            Debug.Log("camera move");
            cameraMain.transform.position = Vector3.Lerp(cameraMain.transform.position, cameraPosLv2.transform.position, 10 * Time.deltaTime);
            if (cameraMain.transform.position == cameraPosLv2.transform.position)
            {
                Debug.Log("girdi");
                moveCam = false;
            }
        }
        sawMain.transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
