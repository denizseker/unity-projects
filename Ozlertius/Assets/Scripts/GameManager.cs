using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SoldierScriptableObject soldier;
    public GameObject player;
    void Start()
    {
        Instantiate(soldier.prefab);
        Instantiate(soldier.prefab);

    }
    void Update()
    {
        SendRaycast();
    }

    void SendRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(player.transform.position,player.transform.TransformDirection(Vector3.forward),out hit))
        {

            hit.collider.GetComponent<>

            Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * hit.distance);
        }
    }
}
