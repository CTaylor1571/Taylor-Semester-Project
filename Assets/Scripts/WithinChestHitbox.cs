using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithinChestHitbox : MonoBehaviour
{
    [SerializeField]
    GameObject openChestText;

    [SerializeField]
    GameObject chest;

    [SerializeField]
    public GameObject itemPrefab;

    [NonSerialized]
    GameManagerScript gmScript;

    [SerializeField]
    private Sprite openSprite;

    bool openable = false;
    bool bought = false;
    int price;
    private void Start()
    {
        gmScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        price = gmScript.GetPrice();
        openChestText = GameObject.Find("OpenChestText");
        openChestText.GetComponent<Text>().color = Color.clear;
    }
    private void Update()
    {
        if (openable && Input.GetKeyDown(KeyCode.E) && !bought)
        {
            if (gmScript.GetCurrency() >= price)
            {
                Debug.Log("Opening chest");
                Instantiate(itemPrefab, gameObject.transform.position, Quaternion.identity);
                chest.GetComponent<SpriteRenderer>().sprite = openSprite;
                gmScript.Spend(price);
                bought = true;
                openable = false;
                openChestText.GetComponent<Text>().color = Color.clear;
            }
            else
            {
                Debug.Log("Insufficient funds");
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player") && !bought)
        {
            openable = true;
            openChestText.GetComponent<Text>().color = Color.white;
            openChestText.GetComponent<Text>().text = "E - Open: $" + price;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Equals("Player") && !bought)
        {
            openable = false;
            openChestText.GetComponent<Text>().color = Color.clear;
        }
    }
}
