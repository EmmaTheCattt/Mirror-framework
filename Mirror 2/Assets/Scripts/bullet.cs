using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Speed = 5;
    public Collider Col;
    public PlayerController controller;
    public string owner;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player")) 
        {
            controller.highscore++;
            controller.saveFile.Highscore = controller.highscore;
            Debug.Log(controller.highscore);
            NetworkServer.Destroy(other.gameObject);
            NetworkServer.Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
