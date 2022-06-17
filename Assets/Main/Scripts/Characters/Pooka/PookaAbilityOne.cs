using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAbilityOne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Quiescence()
    {
        Debug.Log("NOTICE: Execute Illumina Ability One Coroutine.");
        yield return new WaitForSeconds(0.01f);
    }
}
