using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager ins;

    public AudioClip deadUnit;
    public AudioClip explosion;
    public AudioClip minusLife;
    public AudioSource audioSource;

    void Awake()
    {
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }

        if (audioSource == null)
            audioSource = gameObject.GetComponent<AudioSource>();
    }


    public void DeadUnit()
    {
        audioSource.PlayOneShot(deadUnit);
    }

    public void Explosion()
    {
        audioSource.PlayOneShot(explosion);
    }

    public void MinusLife()
    {
        audioSource.PlayOneShot(minusLife);

    }

}
