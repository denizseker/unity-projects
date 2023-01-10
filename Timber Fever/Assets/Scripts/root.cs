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
        //Chainsaw node �zerinden ��kt��� zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise a�ac� olu�turuyoruz
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
        //root aktifse ve bo� de�ilse �zerindeki a�ac� b�y�t�yoruz
        if(isRootActive && !isEmpty)
        {
            //Yapraklar� a�mak i�in skinnedmeshrendereri �ekiyorz
            skinRend = newbody.transform.GetChild(newbody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //A�a� b�y�kl��� 2 olana kadar b�y�t�yoruz
            if (newbody.transform.localScale.y <= 2f)
            {
                newbody.transform.localScale = new Vector3(newbody.transform.localScale.x, newbody.transform.localScale.y + 0.3f * Time.deltaTime, newbody.transform.localScale.z);
            }
            //A�a� b�y�kl��� 1.2 ye ula��nca 
            if(newbody.transform.localScale.y >= 1.2f)
            {
                //Yapraklar tamamen a��lana kadar b�y�t�yoruz.
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
