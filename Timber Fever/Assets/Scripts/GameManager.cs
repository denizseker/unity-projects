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
    private int rotationSpeed = 40;
    public int money;
    private int clickCount = 1;
    

    private void Start()
    {
        money = 200;
        moneyText.text = "Money :"+money+"$";
        roots[0].GetComponent<root>().ActivateRoot();
        roots[0].GetComponent<Renderer>().material.color = Color.green;
    }

    public void OpenNewRoot()
    {
        Debug.Log("Þuanki para :"+ money);

        if(money >= 5)
        {
            money = money - 5;
            moneyText.text = "Money : " + money + "$";
            roots[clickCount].GetComponent<root>().ActivateRoot();
            roots[clickCount].GetComponent<Renderer>().material.color = Color.green;
            clickCount++;
            //roots[5].GetComponent<root>().ActivateRoot();
        }
        else
        {
            Debug.Log("para yetersiz");
        }

    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        middlePipe.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
