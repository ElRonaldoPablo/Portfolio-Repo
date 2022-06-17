using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject respawnPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.Find("WalkJogRunSection") != null)
            {
                player.transform.position = GameObject.Find("RespawnPoint1").transform.position;
            }

            if (GameObject.Find("DashSection") != null)
            {
                player.transform.position = GameObject.Find("RespawnPoint2").transform.position;
            }

            player.transform.position = respawnPoint.transform.position;
        }
    }
}
