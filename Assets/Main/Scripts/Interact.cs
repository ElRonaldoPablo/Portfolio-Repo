using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    //private Player player;
    private PlayerControls playerControls;
    [HideInInspector] public bool isTalking = false;
    private bool isInNPCRange = false;
    private bool canInteract = true;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<Player>();
        playerControls = GetComponentInParent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractWithNPC();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isInNPCRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isInNPCRange = false;
            canInteract = true;
        }
    }

    private void InteractWithNPC()
    {
        if (isInNPCRange && playerControls.interactValue == 1.0f && canInteract)
        {
            isTalking = true;
            canInteract = false;
            Debug.Log("NOTICE: Player has initiated interaction with NPC.");
        }
    }
}
