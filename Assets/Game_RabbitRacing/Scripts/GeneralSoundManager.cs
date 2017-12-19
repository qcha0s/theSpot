using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{


    //Make all the clips static
    //All one shot sounds will be played 

    public static AudioClip PlayerHit, PlayerDie, PlayerAttack, EnemyHit1, EnemyHit2, EnemyHit3, EnemyDie;
    static AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        PlayerHit = Resources.Load<AudioClip>("PlayerHit");
        PlayerDie = Resources.Load<AudioClip>("PlayerDie");
        PlayerAttack = Resources.Load<AudioClip>("PlayerAttack");
        EnemyHit1 = Resources.Load<AudioClip>("EnemyHit1");
        EnemyHit2 = Resources.Load<AudioClip>("EnemyHit2");
        EnemyHit3 = Resources.Load<AudioClip>("EnemyHit3");
        EnemyDie = Resources.Load<AudioClip>("EnemyDie");
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "PlayerHit":
                m_audioSource.PlayOneShot(PlayerHit);
                break;
            case "PlayerDie":
                m_audioSource.PlayOneShot(PlayerDie);
                break;
            case "PlayerAttack":
                m_audioSource.PlayOneShot(PlayerAttack);
                break;
            case "EnemyHit1":
                m_audioSource.PlayOneShot(EnemyHit1);
                break;
            case "EnemyHit2":
                m_audioSource.PlayOneShot(EnemyHit2);
                break;
            case "EnemyHit3":
                m_audioSource.PlayOneShot(EnemyHit3);
                break;
            case "EnemyDie":
                m_audioSource.PlayOneShot(EnemyDie);
                break;
        }
    }
}
