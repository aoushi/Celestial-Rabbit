using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    [SerializeField] float planetMoveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-planetMoveSpeed * Time.deltaTime, 0, 0));
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
