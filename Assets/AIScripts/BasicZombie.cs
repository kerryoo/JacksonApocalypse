using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator zombieFSM;
    [SerializeField] int maxHealth;
    private SoundLibrary soundLibrary;
    private AudioSource soundSource;
    private float movementSpeed = 1.0f;
    private float rotationSpeed = 2.0f;

    private float targetPositionTolerance = 3.0f;
    private float minX = -10;
    private float maxX = 10;
    private float minZ;
    private float maxZ;
    private float yPosition = 10.25f;
    private Vector3 targetPosition;

    private Destructable destructable;
    private int damageMinForLargeSound;
    private bool alive;

    public int fieldOfView = 45;
    public int viewDistance = 10;
    private Transform playerTransform;
    private Vector3 rayDirection;

    float nextWalkingSound;
    float timeBetweenGrunt;

    private float nextDetection;
    private float detectionInterval;

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

        playerTransform = GameObject.Find("Player").transform;

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


            if (nextDetection <= Time.time)
            {
                DetectAspect();
                nextDetection = Time.time + detectionInterval;
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

    private void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;

        if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                var player = hit.collider.GetComponent<Player>();
                if (player != null)
                {
                    Debug.Log("Player detected");
                }
            }
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
