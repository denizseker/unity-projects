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
            //Bodynin rigidbodysini ve collider�n� �ekiyoruz.
            Rigidbody treerb = other.GetComponent<Rigidbody>();
            CapsuleCollider treecol = other.GetComponent<CapsuleCollider>();
            //Bodynin kesildikten sonra istenilen noktaya d��mesi i�in kuvvet uyguluyoruz.
            treerb.AddForce(-transform.right * 60);
            treerb.AddForce(transform.up * 5);
            //A�a. kesildi�i i�in paray� artt�r�yoruz.
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : "+ gameManager.GetComponent<GameManager>().money +"$";
            //Rootun art�k bo� oldu�unu belirtiyoruz ve parenttan ay�r�yoruz.
            other.GetComponentInParent<root>().isEmpty = true;
            other.transform.SetParent(null);
            //Bodynin d��mesi i�in gravity a��yoruz ve trigger �zelli�ini kapat�yoruz.
            treerb.useGravity = true;
            treecol.isTrigger = false;
        }

    }
}
