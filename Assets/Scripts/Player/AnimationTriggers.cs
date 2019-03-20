using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_walkDirtClips;
    private AudioSource m_audioSource;

    private Animator m_animator;

    private SoundTrigger m_soundTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_animator = gameObject.GetComponent<Animator>();
        m_soundTrigger = GameObject.Find("PlayerHandle").transform.Find("SoundTrigger").GetComponent<SoundTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Triggered by the animator every time the player makes a footstep
    private void Step()
    {
        bool isRunning = m_animator.GetBool("IsRunning");
        bool isCrouching = m_animator.GetBool("IsCrouch");
        if(isRunning)
        {
            m_audioSource.volume = 1;
            m_soundTrigger.AddSound(10f);
        }
        else if (isCrouching)
        {
            m_audioSource.volume = 0.25f;
            m_soundTrigger.AddSound(2f);
        }
        else
        {
            m_audioSource.volume = 0.5f;
            m_soundTrigger.AddSound(5f);
        }
        m_audioSource.PlayOneShot(m_walkDirtClips[Random.Range(0, m_walkDirtClips.Length)]);
    }
}
