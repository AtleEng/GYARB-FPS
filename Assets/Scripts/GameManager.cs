using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab;
    [SerializeField] int numberOfTargetsToSpawn;
    [SerializeField] int targetsDestroyedToEndGame;

    [SerializeField] Vector2 xyMax;
    [SerializeField] Vector2 xyMin;


    public int targetFPS = 60;

    int targetsDestroyed = 0;

    float timeElapsed;

    public int shootsFired;
    public int hitShoots;

    public bool isPaused = true;

    [SerializeField] TextMeshPro pointsText;
    [SerializeField] TextMeshPro timerText;
    [SerializeField] TextMeshPro accuracyText;
    [SerializeField] TextMeshPro fpsText;

    private void Start()
    {
        Application.targetFrameRate = targetFPS;
        for (int i = 0; i < numberOfTargetsToSpawn; i++)
        {
            SpawnTarget();
        }
    }
    void Update()
    {
        if (!isPaused)
        {
            timeElapsed += Time.deltaTime;
            string s = timeElapsed.ToString("0.00");

            timerText.text = "Time: " + s;

            int currentFPS = (int)(1f / Time.deltaTime);
            fpsText.text = $"FPS: {currentFPS}";

            if (shootsFired != 0)
            {
                accuracyText.text = "Accuracy: " + (int)((float)hitShoots / (float)shootsFired * 100) + "%";
            }
        }

    }
    private void SpawnTarget()
    {
        Vector2 spawnPosition = GetRandomPositionInRectangle();
        GameObject gO = Instantiate(targetPrefab, spawnPosition, Quaternion.identity, gameObject.transform);
        gO.GetComponent<Target>().gameManager = this;
    }

    private Vector2 GetRandomPositionInRectangle()
    {
        float x = Random.Range(xyMax.x, xyMin.x);
        float y = Random.Range(xyMax.y, xyMin.y);

        return new Vector2(x, y);
    }

    public void TargetDestroyed()
    {
        if (targetsDestroyed == 0)
        {
            isPaused = false;
        }

        targetsDestroyed++;
        pointsText.text = $"Points: {targetsDestroyed}/{targetsDestroyedToEndGame}";

        if (targetsDestroyed >= targetsDestroyedToEndGame)
        {
            Debug.Log("Game Over!");
            foreach (Transform child in transform)
            {
                if (child.GetComponent<Target>() != null) { Destroy(child.gameObject); }

            }
            isPaused = true;
        }
        else
        {
            // Spawn new targets after a target is destroyed.
            SpawnTarget();
        }
    }
}
