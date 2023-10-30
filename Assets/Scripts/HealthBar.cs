using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    

    [SerializeField]
    GameObject fillArea;
    bool isAlive;
    void Start()
    {
        isAlive = true;
        // set slider to 100 (StaticStats.playerMaxHealth)
        float maxHealth = Convert.ToSingle(StaticStats.playerMaxHealth);
        GetComponent<Slider>().maxValue = maxHealth;
        GetComponent<Slider>().value = maxHealth;
    }
    public void UpdateHealth()
    {
        GetComponent<Slider>().maxValue = Convert.ToSingle(StaticStats.playerMaxHealth);
    }
    void Update()
    {
        GetComponent<Slider>().value = Convert.ToSingle(StaticStats.playerHealth);
        if (StaticStats.playerHealth <= 0 && isAlive)
        {
            fillArea.SetActive(false);
            Debug.Log("\"Death\" - HealthBar.cs");
            isAlive = false;
        }
        GetComponentInChildren<Text>().text = "" + (int)Convert.ToSingle(StaticStats.playerHealth) + "/" + (int)Convert.ToSingle(StaticStats.playerMaxHealth);
    }
}
