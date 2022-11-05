using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    //Player objesinin �arpma an�nda daha b�y�k etki yaratabilmesi i�in dynamite scripti i�indeki kodlar�n ayn�s�n� kullan�yorum.

    public float expForce, radius;
    private void OnCollisionEnter(Collision collision)
    {
        KnockBack();
    }

    private void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddExplosionForce(expForce, transform.position, radius);
            }
        }

    }
}
