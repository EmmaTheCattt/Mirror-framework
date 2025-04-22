using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SocialPlatforms;
using Telepathy;

public class PlayerController : NetworkBehaviour
{
    public Camera camera_player;
    public NavMeshAgent player;
    public GameObject Bullet;
    public GameObject tip;
    public NavMeshAgent NAVIGATION;
    public TMP_Text name_text;
    public SaveManager SaveData;

    public GameObject Second_tip;

    public float shoot_interval = 0.5f;
    public float current_interval = 0;

    public Transform[] spawns;
    public int random_num;

    public Transform Look_cam;
    public Vector3 offset = new Vector3(0, 180f, 0);

    private void Awake()
    {
        camera_player = FindObjectOfType<Camera>();
        NAVIGATION = GetComponent<NavMeshAgent>();
        SaveData = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        Look_cam = GameObject.Find("Main Camera").GetComponent<Transform>();

        NAVIGATION.enabled = false;

        GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("Point");

        spawns = new Transform[SpawnPoints.Length];

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            spawns[i] = SpawnPoints[i].transform;
        }

        random_num = Random.Range(0, spawns.Length);
        transform.position = new Vector3(spawns[random_num].position.x, transform.position.y, spawns[random_num].position.z);
        NAVIGATION.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current_interval += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && isLocalPlayer)
        {
            Ray ray = camera_player.ScreenPointToRay(Input.mousePosition);
            RaycastHit destination;

            if (Physics.Raycast(ray, out destination))
            {
                player.SetDestination(destination.point);
            }
        }

        if (Input.GetMouseButtonDown(0) && isLocalPlayer)
        {
            if (shoot_interval <= current_interval)
            {
                Shot();
                current_interval = 0;
            }
        }

        if (isLocalPlayer)
        {
            Camera_on_player();
            Name_players();
        }

        TAGPLAYER();
    }

    [ClientCallback]
    private void TAGPLAYER()
    {
        name_text.transform.LookAt(Look_cam);
        name_text.transform.Rotate(offset);
    }

    [Command(requiresAuthority = false)]
    private void Name_players()
    {
        name_text.text = SaveData.saveFile.name;
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

    [ClientCallback]
    private void Camera_on_player()
    {
        camera_player.transform.position = new Vector3(transform.position.x, transform.position.y + 13.25f, transform.position.z - 10);
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
