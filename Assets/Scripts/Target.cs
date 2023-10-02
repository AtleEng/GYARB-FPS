using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int hp = 50;
    [SerializeField] float speed = 3.5f;
    [SerializeField] float length = 10;
    public void TakeDamage(int amountdamage)
    {
        hp -= amountdamage;
        print(hp);
        if (hp <= 0)
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
