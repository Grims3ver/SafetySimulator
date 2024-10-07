using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory/Item/Healing")]
public class HealingItem : ItemBase
{
    public int healthRestored = 15;

        public void Awake()
        {
            iType = ItemType.Healing;
        }

      
    }


