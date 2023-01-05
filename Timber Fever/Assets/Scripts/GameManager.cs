using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text moneyText;
    public GameObject treeBody;
    public List<GameObject> roots;
    public GameObject middlePipe;
    private int rotationSpeed = 50;
    public int money = 0;
    private int clickCount = 1;
    



    private void Start()
    {
        moneyText.text = "Money : 0$";
        roots[0].GetComponent<root>().ActivateRoot();
        roots[0].GetComponent<Renderer>().material.color = Color.green;
    }

    public void OpenNewRoot()
    {
        roots[clickCount].GetComponent<root>().ActivateRoot();
        roots[clickCount].GetComponent<Renderer>().material.color = Color.green;
        clickCount++;
        //roots[5].GetComponent<root>().ActivateRoot();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        middlePipe.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
