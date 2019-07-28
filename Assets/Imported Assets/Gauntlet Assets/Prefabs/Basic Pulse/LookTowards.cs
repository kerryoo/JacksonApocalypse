using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowards : MonoBehaviour
{
    [SerializeField] Transform target;
   
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
