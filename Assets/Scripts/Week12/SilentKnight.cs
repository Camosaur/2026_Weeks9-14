
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class SilentKnight : MonoBehaviour
{

    public Tilemap tilemap;

    public Tile stone;

    public Vector3Int cellPos;

    public List<TileBase> walkableTiles;

    public Tile grass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public bool isThisOnGrass(Vector3 pos) {

        cellPos = tilemap.WorldToCell(pos);

        bool notOnGrass = true;

        foreach (TileBase theTile in walkableTiles)
        {
            if (theTile == tilemap.GetTile(cellPos)) { 
                notOnGrass = false;
                break;
            }
        }

        return notOnGrass;
    }

    public void changeTile() {

        TileBase currentTile = tilemap.GetTile(cellPos);

        if (walkableTiles.Contains(currentTile)) {
            
            int i  = walkableTiles.IndexOf(currentTile);
            //Debug.Log(i);

            if (i < walkableTiles.Count-1)
            {
                tilemap.SetTile(cellPos, walkableTiles[i + 1]);
            }
            else {
                tilemap.SetTile(cellPos, grass);
            }

        }
    }
}
