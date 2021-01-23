using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public float starting_money;
    public float bank;


    private GameObject[] balanceViewer;

    // Start is called before the first frame update
    void Start()
    {
        bank = starting_money;

        balanceViewer = GameObject.FindGameObjectsWithTag("Bank Balance Viewer");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in balanceViewer){
            g.GetComponent<Text>().text = bank.ToString();
        }
    }


    public bool spend(float value){
        if (bank >= value){
            bank -= value;
            return true;
        }
        else return false;
    }
}
