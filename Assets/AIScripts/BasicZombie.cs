using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SoundLibrary soundLibrary;
    private AudioSource soundSource;
    private float movementSpeed = -1.0f;
    private float rotationSpeed = 2.0f;
    private float targetPositionTolerance = 3.0f;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    private float yPosition;
    private Vector3 targetPosition;

    float nextWalkingSound;
    float timeBetweenGrunt;


    void Start()
    {
        minX = -10f;
        maxX = 10f;
        minZ = -10f;
        maxZ = 10f;
        soundSource = gameObject.GetComponent<AudioSource>();
        animator.SetBool("Walk", true);

        GetNextPosition();
    }

    void Update()
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

    }

    void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), yPosition, Random.Range(minZ, maxZ));

    }
}
