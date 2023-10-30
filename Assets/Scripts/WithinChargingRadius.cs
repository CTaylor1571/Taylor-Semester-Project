using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithinChargingRadius : MonoBehaviour
{

    [SerializeField]
    GameObject percentageText;

    [SerializeField]
    GameObject teleporter;

    int percentage;
    bool withinRadius;

    public void StartCharging()
    {
        percentage = 0;
        percentageText.GetComponent<Text>().text = "0%";
        InvokeRepeating("ChargeUpdate", 0f, 0.95f);
    }

    private void ChargeUpdate()
    {
        if (withinRadius)
        {
            if (percentage >= 100)
            {
                teleporter.GetComponent<WithinTeleporterHitbox>().EndSequence();
            }
            else
            {
                percentage++;
                percentageText.GetComponent<Text>().text = percentage.ToString() + "%";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            percentageText.GetComponent<Text>().color = Color.white;
            withinRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            withinRadius = false;
            percentageText.GetComponent<Text>().color = Color.red;
        }
    }
}
