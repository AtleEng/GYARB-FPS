using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] public float hp = 50f;
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
}
