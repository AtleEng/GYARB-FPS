using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float hp = 50f;
    [SerializeField] float speed = 3.5f;
    [SerializeField] float length = 10;
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
        transform.position = new Vector3(-length + Mathf.PingPong(Time.time * speed, length * 2), transform.position.y, transform.position.z);
    }
}
