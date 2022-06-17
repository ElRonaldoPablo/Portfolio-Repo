using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    #region Rotation Variables

    [Header("Rotation Speed")]
    [Tooltip("This variable controls the global rotation speed for all axis rotations.")]
    [Range(-100.0f, 100.0f)]
    [SerializeField]
    private float overallSpeed = 5.0f;
    
    [Header("Rotation Speed (per Axis)")]
    [Range(-100.0f, 100.0f)]
    [Tooltip("Speed multiplier for X axis rotation. Negative value for counter-clockwise direction.")]
    [SerializeField]
    private float xRotation = 0.0f;

    [Range(-100.0f, 100.0f)]
    [Tooltip("Speed multiplier for Y axis rotation. Negative value for counter-clockwise direction.")]
    [SerializeField]
    private float yRotation = 0.0f;

    [Range(-100.0f, 100.0f)]
    [Tooltip("Speed multiplier for Z axis rotation. Negative value for counter-clockwise direction.")]
    [SerializeField]
    private float zRotation = 0.0f;
    
    [Header("Axis Toggle (ON/OFF)")]
    [Tooltip("Click to enable X Axis rotation.")]
    [SerializeField]
    private bool xRotationToggle = false;
    [Tooltip("Click to enable Y Axis rotation.")]
    [SerializeField]
    private bool yRotationToggle = false;
    [Tooltip("Click to enable Z Axis rotation.")]
    [SerializeField]
    private bool zRotationToggle = false;
    
    private float xAxisRotation;
    private float yAxisRotation;
    private float zAxisRotation;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        xAxisRotation = overallSpeed * Time.deltaTime;
        yAxisRotation = overallSpeed * Time.deltaTime;
        zAxisRotation = overallSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        AxisRotationToggle();
    }

    #region Axis Rotation Functions

    public void RotateOnXAxis()
    {
        Vector3 rotation = new Vector3((xRotation * xAxisRotation), 0, 0);
        transform.Rotate(rotation, Space.Self);
        
    }

    public void RotateOnYAxis()
    {
        Vector3 rotation = new Vector3(0, (yRotation * yAxisRotation), 0);
        transform.Rotate(rotation, Space.Self);
    }

    public void RotateOnZAxis()
    {
        Vector3 rotation = new Vector3(0, 0, (zRotation * zAxisRotation));
        transform.Rotate(rotation, Space.Self);
    }

    public void AxisRotationToggle()
    {
        if (xRotationToggle == true)
            RotateOnXAxis();
        if (yRotationToggle == true)
            RotateOnYAxis();
        if (zRotationToggle == true)
            RotateOnZAxis();
    }

    #endregion
}
