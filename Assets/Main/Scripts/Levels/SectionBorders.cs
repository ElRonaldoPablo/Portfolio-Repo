using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionBorders : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1000.0f)] private float xSize = 1.0f;
    [SerializeField][Range(0.0f, 1000.0f)] private float ySize = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector2(xSize, ySize));
    }
}
