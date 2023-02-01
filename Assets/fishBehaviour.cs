using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public float normalSpeed = 1.0f;
    public float chaseSpeed = 2.0f;
    public float fleeSpeed = 3.0f;
    public float frequency = 1.0f;
    public float magnitude = 0.1f;
    public float changeDirectionTime = 5.0f;
    public float fleeRadius = 10.0f;
    public float fleeDuration = 5.0f;
    public float floatingForce = 5.0f;

    Vector3 localPosition;
    float timer;
    Quaternion randomRotation;
    float x;

    GameObject[] players;
    GameObject closestPlayer;
    Vector3 previousPlayerPosition;
    float fleeTimer;
    Rigidbody rb;

    void Start()
    {
        localPosition = transform.localPosition;
        timer = changeDirectionTime;
        rb = GetComponent<Rigidbody>();
        x = 0f;
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

            if (closestDistance <= fleeRadius && closestPlayer.transform.position != previousPlayerPosition)
            {
                randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                transform.rotation = randomRotation;
                previousPlayerPosition = closestPlayer.transform.position;
                fleeTimer = fleeDuration;
                rb.velocity = transform.forward * fleeSpeed;
            }
            else if (fleeTimer > 0)
            {
                fleeTimer -= Time.deltaTime;
                rb.velocity = transform.forward * fleeSpeed;
            }
            else
            {
                transform.LookAt(closestPlayer.transform);
                previousPlayerPosition = closestPlayer.transform.position;
                rb.velocity = transform.forward * chaseSpeed;
            }
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

            rb.velocity = transform.forward * normalSpeed;
        }

        x += Time.deltaTime * frequency;
        float y = Mathf.PerlinNoise(x, 0f) * magnitude;
        rb.position = new Vector3(rb.position.x, localPosition.y + y, rb.position.z);
        rb.AddForce(Vector3.up * floatingForce);
    }
}
