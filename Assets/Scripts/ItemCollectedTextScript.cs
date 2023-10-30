using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectedTextScript : MonoBehaviour
{

    [NonSerialized]
    Text t;

    int numStarted;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        t.color = Color.clear;
        numStarted = 0;
    }

    // Update is called once per frame
    public void Popup(string itemName)
    {
        itemName = StaticStats.getNameFromItemNum(itemName);
        t.text = itemName + " Collected!";
        StartCoroutine(waiter());    
    }

    IEnumerator waiter()   //https://stackoverflow.com/a/30065183 for the waiting for seconds implementation
    {
        numStarted++;
        t.color = Color.white;
        yield return new WaitForSeconds(2);
        if(numStarted<2) { 
            t.color = Color.clear;
        }
        numStarted--;
    }
}
