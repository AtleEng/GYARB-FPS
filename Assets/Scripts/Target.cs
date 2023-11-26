using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;

    [SerializeField] int hp = 50;
    [SerializeField] float speed = 3.5f;
    [SerializeField] Vector3[] movePoints;

    int pointIndex;

    public void Start()
    {
        for (int i = 0; i < movePoints.Length; i++)
        {
            movePoints[i] += transform.position;
        }
    }

    public void TakeDamage(int amountdamage)
    {
        hp -= amountdamage;
        print($"{amountdamage} damage dealt to: {gameObject.name}");
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //lägg in cool particel effect här
        if (gameManager != null) { gameManager.TargetDestroyed(); }
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
