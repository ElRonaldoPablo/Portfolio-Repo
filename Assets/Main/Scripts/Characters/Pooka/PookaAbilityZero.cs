using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAbilityZero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GuidingLight()
    {
        Debug.Log("NOTICE: Execute Illumina Ability Zero Coroutine.");
        yield return new WaitForSeconds(0.01f);
    }
}
