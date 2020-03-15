using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    private float secondsCount;
    private int minuteCount;
    public TextMeshPro timerText;
    private bool timerStart = false;

    //podatki za merjenje casa
    public static class Data
    {
        public static float shraniSekunde = 0;
        public static int shraniMinute = 0;
        public static float shraniSekundeRekord = 59;
        public static int shraniMinuteRekord = 60;
        public static int poskus = 1;
        public static bool prikazZmagovalnegaCasa = false;
    }

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

        if (timerStart)
        {
            UpdateTimerUI();
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
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                SceneManager.LoadScene("Level 1");
            }
            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Level 2");
            }
        }
        else if (collision.gameObject.name == "Start_wall")
        {
            timerStart = true;
            UpdateTimerUI();
            source.PlayOneShot(clipStart);
        }
        else if (collision.gameObject.name == "Finish_wall" && timerStart == true)
        {
            Data.prikazZmagovalnegaCasa = true;
            Data.shraniSekunde = secondsCount;
            Data.shraniMinute = minuteCount;
            if (Data.shraniSekundeRekord >= Data.shraniSekunde && Data.shraniMinuteRekord >= Data.shraniMinute)
            {
                Data.shraniMinuteRekord = Data.shraniMinute;
                Data.shraniSekundeRekord = Data.shraniSekunde;
            }
            else
            {
                /*Data.shraniMinuteRekord = Data.shraniMinute;
                Data.shraniSekundeRekord = Data.shraniSekunde;*/
            }
            Data.poskus = Data.poskus + 1;
            source.PlayOneShot(clipFinish);
            timerStart = false;
            if(SceneManager.GetActiveScene().name == "Level 1")
            {
                System.Threading.Thread.Sleep(300);
                SceneManager.LoadScene("Level 2");
            }
            else if(SceneManager.GetActiveScene().name == "Level 2")
            {
                System.Threading.Thread.Sleep(300);
                SceneManager.LoadScene("Menu");
            }
        }
    }


    public void UpdateTimerUI()
    {
        secondsCount += Time.deltaTime;

        if (Data.prikazZmagovalnegaCasa == false)
        {
            timerText.text = minuteCount + "m:" + (int)secondsCount + "s \n"
                + "Čas prejšnje igre:" + Data.shraniMinute + "m" + (int)Data.shraniSekunde + "s \n"
                + "Poskus:" + Data.poskus;
        }
        else
        {
            timerText.text = minuteCount + "m:" + (int)secondsCount + "s \n"
                + "Čas prejšnje igre:" + Data.shraniMinute + "m" + (int)Data.shraniSekunde + "s \n"
                + "Najboljsi čas:" + Data.shraniMinuteRekord + "m" + (int)Data.shraniSekundeRekord + "s \n"
                + "Poskus:" + Data.poskus;
        }

        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }
}
