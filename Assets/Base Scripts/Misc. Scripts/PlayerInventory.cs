using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryContainer inventory;

    public Canvas inventoryCanvas;
    private bool actionsEnabled;

    public void Start()
    {
        inventoryCanvas.enabled = false;
        actionsEnabled = true; 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();

        //item exists
        if (item != null)
        {
            inventory.AddItemToInventory(item.item, 1);
            Destroy(collision.gameObject);
        }
    }



    public void Update()
    {
        CheckIfInventoryButtonPressed();
    }

    public void CheckIfInventoryButtonPressed()
    {

        //if the canvas is NOT already open, open it!
        if (Input.GetKeyUp(KeyCode.E) && !inventoryCanvas.enabled && actionsEnabled)
        {
            inventoryCanvas.enabled = true; 
        } else if (Input.GetKeyUp(KeyCode.E) && inventoryCanvas.enabled && actionsEnabled)
        {
            inventoryCanvas.enabled = false;
        }
    }

    public void DisableInventoryInterface() { actionsEnabled = false; }
    public void EnableInventoryInterface() { actionsEnabled = true; }

    //todo: fix inventory ON CLOSE behaviour later
    /*private void OnApplicationQuit()
    {
        inventory.inventoryContents.Clear();
    } */

    /* current behaviour: empty inventory on quit 
 * this may be changed later, if we want to save inventory between saves
 * */
}
