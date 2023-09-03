using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject PauseMenuController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !PauseMenuController.activeInHierarchy)
        {
            PauseMenuController.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuController.activeInHierarchy)
        {
            PauseMenuController.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void continueGame()
    {
        
        PauseMenuController.SetActive(false);
        Time.timeScale = 1f;
    }
}
