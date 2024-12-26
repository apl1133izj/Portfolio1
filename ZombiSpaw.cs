using System.Collections;
using UnityEngine;


public class ZombiSpaw : MonoBehaviour
{
    public Transform[] zombiSpawPoint;
    public Transform[] zombiSpawEffectPoint;
    public GameObject zombiGameObject;
    public bool spawEnd;
    public bool spawBool; //���� ���������� Ȯ��
    public GameObject spawShilde;
    public GameObject spawEffect;
    GameObject ZombiInst;
    GameObject effect;
    GameObject zombiFind;
    private void Start()
    {
        StartCoroutine(SpawTime());
    }

    private void Update()
    {
        zombiFind = GameObject.Find("Zombi(Clone)");
        if (zombiFind == null && !spawBool)
        {
            spawShilde.gameObject.SetActive(false);
        }
        else
        {
            spawShilde.gameObject.SetActive(true);
        }
    }
    IEnumerator SpawTime()
    {
        while (spawBool)
        {
            spawBool = false;

            for (int count = 0; count < zombiSpawPoint.Length; count++)
            {
                spawBool = true;
                effect = Instantiate(spawEffect, zombiSpawEffectPoint[count].position, Quaternion.identity);
                // Y�� ȸ���� ����
                Vector3 direction = zombiSpawEffectPoint[count].position - transform.position;
                direction.y = 0; // ���� ���� ���͸� ���
                if (direction != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                }
                yield return new WaitForSeconds(3f);
                ZombiInst = Instantiate(zombiGameObject, zombiSpawPoint[count].position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                Destroy(effect);
            }
        }
        spawEnd = false;
    }
}

