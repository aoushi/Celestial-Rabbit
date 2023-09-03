using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUI;
    public static float scoreovic;
    float verticalScoreTemporary;
   
    
    // Start is called before the first frame update
    void Start()
    {
        //verticalSpeedTemporary = PlayerController.verticalSpeed;
        scoreUI.text = "Score: 0";
    }

    // Update is called once per frame
    void LateUpdate()
    {
        verticalScoreTemporary += (Time.deltaTime * 367);
        scoreUI.text = System.Math.Round(verticalScoreTemporary, 0).ToString();
    }
}
