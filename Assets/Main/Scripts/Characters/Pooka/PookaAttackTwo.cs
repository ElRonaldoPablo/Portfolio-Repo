using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAttackTwo : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private GameObject target = null;

    [SerializeField] private GameObject orb = null;

    [SerializeField] private Player player = null;
    [SerializeField] private Pooka pooka = null;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pooka = GameObject.FindGameObjectWithTag("Pooka").GetComponent<Pooka>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = player._closest;
    }

    public IEnumerator Projectile()
    {
        Debug.Log("NOTICE | Execute Illumina Attack Two: Projectile.");
        yield return new WaitForSeconds(0.01f);

        pooka.faceTarget = true;
        rb.velocity = new Vector2(0.0f, 0.0f);
        yield return new WaitForSeconds(0.75f);

        // Play animation jumping up and hitting light orb

        GameObject projectileClone = Instantiate(orb, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
        pooka.faceTarget = false;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
            
        }
        else
        {
            //Debug.LogWarning("NOTICE: No target detected.");
            return;
        }
    }
}
