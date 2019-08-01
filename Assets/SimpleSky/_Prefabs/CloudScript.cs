using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(3f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x > 200)
            Destroy(this.gameObject);
    }
}
