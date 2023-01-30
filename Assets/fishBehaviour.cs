using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public float speed = 5.0f;
    public float frequency = 1.0f;
    public float magnitude = 0.1f;
    public float changeDirectionTime = 5.0f;

    Vector3 pos, localPosition;
    float timer;
    Quaternion randomRotation;

    GameObject[] players;
    GameObject closestPlayer;

    void Start()
    {
        pos = transform.position;
        localPosition = transform.localPosition;
        timer = changeDirectionTime;
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            float closestDistance = Mathf.Infinity;

            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player;
                }
            }

            transform.LookAt(closestPlayer.transform);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                transform.rotation = randomRotation;
                timer = changeDirectionTime;
            }
        }

        pos += transform.forward * Time.deltaTime * speed;
        localPosition.y = Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * magnitude;
        transform.position = pos + localPosition;
    }
}