using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildingBuilder : MonoBehaviour
{
    public float buildingCost;

    public Tilemap buildingLayer;
    public Tilemap groundLayer;
    public Tilemap ghostLayer;

    private GridLayout buildingGrid;
    private GridLayout groundGrid;
    private GridLayout ghostGrid;

    public Toggle BuildingBtn;

    public Tile bothBuildingTile;
    public Tile leftBuildingTile;
    public Tile rightBuildingTile;
    public Tile middleBuildingTile;


    private GameObject economyObject;
    private Economy economy;

    void Start()
    {
        buildingGrid = buildingLayer.layoutGrid;
        groundGrid = groundLayer.layoutGrid;
        ghostGrid = ghostLayer.layoutGrid;

		BuildingBtn.onValueChanged.AddListener(delegate{Ghost(BuildingBtn);});

        economyObject = GameObject.FindWithTag("Economy");
        economy = economyObject.GetComponent<Economy>();
    }

    void Update(){
        if (BuildingBtn.isOn) Ghost(BuildingBtn);
        if (!this.BuildingBtn.interactable || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1)){
            stop(BuildingBtn);
        }
    }

    void Ghost(Toggle toggle){
        if(toggle.isOn){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellpos = buildingGrid.WorldToCell(pos);
            ghostLayer.ClearAllTiles();
            ghostLayer.SetTile(cellpos, bothBuildingTile);
            if (validPlace(cellpos)){
                ghostLayer.color = Color.green;
                if(Input.GetMouseButton(0)){
                    if(economy.spend(buildingCost)){
                        Build(Camera.main.ScreenToWorldPoint(Input.mousePosition), toggle);
                    }
                    //TODO flair that says "not enough money!"
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
            return rightBuildingTile;
        }
        else if (!buildingLayer.HasTile(left) && buildingLayer.HasTile(right)){
            return leftBuildingTile;
        }
        else if (buildingLayer.HasTile(left) && buildingLayer.HasTile(right)){
            return middleBuildingTile;
        }
        else{
            return bothBuildingTile;
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
    void stop(Toggle toggle){
        toggle.isOn = false;
    }
}

