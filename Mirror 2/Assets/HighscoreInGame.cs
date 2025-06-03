using Mirror;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreInGame : MonoBehaviour
{

    public SaveFile saveFile;
    public TMP_Text name_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            string json = PlayerPrefs.GetString("save");
            saveFile = JsonConvert.DeserializeObject<SaveFile>(json);
        }

        name_text.text = "Highscore: " + Convert.ToString(saveFile.Highscore);
    }
}
