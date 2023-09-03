using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    public int enemySpeed;
    public List<GameObject> meteors;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateEnemy", 2);
    }
    private void CreateEnemy()
    {
        float enemyXRand, enemyYRand, enemyZRand;
        int meteorIndex = Random.Range(0, meteors.Count);
        int enemyPos = Random.Range(1, 4);
        float y_force = 0;

        
        if (enemyPos == 1)
        {

            enemyXRand = Random.Range(point1.transform.position.x - 8, point1.transform.position.x);
            enemyYRand = Random.Range(point1.transform.position.y, point1.transform.position.y);
            enemyZRand = Random.Range(point1.transform.position.z, point1.transform.position.z);
            Instantiate(meteors[meteorIndex], new Vector3(enemyXRand, enemyYRand, enemyZRand), new Quaternion());
            y_force = -(float)enemySpeed;
            meteors[meteorIndex].transform.position = new Vector3(enemyXRand, enemyYRand - 2, 0);
        }
        else if (enemyPos == 2)
        {

            enemyXRand = Random.Range(point2.transform.position.x, point2.transform.position.x);
            enemyYRand = Random.Range(point2.transform.position.y - 5, point2.transform.position.y);
            enemyZRand = Random.Range(point2.transform.position.z, point2.transform.position.z);
            y_force = 0f;
            Instantiate(meteors[meteorIndex], new Vector3(enemyXRand, enemyYRand, 0), new Quaternion());
        }
        else if (enemyPos == 3)
        {

            enemyXRand = Random.Range(point3.transform.position.x - 8, point3.transform.position.x);
            enemyYRand = Random.Range(point3.transform.position.y, point3.transform.position.y);
            enemyZRand = Random.Range(point3.transform.position.z, point3.transform.position.z);
            Instantiate(meteors[meteorIndex], new Vector3(enemyXRand, enemyYRand, 0), new Quaternion());
            y_force = (float)enemySpeed;
            meteors[meteorIndex].transform.position = new Vector3(enemyXRand, enemyYRand + 2, enemyZRand);
        }

        
        Invoke("CreateEnemy", 2);

    }

}
