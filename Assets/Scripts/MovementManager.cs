using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed, jumpHeight;
    public bool onGround;
    public TextMeshPro timerText;
    private float secondsCount;
    private int minuteCount;
    private bool timerStart = false;
    public AudioClip clipJump;
    public AudioClip clipStart;
    public AudioClip clipFinish;
    public AudioSource source;
    void Start()
    {
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }

        if(Input.GetAxisRaw("Vertical") > 0 && onGround)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            source.PlayOneShot(clipJump);
            
        }

        if(timerStart)
        {
            UpdateTimerUI();
        }
    }
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "ground" || obj.gameObject.tag == "CrumblePlatform")
        {
            onGround = true;
        }
        else if(obj.gameObject.tag == "Start" && timerStart == false)
        {
            UpdateTimerUI();
            timerStart = true;
            source.PlayOneShot(clipStart);
        }
        else if(obj.gameObject.tag == "Finish" && timerStart == true)
        {
            timerStart = false;
            source.PlayOneShot(clipFinish);
        }
    }
    private void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "ground")
        {
            onGround = false;
        }
    }
    public void UpdateTimerUI()
    {
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }
}
