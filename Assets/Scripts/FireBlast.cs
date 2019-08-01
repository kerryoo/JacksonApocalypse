using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject explosion;
    [SerializeField] float timeToLive;

    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        BasicZombie destructable = other.GetComponent<BasicZombie>();
        if (destructable != null)
        {
            destructable.takeDamage(playerStats.damageDealt);

        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameObject explosionObj = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionObj, 1f);
    }
}
