using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody playerRigidbody;
    private float runSpeed = 10.0f;
    private int lane = 0;
    private float velocity;
    private float smoothValue = 0.3f;
    private int score;
    public Text scoreText;

    private float jumpForce = 4.0f;


	void Start ()
	{
	    anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        score = 0;
	}
	
	void Update ()
    {
        transform.Translate(0, 0, runSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lane > -1)
            {
                lane--;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lane < 1)
            {
                lane++;
            }
        }

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.SmoothDamp(newPosition.x,lane,ref velocity,smoothValue);
        transform.position = newPosition;

    }
    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.y < 0.6f)
            {
                anim.SetTrigger("isJumping");
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            print(score);
            Instantiate(other.transform.GetChild(0), other.transform.position, Quaternion.identity);
            //other.gameObject.SetActive(false);

        }

        if (other.CompareTag("obstacle"))
        {
            StartCoroutine(SlowDownPlayer());
            Instantiate(other.transform.GetChild(0), other.transform.position, Quaternion.identity);
            print("Speed: " + runSpeed);
        }
    }

    IEnumerator SlowDownPlayer()
    {
        runSpeed = 5;
        yield return new WaitForSeconds(3);
        runSpeed = 10;
    }

}
