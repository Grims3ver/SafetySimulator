using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class TilemapManager : MonoBehaviour
{
 
    //grab all variables for level builder from script; avoid public refs

    //compress the bounds of the tilemap, and put a Box Collider 2D on the Layer "Ground"
    //this prevents player from constantly being in falling state
    //while still providing solid walls (as tilemap collider is on Default layer)
    void Start()
    {
        Tilemap baseMap = this.GetComponent<Tilemap>();
        TilemapRenderer baseRenderer = this.GetComponent<TilemapRenderer>();
        baseMap.CompressBounds();
     
        BoxCollider2D floorCollider = gameObject.GetComponent<BoxCollider2D>();

        //null check, prevents addition of unnecessary colliders
        if (!floorCollider)
        {
           floorCollider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        }

        //"switching" the collider should resize it if it has not been so already
        floorCollider.enabled = false;
        floorCollider.enabled = true; 

        int groundLayer = LayerMask.NameToLayer("Ground");
        floorCollider.gameObject.layer = groundLayer;
        baseMap.enabled = false;
        baseRenderer.enabled = false; 
    }

    
}
