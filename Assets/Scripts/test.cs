using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float checkInterval = 1f;
    public float moveSpeed = 5f;
    public float proximityThreshold = 10f;
    public float fleeDistance = 20f;

    private GameObject[] players;
    private GameObject closestPlayer;
    private Vector3 moveDirection;
    private float randomInterval;

    void Start()
    {
        InvokeRepeating("CheckForPlayers", 0f, checkInterval);
    }

    void CheckForPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int totalPlayers = players.Length;
        int closePlayers = 0;
        int stillPlayers = 0;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= proximityThreshold)
            {
                if (player.GetComponent<Rigidbody>().velocity.magnitude == 0f)
                {
                    stillPlayers++;
                }
                closePlayers++;
            }
        }

        Debug.Log("Total players: " + totalPlayers + " Close players: " + closePlayers + " Still players: " + stillPlayers);

        if (stillPlayers > 0)
        {
            closestPlayer = FindClosestPlayer(true);

            if (closestPlayer != null)
            {
                randomInterval = Random.Range(1f, 5f);
                Invoke("MoveTowardsPlayer", randomInterval);
            }
        }
    }

    void MoveTowardsPlayer()
    {
        moveDirection = (closestPlayer.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(moveDirection);

        float distance = Vector3.Distance(transform.position, closestPlayer.transform.position);

        if (distance > proximityThreshold)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    GameObject FindClosestPlayer(bool stillOnly)
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < closestDistance && (!stillOnly || player.GetComponent<Rigidbody>().velocity.magnitude == 0f))
            {
                closestDistance = distance;
                closest = player;
            }
        }

        return closest;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveDirection = (transform.position - other.transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += moveDirection * fleeDistance;
        }
    }
}
