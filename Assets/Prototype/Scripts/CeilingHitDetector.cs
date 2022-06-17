using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingHitDetector : MonoBehaviour
{
    SpriteRenderer sprRenderer;
    private bool touched = false;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        touched = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touched)
        {
            sprRenderer.color = Color.red;
        }
        else
        {
            sprRenderer.color = Color.HSVToRGB(17.0f, 17.0f, 17.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touched = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touched = false;
        }
    }
}
