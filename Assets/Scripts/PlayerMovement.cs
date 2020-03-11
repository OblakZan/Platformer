using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;

    public GameObject player;
    private Vector3 offset;

    public AudioClip clipJump;
    public AudioClip clipStart;
    public AudioClip clipFinish;
    public AudioClip clipDeath;
    public AudioSource source;
        

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            source.PlayOneShot(clipJump);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "wall_bottom")
        {
            source.PlayOneShot(clipDeath);
            System.Threading.Thread.Sleep(300);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
