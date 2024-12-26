using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterFalse : MonoBehaviour
{
    public Animator animator;
    public void CounterHitFalse()
    {
        animator.SetBool("CounterHit Bool", false);
    }
}
