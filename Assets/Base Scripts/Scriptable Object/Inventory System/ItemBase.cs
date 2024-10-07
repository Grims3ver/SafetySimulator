using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    CellPhone,
    Cleaning,
    Healing,
    Codex,
    Default
};
public abstract class ItemBase : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType iType;
    public Sprite itemSprite; 
    [TextArea(20,25)]
    public string itemDescrip; 
    

}
