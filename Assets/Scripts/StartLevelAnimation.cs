using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelAnimation : MonoBehaviour
{

    [SerializeField] Animator startAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        startAnimation.SetTrigger("Start");
    }

 
}
