using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_rotate : MonoBehaviour
{
    public Transform Look_cam;
    public Vector3 offset = new Vector3(0, 180f, 0);

    // Start is called before the first frame update
    void Start()
    {
        Look_cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Look_cam);
        transform.Rotate(offset);
    }
}
