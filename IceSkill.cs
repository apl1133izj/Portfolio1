using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceSkill : MonoBehaviour
{
    public GameObject[] monsterSerch;
    float monsterDis;
    public Vector3[] monsterPos;
    bool isMoveIceSkill;
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
        monsterSerch = validMonsters.ToArray();
        monsterPos = new Vector3[monsterSerch.Length];

        // ���� ��ġ ����
        for (int i = 0; i < monsterSerch.Length; i++)
        {
            monsterPos[i] = monsterSerch[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < monsterSerch.Length; i++)
        {
            monsterPos[i] = monsterSerch[i].transform.position + new Vector3(0, 5, 0);
        }
        if (!isMoveIceSkill)
        {
            StartCoroutine(MonsterMove());
        }
    }

    IEnumerator MonsterMove()
    {
        isMoveIceSkill = true;
        for (int i = 0; i < monsterSerch.Length; i++)
        {
            while (Vector3.Distance(gameObject.transform.position, monsterPos[i]) > 0.1f)
            {
                // MoveTowards�� ��ȯ ���� ��ġ�� ����
                gameObject.transform.position = Vector3.MoveTowards(
                    gameObject.transform.position, monsterPos[i], 30f * Time.deltaTime
                );
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }
        Destroy(gameObject);
        Debug.Log("����");
    }
}
