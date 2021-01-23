using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class OfficeBuilder : MonoBehaviour
{
    public float OfficeCost;
    public Tilemap buildingLayer;
    public Tilemap groundLayer;
    public Tilemap ghostLayer;
    public Tilemap officeLayer;

    public Tile DeskTile;

    private GridLayout buildingGrid;
    private GridLayout groundGrid;
    private GridLayout ghostGrid;
    private GridLayout officeGrid;
    public Toggle DeskBtn;

    private GameObject economyObject;
    private Economy economy;
    // Start is called before the first frame update
    void Start()
    {
        buildingGrid = buildingLayer.layoutGrid;
        groundGrid = groundLayer.layoutGrid;
        ghostGrid = ghostLayer.layoutGrid;
        officeGrid = officeLayer.layoutGrid;

        economyObject = GameObject.FindWithTag("Economy");
        economy = economyObject.GetComponent<Economy>();

        DeskBtn.onValueChanged.AddListener(delegate{Ghost(DeskBtn);});
    }

    // Update is called once per frame
    void Update()
    {
        if (DeskBtn.isOn) Ghost(DeskBtn);
    }
    void Ghost(Toggle toggle){
        if(toggle.isOn){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellpos = buildingGrid.WorldToCell(pos);
            ghostLayer.ClearAllTiles();
            ghostLayer.SetTile(cellpos, DeskTile);
            if (validPlace(cellpos)){
                ghostLayer.color = Color.green;
                if(Input.GetMouseButton(0)){
                    if(economy.spend(OfficeCost)){
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
        Vector3Int CellPos = officeGrid.WorldToCell(Pos);
        Tile tile = DeskTile;
        officeLayer.SetTile(CellPos, tile);
        if(!Input.GetKey(KeyCode.LeftShift)){
            toggle.isOn = false;
        }
        ghostLayer.ClearAllTiles();
    }
    bool validPlace(Vector3Int CellPos){
        //is position over building.
        if (buildingLayer.HasTile(CellPos)){
            return true;
        }
        else {
            return false;
        }
    }
}
