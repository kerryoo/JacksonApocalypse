using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject explosion;
    [SerializeField] int damage;
    [SerializeField] float timeToLive;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var destructable = other.GetComponent<Destructable>();
        if (destructable != null)
        {
            destructable.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameObject explosionObj = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionObj, 1f);
    }
}
