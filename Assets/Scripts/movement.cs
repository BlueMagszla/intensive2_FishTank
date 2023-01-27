using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position; // save initial position
    }

    void Update() {
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        yield return new WaitForSeconds(5f); // wait for 5 seconds
        for (int i = 0; i < 3; i++) {
            float x = Random.Range(-10f, 10f); // generate random x position
            float z = Random.Range(-10f, 10f); // generate random z position
            transform.position = new Vector3(x, transform.position.y, z); // move to random position
            yield return new WaitForSeconds(1f); // wait for 1 second
        }
        transform.position = initialPosition; // return to initial position
        yield return new WaitForSeconds(3f); // wait for 3 seconds
    }
}