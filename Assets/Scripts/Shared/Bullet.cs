using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] float damage;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        var destructable = col.transform.GetComponent<Destructable>();
        if (destructable == null)
            return;
        destructable.TakeDamage(damage);
        Destroy(gameObject);
    }
}
