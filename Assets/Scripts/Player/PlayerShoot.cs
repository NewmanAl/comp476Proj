
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Shooter gun;
    public Camera cam;
    public float damage = 1f;
    public float range = 100f;
    public ParticleSystem muzzelFlash;
    private float fireTime;
    private SoundTrigger soundTrigger;

    private void Start()
    {
        soundTrigger = GameObject.Find("PlayerHandle").transform.Find("SoundTrigger").GetComponent<SoundTrigger>();
    }
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (GameManager.Instance.InputController.fire)
        {
            if(gun.Fire())
            {
                soundTrigger.AddSound(soundTrigger.GetSoundRadius() * 3.5f + 1);
                muzzelFlash.Play();
                fireTime = Time.time;
                RaycastHit hit;
                int layerMask = (1 << 9); //Zombie is on layer 9
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, layerMask))
                {
                    Health target = hit.transform.GetComponent<Health>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }
                }
            }
        }

        if(Time.time - fireTime >= 0.1f)
        {
            muzzelFlash.Stop();
        }
    }





}
