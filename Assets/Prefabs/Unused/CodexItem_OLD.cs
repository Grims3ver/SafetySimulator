using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Codex Object", menuName = "Inventory/Item/Codex")]
public class CodexItem_OLD : ItemBase
{
    public void Awake()
    {
        iType = ItemType.Codex;
    }
}
