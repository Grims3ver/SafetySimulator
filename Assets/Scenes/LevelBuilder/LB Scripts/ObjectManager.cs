using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Custom Manager", menuName = "Tilemap Editor/Manager")]
public class ObjectManager : ScriptableObject
{
    public TileBase tileType; 
    public string tileName;

}
