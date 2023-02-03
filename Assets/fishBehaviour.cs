using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    //normal speed is the fish's speed for when there are no players.
    public float normalSpeed = 3.0f;
    //chase is for when a fish is moving towards a player.
    public float chaseSpeed = 5.0f;
    //flee is for when a fish is moving away from a player.
    public float fleeSpeed = 7.0f;
    //fish movement variables, makes the fish bob up and down.
    public float frequency = 1.0f;
    public float magnitude = 0.8f;
    //fish moves in random direction every 5 seconds.
    public float changeDirectionTime = 5.0f;
    //fish fleeing from players variables.
    public float fleeRadius = 10.0f;
    public float fleeDuration = 5.0f;
    //keeps fish afloat
    public float floatingForce = 5.0f;
     public float torque = 10f;

    Vector3 localPosition;
    float timer;
    Quaternion randomRotation;
    float x;

    //variables to check how many players there are, which is closest, and keeping the player's positions.
    GameObject[] players;
    GameObject closestPlayer;
    Vector3 previousPlayerPosition;

    float fleeTimer;
    //rigidbody is required to make the fish move and collide with other objects (ex. other fish and the glass)
    Rigidbody rb;

    void Start()
    {
        localPosition = transform.localPosition;
        timer = changeDirectionTime;
        rb = GetComponent<Rigidbody>();
        x = transform.position.x;
        randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        //check how many objects have the tag "player"
        players = GameObject.FindGameObjectsWithTag("Player");

        //if there's more than one player, check the distance of each player and find the closest player to our fish.
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

            //if the player is moving while a fish is moving towards it, the fish will turn in a random direction and flee quickly for several seconds. 
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
                //if the fish flee countdown is greater than zero, keep counting down the timer and don't change the speed. 
                fleeTimer -= Time.deltaTime;
                rb.velocity = transform.forward * fleeSpeed;
            }
            else
            {   
                //if the player is still, the fish will rotate to face the player and then move towards the player. 
                transform.LookAt(closestPlayer.transform);
                previousPlayerPosition = closestPlayer.transform.position;
                rb.velocity = transform.forward * chaseSpeed;
            }
        }
        else
        {   
            // //if there's no player, change direction every 5 seconds. 
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Vector3 randomTorque = new Vector3(Random.Range(-torque, torque), 0, Random.Range(-torque, torque));
                //rb.AddTorque(randomTorque);
                transform.rotation = randomRotation;
                timer = changeDirectionTime;
            }

            //allows the fish to move around.
            rb.velocity = transform.forward * normalSpeed;
        }

        //makes the fish more realisitically
        x += Time.deltaTime * frequency;
        float y = Mathf.PerlinNoise(x, 0f) * magnitude;
        rb.position = new Vector3(rb.position.x, localPosition.y + y, rb.position.z);
        //keeps the fish afloat so it doesn't sink like a stone.
        rb.AddForce(Vector3.up * floatingForce);
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Fish"))
        {
            Vector3 currentVelocity = rb.velocity;
            Vector3 currentAngularVelocity = rb.angularVelocity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
