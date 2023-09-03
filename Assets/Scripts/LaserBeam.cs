using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(40f * Time.deltaTime, 0, 0);
    }

}
