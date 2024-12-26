using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrail : MonoBehaviour
{
    //�¾����� ��Ʈ ��ƼŬ(Demon_damaged)
    public GameObject g_HitFire;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WaterHitPaticle"))
        {
            Debug.Log("OnCollisionEnter");
            Destroy(gameObject);
        }
         
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("WaterHitPaticle"))
        {
            Destroy(gameObject,0.2f);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GameObject hitPaticle = Instantiate(g_HitFire, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(hitPaticle, 2);
        }
    }
}
