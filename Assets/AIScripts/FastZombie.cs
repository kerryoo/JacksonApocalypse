using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastZombie : Zombie
{
    [SerializeField] Animator animator;
    private float jumpCooldown;
    private float lastJump;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (playerFound && (lastJump + jumpCooldown <= Time.time))
            JumpTowardsPlayer();
        
    }

    public override void JumpTowardsPlayer()
    {
        transform.LookAt(playerTransform);
        zombieBody.AddForce(Vector3.up * 4, ForceMode.Impulse);
        zombieBody.AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse);
        lastJump = Time.time;
    }

    public override void AssignStats()
    {
        maxHealth = 1;
        movementSpeed = 3;
        rotationSpeed = 3;
        minX = minZ = -10;
        maxX = maxZ = 10;
        damageMinForLargeSound = 5;
        fieldOfView = 45;
        viewDistance = 9;
        timeBetweenGruntMin = 6;
        timeBetweenGruntMax = 9;
        detectionInterval = 2;
    }
}
