using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGOPLAYER : MonoBehaviour
{

    public Camera PLAYER_CAMERA;

    // Start is called before the first frame update
    void Start()
    {
        PLAYER_CAMERA = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PLAYER_CAMERA.transform.position = new Vector3(transform.position.x, transform.position.y + 13.25f, transform.position.z - 10);
    }
}
