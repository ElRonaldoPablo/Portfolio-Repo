using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAttackZero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Taunt()
    {
        Debug.Log("NOTICE | Execute Illumina Attack Zero: Taunt.");
        yield return new WaitForSeconds(0.01f);
    }
}
