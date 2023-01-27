using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//my code
public class follow : MonoBehaviour
{

    public float speed = 1.0f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if target(person) moves, this object (fish) moves away. Otherwise move towards the target.
        if(target.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            Vector3 dir = transform.position - target.position;
            transform.Translate(dir * speed * Time.deltaTime);
        }else{
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }
}
