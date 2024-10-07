using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //better text editing with pro than basic package
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public InventoryContainer inventory;

    private int imageSlot = 2;
    private int textSlot = 0; 

    TMPro.TMP_Text[] text;
    Image[] itemIcon;

    Dictionary<InventoryPlace, GameObject> allItems = new Dictionary<InventoryPlace, GameObject>();


    void Start()
    {
        text = GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        itemIcon = GetComponentsInChildren<Image>();
        BuildDisplay();
      
    }


    void Update()
    {
        UpdateDisplay();

    }

    public void BuildDisplay()
    {
        
        for (int i = 0; i < inventory.inventoryContents.Count; ++i)
        {
            //set the overall position (create in world - this is what will be added to the inventory)
            var temp = Instantiate(inventory.inventoryContents[i].itemContained.itemPrefab, Vector3.zero, Quaternion.identity, transform);
            

            itemIcon[imageSlot].sprite = inventory.inventoryContents[i].itemContained.itemSprite;

            //display the number of each item in each inventory place

           if (text[i] != null && !inventory.inventoryContents[i].itemContained.iType.ToString().Equals("CellPhone"))
            {

                text[i].text = inventory.inventoryContents[i].itemAmnt.ToString();
                
            }

            //it works out mathematically that each slot is two images away from one another 
            imageSlot += 2;
            ++textSlot; //keeps track of the current item frame
            allItems.Add(inventory.inventoryContents[i], temp);
      
        }

        for (int i = textSlot; i < text.Length; ++i)
        {
            text[i].text = "";
        }
        
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.inventoryContents.Count; ++i)
        {
            //already present, so update value
            if (allItems.ContainsKey(inventory.inventoryContents[i]))
            {
                text[i].text = inventory.inventoryContents[i].itemAmnt.ToString();
               
            } else
            {
                var temp = Instantiate(inventory.inventoryContents[i].itemContained.itemPrefab, Vector3.zero, Quaternion.identity, transform);
                //set inventory icon position
                itemIcon[imageSlot].sprite = inventory.inventoryContents[i].itemContained.itemSprite;
                //display the number of each item in each inventory place
                text[i].text = inventory.inventoryContents[i].itemAmnt.ToString();

              //  temp.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContents[i].itemAmnt.ToString();
                

                allItems.Add(inventory.inventoryContents[i], temp);
            }
        }
    }
}
