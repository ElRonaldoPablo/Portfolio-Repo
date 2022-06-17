using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymSectionDetector : MonoBehaviour
{
    [SerializeField] private GameObject walkJogRunPanel = null;
    [SerializeField] private GameObject jumpPanel = null;
    [SerializeField] private GameObject dashPanel = null;

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
        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "WalkJogRun_Trigger")
        {
            walkJogRunPanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "Jump_Trigger")
        {
            jumpPanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "Dash_Trigger")
        {
            dashPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "WalkJogRun_Trigger")
        {
            walkJogRunPanel.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "Jump_Trigger")
        {
            jumpPanel.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.name == "Dash_Trigger")
        {
            dashPanel.SetActive(false);
        }
    }

    
}
