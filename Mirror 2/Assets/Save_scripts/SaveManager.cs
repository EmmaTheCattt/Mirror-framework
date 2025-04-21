using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public SaveFile saveFile;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            string json = PlayerPrefs.GetString("Save");
            saveFile = JsonUtility.FromJson<SaveFile>(json);
        }
        else
        {
            saveFile = new SaveFile();
            saveFile.name = "";
            saveFile.Highscore = 0;
            saveFile.hero = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //saving
        string JsonSave = JsonUtility.ToJson(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.Save();
    }
}
