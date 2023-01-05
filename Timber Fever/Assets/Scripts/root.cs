using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    public GameObject treeBody;

    public bool isEmpty = true;

    public bool isRootActive = false;

    public bool isTimerActive = false;
    public int sayac = 1;


    //Chainsaw root üstünden geçince
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise timer baþlýyor ve aðaçlar büyüyor.
            if (isRootActive && !isTimerActive)
            {
                StartCoroutine(SpawnTreeTimer());
            }
            
        }
    }

    //Click eventi ile rootu aktif ediyoruz.
    public void ActivateRoot()
    {
        Debug.Log(this.name + ": aktif edildi.");
        isRootActive = true;
    }


    //Aðaçlarýn büyüyeceði sonsuz döngü
    public IEnumerator SpawnTreeTimer()
    {
        sayac = sayac + 1;
        isTimerActive = true;
        yield return new WaitForSeconds(7.2f);
        //Büyüme süresi geçtikten sonra root boþ ise aðacý ekliyoruz.
        if (isEmpty)
        {

            GameObject body = Instantiate(treeBody,new Vector3(transform.position.x,transform.position.y + 0.6f,transform.position.z),Quaternion.identity);
            body.transform.SetParent(transform);
            isEmpty = false;
        }
        StartCoroutine(SpawnTreeTimer());
    }


}
