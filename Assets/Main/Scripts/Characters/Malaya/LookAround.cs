using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    #region Out-of-Inspector Variables
    ProjectIlluminaIA piInputActions;
    PlayerControls playerControls;
    Player player;
    #endregion

    [SerializeField] private GameObject cameraTarget = null;

    [Header("Targeting Variables")]
    [SerializeField] private Transform originalPoint = null;
    [SerializeField] private Transform lookUpPoint = null;
    [SerializeField] private Transform lookDownPoint = null;

    [Space]
    public Vector3 velocity = Vector3.zero;
    [Range(0.0f, 1.0f)] public float smoothTime = 0.3f;
    [SerializeField][Range(0.0f, 10.0f)] private float maxSpeed = 5.5f;

    #region Hidden Boolean Variables
    [HideInInspector] public bool lookUp = false;
    [HideInInspector] public bool lookDown = false;
    [HideInInspector] public bool looking = false;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        piInputActions = new ProjectIlluminaIA();
        playerControls = GetComponent<PlayerControls>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        looking = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAroundDetection();
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        

    }

    private void LookAroundDetection()
    {
        #region Look Up Checks
        if (playerControls.look.y > 0.0f && playerControls.move.x == 0)
        {
            lookUp = true;
        }

        if (playerControls.look.y >= 0.0f && playerControls.move.x > 0 || playerControls.look.y >= 0.0f && playerControls.move.x < 0)
        {
            lookUp = false;
        }
        #endregion

        #region Look Down Checks
        if (playerControls.look.y < 0.0f && playerControls.move.x == 0)
        {
            lookDown = true;
        }

        if (playerControls.look.y <= 0.0f && playerControls.move.x > 0 || playerControls.look.y <= 0.0f && playerControls.move.x < 0)
        {
            lookDown = false;
        }
        #endregion

        #region Disable Looking On X-Axis Movement or 0 on Y-Axis
        if (playerControls.move.x != 0)
        {
            lookDown = false;
            lookUp = false;
            looking = false;
        }

        if (playerControls.look.y == 0.0f)
        {
            lookUp = false;
            lookDown = false;
        }
        #endregion
    }

    public IEnumerator LookUp()
    {
        looking = true;
        cameraTarget.transform.position = Vector3.SmoothDamp(cameraTarget.transform.position, lookUpPoint.transform.position, ref velocity, smoothTime);
        yield return new WaitForSeconds(0.01f);
    }

    public IEnumerator BackToOriginalCamPosition()
    {
        cameraTarget.transform.position = Vector3.SmoothDamp(cameraTarget.transform.position, originalPoint.transform.position, ref velocity, smoothTime, maxSpeed);
        yield return new WaitForSeconds(0.001f);

        if (cameraTarget.transform.position == originalPoint.transform.position)
        {
            looking = false;
        }
    }

    public IEnumerator LookDown()
    {
        looking = true;
        cameraTarget.transform.position = Vector3.SmoothDamp(cameraTarget.transform.position, lookDownPoint.transform.position, ref velocity, smoothTime);
        yield return new WaitForSeconds(0.01f);
    }

    #region OnEnable/OnDisable Functions
    private void OnEnable()
    {
        piInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        piInputActions.Player.Disable();
    }
    #endregion
}
