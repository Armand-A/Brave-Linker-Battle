using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSound : MonoBehaviour
{
    public AudioSource sfx;
    public void playSound(){
        sfx.Play();
    }
}
