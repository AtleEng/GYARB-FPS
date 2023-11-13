using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int hp = 50;
    [SerializeField] float speed = 3.5f;
    [SerializeField] Vector3[] movePoints;

    [HideInInspector] public Door door;

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
        //lägg in cool particel effect här

        door.UpdateTargets(this);
        Destroy(gameObject);
    }
    public void Update()
    {
        if (movePoints.Length == 0) { return; }
        if ((movePoints[pointIndex] - transform.position).magnitude < 0.01f)
        {
            pointIndex++;
            if (pointIndex >= movePoints.Length) { pointIndex = 0; }
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex], Time.deltaTime * speed);
    }
}
