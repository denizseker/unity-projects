using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class deneme : MonoBehaviour
{

    private GameObject player;
    private NavMeshAgent agent;
    public bool isFollow = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isFollow = !isFollow;
        }
        if (isFollow) //agent following
        {
            agent.SetDestination(player.transform.position);
        }
        else //agent stoping
        {
            agent.GetComponent<NavMeshAgent>().ResetPath();
        }

    }
}
