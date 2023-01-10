using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsawcontrol : MonoBehaviour
{
    public GameObject gameManager;


    public void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tree")
        {
            //Bodynin rigidbodysini ve colliderýný çekiyoruz.
            Rigidbody treerb = other.GetComponent<Rigidbody>();
            CapsuleCollider treecol = other.GetComponent<CapsuleCollider>();
            //Bodynin kesildikten sonra istenilen noktaya düþmesi için kuvvet uyguluyoruz.
            treerb.AddForce(transform.forward * 50);
            treerb.AddForce(transform.up * 15);
            //Aðaç kesildiði için parayý arttýrýyoruz.
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : " + gameManager.GetComponent<GameManager>().money + "$";
            //Rootun artýk boþ olduðunu belirtiyoruz ve parenttan ayýrýyoruz.
            other.GetComponentInParent<root>().isEmpty = true;
            //Destroy(other.transform.parent.gameObject); //Pivot boþ objeyi siliyoruz
            other.transform.SetParent(null);
            //Bodynin düþmesi için gravity açýyoruz ve trigger özelliðini kapatýyoruz.
            treerb.useGravity = true;
            treecol.isTrigger = false;
        }

    }
}
