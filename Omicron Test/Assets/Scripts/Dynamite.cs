using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    //Patlama gücü ve alaný
    public float expForce, radius;

    //Patlama oluþmasu için çarpýþma testi
    private void OnCollisionEnter(Collision collision)
    {
        //Player objesi çarptýðý zaman KnockBack fonksiyonu ile patlama sonrasý etraftaki objelere velocity uygulanýyor.
        if (collision.transform.gameObject.name == "Man")
        {
            KnockBack();
            Destroy(gameObject);
        }
        
            
    }

    //Patlama anýnda etraftaki objelere velocity uygulama fonksiyonu
    private void KnockBack()
    {

        //Colliders listesi içerisine objenin konumu ve etki alaný içerisindeki diðer objeler atýlýyor.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //Alan içerisindeki objelere foreach yardýmý ile sýra sýra güç uygulanýyor
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            //Objenin rigidbodysi var ise
            if (rb != null)
            {
                //Çarpýþma öncesi objeler hareket etmesin diye kapalý ve açýk olan deðerler tersine çevriliyor.
                rb.useGravity = true;
                rb.isKinematic = false;
                //addexplosionforce fonksiyonu editorden aldýðýmýz deðerler ile objelere velocity uyguluyor.
                rb.AddExplosionForce(expForce, transform.position, radius);
            }
        }

    }


}
