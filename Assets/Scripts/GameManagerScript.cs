using NinjaController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField]
    GameObject currencyDisplayText;

    [SerializeField]
    GameObject itemDisplayText;

    GameObject healthBar;

    int currencyReal;
    int currencyDisplay;

    int chestPrice = 300;

    bool isAlive;

    ArrayList itemArr;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        healthBar = GameObject.Find("Slider");
        chestPrice = 300;
        currencyReal = 0;
        currencyDisplay = 0;
        if (StaticStats.stageNum == 0)
        {
            itemArr = new ArrayList { };
        }
        else
        {
            itemArr = StaticStats.itemsArr;
        }
        UpdateItemDisplay();
        StaticStats.playerHealth = StaticStats.playerMaxHealth;
        StaticStats.numEnemies = 6;
        StartCoroutine(DifficultyIncrement());
    }
    IEnumerator DifficultyIncrement()
    {
        yield return new WaitForSeconds(32);
        StaticStats.difficulty += 0.05;
        Debug.Log("Difficult increased. Multiplier: " +  StaticStats.difficulty);
        StartCoroutine(DifficultyIncrement());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Earn(200);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            StaticStats.playerHealth-=25;
        }

        if (StaticStats.playerHealth < StaticStats.playerMaxHealth && isAlive)
        {
            if (StaticStats.playerHealth> 0)
            {
                StaticStats.playerHealth += 0.004 + 0.001 * StaticStats.numItem7;
            }
            else
            {
                isAlive = false;
                Debug.Log("Death - GameManagerScript.cs");
            }
        }

        if (currencyReal>currencyDisplay) 
        {
            currencyDisplay++;
        } else if (currencyReal < currencyDisplay)
        {
            currencyDisplay--;
        }


        UpdateMoneyDisplay();
    }

    public void collectedItem(string item)
    {
        int num = 0;
        if (item.Equals("Item6"))
        {
            StaticStats.playerHealth += 15;
            StaticStats.playerMaxHealth += 15;
        }
        foreach (string collectedItem in  itemArr)
        {
            if (collectedItem.IndexOf(item)!=-1)
            {
                int quantity = int.Parse(collectedItem.Substring(collectedItem.Length - 1));
                itemArr[num] = collectedItem.Substring(0,collectedItem.Length-1) + (quantity+1);
                StaticStats.PlusItem(int.Parse(item.Substring(item.Length - 1)));
                UpdateItemDisplay();
                return;
            }
            num++;
        }
        itemArr.Add(item + " x1");
        StaticStats.PlusItem(int.Parse(item.Substring(item.Length - 1)));
        UpdateItemDisplay();
        healthBar.GetComponent<HealthBar>().UpdateHealth();
    }


    void UpdateItemDisplay()
    {
        string text = "Items:\r\n----------------------------\r\n";
        foreach (string collectedItem in itemArr)
        {
            string itemName = StaticStats.getNameFromItemNum(collectedItem.Substring(0, collectedItem.Length - 3)) + collectedItem.Substring(collectedItem.Length-3);
            text += itemName + "\r\n";
        }
        itemDisplayText.GetComponent<Text>().text = text;
    }

    void UpdateMoneyDisplay()
    {
        currencyDisplayText.GetComponent<Text>().text = "Money: "+currencyDisplay;
    }



    public int GetCurrency()
    {
        return currencyReal;
    }

    public int GetPrice()
    {
        return (int)(chestPrice * StaticStats.difficulty);
    }

    public void Spend(int amount)
    {
        currencyReal -= amount;
    }
    public void Earn(int amount)
    {
        currencyReal += amount;
    }

    public ArrayList GetArr()
    {
        return itemArr;
    }
}
