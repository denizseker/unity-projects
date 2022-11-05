using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Temelin d�n�� h�z�
    public float rotateSpeed;
    //Temel objenin atanmas�
    public GameObject platformBase;

   //Fixed update i�inde rotate speed ve y�n kullanarak objenin d�nd�r�lmesi
    void FixedUpdate()
    {
        platformBase.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
