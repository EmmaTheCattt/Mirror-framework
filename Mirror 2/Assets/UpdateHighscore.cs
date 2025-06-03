using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateHighscore : MonoBehaviour
{

    public SaveManager SaveData;
    public TMP_Text name_text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveData.in_menu == true)
        {
            name_text.text = "Highscore: " + Convert.ToString(SaveData.saveFile.Highscore);
        }
    }
}
