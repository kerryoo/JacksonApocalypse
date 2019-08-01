using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats m_PlayerStats;
    public PlayerStats PlayerStats
    {
        get
        {
            if(m_PlayerStats == null)
            {
                m_PlayerStats = GetComponent<PlayerStats>();
            }
            return m_PlayerStats;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
