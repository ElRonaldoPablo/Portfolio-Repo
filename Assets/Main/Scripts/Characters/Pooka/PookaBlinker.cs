using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaBlinker : MonoBehaviour
{
    private SpriteRenderer sprRndrr;
    [SerializeField] private GameObject pooka = null;
    [SerializeField] private Transform blinkPos;
    private bool blinkToPos = false;

    // Start is called before the first frame update
    void Start()
    {
        sprRndrr = GameObject.FindGameObjectWithTag("Pooka").GetComponent<SpriteRenderer>();
        pooka = GameObject.FindGameObjectWithTag("Pooka");
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkToPos)
        {
            BlinkPookaToOtherSide();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pooka"))
        {
            blinkToPos = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pooka"))
        {
            blinkToPos = false;
            sprRndrr.sortingOrder = 1;
        }
    }

    private void BlinkPookaToOtherSide()
    {
        sprRndrr.sortingOrder = -1;
        pooka.transform.position = blinkPos.transform.position;
    }
}
