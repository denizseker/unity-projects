using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    public GameObject treeBody;

    private GameObject newbody;
    private SkinnedMeshRenderer skinRend;

    public bool isEmpty = true;

    public bool isRootActive = false;

    public bool isTimerActive = false;
    public int sayac = 1;


    private void OnTriggerExit(Collider other)
    {
        //Chainsaw node üzerinden çýktýðý zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise aðacý oluþturuyoruz
            if (isRootActive && !isTimerActive)
            {
                newbody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
                newbody.transform.SetParent(transform);
                newbody.gameObject.SetActive(true);
                isEmpty = false;
            }
            
        }
    }

    //Click eventi ile rootu aktif ediyoruz.
    public void ActivateRoot()
    {
        
        Debug.Log(this.name + ": aktif edildi.");
        isRootActive = true;
    }

    private void FixedUpdate()
    {
        //root aktifse ve boþ deðilse üzerindeki aðacý büyütüyoruz
        if(isRootActive && !isEmpty)
        {
            //Yapraklarý açmak için skinnedmeshrendereri çekiyorz
            skinRend = newbody.transform.GetChild(newbody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //Aðaç büyüklüðü 2 olana kadar büyütüyoruz
            if (newbody.transform.localScale.y <= 2f)
            {
                newbody.transform.localScale = new Vector3(newbody.transform.localScale.x, newbody.transform.localScale.y + 0.3f * Time.deltaTime, newbody.transform.localScale.z);
            }
            //Aðaç büyüklüðü 1.2 ye ulaþýnca 
            if(newbody.transform.localScale.y >= 1.2f)
            {
                //Yapraklar tamamen açýlana kadar büyütüyoruz.
                if(skinRend.GetBlendShapeWeight(0) >= 0)
                {
                    skinRend.SetBlendShapeWeight(0, skinRend.GetBlendShapeWeight(0) - 40 * Time.deltaTime);
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
