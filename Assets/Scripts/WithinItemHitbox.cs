using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WithinItemHitbox : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    GameObject itemObject;

    [NonSerialized]
    public string[] itemArr = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6", "Item7" };

    [NonSerialized]
    GameObject itemCollectedText;

    [NonSerialized]
    GameObject gameMan;

    private void Start()
    {
        int rand = UnityEngine.Random.Range(0, 7);
        itemName = itemArr[rand];
        itemObject.GetComponent<SpriteRenderer>().sprite = sprites[rand];
        itemCollectedText = GameObject.Find("ItemCollectedText");
        gameMan = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            Debug.Log(itemName + " collected!");
            Destroy(itemObject);
            itemCollectedText.GetComponentInChildren<ItemCollectedTextScript>().Popup(itemName);
            //try GameManagerScript.CollectedItem()
            gameMan.GetComponentInChildren<GameManagerScript>().collectedItem(itemName);
        }
    }
}