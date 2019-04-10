using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_walkDirtClips;
    [SerializeField]
    private AudioClip[] m_attackClips;
    [SerializeField]
    private AudioClip[] m_moanClips;
    private AudioSource m_stepSource;
    private AudioSource m_attackSource;
    private AudioSource m_moanSource;

    private Animator m_animator;

    private float m_nextMoanTime;
    
    // Start is called before the first frame update
    void Start()
    {

        m_animator = GetComponent<Animator>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        m_stepSource = audioSources[0];
        m_attackSource = audioSources[1];
        m_moanSource = audioSources[2];
        
        //Zombie animations do not seem to let animation events be set
        //through the Unity editor...
        //Let's add them manually!

        AnimationClip[] clips = m_animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip c in clips)
        {
            if(c.name == "walk")
            {
                AnimationEvent e1 = new AnimationEvent();
                e1.functionName = "Step";
                e1.time = 0.26409495f;
                c.AddEvent(e1);

                AnimationEvent e2 = new AnimationEvent();
                e2.functionName = "Step";
                e2.time = 0.68397623f;
                c.AddEvent(e2);
            }

            if(c.name == "attack")
            {
                AnimationEvent e1 = new AnimationEvent();
                e1.functionName = "Attack";
                e1.time = 0.25873017f;
                c.AddEvent(e1);

                AnimationEvent e2 = new AnimationEvent();
                e2.functionName = "Attack";
                e2.time = 0.6952381f;
                c.AddEvent(e2);

                AnimationEvent e3 = new AnimationEvent();
                e3.functionName = "Attack";
                e3.time = 0.8222222f;
                c.AddEvent(e3);
            }
        }

        m_nextMoanTime = Time.time + Random.Range(4f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        //periodically moan
        if(Time.time >= m_nextMoanTime)
        {
            m_moanSource.pitch = Random.Range(0.85f, 1.1f);
            m_moanSource.PlayOneShot(m_moanClips[Random.Range(0, m_moanClips.Length)]);
            m_nextMoanTime = Time.time + Random.Range(4f, 15f);
        }

    }

    private void Step()
    {
        m_stepSource.PlayOneShot(m_walkDirtClips[Random.Range(0, m_walkDirtClips.Length)]);
    }

    private void Attack()
    {
        m_attackSource.pitch = Random.Range(0.9f, 1.1f);
        m_attackSource.PlayOneShot(m_attackClips[Random.Range(0, m_attackClips.Length)]);
    }

}
