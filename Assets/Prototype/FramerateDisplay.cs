using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FramerateDisplay : MonoBehaviour
{
    private float framerate;
    [SerializeField] private TextMeshProUGUI framerateCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        framerate = 1 / Time.deltaTime;
        FramerateCounterDisplay();
    }

    void FramerateCounterDisplay()
    {
        framerateCounter.text = framerate.ToString("000.0");
    }
}
