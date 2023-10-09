using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int hp = 50;
    [SerializeField] float speed = 3.5f;

    [SerializeField] Vector3[] movePoints;
    int pointIndex;
    public void TakeDamage(int amountdamage)
    {
        hp -= amountdamage;
        print(hp + " damage dealt to: " + gameObject.name);
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
        //transform.position = new Vector3(-length + Mathf.PingPong(Time.time * speed, length * 2), transform.position.y, transform.position.z);
        if ((movePoints[pointIndex] - transform.position).magnitude < 0.01f)
        {
            pointIndex++;
            if (pointIndex >= movePoints.Length) { pointIndex = 0; }
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex], Time.deltaTime * speed);

    }
}
