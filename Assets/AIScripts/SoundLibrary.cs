using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    [SerializeField] List<AudioClip> ZombieWalkSounds;
    [SerializeField] List<AudioClip> ZombieLowDamageSounds;
    [SerializeField] List<AudioClip> ZombieHighDamageSounds;
    [SerializeField] List<AudioClip> ZombieDeathSounds;

    public AudioClip GetRandomWalkSound()
    {
        return ZombieWalkSounds[Random.Range(0, ZombieWalkSounds.Count - 1)];

    }

    public AudioClip GetRandomLowDamageSound()
    {
        return ZombieLowDamageSounds[Random.Range(0, ZombieWalkSounds.Count - 1)];

    }

    public AudioClip GetRandomHighDamageSound()
    {

        return ZombieHighDamageSounds[Random.Range(0, ZombieWalkSounds.Count - 1)];


    }

    public AudioClip GetRandomDeathSound()
    {
        return ZombieDeathSounds[Random.Range(0, ZombieWalkSounds.Count - 1)];

    }
}
