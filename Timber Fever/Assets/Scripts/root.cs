using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    //prefab a�a�
    public GameObject treeBody;
    //game manager objesi
    public GameObject gameManager;
    //spawn edilen a�a�
    private GameObject newBody;
    //yapraklar�n a��lmas�n� tutan script
    private SkinnedMeshRenderer skinRend;

    //Rootun doluluk/aktiflik durumu
    public bool isRootEmpty = true;
    public bool isRootActive = false;


    private void OnTriggerExit(Collider other)
    {
        //Chainsaw node �zerinden ��kt��� zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise a�ac� olu�turuyoruz
            if (isRootActive)
            {
                //A�ac� rootun �zerine spawn ediyoruz
                newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.12f, transform.position.z), Quaternion.identity);
                //A�ac�n parentini root yap�yoruz
                newBody.transform.SetParent(transform);
                //A�ac� aktif hale getiriyoruz
                newBody.SetActive(true);
                //Rootun art�k dolu oldu�unu s�yl�yoruz
                isRootEmpty = false;
            }
            
        }
    }

    //Click eventi ile root kapatma
    public void CloseRoot()
    {
        //Root active ve bo� ise
        if (isRootActive && isRootEmpty)
        {
            //Root bo� oldu�u i�in bodyi olu�turuyoruz
            newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
            //B�y�m�� haline getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Bodyi yok ediyoruz
            Destroy(newBody);
            //Rootu deaktif edip bo� oldu�unu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Paray� ekleyip ekrana yazd�r�yoruz
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : " + gameManager.GetComponent<GameManager>().money + "$";
            gameObject.SetActive(false);
        }
        //Root active ama dolu ise (a�a� var)
        else
        {
            //A�ac� b�y�k hale getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Yok ediyoruz
            Destroy(newBody);
            //Rootu deaktif edip bo� oldu�unu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Paray� ekleyip ekrana yazd�r�yoruz
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
        //root aktifse ve bo� de�ilse �zerindeki a�ac� b�y�t�yoruz
        if(isRootActive && !isRootEmpty)
        {
            //Yapraklar� a�mak i�in skinnedmeshrendereri �ekiyorz
            skinRend = newBody.transform.GetChild(newBody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //A�a� b�y�kl��� 2 olana kadar b�y�t�yoruz
            if (newBody.transform.localScale.y <= 4.5f)
            {
                newBody.transform.localScale = new Vector3(newBody.transform.localScale.x, newBody.transform.localScale.y + 0.9f * Time.deltaTime, newBody.transform.localScale.z);
            }
            //A�a� b�y�kl��� 1.2 ye ula��nca 
            if(newBody.transform.localScale.y >= 3f)
            {
                //Yapraklar tamamen a��lana kadar b�y�t�yoruz.
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




    //A�a�lar�n b�y�yece�i sonsuz d�ng�
    //public IEnumerator SpawnTreeTimer()
    //{
    //    sayac = sayac + 1;
    //    isTimerActive = true;
    //    yield return new WaitForSeconds(9f);
    //    //B�y�me s�resi ge�tikten sonra root bo� ise a�ac� ekliyoruz.
    //    if (isEmpty)
    //    {

    //        GameObject body = Instantiate(treeBody,new Vector3(transform.position.x,transform.position.y + 0.6f,transform.position.z),Quaternion.identity);
    //        body.transform.SetParent(transform);
    //        isEmpty = false;
    //    }
    //    StartCoroutine(SpawnTreeTimer());
    //}


}
