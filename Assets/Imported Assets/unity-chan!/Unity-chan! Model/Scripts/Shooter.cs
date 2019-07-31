using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire;
    [SerializeField] Camera _camera;

    public bool fireAllowed;
    public float lastFire;
    
    
    void Update()
    {
        fireAllowed = Time.time > lastFire + rateOfFire;
        if (Input.GetButton("Fire2") && Input.GetButton("Fire1") && fireAllowed)
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                Debug.DrawLine(transform.position, hit.point, Color.red, 20f);
                Instantiate(projectile, transform.position, Quaternion.LookRotation(hit.point - transform.position));

            }

            lastFire = Time.time;
        }


    }
}
