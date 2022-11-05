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
        //Su ile engellerin etkileþime girmesi
        if(other.transform.tag == "obstacle")
        {
            //Düþen engel sayýsý
            dropCount++;

            //Düþen engel sayýsý % cinsinden texte yazdýrýlýyor
            double dropPercent = dropCount * 0.5;
            percentText.text = "%"+ dropPercent.ToString();

            progressBar.value += 0.005f;
            //Engeller düþtükten sonra yok ediliyor.
            Destroy(other.gameObject);
        }
    }
}
