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

    void OnCreateCharactor(NetworkConnectionToClient connection, CreateCustomAvatarMessage message)
    {
        GameObject gameObject = Instantiate(avatars[message.AvatarIndex]);
        Player player = gameObject.GetComponent<Player>();
      
        NetworkServer.AddPlayerForConnection(connection, gameObject);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        NetworkServer.RegisterHandler<CreateCustomAvatarMessage>(OnCreateCharactor);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();


        CreateCustomAvatarMessage message = new()
        {
            //AvatarIndex = Random.Range(0, avatars.Length)
            AvatarIndex = saveData.saveFile.hero
        };

        NetworkClient.Send(message);
    }
}
