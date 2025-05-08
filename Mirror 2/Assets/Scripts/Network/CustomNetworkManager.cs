using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class CustomNetworkManager : NetworkManager
{
    public GameObject[] avatars;
    public SaveManager saveData;

    private void Awake()
    {
        saveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    private void Update()
    {
        saveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }


    void OnCreateCharactor(NetworkConnectionToClient connection, CreateCustomAvatarMessage message)
    {

        saveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        GameObject gameObject = Instantiate(avatars[message.AvatarIndex]);
        gameObject.GetComponent<PlayerController>().name_text.text = message.AvatarName; 
        Player player = gameObject.GetComponent<Player>();


        NetworkServer.AddPlayerForConnection(connection, gameObject);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        saveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        NetworkServer.RegisterHandler<CreateCustomAvatarMessage>(OnCreateCharactor);
    }

    public override void OnClientConnect()
    {

        saveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        base.OnClientConnect();

        CreateCustomAvatarMessage message = SetNames();

        NetworkClient.Send(message);
    }

    private CreateCustomAvatarMessage SetNames()
    {
        CreateCustomAvatarMessage message = new()
        {
            //AvatarIndex = Random.Range(0, avatars.Length)
            AvatarIndex = saveData.saveFile.hero,
            AvatarName = saveData.saveFile.name
        };
        return message;
    }
}
