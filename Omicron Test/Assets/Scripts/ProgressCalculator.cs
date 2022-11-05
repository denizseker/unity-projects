using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCalculator : MonoBehaviour
{
    private int dropCount;
    public Slider progressBar;
    public Text percentText;
    private void OnTriggerEnter(Collider other)
    {
        //Su ile engellerin etkile�ime girmesi
        if(other.transform.tag == "obstacle")
        {
            //D��en engel say�s�
            dropCount++;

            //D��en engel say�s� % cinsinden texte yazd�r�l�yor
            double dropPercent = dropCount * 0.5;
            percentText.text = "%"+ dropPercent.ToString();

            progressBar.value += 0.005f;
            //Engeller d��t�kten sonra yok ediliyor.
            Destroy(other.gameObject);
        }
    }
}
