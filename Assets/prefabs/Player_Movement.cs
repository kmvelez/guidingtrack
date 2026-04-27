using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    float m_footstepDistanceCounter;
    public float footstepSFXFrequency = 1f;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 lastPosition = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //AkSoundEngine.PostEvent("Player_Jump", gameObject);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        float chosenFootstepSFXFrequency = footstepSFXFrequency;
        if (m_footstepDistanceCounter >= 1f / chosenFootstepSFXFrequency)
        {
            m_footstepDistanceCounter = 0f;
            //m_playerEvent.PlayFootstepSound();
            //AkSoundEngine.PostEvent("Player_Footstep", gameObject);
            //audioSource.PlayOneShot(footstepSFX);

        }

        // keep track of distance traveled for footsteps sound
        //m_footstepDistanceCounter += velocity * Time.deltaTime;
    }
    void Footsteps()
    {
        if (lastPosition != transform.position)
        {
            lastPosition = transform.position;
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else if (lastPosition == transform.position)
        {
            GetComponent<AudioSource>().Stop();
        }

    }
}
