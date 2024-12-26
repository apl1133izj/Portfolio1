using System.Collections.Generic;
using UnityEngine;

public class GravitySkill : MonoBehaviour
{
    public GameObject[] monsterGravity;
    void Start()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (monsters.Length == 0) // ���� ó��
        {
            return;
        }

        // �ӽ� ����Ʈ�� ����� �Ÿ� ���ǿ� �´� ���͸� ����
        List<GameObject> validMonsters = new List<GameObject>();

        foreach (GameObject monster in monsters)
        {
            if (monster == null) continue; // ���� ó��

            float disMonster = Vector3.Distance(gameObject.transform.position, monster.transform.position);
            if (disMonster <= 50) // �Ÿ� ����
            {
                validMonsters.Add(monster);
            }
        }

        // �Ÿ� ���ǿ� �´� ���͵�� �迭 �ʱ�ȭ
        monsterGravity = validMonsters.ToArray();

    }

    // Update is called once per frame
    void Update()
    { 
        // ���� ��ġ ����
        for (int i = 0; i < monsterGravity.Length; i++)
        {
            monsterGravity[i].transform.position = Vector3.MoveTowards(gameObject.transform.position, monsterGravity[i].transform.position, 10);
        }
    }
}
