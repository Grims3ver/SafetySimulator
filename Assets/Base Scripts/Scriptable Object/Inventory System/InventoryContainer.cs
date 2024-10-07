using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryContainer : ScriptableObject
{
    public List<InventoryPlace> inventoryContents = new List<InventoryPlace>();


    public void AddItemToInventory(ItemBase newItem, int newAmnt)
    {
        bool duplicateItem = false; 
        for (int i = 0; i < inventoryContents.Count; ++i)
        {
            //if they have the same prefab + descrip (must be the same object)
            if (newItem.itemPrefab == inventoryContents[i].itemContained.itemPrefab && newItem.itemDescrip == inventoryContents[i].itemContained.itemDescrip)
            {
                inventoryContents[i].AddItems(newAmnt); //add the item
                duplicateItem = true; 
                break;
            }
        }

        if (!duplicateItem)
        {
            InventoryPlace itemToAdd = new InventoryPlace(newItem, newAmnt);
            inventoryContents.Add(itemToAdd);
        }
    }

}

[System.Serializable]
public class InventoryPlace
{

    public ItemBase itemContained;  
    public int itemAmnt; 

    public InventoryPlace(ItemBase newItem, int itemAmnt) 
    {
        itemContained = newItem;
        this.itemAmnt = itemAmnt; 
    }

    public void AddItems(int amntToAdd)
    {
        itemAmnt += amntToAdd;
    }

}
