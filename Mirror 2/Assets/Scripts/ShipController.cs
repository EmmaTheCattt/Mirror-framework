using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShipController : NetworkBehaviour
{

    public Rigidbody rb;
    public Animator ani;

    public bool Player_on;

    public GameObject The_player;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_on = true;
            ani.SetBool("Fly", Player_on);
        }
    }
}
