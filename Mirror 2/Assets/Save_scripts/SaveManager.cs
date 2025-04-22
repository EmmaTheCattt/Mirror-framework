using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{

    public GameObject[] Menu_players;
    public SaveFile saveFile;
    public string Input;

    [SerializeField] private TMP_Dropdown dropdown;

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
            saveFile.name = "Player";
            saveFile.Highscore = 0;
            saveFile.hero = 0;
        }

        //saving
        string JsonSave = JsonConvert.SerializeObject(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.Save();

        for (int i = 0; i < Menu_players.Length; i++)
        {
            Menu_players[i].SetActive(false);
        }

        Menu_players[saveFile.hero].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //saving
        string JsonSave = JsonConvert.SerializeObject(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.Save();
    }

    public void ReadStringInput(string s)
    {
        if (s.Length == 0)
        {
            saveFile.name = "Player";
        }
        else
        {
            saveFile.name = s;
        }
    }

    public void OnHeroChange()
    {
        saveFile.hero = dropdown.value;

        for (int i = 0; i < Menu_players.Length; i++)
        {
            Menu_players[i].SetActive(false);
        }

        Menu_players[saveFile.hero].SetActive(true);
    }
}
