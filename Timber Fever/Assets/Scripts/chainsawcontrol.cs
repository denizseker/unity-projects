using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsawcontrol : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tree")
        {
            Rigidbody treerb = other.GetComponent<Rigidbody>();
            CapsuleCollider treecol = other.GetComponent<CapsuleCollider>();
            treerb.AddForce(-transform.right * 60);
            treerb.AddForce(transform.up * 5);

            treerb.useGravity = true;
            treecol.isTrigger = false;
        }
    }
}
