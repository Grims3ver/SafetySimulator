using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cleaning Object", menuName = "Inventory/Item/Cleaning")]
public class CleaningItem : ItemBase
{
    public void Awake()
    {
        iType = ItemType.Cleaning;
    }
}

