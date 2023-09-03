using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInstantiator : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpList;
    [SerializeField] float instantiateRadius;
    Vector3 position;
    int randomPowerUp;
    float randomPowerUpTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GeneratePowerUp", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePowerUp()
    {
        randomPowerUp = Random.Range(0, powerUpList.Length);
        randomPowerUpTimer = Random.Range(7, 10);
        position = new Vector3(30, Random.Range(-instantiateRadius, instantiateRadius), 0);
        Instantiate(powerUpList[randomPowerUp], position, new Quaternion(0,0,0,0));
        Invoke("GeneratePowerUp", randomPowerUpTimer);
    }
}
