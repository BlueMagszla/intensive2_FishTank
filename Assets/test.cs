using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed = 5.0f;
    private GameObject[] players;
    private GameObject closestPlayer;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
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

        Vector3 direction = closestPlayer.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              Time.deltaTime * 5);
        if (closestPlayer.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= direction.normalized * speed * Time.deltaTime;
        }
    }
}



// public class detectPerson : MonoBehaviour
// {
//     public float speed;
//     private GameObject player;
//     private bool canMove = true;

//     void Start()
//     {
//         player = GameObject.FindWithTag("Player");
//     }

//     void Update()
//     {
//         // if(canMove)
//         // {
//             Vector3 direction = player.transform.position - transform.position;
//             transform.rotation = Quaternion.Slerp(transform.rotation,
//                                                   Quaternion.LookRotation(direction),
//                                                   Time.deltaTime * 5);
//             if (player.GetComponent<Rigidbody>().velocity.magnitude == 0)
//             {
//                 transform.position += direction.normalized * speed * Time.deltaTime;
//             }
//             else
//             {
//                 transform.position -= direction.normalized * (speed * 3) * Time.deltaTime;
//             }
//         //}
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if(collision.gameObject.CompareTag("Fish"))
//         {
//             canMove = false;
//         }
//     }

// }


// public class detectPerson : MonoBehaviour
// {
//    public float speed;
//     private GameObject player;

//     void Start()
//     {
//         player = GameObject.FindWithTag("Player");
//     }

//     void Update()
//     {
//         Vector3 direction = player.transform.position - transform.position;
//         transform.rotation = Quaternion.Slerp(transform.rotation,
//                                               Quaternion.LookRotation(direction),
//                                               Time.deltaTime * 5);
//         if (player.GetComponent<Rigidbody>().velocity.magnitude == 0)
//         {
//             transform.position += direction.normalized * speed * Time.deltaTime;
//         }
//         else
//         {
//             transform.position -= direction.normalized * speed * Time.deltaTime;
//         }
//     }
// }