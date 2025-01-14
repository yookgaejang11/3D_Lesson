using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int whtPotion;
    public GameObject Hp_Item;
    public GameObject Speed_Item;
    public enum Items
    {
        SpeedPotion,
        HpPotion,
    }
    public Items currentItem;


    public GameObject SetItem(GameObject obj)
    {
        whtPotion = Random.Range(1, 100);

        if (whtPotion <= 50)
        {
            currentItem = Items.SpeedPotion;
        }
        else
        {
            currentItem = Items.HpPotion;
        }

        switch(currentItem)
        {
            case Items.SpeedPotion:
                obj = Speed_Item;
                break;
            case Items.HpPotion:
                obj = Hp_Item;
                break;
        }

        return obj;
    }



}
