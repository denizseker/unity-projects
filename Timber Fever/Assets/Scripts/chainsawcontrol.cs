using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsawcontrol : MonoBehaviour
{
    public GameObject gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tree")
        {
            //Bodynin rigidbodysini ve collider?n? ?ekiyoruz.
            Rigidbody treerb = other.GetComponent<Rigidbody>();
            CapsuleCollider treecol = other.GetComponent<CapsuleCollider>();
            //Bodynin kesildikten sonra istenilen noktaya d??mesi i?in kuvvet uyguluyoruz.
            treerb.AddForce(transform.forward * 50);
            treerb.AddForce(transform.up * 15);
            //A?a? kesildi?i i?in paray? artt?r?yoruz.
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = "Money : " + gameManager.GetComponent<GameManager>().money + "$";
            //Rootun art?k bo? oldu?unu belirtiyoruz ve parenttan ay?r?yoruz.
            other.GetComponentInParent<root>().isRootEmpty = true;
            //Destroy(other.transform.parent.gameObject); //Pivot bo? objeyi siliyoruz
            other.transform.SetParent(null);
            //Bodynin d??mesi i?in gravity a??yoruz ve trigger ?zelli?ini kapat?yoruz.
            treerb.useGravity = true;
            treecol.isTrigger = false;
        }

    }
}
