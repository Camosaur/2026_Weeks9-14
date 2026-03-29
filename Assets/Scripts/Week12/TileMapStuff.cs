using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
//using Unity.Cinemachine;

public class TileMapStuff : MonoBehaviour
{

    public Tilemap tilemap;

    public Transform duck;

    public Tile flower;

    //public CinemachineImpulseSource 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

        Vector3 gridPosition = tilemap.GetCellCenterWorld(cellPos);

        //Debug.Log(cellPos);

        duck.position = gridPosition;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log(tilemap.GetTile(cellPos));
            tilemap.SetTile(cellPos, flower);
        }
    }
}
