using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildingBuilder : MonoBehaviour
{
    public Tilemap buildingLayer;
    public Tilemap groundLayer;
    public Tilemap ghostLayer;

    private GridLayout buildingGrid;
    private GridLayout groundGrid;
    private GridLayout ghostGrid;

    public Toggle emptyOfficeBtn;

    public Tile bothOfficeTile;
    public Tile leftOfficeTile;
    public Tile rightOfficeTile;
    public Tile middleOfficeTile;

    void Start()
    {
        buildingGrid = buildingLayer.layoutGrid;
        groundGrid = groundLayer.layoutGrid;
        ghostGrid = ghostLayer.layoutGrid;

		emptyOfficeBtn.onValueChanged.AddListener(delegate{Ghost(emptyOfficeBtn);});
    }

    void Update(){
        if (emptyOfficeBtn.isOn) Ghost(emptyOfficeBtn);
        //TODO if esc key is pressed turn of toggle.
    }

    //TODO change colour to show if position is acceptible 
    void Ghost(Toggle toggle){
        if(toggle.isOn){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellpos = buildingGrid.WorldToCell(pos);
            ghostLayer.ClearAllTiles();
            ghostLayer.SetTile(cellpos, bothOfficeTile);
            if(Input.GetMouseButton(0)){
                Build(Camera.main.ScreenToWorldPoint(Input.mousePosition), toggle);
            }
        }
    }



    void Build(Vector3 Pos, Toggle toggle){
        Vector3Int Position = buildingGrid.WorldToCell(Pos);
        Tile tile = getType(Position);
        buildingLayer.SetTile(Position, tile);
        RefreshTile(Position + Vector3Int.left);
        RefreshTile(Position + Vector3Int.right);
        //TODO fix refresh
        if(!Input.GetKey(KeyCode.LeftShift)){
            toggle.isOn = false;
            ghostLayer.ClearAllTiles();
        }
    }

    void RefreshTile(Vector3Int position){
        if (buildingLayer.GetTile(position)){
            Tile tile = getType(position);
            buildingLayer.SetTile(position, tile);
        }
    }

    Tile getType(Vector3 Position){
        //get if this is a both / left / right / middle tile
        Vector3Int position = buildingGrid.WorldToCell(Position);
        Vector3Int left = position + Vector3Int.left;
        Vector3Int right = position + Vector3Int.right;
        if (buildingLayer.GetTile(left) && !buildingLayer.GetTile(right)){
            return rightOfficeTile;
        }
        else if (!buildingLayer.GetTile(left) && buildingLayer.GetTile(right)){
            return leftOfficeTile;
        }
        else if (buildingLayer.GetTile(left) && buildingLayer.GetTile(right)){
            return middleOfficeTile;
        }
        else{
            return bothOfficeTile;
        }

    }
}

