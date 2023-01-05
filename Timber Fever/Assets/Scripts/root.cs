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


    //Chainsaw root �st�nden ge�ince
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise timer ba�l�yor ve a�a�lar b�y�yor.
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


    //A�a�lar�n b�y�yece�i sonsuz d�ng�
    public IEnumerator SpawnTreeTimer()
    {
        sayac = sayac + 1;
        isTimerActive = true;
        yield return new WaitForSeconds(7.2f);
        //B�y�me s�resi ge�tikten sonra root bo� ise a�ac� ekliyoruz.
        if (isEmpty)
        {

            GameObject body = Instantiate(treeBody,new Vector3(transform.position.x,transform.position.y + 0.6f,transform.position.z),Quaternion.identity);
            body.transform.SetParent(transform);
            isEmpty = false;
        }
        StartCoroutine(SpawnTreeTimer());
    }


}
