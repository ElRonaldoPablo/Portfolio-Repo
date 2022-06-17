using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Collider2D coll = null;
    private SpriteRenderer sprRndrr = null;

    [Header("Character Properties")]
    [Range(0.0f, 9999.9f)] public float maxHP = 100.0f;
    [Range(0.0f, 9999.9f)] public float currentHP = 100.0f;

    [Header("Hit & Death Variables")]
    public GameObject hitPE;
    public GameObject deathPE;

    private bool died = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        sprRndrr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Temporary Enemy Dummy Death
        if (currentHP <= 0 && !died)
        {
            StartCoroutine(Die());
            died = true;
        }
        #endregion
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        GameObject hitPEClone = Instantiate(hitPE, transform.position, Quaternion.identity);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private IEnumerator Die()
    {
        //Debug.Log("Enemy died.");
        GameObject deathPEClone = Instantiate(deathPE, new Vector2(transform.position.x, transform.position.y + 0.2f), deathPE.transform.rotation);
        //Destroy(this.gameObject);
        coll.enabled = false;
        sprRndrr.color = new Color(1.0f, 0.44f, 0.44f, 0.0f);

        yield return new WaitForSeconds(3.0f);

        coll.enabled = true;
        sprRndrr.color = new Color(1.0f, 0.44f, 0.44f, 1.0f);
        currentHP = maxHP;

        died = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RevolverBullet"))
        {
            TakeDamage(other.gameObject.GetComponent<RevolverBullet>().damage);
        }
    }
}
