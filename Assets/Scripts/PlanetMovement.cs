using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] GameObject[] planetList;
    [SerializeField] float planetTimeMin, planetTimeMax;
    private int randomPlanet;
    
    void Start()
    {
        Invoke("PlanetInstantiator", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlanetInstantiator()
    {   
        randomPlanet = Random.Range(0, planetList.Length);
        float randomPlanetTimer = Random.Range(planetTimeMin, planetTimeMax);
        Instantiate(planetList[randomPlanet]);
        Invoke("PlanetInstantiator", randomPlanetTimer);     
        
    }
}
