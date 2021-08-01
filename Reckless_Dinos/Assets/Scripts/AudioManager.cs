using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Handling Singleton Pattern.
    public static AudioManager i;


    /* INDEX SOUND KEYS
     * 0 = Coin.
     * 1 = BtnClick.
     * 2 = .
       3 =
       4 =
         */
    public AudioClip[] gameSFX;

    AudioSource gameMusic;
   

    // Start is called before the first frame update
    void Start()
    {
        i = this;
        gameMusic = GetComponent<AudioSource>();
        //PlaySound(gameSFX[0]);

        
    }

    public void PlaySound(AudioClip clipToPlay) {
        gameMusic.PlayOneShot(clipToPlay);
    
    }
}
