using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float hp = 50f;

    [SerializeField] float distance = 5f; 

    float speed = 3.5f;
    public void TakeDamage(float amountdamage)
    {
        hp -= amountdamage;
        if (hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, distance), transform.position.y, transform.position.z);
    }
}
