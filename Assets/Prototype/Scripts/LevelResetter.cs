using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    [SerializeField] private GameObject miniBoss = null;
    [SerializeField] private GameObject levelResetter = null;

    // Start is called before the first frame update
    void Start()
    {
        miniBoss = GameObject.Find("MiniBoss");
    }

    // Update is called once per frame
    void Update()
    {
        if (miniBoss)
        {
            return;
        }
        else
        {
            levelResetter.SetActive(true);
        }
    }
}
