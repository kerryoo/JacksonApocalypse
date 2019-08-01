using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    Rigidbody zombieBody;
    protected int maxHealth = 2;

    protected SoundLibrary soundLibrary;
    protected AudioSource soundSource;
    protected float movementSpeed = 1.0f;
    protected float rotationSpeed = 2.0f;

    protected float targetPositionTolerance = 3.0f;
    protected float minX = -10;
    protected float maxX = 10;
    protected float minZ = -10;
    protected float maxZ = 10;
    protected float yPosition = 10.25f;
    protected Vector3 targetPosition;

    protected Destructable destructable;
    protected int damageMinForLargeSound = 5;
    protected bool alive;
    protected bool playerFound;

    public int fieldOfView = 45;
    public int viewDistance = 4;
    protected Transform playerTransform;
    protected Vector3 rayDirection;

    protected float nextWalkingSound;
    protected float timeBetweenGruntMin = 6;
    protected float timeBetweenGruntMax = 9;

    protected float nextDetection;
    protected float detectionInterval = 2;

    public delegate void OnPlayerFound();
    public event OnPlayerFound AttackPlayer;


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
        destructable = new Destructable();
        destructable.healthRemaining = maxHealth;
        alive = true;

        soundLibrary = GameObject.Find("SoundLibrary").GetComponent<SoundLibrary>();
        soundSource = gameObject.GetComponent<AudioSource>();
        zombieBody = gameObject.GetComponent<Rigidbody>();
        animator.SetBool("Walk", true);

        playerTransform = GameObject.Find("Player").transform;

        GetNextPosition();
    }

    public virtual void Update()
    {
        if (alive)
        {
            if (playerFound)
            {
                Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
            } else
            {
                WanderAimlessly();
                if (nextDetection <= Time.time)
                {
                    DetectAspect();
                    nextDetection = Time.time + detectionInterval;
                }

            }


            if (nextWalkingSound <= Time.time)
            {
                soundSource.PlayOneShot(soundLibrary.GetRandomWalkSound());
                nextWalkingSound = Time.time + Random.Range(timeBetweenGruntMin, timeBetweenGruntMax);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(destructable.healthRemaining);
        }

        }


    }

    public virtual void WanderAimlessly()
    {
        if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance)
            GetNextPosition();

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 nextPosition = new Vector3(0, 0, movementSpeed * Time.deltaTime);

        transform.Translate(nextPosition);
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

    public void DetectAspect()
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
                    JumpTowardsPlayer();
                    playerFound = true;
                }
            }
        }

    }

    private void JumpTowardsPlayer()
    {
        transform.LookAt(playerTransform);
        zombieBody.AddForce(Vector3.up * 4, ForceMode.Impulse);
        zombieBody.AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse);
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
