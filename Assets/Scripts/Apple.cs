using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public Collider2D applearea;
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        //Debug.Log(applearea.bounds.min.x);
        //Debug.Log(applearea.bounds.max.x);
        //Debug.Log(applearea.bounds.min.y);
        //Debug.Log(applearea.bounds.max.y);

        //Random.Range(applearea.bounds.min.x, applearea.bounds.max.x);
        //Random.Range(applearea.bounds.min.y, applearea.bounds.max.y);


        RandomPosition();

    }

    void RandomPosition()
    {
        transform.position = new Vector3(
           Random.Range(applearea.bounds.min.x, applearea.bounds.max.x),
           Random.Range(applearea.bounds.min.y, applearea.bounds.max.y),
           0);

    }
}
