using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startposx, startposy;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distx = (cam.transform.position.x * parallaxEffect);
        float disty = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposx + distx, startposy + disty, transform.position.z);
    }
}
