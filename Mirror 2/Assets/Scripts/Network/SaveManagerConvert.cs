using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SaveManagerConvert : NetworkBehaviour
{
    public SaveManager dataSave;
    public string OnlineName;
    // Start is called before the first frame update

    void Start()
    {
        dataSave = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        if (isLocalPlayer)
        {
            GetSaveData();
        }
    }

    [Command(requiresAuthority = false)]
    private void GetSaveData()
    {
        OnlineName = dataSave.saveFile.name;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
