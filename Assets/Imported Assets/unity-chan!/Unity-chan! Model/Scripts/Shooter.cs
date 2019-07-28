using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire;
    [SerializeField] RotateToMouse rotateToMouse;

    public bool fireAllowed;
    public float lastFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireAllowed = Time.time > lastFire + rateOfFire;
        if (Input.GetButton("Fire1") && fireAllowed)
        {
            GameObject vfx = Instantiate(projectile, crosshair.transform.position, crosshair.transform.rotation);
            //vfx.transform.rotation = rotateToMouse.rotation;
            Debug.Log(crosshair.transform.rotation);

            lastFire = Time.time;
        }


    }
}
