using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{

    public SaveFile saveFile;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            string json = PlayerPrefs.GetString("save");
            saveFile = JsonConvert.DeserializeObject<SaveFile>(json);
        }
        else
        {
            saveFile = new SaveFile();
            saveFile.name = "";
            saveFile.Highscore = 0;
            saveFile.hero = 0;
        }

        //saving
        string JsonSave = JsonConvert.SerializeObject(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        //saving
        string JsonSave = JsonConvert.SerializeObject(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.Save();
    }
}
