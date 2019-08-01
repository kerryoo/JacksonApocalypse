using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : Zombie
{
    [SerializeField] Animator animator;
    public override void Start()
    {
        animator.SetBool("Walk", true);
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void AssignStats()
    {
        maxHealth = 2;
        movementSpeed = 1;
        rotationSpeed = 2;
        minX = minZ = -10;
        maxX = maxZ = 10;
        damageMinForLargeSound = 5;
        fieldOfView = 45;
        viewDistance = 4;
        timeBetweenGruntMin = 6;
        timeBetweenGruntMax = 9;
        detectionInterval = 2;
    }

    public override IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        return base.Die();
    }
}
