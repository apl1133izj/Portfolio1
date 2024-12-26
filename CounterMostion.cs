using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMostion : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            animator.SetBool("CounterHit Bool", true);
            //attackSound.PlayOneShot(attackClip[5], 1.0f);
        }
    }
}
