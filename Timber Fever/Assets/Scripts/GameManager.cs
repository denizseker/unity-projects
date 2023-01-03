using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject middlePipe;
    private int rotationSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        middlePipe.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
