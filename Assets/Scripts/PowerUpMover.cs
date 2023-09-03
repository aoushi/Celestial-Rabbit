using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMover : MonoBehaviour
{
    [SerializeField] float powerUpMoveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-powerUpMoveSpeed * Time.deltaTime, 0, 0));
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
