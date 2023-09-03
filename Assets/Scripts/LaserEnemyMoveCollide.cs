using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemyMoveCollide : MonoBehaviour
{

    float timeElapsed;
    float lerpDuration = 2;
    float startValue = 0;
    float endValue = 10f;
    float valueToLerp;
    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 1f;
    private bool hasArrived = false;
    [SerializeField] GameObject enemyLaser;

    private AudioSource BoomAudio;
    private GameObject SFX1;

    // Start is called before the first frame update
    void Start()
    {

        SFX1 = GameObject.FindGameObjectWithTag("SFX1");

        BoomAudio = SFX1.GetComponent<AudioSource>();

        



    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            transform.position -= new Vector3(valueToLerp * Time.deltaTime, 0, 0);
            timeElapsed += Time.deltaTime;
        }

        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        }
        else if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
        }


        if (timeElapsed >= lerpDuration) 
        {
            if (!hasArrived)
            {
                Instantiate(enemyLaser,new Vector3(transform.position.x, transform.position.y, transform.position.z),new Quaternion(0,0,0,0));
                hasArrived = true;
                float randX = Random.Range(-5.0f, 6f);
                StartCoroutine(MoveToPoint(new Vector3(transform.position.x + Random.Range(-.5f, .5f), randX, 0)));

            }
        }


    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            BoomAudio.Play();
            Debug.Log("contact");
            Destroy(collision.gameObject);
            Destroy(gameObject);
 
        }
    }

}
