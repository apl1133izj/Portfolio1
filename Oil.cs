using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Oil : MonoBehaviour
{

    public GameObject residualfire;//�ܺ� ��ƼŬ

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Load"))
        {
            GameObject instresidualfire = Instantiate(residualfire, other.transform.position + new Vector3(0,5,0), Quaternion.identity);
            Destroy(instresidualfire, 3f);
        }
    }
}
