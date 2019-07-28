using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBlast : MonoBehaviour
{
    public float speed;
    public float timeToLive;
    public float damage;
    [SerializeField] GameObject explosionPrefab;

    private bool collided;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


    }



    

    

    private void OnDestroy()
    {
        if (!collided)
        {
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(explosion, 2f);
        }
    }

    public void Slow(float[] slowAndDuration)
    {
        StartCoroutine(StartSlow(slowAndDuration[0], slowAndDuration[1]));
    }

    IEnumerator StartSlow(float slowFactor, float timeToSlow)
    {
        float originalMoveSpeed = speed;
        speed *= slowFactor;

        yield return new WaitForSeconds(timeToSlow);

        speed = originalMoveSpeed;
    }


}
