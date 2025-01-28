using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {
    public float speed;
    public float clampLeft;
    public float clampRight;

    private float cameraX;

    // Use this for initialization
    void Start () {
        cameraX = transform.position.x;
		
	}
	
	// Update is called once per frame
	void Update () {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        
    }
}
