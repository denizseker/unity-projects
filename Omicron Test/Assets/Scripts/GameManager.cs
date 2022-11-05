using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Temelin dönüþ hýzý
    public float rotateSpeed;
    //Temel objenin atanmasý
    public GameObject platformBase;

   //Fixed update içinde rotate speed ve yön kullanarak objenin döndürülmesi
    void FixedUpdate()
    {
        platformBase.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
