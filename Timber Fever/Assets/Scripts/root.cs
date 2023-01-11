using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    //prefab aðaç
    public GameObject treeBody;
    //game manager objesi
    public GameObject gameManager;
    //spawn edilen aðaç
    private GameObject newBody;
    //yapraklarýn açýlmasýný tutan script
    private SkinnedMeshRenderer skinRend;

    //Rootun doluluk/aktiflik durumu
    public bool isRootEmpty = true;
    public bool isRootActive = false;


    private void OnTriggerExit(Collider other)
    {
        //Chainsaw node üzerinden çýktýðý zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise aðacý oluþturuyoruz
            if (isRootActive)
            {
                //Aðacý rootun üzerine spawn ediyoruz
                newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.12f, transform.position.z), Quaternion.identity);
                //Aðacýn parentini root yapýyoruz
                newBody.transform.SetParent(transform);
                //Aðacý aktif hale getiriyoruz
                newBody.SetActive(true);
                //Rootun artýk dolu olduðunu söylüyoruz
                isRootEmpty = false;
            }
            
        }
    }

    //Click eventi ile root kapatma
    public void CloseRoot()
    {
        //Root active ve boþ ise
        if (isRootActive && isRootEmpty)
        {
            //Root boþ olduðu için bodyi oluþturuyoruz
            newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
            //Büyümüþ haline getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Bodyi yok ediyoruz
            Destroy(newBody);
            //Rootu deaktif edip boþ olduðunu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Parayý ekleyip ekrana yazdýrýyoruz
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : " + gameManager.GetComponent<GameManager>().money + "$";
            gameObject.SetActive(false);
        }
        //Root active ama dolu ise (aðaç var)
        else
        {
            //Aðacý büyük hale getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Yok ediyoruz
            Destroy(newBody);
            //Rootu deaktif edip boþ olduðunu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Parayý ekleyip ekrana yazdýrýyoruz
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : " + gameManager.GetComponent<GameManager>().money + "$";
            gameObject.SetActive(false);
        }
        

    }
    //Click eventi ile rootu aktif ediyoruz.
    public void ActivateRoot()
    {
        gameObject.SetActive(true);
        //Debug.Log(this.name + ": aktif edildi.");
        isRootActive = true;
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    private void FixedUpdate()
    {
        //root aktifse ve boþ deðilse üzerindeki aðacý büyütüyoruz
        if(isRootActive && !isRootEmpty)
        {
            //Yapraklarý açmak için skinnedmeshrendereri çekiyorz
            skinRend = newBody.transform.GetChild(newBody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //Aðaç büyüklüðü 2 olana kadar büyütüyoruz
            if (newBody.transform.localScale.y <= 4.5f)
            {
                newBody.transform.localScale = new Vector3(newBody.transform.localScale.x, newBody.transform.localScale.y + 0.9f * Time.deltaTime, newBody.transform.localScale.z);
            }
            //Aðaç büyüklüðü 1.2 ye ulaþýnca 
            if(newBody.transform.localScale.y >= 3f)
            {
                //Yapraklar tamamen açýlana kadar büyütüyoruz.
                if(skinRend.GetBlendShapeWeight(0) >= -75)
                {
                    float deneme = skinRend.GetBlendShapeWeight(0) - 80 * Time.deltaTime;
                    skinRend.SetBlendShapeWeight(0, deneme);
                }
                
            }

            //newbody.transform.GetChild(i).transform.localScale.y
            //newbody.transform.GetChild(i).transform.gameObject.SetActive(true);
            //newbody.transform.GetChild(i).transform.localScale = new Vector3(1, newbody.transform.GetChild(i).transform.localScale.y + 0.1f * Time.deltaTime, 1);
            //newbody.transform.localScale = new Vector3(newbody.transform.localScale.x, newbody.transform.localScale.y + 0.4f * Time.deltaTime, newbody.transform.localScale.z);
        }

    }




    //Aðaçlarýn büyüyeceði sonsuz döngü
    //public IEnumerator SpawnTreeTimer()
    //{
    //    sayac = sayac + 1;
    //    isTimerActive = true;
    //    yield return new WaitForSeconds(9f);
    //    //Büyüme süresi geçtikten sonra root boþ ise aðacý ekliyoruz.
    //    if (isEmpty)
    //    {

    //        GameObject body = Instantiate(treeBody,new Vector3(transform.position.x,transform.position.y + 0.6f,transform.position.z),Quaternion.identity);
    //        body.transform.SetParent(transform);
    //        isEmpty = false;
    //    }
    //    StartCoroutine(SpawnTreeTimer());
    //}


}
