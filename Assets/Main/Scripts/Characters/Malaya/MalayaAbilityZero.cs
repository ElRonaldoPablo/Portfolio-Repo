using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalayaAbilityZero : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform revolverPoint = null;
    [SerializeField] private GameObject bullet = null;

    [SerializeField][Range(0.0f, 12.0f)] private float holsterTime = 0.8f;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GetComponentInParent<Player>();
        revolverPoint = GameObject.Find("RevolverPoint").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RevolverOfCesario()
    {
        player._stationaryAbility = true;

        Debug.Log("Malaya RT Ability Zero has been used.");
        GameObject bulletClone1 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        GameObject bulletClone2 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        GameObject bulletClone3 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        GameObject bulletClone4 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        GameObject bulletClone5 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        GameObject bulletClone6 = Instantiate(bullet, revolverPoint.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(holsterTime);
        player._stationaryAbility = false;
    }
}
