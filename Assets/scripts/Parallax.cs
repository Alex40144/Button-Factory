using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startposx, startposy;
    private Camera cam;
    public float parallaxEffect;

    private float textureUnitSizeX;

    void Start()
    {
        cam = Camera.main;
        
        startposx = transform.position.x;
        startposy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

    }

    void LateUpdate()
    {
        float distx = (cam.transform.position.x * parallaxEffect);
        float disty = (cam.transform.position.y * parallaxEffect);

        float temp = cam.transform.position.x * (1-parallaxEffect);

        this.transform.position = new Vector3(startposx + distx, startposy + disty, transform.position.z);
    }
}
