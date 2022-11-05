using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    //Patlama g�c� ve alan�
    public float expForce, radius;

    //Patlama olu�masu i�in �arp��ma testi
    private void OnCollisionEnter(Collision collision)
    {
        //Player objesi �arpt��� zaman KnockBack fonksiyonu ile patlama sonras� etraftaki objelere velocity uygulan�yor.
        if (collision.transform.gameObject.name == "Man")
        {
            KnockBack();
            Destroy(gameObject);
        }
        
            
    }

    //Patlama an�nda etraftaki objelere velocity uygulama fonksiyonu
    private void KnockBack()
    {

        //Colliders listesi i�erisine objenin konumu ve etki alan� i�erisindeki di�er objeler at�l�yor.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //Alan i�erisindeki objelere foreach yard�m� ile s�ra s�ra g�� uygulan�yor
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            //Objenin rigidbodysi var ise
            if (rb != null)
            {
                //�arp��ma �ncesi objeler hareket etmesin diye kapal� ve a��k olan de�erler tersine �evriliyor.
                rb.useGravity = true;
                rb.isKinematic = false;
                //addexplosionforce fonksiyonu editorden ald���m�z de�erler ile objelere velocity uyguluyor.
                rb.AddExplosionForce(expForce, transform.position, radius);
            }
        }

    }


}
