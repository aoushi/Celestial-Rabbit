using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    private AudioSource enemyLaserSFX;
    // Start is called before the first frame update
    void Start()
    {
        enemyLaserSFX= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-7f* Time.deltaTime, 0, 0);
        if(transform.position.x < -20) 
        { 
            Destroy(gameObject);
        }

    }
}
