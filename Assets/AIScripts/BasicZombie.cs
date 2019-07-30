using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int maxHealth;
    private SoundLibrary soundLibrary;
    private AudioSource soundSource;
    private float movementSpeed = 1.0f;
    private float rotationSpeed = 2.0f;
    private float targetPositionTolerance = 3.0f;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    private float yPosition;
    private Vector3 targetPosition;
    private Destructable destructable;
    private int damageMinForLargeSound;
    private bool alive;

    float nextWalkingSound;
    float timeBetweenGrunt;

    public class Destructable
    {
        public int healthRemaining;

        public bool takeDamage(int amount) //returns true if dead, false if still alive
        {
            if (healthRemaining <= amount)
                return true;

            healthRemaining -= amount;
            return false;
        }
    }


    public virtual void Start()
    {
        minX = -10f;
        maxX = 10f;
        minZ = -10f;
        maxZ = 10f;
        yPosition = 10.25f;
        timeBetweenGrunt = 7;
        damageMinForLargeSound = 5;
        destructable = new Destructable();
        destructable.healthRemaining = maxHealth;
        alive = true;

        soundLibrary = GameObject.Find("SoundLibrary").GetComponent<SoundLibrary>();
        soundSource = gameObject.GetComponent<AudioSource>();
        animator.SetBool("Walk", true);

        GetNextPosition();
    }

    public virtual void Update()
    {
        if (alive)
        {
            if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance)
                GetNextPosition();

            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 nextPosition = new Vector3(0, 0, movementSpeed * Time.deltaTime);

            transform.Translate(nextPosition);

            if (nextWalkingSound <= Time.time)
            {
                soundSource.PlayOneShot(soundLibrary.GetRandomWalkSound());
                nextWalkingSound = Time.time + timeBetweenGrunt;
            }



            if (Input.GetKeyDown(KeyCode.P))
            {
                takeDamage(10);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                takeDamage(2);
            }
        }

    }

    public virtual void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), yPosition, Random.Range(minZ, maxZ));
    }

    public virtual void takeDamage(int amount)
    {
        bool dead = destructable.takeDamage(amount);
        if (dead)
        {
            StartCoroutine(Die());
        } else
        {
            if (amount >= damageMinForLargeSound)
                soundSource.PlayOneShot(soundLibrary.GetRandomHighDamageSound());
            else
                soundSource.PlayOneShot(soundLibrary.GetRandomLowDamageSound());
        }
    }

    IEnumerator Die()
    {
        alive = false;
        soundSource.PlayOneShot(soundLibrary.GetRandomDeathSound());
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
        
    }


}
