using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{

public Transform platform;
public AudioSource audioSource;

public static bool playMusic = false;
public static bool isMusicPlaying = false;

float audioFull = 15f;
float audioStart = 90f;

void Update()
{
        float dist = Vector3.Distance(transform.position, platform.position);
        if (dist > audioStart)
                audioSource.volume = 0;
        else if (dist < audioFull)
                audioSource.volume = 1f;
        else
                audioSource.volume = 1 - ((dist-audioFull) / (audioStart - audioFull));
}
}
