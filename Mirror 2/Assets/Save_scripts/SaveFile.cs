using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

[System.Serializable]
public class SaveFile
{
    public string name;
    public int Highscore;
    public int hero;
    public bool in_menu;
}
