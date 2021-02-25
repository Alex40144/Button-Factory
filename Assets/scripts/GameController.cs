using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float panspeed = 10f;
    public float zoomFactor = 3f;
    public float zoomSpeed = 10f;
    public float minZoom;
    public float maxZoom;


    private Camera cam;
    private float targetZoom;
    public bool gamePaused = false;
    private GameObject[] pauseObjects;
    private GameObject[] BuildingToggles;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        //hide pause screen
        pauseObjects = GameObject.FindGameObjectsWithTag("Show On Pause"); //menu screen
        BuildingToggles = GameObject.FindGameObjectsWithTag("Disable On Pause"); //user interface
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        ManualCamera();
        Zoom();
        UI();
    }


    void Zoom(){
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        if (transform.position.y < -2.5 + cam.orthographicSize){
                this.transform.Translate(0, 1 * panspeed * Time.deltaTime, 0);
            }
    }

    void ManualCamera(){
        if (Input.GetKey("w")){
            if (transform.position.y < 1000){
                this.transform.Translate(0, 1 * panspeed * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey("s")){
            if (transform.position.y > -2.5 + cam.orthographicSize){
                this.transform.Translate(0, -1 * panspeed * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey("a")){
            if (transform.position.x > -50 + cam.orthographicSize * cam.aspect){
                this.transform.Translate( -1 * panspeed * Time.deltaTime, 0, 0);
            }  
        }
        if (Input.GetKey("d")){
            if (transform.position.x < 50 - cam.orthographicSize * cam.aspect){
                this.transform.Translate( 1 * panspeed * Time.deltaTime, 0, 0);
            }
        }
    }

    void UI(){
        if(Input.GetKeyDown(KeyCode.Escape)){ //TODO and something ins't active
            gamePaused = !gamePaused;
            Pause();
        }
    }
    void Pause(){
        if (gamePaused){
            Time.timeScale = 0f;
            foreach(GameObject item in pauseObjects){
                item.SetActive(true);
            }
            foreach(GameObject item in BuildingToggles){
                item.GetComponent<Toggle>().interactable = false;
            }
        }
        else{
            Time.timeScale = 1;
            foreach(GameObject item in pauseObjects){
                item.SetActive(false);
            }
            foreach(GameObject item in BuildingToggles){
                item.GetComponent<Toggle>().interactable = true;
            }
        }
    }

    public bool gameState(){
        return !gamePaused;
    }
}
