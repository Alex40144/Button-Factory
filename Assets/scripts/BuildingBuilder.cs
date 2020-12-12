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
            if (validPlace(cellpos)){
                ghostLayer.color = Color.green;
                if(Input.GetMouseButton(0)){
                    Build(Camera.main.ScreenToWorldPoint(Input.mousePosition), toggle);
                }
            }
            else {
                ghostLayer.color = Color.red;
            }
        }
        else {
            ghostLayer.ClearAllTiles();
        }
    }



    void Build(Vector3 Pos, Toggle toggle){
        Vector3Int CellPos = buildingGrid.WorldToCell(Pos);
        Tile tile = getType(CellPos);
        buildingLayer.SetTile(CellPos, tile);
        RefreshTile(CellPos + new Vector3Int( 1, 0, 0));
        RefreshTile(CellPos + new Vector3Int(-1, 0, 0));
        if(!Input.GetKey(KeyCode.LeftShift)){
            toggle.isOn = false;
        }
        ghostLayer.ClearAllTiles();
    }
    //TODO fix refresh
    void RefreshTile(Vector3Int CellPos){
        if (buildingLayer.HasTile(CellPos)){
            Tile tile = getType(CellPos);
            buildingLayer.SetTile(CellPos, tile);
        }
    }

    Tile getType(Vector3Int CellPos){
        //get if this is a both / left / right / middle tile
        Vector3Int right = CellPos + new Vector3Int(1, 0, 0);
        Vector3Int left = CellPos + new Vector3Int(-1, 0, 0);
        if (buildingLayer.HasTile(left) && !buildingLayer.HasTile(right)){
            return rightOfficeTile;
        }
        else if (!buildingLayer.HasTile(left) && buildingLayer.HasTile(right)){
            return leftOfficeTile;
        }
        else if (buildingLayer.HasTile(left) && buildingLayer.HasTile(right)){
            return middleOfficeTile;
        }
        else{
            return bothOfficeTile;
        }

    }

    bool validPlace(Vector3Int CellPos){
        //is position above building or above ground.
        Vector3Int down = CellPos + new Vector3Int(0, -1, 0);
        if (buildingLayer.HasTile(down) || CellPos.y == -7){
            return true;
        }
        else {
            return false;
        }
    }
}

