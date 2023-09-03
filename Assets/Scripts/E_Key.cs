using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Key : MonoBehaviour
{
    [SerializeField] Image cooldown;
    private float waitTime = 5.1f;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isLaserActive)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }

        if(PlayerController.isLaserAquired)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            cooldown.fillAmount= 1f;
        }

    }
}
