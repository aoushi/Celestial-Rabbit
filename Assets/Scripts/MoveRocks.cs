using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocks : MonoBehaviour
{
    private Rigidbody2D rgb2d;
    
    // Start is called before the first frame update
    void Start()
    {
       rgb2d = gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
        {
            rgb2d.AddForce(new Vector2(-100f * Time.deltaTime, Random.Range(0, 150f) *Time.deltaTime));
        } else if (transform.position.y > 1) 
        {
            rgb2d.AddForce(new Vector2(-100f * Time.deltaTime, Random.Range(-150f, 0) * Time.deltaTime));
        }
        else
        {
            rgb2d.AddForce(new Vector2(-100f * Time.deltaTime, Random.Range(-50f, 50f) * Time.deltaTime));
        }

        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }

    }
}
