using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<ObjectManager> objs = new List<ObjectManager>();

    private string levelNumb = "";

    //prepare instance if unassigned
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }


        if (SceneManager.GetActiveScene().name == "TilemapEditor")
        {
            levelNumb = "1";
        }
        else if (SceneManager.GetActiveScene().name == "TilemapEditor1")
        {
            levelNumb = "2";
        }
        else if (SceneManager.GetActiveScene().name == "TilemapEditor2")
        {
            levelNumb = "3";
        }

        //try to load the previous level if there is one, otherwise, do nothing
        try
        {
            string savePath = File.ReadAllText(Application.dataPath + "/testLevel" + levelNumb + ".json");
            SaveData loadData = JsonUtility.FromJson<SaveData>(savePath);
            Debug.Log("Loading Level!");
            //clear all previous tiles [including unsaved]

            currentTilemap.ClearAllTiles();

            for (int i = 0; i < loadData.tilePositions.Count; ++i)
            {
                //load the tiles
                currentTilemap.SetTile(loadData.tilePositions[i], SetPreviousTiles(loadData, i).tileType);
            }
        }
        catch (FileNotFoundException E)
        {
            return;
        }

    }

    public Tilemap currentTilemap;

    //this function is responsible for saving newly added tiles
    private ObjectManager GetNewTiles(TileBase holder)
    {
        //for each variable in our object list 
        foreach (var o in objs)
        {
            //check type, if match, return
            if (o.tileType == holder)
            {
                return o;
            }
        }
        //else return default, nothing to do
        return default(ObjectManager);
    }

    public void SaveLevel()
    {
        print("I'm saving!");
        //new SaveData object for later
        SaveData saveData = new SaveData();

        //get the total size of the map
        BoundsInt tilemapBoundary = currentTilemap.cellBounds;

        //iterate through to grab every tile
        for (int i = tilemapBoundary.xMin; i < tilemapBoundary.xMax; ++i)
        {
            for (int j = tilemapBoundary.yMin; j < tilemapBoundary.yMax; ++j)
            {
                //2d don't care about z
                TileBase holder = currentTilemap.GetTile(new Vector3Int(i, j, 0));
                ObjectManager holderObjID = GetNewTiles(holder);

                if (holderObjID != null)
                {
                    saveData.tiles.Add(holderObjID.tileName);
                    saveData.tilePositions.Add(new Vector3Int(i, j, 0));
                }
            }
        }
        //test path
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.dataPath + "/testLevel" + levelNumb + ".json", json);
    }

    //load tiles in a similar manner as to saving; cycle through all valid tiles, retrieve them if
    //they exist, and place them in the scene
    private ObjectManager SetPreviousTiles(SaveData loadData, int i)
    {
        //for each variable in our object list 
        foreach (var t in objs)
        {
            //check name if match, return
            if (t.tileName == loadData.tiles[i])
            {
                return t;
            }
        }
        //else return default, nothing to do
        return default(ObjectManager);
    }

   public void LoadLevel()
    {
        print("Loading Level!");
        //test path
        string savePath = File.ReadAllText(Application.dataPath + "/testLevel" + levelNumb + ".json");
        SaveData loadData = JsonUtility.FromJson<SaveData>(savePath);

        //clear all previous tiles [including unsaved]

        currentTilemap.ClearAllTiles();

        for (int i = 0; i < loadData.tilePositions.Count; ++i)
        {
            //load the tiles
            currentTilemap.SetTile(loadData.tilePositions[i], SetPreviousTiles(loadData, i).tileType);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel();
        }
    }

    public class SaveData
    {
        public List<string> tiles = new List<string>();
        public List<Vector3Int> tilePositions = new List<Vector3Int>();
    }
}

