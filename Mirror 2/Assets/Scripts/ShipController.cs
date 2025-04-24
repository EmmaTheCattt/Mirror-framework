using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using Unity.VisualScripting;

public class ShipController : NetworkBehaviour
{

    public Rigidbody rb;
    public Collider col;

    public bool Player_on = false;

    public GameObject The_player = null;

    public Vector3 col_size;

    // Start is called before the first frame update
    void Start()
    {
        col_size = col.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_on == false && isLocalPlayer)
        {
            col.transform.localScale = new Vector3(2f, 2, 2f);
        }
        else
        {
            col.transform.localScale = col_size;
        }

        if (The_player != null && Player_on == true)
        {
            if (The_player.GetComponent<NetworkIdentity>().isLocalPlayer == true)
            {
                The_player.GetComponent<NavMeshAgent>().enabled = false;
                The_player.transform.position = new Vector3(transform.position.x, -100, transform.position.z);
                The_player.GetComponent<PlayerController>().On_ship = true;
                The_player.GetComponent<PlayerController>().camera_player.transform.position = new Vector3(transform.position.x, transform.position.y + 13.25f, transform.position.z - 10);

                if (transform.position.y < 10)
                {
                    transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_on = true;
            The_player = other.gameObject;
        }
    }
}
