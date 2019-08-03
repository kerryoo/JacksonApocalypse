using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public float rateOfFire;
    public int damageDealt;
    public int healthRemaining;
    public bool flinch;
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

    public void handleDamage(int amount)
    {
        if (!flinch)
        {
            if (amount >= healthRemaining)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(takeDamage(amount));
            }
        }
    }

    private IEnumerator Die()
    {
        flinch = true;
        unityChan.anim.SetBool("Dead", true);
        yield return new WaitForSeconds(10f);
        flinch = false;
    }

    private IEnumerator takeDamage(int amount)
    {
        yield return new WaitForSeconds(0.3f);
        unityChan.anim.speed = 2;
        unityChan.anim.SetTrigger("Damage");
        healthRemaining -= amount;

        flinch = true;
        yield return new WaitForSeconds(0.6f);
        unityChan.anim.speed = 1;
        flinch = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(damageDealt);
        }
    }


}
