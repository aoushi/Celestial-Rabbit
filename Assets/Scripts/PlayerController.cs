using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Speed")]
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float interpolationAmount = 1f;
    [SerializeField] float speedBoostAmount = 1f;

    [Header("Skills")]
    public bool speedBoost = false;
    public bool shieldBoost = false;

    [Header("Weapons")]
    static public bool isLaserAquired = false;
    static public bool isLaserActive = false;
    [SerializeField] GameObject laser;
    [SerializeField] bool isVortexActive = false;
    [SerializeField] float powerUpDuration;
    public float powerUpTimer;
    public float timeBetweenLasers;
    

    [Header("Physics")]
    [SerializeField] float MaxSpeed;
    private Rigidbody2D rb2d;

    [Header("Interpolate")]
    public Transform a;
    public Transform b;
    [SerializeField] float interpolateSpeed;

    [Header("Audio")]
    [SerializeField] AudioSource powerUpSFX;
    [SerializeField] AudioSource laserBeamSFX;

    [Header("Animator")]
    private Animator animator;

    public Animator powerUpClone;

    int scoreTemp = 0;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    private Transform current;
    private Transform target;
    private float sinTime;

    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject scoreScreen;

    [SerializeField] int hp = 1;

    private float verticalSpeedTemp;
    
    public static bool isInsideInPlanet1 = false;

 


    private void Awake()
    {
        if(instance!= null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        Time.timeScale = 1f;

        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        current = a;
        target= b;
        transform.position = current.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        powerUpTimer += Time.deltaTime;
        timeBetweenLasers += Time.deltaTime;

        transform.rotation = new Quaternion(0, 0, 0, 0);

        InterpolateBetween();
        MoveCharacter();
        OutOfBounds();

        if (shieldBoost)
        {
            Shield();
            
        }

        CheckAlive();
     
        
        if (isLaserAquired && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLaserActive)
            {
                powerUpTimer = 0;
                isLaserActive = true;
                animator.SetBool("isLaserActive", true);
            }
            
            if(timeBetweenLasers > 0.51f)
            {
                laserBeamSFX.Play();
                LaserWeapon();
            }
            
            
            if (powerUpTimer >= powerUpDuration)
            {
                isLaserActive= false;
                animator.SetBool("isLaserActive", false);
                isLaserAquired = false;
                powerUpTimer = 0;
            }
            

        }
    }


    #region controller

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, verticalSpeed * Time.deltaTime ,0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -verticalSpeed * Time.deltaTime, 0);
        }
    }

    void OutOfBounds()
    {
        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        }
        else if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
        } 
    }









    #endregion

    #region weapons

    private void LaserWeapon()
    {


        Instantiate(laser, new Vector3(transform.position.x + 1.5f, transform.position.y + 0.08f, transform.position.z), new Quaternion(0,0,0,0));
        
        Destroy(GameObject.Find("LaserBeam(Clone)"), 0.5f);
        timeBetweenLasers = 0;

    }





    #endregion

    #region powerUps

    void Shield()
    {
        if(hp <= 0)
        {
            hp = 1;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log("abc" + gameObject.transform.GetChild(1).gameObject.activeInHierarchy);
            shieldBoost = false;
            
        }else{
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    #endregion

    #region timer

    IEnumerator GeneralTimer(float time)
    {
        
        yield return new WaitForSeconds(time);
    }

    #endregion

    #region colliders

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaserWeapon") && !isLaserAquired)
        {
            other.GetComponentInChildren<Animator>().enabled = true;
            other.GetComponentInChildren<Animator>().SetBool("shield", true);
            isLaserAquired = true;
            powerUpSFX.Play();
            Destroy(other.gameObject,1);
            
        }
        if (other.CompareTag("ShieldPowerUp") && !shieldBoost)
        {

            other.GetComponentInChildren<Animator>().enabled = true;
            other.GetComponentInChildren<Animator>().SetBool("shield", true);
            shieldBoost = true;
            powerUpSFX.Play();
            Destroy(other.gameObject,1);

        }
        if (other.CompareTag("LaserEnemy"))
        {
            hp -= 1;
            Debug.Log("hit");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            hp -= 1;
            Debug.Log("enemy");
        }



    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Planet1")
        {
            float gravity1 = 200f * Time.deltaTime;
            verticalSpeedTemp = this.verticalSpeed;
            this.verticalSpeed = 7f;
            rb2d.AddForce(new Vector3(0, gravity1, 0));
            VelocityLimiter();
            isInsideInPlanet1 = true;
        }
        if (other.gameObject.tag == "Planet2")
        {
            float gravity2 = 175f * Time.deltaTime;
            verticalSpeedTemp = this.verticalSpeed;
            this.verticalSpeed = 8f;
            if (this.gameObject.transform.position.y < 0)
                rb2d.AddForce(new Vector3(0, gravity2, 0));
            else
                rb2d.AddForce(new Vector3(0, -gravity2, 0));
            VelocityLimiter();
        }
        if (other.gameObject.tag == "Planet3")
        {
            float gravity3 = 250f * Time.deltaTime;
            verticalSpeedTemp = this.verticalSpeed;
            this.verticalSpeed = 6f;
            rb2d.AddForce(new Vector3(0, -gravity3, 0));
            VelocityLimiter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet1")
        {
            this.verticalSpeed = verticalSpeedTemp;
            rb2d.velocity= Vector3.zero;
        }

        if (collision.gameObject.tag == "Planet2")
        {
            this.verticalSpeed = verticalSpeedTemp;
            rb2d.velocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Planet3")
        {
            this.verticalSpeed = verticalSpeedTemp;
            rb2d.velocity = Vector3.zero;
        }
    }



    #endregion

    #region Interpolator
    void InterpolateBetween()
    {

        current.position = new Vector3(current.position.x, transform.position.y, 0);
        target.position = new Vector3(target.position.x, transform.position.y, 0);
        
        if(transform.position != target.position) 
        {
            sinTime += Time.deltaTime * interpolateSpeed;
            sinTime= Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);
            transform.position = Vector3.Lerp(current.position, target.position, t);

            swap();

        }

    }

    public void swap()
    {
        if(transform.position != target.position)
        {
            return;
        }

        Transform t = current;
        current = target;
        target = t;
        sinTime = 0;
    }

    public float evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }
    #endregion

    #region gameRules
    void CheckAlive()
    {
        if(hp <= 0)
        {
            GameFinish();
        }
    }
    #endregion

    void VelocityLimiter()
    {
        if (rb2d.velocity.magnitude > 4f)
        {
            rb2d.velocity = Vector3.ClampMagnitude(rb2d.velocity, 4f);
        }
    }

    #region setget
    public bool GetLaserAquired()
    {
        return isLaserAquired;
    }

    public bool GetLaserActive()
    {
        return isLaserActive;
    }

    #endregion

    #region highscore
    public void GameFinish()
    {



        if (int.Parse(score.text) > PlayerPrefs.GetInt("HighScore", 0))
        {

            scoreTemp = int.Parse(score.text);
            Debug.Log(scoreTemp);
            PlayerPrefs.SetInt("HighScore", scoreTemp);
            highScore.text = scoreTemp.ToString();

        }

        Time.timeScale = 0f;
        scoreScreen.SetActive(false);
        endScreen.SetActive(true);
        

    }
    #endregion
}
