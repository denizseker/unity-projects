using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    //Player objesinin çarpma anýnda daha büyük etki yaratabilmesi için dynamite scripti içindeki kodlarýn aynýsýný kullanýyorum.

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
