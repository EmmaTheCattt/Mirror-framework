using Mirror;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : NetworkBehaviour
{
    public Camera camera_player;
    public NavMeshAgent player;
    public GameObject Bullet;
    public GameObject tip;
    public NavMeshAgent NAVIGATION;

    public bullet Bul_scipt;

    public TMP_Text name_text;

    [SyncVar]
    public string playerDisplayName;

    public SaveManager SaveData;
    public SaveManagerConvert OnlineData;

    public GameObject Second_tip;

    public float shoot_interval = 0.5f;
    public float current_interval = 0;

    public bool On_ship = false;

    public Transform[] spawns;
    public int random_num;

    public Transform Look_cam;
    public Vector3 offset = new Vector3(0, 180f, 0);

    public int highscore = 0;

    public SaveFile saveFile;

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

        if (isLocalPlayer) { CmdSendName(PlayerPrefs.GetString("PlayerName")); }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) { CmdSendName(PlayerPrefs.GetString("PlayerName")); }
    }

    // Update is called once per frame
    void Update()
    {
        saveFile.Highscore = highscore;
        //saving
        string JsonSave = JsonConvert.SerializeObject(saveFile);
        PlayerPrefs.SetString("save", JsonSave);
        PlayerPrefs.SetString("PlayerName", saveFile.name);
        PlayerPrefs.SetInt("Highscore", saveFile.Highscore);
        PlayerPrefs.Save();

        Debug.Log(highscore);

        if (isLocalPlayer) {
            if (PlayerPrefs.HasKey("save"))
            {
                string json = PlayerPrefs.GetString("save");
                saveFile = JsonConvert.DeserializeObject<SaveFile>(json);
            }

            CmdSendName(saveFile.name);
            name_text.name = playerDisplayName;
        }

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
        }
    }

    [Command(requiresAuthority = false)]
    private void Shot()
    {
        GameObject Bul = Instantiate(Bullet, new Vector3(tip.transform.position.x, tip.transform.position.y, tip.transform.position.z), Quaternion.identity);
        Bul.transform.rotation = tip.transform.rotation;
        Bul_scipt = Bul.GetComponent<bullet>();
        Bul_scipt.controller = GetComponent<PlayerController>();
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
        if (On_ship == false)
        {
            camera_player.transform.position = new Vector3(transform.position.x, transform.position.y + 13.25f, transform.position.z - 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            /*
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
            */
        }
    }

    [Command]
    public void CmdSendName(string playerName)
    {

        Command_SetPlayerName(playerName);
    }

    [ClientRpc]
    public void Command_SetPlayerName(string playerName)
    {
        playerDisplayName = playerName;
    }
}
