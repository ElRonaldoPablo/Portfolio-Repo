using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://answers.unity.com/questions/1086765/any-relatively-simple-way-to-handle-2d-slopes-with.html")]
public class Player : MonoBehaviour
{
    #region Out-of-Inspector Variables

    private ProjectIlluminaIA _piInputActions;
    private PlayerControls _playerControls;
    private Animator _anim;

    private LookAround _lookAround;
    private Movement _movement;
    private Jump _jump;
    private Dash _dash;
    private Heal _heal;
    private MalayaAbility _ability;

    #endregion

    [Header("Character Properties")]
    [Range(0.0f, 9999.9f)] public float _maxHP = 100.0f;
    [Range(0.0f, 9999.9f)] public float _currentHP = 100.0f;

    [Header("Rejuvenation")]
    [SerializeField][Range(0.0f, 6.0f)] private float _healWaitTime = 1.0f;

    [Header("Referenced Components")]
    //[SerializeField] private GameObject pooka = null;
    [HideInInspector] public Rigidbody2D _rb = null;
    [SerializeField] private CapsuleCollider2D _capCollider;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Targeting")]
    public GameObject _closest = null;

    #region (Hidden) State Booleans
    //[Header("State Booleans")]
    [HideInInspector] public bool _isGrounded = false;
    [HideInInspector] public bool _isWalking = false;
    [HideInInspector] public bool _isJogging = false;
    [HideInInspector] public bool _isRunning = false;
    [HideInInspector] public bool _facingRight = true;
    [HideInInspector] public bool _ceilingHit = false;
    [HideInInspector] public bool _isOnSlope = false;
    [HideInInspector] public bool _disableFlipping = false;
    [HideInInspector] public bool _disableDashing = false;
    [HideInInspector] public bool _disableJumping = false;
    [HideInInspector] public bool _stationaryAbility = false;
    //[HideInInspector] public bool isTalking = false;
    #endregion

    void Awake()
    {
        #region Player Components
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        #endregion
        #region Input Classes/Components
        _piInputActions = new ProjectIlluminaIA();
        _playerControls = GetComponent<PlayerControls>();
        #endregion
        #region State Classes/Components
        _lookAround = GetComponent<LookAround>();
        _movement = GetComponent<Movement>();
        _jump = GetComponent<Jump>();
        _dash = GetComponent<Dash>();
        #endregion

        //pooka = GameObject.FindGameObjectWithTag("Pooka");
        _heal = GetComponent<Heal>();

        _ability = GetComponent<MalayaAbility>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        FindClosestEnemy();

        if (_stationaryAbility)
        {
            _disableFlipping = true;
            _disableJumping = true;
            _disableDashing = true;
        }
        else if (!_stationaryAbility)
        {
            _disableFlipping = false;
            _disableJumping = false;
            _disableDashing = false;
        }

        Flip();

        _movement.MovementStateDetection();
        _jump.JumpStateDetection();
        _dash.DashStateDetection();
    }

    void FixedUpdate()
    {
        #region Look Up/Down Functions

        LookAroundBoolDisabler();

        if (_lookAround.lookUp && _jump.isGrounded)
        {
            StartCoroutine(_lookAround.LookUp());
        }
        else if (!_lookAround.lookUp && _jump.isGrounded)
        {
            StartCoroutine(_lookAround.BackToOriginalCamPosition());
        }

        if (_lookAround.lookDown && _jump.isGrounded)
        {
            StartCoroutine(_lookAround.LookDown());
        }
        else if (!_lookAround.lookDown && _jump.isGrounded)
        {
            StartCoroutine(_lookAround.BackToOriginalCamPosition());
        }
        #endregion
        #region Jump/Double Jump Functions
        if (_jump.isJumping && !_disableJumping)
        {
            StartCoroutine(_jump.JumpFunction());
        }

        if (_jump.activateDoubleJump)
        {
            StartCoroutine(_jump.DoubleJump());
        }

        #endregion
        #region Dash Functions
        if (_dash.isDashing && !_dash.upgradedDash)
        {
            StartCoroutine(_dash.DashFunction());
        }
        else if (_dash.isDashing && _dash.upgradedDash)
        {
            StartCoroutine(_dash.UpgradedDash());
        }
        #endregion
        #region X-Axis Movement Functions
        else if (!_dash.isDashing && !_lookAround.looking && !_stationaryAbility)
        {
            _movement.MovementFunction();
        }
        #endregion
    }

    private void Flip()
    {
        if (_facingRight == false && _playerControls.move.x > 0.0f && !_disableFlipping)
        {
            _facingRight = !_facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }
        else if (_facingRight == true && _playerControls.move.x < 0.0f && !_disableFlipping)
        {
            _facingRight = !_facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }
    }

    private void LookAroundBoolDisabler()
    {
        if (_lookAround.lookUp == true && _isGrounded && _lookAround.looking || _lookAround.lookDown == true && _isGrounded && _lookAround.looking)
        {
            _rb.velocity = Vector2.zero;
            
            _disableJumping = true;
            _disableDashing = true;
            _disableFlipping = true;
        }
        if (_lookAround.looking)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            _rb.velocity = Vector2.zero;

            _disableJumping = true;
            _disableDashing = true;
            _disableFlipping = true;
        }
        else if (_lookAround.lookUp == false || _lookAround.lookDown == false)
        {
            _rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            _disableJumping = false;
            _disableDashing = false;
            _disableFlipping = false;
        }
    }

    public IEnumerator Rejuvenation()
    {
        _heal.isHealing = false;
        yield return new WaitForSeconds(_healWaitTime);
        _heal.isHealing = true;

        if (_heal.isHealing)
        {
            if (_currentHP < _maxHP)
            {

                _currentHP += _heal.healAmount;

                if (_currentHP > _maxHP)
                {
                    _currentHP = _maxHP;
                }

                _heal.isHealing = false;
            }
            else if (_currentHP >= _maxHP)
            {
                _currentHP = _maxHP;
                _heal.isHealing = false;
            }
        }

        _heal.canHeal = false;
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator UpgradedRejuvenation()
    {
        _heal.isHealing = false;
        _heal.canHeal = false;
        yield return new WaitForSeconds(_healWaitTime);
        _heal.isHealing = true;

        if (_heal.isHealing)
        {
            if (_currentHP < _maxHP)
            {
                _currentHP += _heal.advancedHealAmount;

                if (_currentHP > _maxHP)
                {
                    _currentHP = _maxHP;
                }

                _heal.isHealing = false;
            }
            else if (_currentHP >= _maxHP)
            {
                _currentHP = _maxHP;
                _heal.isHealing = false;
            }
        }

        _heal.canHeal = false;
        yield return new WaitForSeconds(0.1f);
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //GameObject closest
        float distance = Mathf.Infinity;
        Vector2 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector2 diff = enemy.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                _closest = enemy;
                distance = curDistance;
            }
        }

        if (enemies == null)
        {
            return null;
        }

        return _closest;
    }

    #region Collision & Trigger Checks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ceiling"))
        {
            _ceilingHit = true;
        }

        if (other.gameObject.CompareTag("UnstableGround") || other.gameObject.CompareTag("Slope"))
        {
            _boxCollider.enabled = false;
            _capCollider.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ceiling"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
        }

        if (other.gameObject.CompareTag("Slope"))
        {
            _isOnSlope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ceiling"))
        {
            _ceilingHit = false;
        }

        if (other.gameObject.CompareTag("UnstableGround") || other.gameObject.CompareTag("Slope"))
        {
            _boxCollider.enabled = true;
            _capCollider.enabled = false;
        }
    }
    #endregion

    #region OnEnable/OnDisable Functions
    private void OnEnable()
    {
        _piInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _piInputActions.Player.Disable();
    }
    #endregion
}
