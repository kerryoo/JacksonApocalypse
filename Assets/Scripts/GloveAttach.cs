using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveAttach : MonoBehaviour
{
    [SerializeField] Transform hand;
    
    void Update()
    {
        transform.position = hand.position;
        transform.rotation = hand.rotation;
    }
}
