using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalayaAbilityOne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShurikenOfNinja()
    {
        Debug.Log("Malaya RT Ability One has been used.");
        yield return new WaitForSeconds(0.01f);
    }
}
