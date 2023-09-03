using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    private bool noMoreLaserEnemy = false;
    Vector3 randomPosition;
    [SerializeField] float instantiateRadius;
    [SerializeField] GameObject laserEnemy;
    private float randomInterval1 = 8;
    private float randomInterval2 = 12;
    

    void Start()
    {
        Invoke("LaserEnemySpawner", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("LaserEnemy").Length >= 8)
        {
            noMoreLaserEnemy = true;
        }
        else
        {
            noMoreLaserEnemy= false;
        }

       
    }


    void LaserEnemySpawner()
    {
        randomPosition = new Vector3(20 - (Random.Range(0,8)), Random.Range(-instantiateRadius, instantiateRadius), 0);


        if (!noMoreLaserEnemy) 
        { 
            Instantiate(laserEnemy, randomPosition, new Quaternion(0,0,0,0));
        }

        Invoke("LaserEnemySpawner", Random.Range(randomInterval1, randomInterval2));
    }
}
