using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_player_script : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Menu_players;
    public SaveManager SaveData;
    void Start()
    {
        SaveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Menu_players.Length; i++)
        {
            Menu_players[i].SetActive(false);
        }

        Menu_players[SaveData.saveFile.hero].SetActive(true);
    }
}
