using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public float rateOfFire;
    public int damageDealt;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        rateOfFire = 1f;
        damageDealt = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            rateOfFire /= 2;
            damageDealt = 2;
        }

        
    }
}
