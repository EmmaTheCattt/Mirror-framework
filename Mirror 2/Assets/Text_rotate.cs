using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Text_rotate : MonoBehaviour
{

    public SaveManager SaveData;
    public TMP_Text name_text;
    public Transform Look_cam;
    public Vector3 offset = new Vector3(0, 180f, 0);

    public bool In_menu = true;

    // Start is called before the first frame update
    
    private void Start()
    {
        Look_cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        SaveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Look_cam);
        transform.Rotate(offset);
        
        if (SaveData.in_menu == true) 
        {
            name_text.text = SaveData.saveFile.name;
        }
    }
}
