using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCanvasActivator : MonoBehaviour
{
    [SerializeField] private GameObject cineCanvas = null;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateCineCanvas());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivateCineCanvas()
    {
        yield return new WaitForSeconds(3.0f);
        cineCanvas.SetActive(true);
    }
}
