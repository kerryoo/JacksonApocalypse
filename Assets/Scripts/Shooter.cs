using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Camera _camera;
    [SerializeField] PlayerStats playerStats;

    public bool fireAllowed;
    public float lastFire;
    public int damage;
    private FireBlast fireBlast;


    private void Start()
    {
        fireBlast = projectile.GetComponent<FireBlast>();
        fireBlast.playerStats = playerStats;
    }

    void Update()
    { 
        fireAllowed = Time.time > lastFire + playerStats.rateOfFire;
        if (Input.GetButton("Fire2") && Input.GetButton("Fire1") && fireAllowed)
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(projectile, transform.position, Quaternion.LookRotation(hit.point - transform.position));

            }

            lastFire = Time.time;
        }


    }
}
