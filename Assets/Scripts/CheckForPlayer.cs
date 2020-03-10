using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{


    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D obj)
    {
       if(obj.gameObject.tag == "CrumblePlatform")
        {
            m_Animator.SetTrigger("HasPlayerOn");
        }
    }
}
