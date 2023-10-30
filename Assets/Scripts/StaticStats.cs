using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStats
{
    public static int numItem1 = 0; // increases jumps
    public static int numItem2 = 0; // increases movement speed
    public static int numItem3 = 0; // increases accuracy
    public static int numItem4 = 0; // increases firing speed
    public static int numItem5 = 0; // increases critical chance
    public static int numItem6 = 0; // increases health
    public static int numItem7 = 0; // increases regen

    public static int stageNum = 0;
    public static double difficulty = 1;
    public static int numEnemies = 0;
    public static bool teleporterActive = false;

    public static double playerHealth = 100;
    public static double playerMaxHealth = 100;

    public static ArrayList itemsArr;
    public static void PlusItem(int itemNum)
    {
        if (itemNum == 1)
        {
            numItem1++;
        } else if (itemNum == 2)
        {
            numItem2++;
        } else if (itemNum == 3)
        {
            numItem3++;
        } else if (itemNum == 4)
        {
            numItem4++;
        } else if (itemNum == 5)
        {
            numItem5++;
        } else if (itemNum == 6)
        {
            numItem6++;
        } else if (itemNum == 7)
        {
            numItem7++;
        }
    }
    public static string getNameFromItemNum(string itemNum)
    {
        if (itemNum.Equals("Item1"))
        {
            return "Jump Wings";
        }
        else if (itemNum.Equals("Item2"))
        {
            return "Speed Solution";
        }
        else if (itemNum.Equals("Item3"))
        {
            return "Accuracy Chip";
        }
        else if (itemNum.Equals("Item4"))
        {
            return "Battery";
        }
        else if (itemNum.Equals("Item5"))
        {
            return "Red-Tinted Scope";
        }
        else if (itemNum.Equals("Item6"))
        {
            return "Heart";
        }
        else
        {
            return "Regeneration Potion";
        }
    }
}
