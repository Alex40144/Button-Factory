using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")){
            if (transform.position.y < 1000){
                this.transform.Translate(0, 1 * speed * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey("s")){
            if (transform.position.y > 0){
                this.transform.Translate(0, -1 * speed * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey("a")){
            if (transform.position.x > -10){
                this.transform.Translate( -1 * speed * Time.deltaTime, 0, 0);
            }  
        }
        if (Input.GetKey("d")){
            if (transform.position.x < 10){
                this.transform.Translate( 1 * speed * Time.deltaTime, 0, 0);
            }
        }
    }
}
