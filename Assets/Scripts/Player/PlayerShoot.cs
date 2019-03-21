
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
    void Update()
    {
       
        Shoot();
    }
    void Shoot()
    {
        if (GameManager.Instance.InputController.fire)
        {
                gun.Fire();

            muzzelFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Health target = hit.transform.GetComponent<Health>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
      }
        muzzelFlash.Stop();
    }





}
