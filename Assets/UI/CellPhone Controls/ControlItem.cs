using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ControlItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryContainer inventory;
   // public Item item;

    private bool mouseOver = false;

    private void Start()
    {
        UpdateSlotInfo();
    }


    //todo: add behaviour for on click 

    public void Update()
    {
        if (mouseOver)
        {
            print("here!");
            //do nothing for now 
        }
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false; 
    }

   public void UpdateSlotInfo()
    {
        TMPro.TMP_Text tMP_Text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Image[] itemIcon = GetComponentsInChildren<Image>();

        //if (item)
       // {
            var temp = Instantiate(inventory.inventoryContents[0].itemContained.itemPrefab, Vector3.zero, Quaternion.identity, transform);
           itemIcon[1].sprite = inventory.inventoryContents[0].itemContained.itemSprite;

            tMP_Text.text= "test";

       // } else
      //  {
          //  tMP_Text.text = "";
            //itemIcon[1].sprite = null;
            //itemIcon[1].color = Color.clear;
       // }
    } 
}