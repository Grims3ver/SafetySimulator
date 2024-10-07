using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Default Object", menuName = "Inventory/Item/Default")]
public class DefaultItem : ItemBase
{
    public void Awake()
    {
        iType = ItemType.Default;
    }
}
