using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("NOTICE: Player has entered NPC interact range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("NOTICE: Player has left NPC interact range.");
    }
}
