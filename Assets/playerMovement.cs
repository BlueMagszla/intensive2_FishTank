using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float startTime;
    private float endTime;
    private float waitTime = 10f;
    private float moveDuration = 2f;

    private Vector3 startingPosition;
    private Vector3 endPosition;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        endTime = startTime + Random.Range(7f, 15f);

        startingPosition = transform.position;
        endPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= endTime)
        {
            gameObject.SetActive(true);

            if (Time.time >= endTime + waitTime)
            {
                transform.position = Vector3.Lerp(startingPosition, endPosition, (Time.time - (endTime + waitTime)) / moveDuration);
            }
        }
    }
}

