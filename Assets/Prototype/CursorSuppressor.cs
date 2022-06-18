using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSuppressor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
