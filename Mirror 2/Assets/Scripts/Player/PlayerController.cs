using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : NetworkBehaviour
{
    public Camera camera;
    public NavMeshAgent player;
    public GameObject Bullet;
    public GameObject tip;

    public GameObject Second_tip;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isLocalPlayer)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit destination;

            if (Physics.Raycast(ray, out destination))
            {
                player.SetDestination(destination.point);
            }
        }

        if (Input.GetMouseButtonDown(0) && isLocalPlayer)
        {
            Shot();

        }

    }

    [Command(requiresAuthority = false)]
    private void Shot()
    {
        GameObject Bul = Instantiate(Bullet, new Vector3(tip.transform.position.x, tip.transform.position.y, tip.transform.position.z), Quaternion.identity);
        Bul.transform.rotation = tip.transform.rotation;
        NetworkServer.Spawn(Bul);

        if (Second_tip != null)
        {
            GameObject Bul_v2 = Instantiate(Bullet, new Vector3(Second_tip.transform.position.x, Second_tip.transform.position.y, Second_tip.transform.position.z), Quaternion.identity);
            Bul_v2.transform.rotation = tip.transform.rotation;
            NetworkServer.Spawn(Bul_v2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }
    }
}
