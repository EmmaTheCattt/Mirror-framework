using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Speed = 5;
    public Collider Col;

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
    }
}
