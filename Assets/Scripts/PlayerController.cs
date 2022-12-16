using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    //[SerializeField] float moveSpeed = 5f;
    [SerializeField] float speed = 10f;
    private int desiredLine = 1; // 0 = left , 1 = midddle , 2 = right 
    private const float LANE_DISTANCE = 3f;

    //turning
    private const float TURN_SPEED = 0.5f;

    //jumbing
    private float verticalVelocity;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gravity = 9f;

    //animation
    private Animator Anime;

    [SerializeField] AudioSource DieAudio;

    //private bool died;

    private void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        Anime = GetComponent<Animator>();
    }

    private void Update()
    {
        // gather the inputs on which lane we should be
        if ((Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLane(true);
        }
        if ((Input.GetKeyDown(KeyCode.D)) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveLane(false);
        }

        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLine == 0)
        {
            transform.position += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLine == 2)
        {
            transform.position += Vector3.right * LANE_DISTANCE;
        }

        //lets calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;


        bool isGrounded = IsGrounded();
        Anime.SetBool("grounded", isGrounded);

        //jump
        if (isGrounded)
        {
            verticalVelocity = -0.1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Anime.SetTrigger("jump");
                verticalVelocity = jumpForce;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            //fast falling mechanic
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = -jumpForce;
            }
        }


        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move the character
        controller.Move(moveVector * Time.deltaTime);

        //rotate the character to where he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }

        //if (died = true)
        //{
        //    DieAudio.enabled = false;
        //}

    }

    

    private void moveLane(bool goingRight)
    {
        //if (!goingRight)
        //{
        //    desiredLine--;
        //    if (desiredLine == -1)
        //    {
        //        desiredLine = 0;
        //    }
        //}
        //else
        //{
        //    desiredLine++;
        //    if (desiredLine == 3)
        //    {
        //        desiredLine = 2;
        //    }
        //}

        desiredLine += goingRight ? 1 : -1;
        desiredLine = Mathf.Clamp(desiredLine, 0, 2);

    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(
                controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
            controller.bounds.center.z),
            Vector3.down);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);

    }

    private void StartSliding()
    {
        Anime.SetBool("slide",true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2,controller.center.z);
    }

    private void StopSliding()
    {
        Anime.SetBool("slide", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Coin")
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "vehicle")
    //    {
    //        Debug.Log("collide with : "+collision.gameObject.name);
    //       // Anime.SetTrigger("die");
    //        Time.timeScale = 0.5f;
    //        Invoke("dieFN",5f);
    //    }
    //    else if (collision.gameObject.tag == "barriers")
    //    {
    //        Debug.Log("collide with : " + collision.gameObject.name);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "vehicle")
        {
            Death();
        }
        //else if (other.tag == "barriers")
        //{
        //    Anime.SetTrigger("die");
        //    Time.timeScale = 0.5f;
        //    Invoke("dieFN", 5f);
        //}
    }

    //public void DieOverAnime()
    //{
    //    Anime.SetBool("over", false);
    //}

    public void dieFN()
    {
        GameManager.instance.diePanel.SetActive(true);
        DieAudio.enabled = false;
    }

    public void Death()
    {
        DieAudio.Play();
        GameManager.instance.audioSource.enabled = false;
        speed = 1;
        Anime.SetTrigger("die");
        Time.timeScale = 0.5f;
        GameManager.instance.scoreSpeed = 0;
        //Invoke("DieOverAnime", 0.5f);
        Invoke("dieFN", 2f);
        //died = true;
    }
     


}

