using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public float rateOfFire;
    public int damageDealt;
    public int healthRemaining;
    public UnityChan.UnityChanControlScriptWithRgidBody unityChan;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        rateOfFire = 1f;
        damageDealt = 1;
        healthRemaining = maxHealth;
        unityChan = gameObject.GetComponent<UnityChan.UnityChanControlScriptWithRgidBody>();

    }

    public void takeDamage(int amount)
    {
        if (amount >= healthRemaining)
        {
            StartCoroutine(Die());
        }
        else
        {
            unityChan.anim.SetTrigger("damage");
            healthRemaining -= amount;
        }
        Debug.Log(healthRemaining);
    }

    private IEnumerator Die()
    {
        unityChan.anim.SetBool("Dead", true);
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(damageDealt);
        }

        
    }


}
