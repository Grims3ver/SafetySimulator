using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    //we need to have reference to the tilemap and current tile in our scene
    [SerializeField] Tilemap currentTilemap;
    //TileBase holds a wider range of objects than simply Tile
    [SerializeField] TileBase currentTile;
    //reference to the camera
    [SerializeField] Camera mainCamera;

    private void Update()
    {
        //get mouse position
        Vector3Int mousePosition = currentTilemap.WorldToCell(mainCamera.ScreenToWorldPoint(Input.mousePosition));

        //place tile on left click at position
        if (Input.GetMouseButton(0))
        {
            AddTile(mousePosition);
        //remove tile on right click
        } else if (Input.GetMouseButton(1))
        {
            RemoveTile(mousePosition);
        }
    } 

    void AddTile(Vector3Int position)
    {
        currentTilemap.SetTile(position, currentTile);
    }

    void RemoveTile(Vector3Int position)
    {
        //setting current tile to null
        currentTilemap.SetTile(position, null); 
    }
}
