using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] public float hp = 50f;

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
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 5), transform.position.y, transform.position.z);
    }
}
