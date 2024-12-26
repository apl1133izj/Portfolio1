using System.Collections.Generic;
using UnityEngine;

public class GravitySkill : MonoBehaviour
{
    public GameObject[] monsterGravity;
    void Start()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (monsters.Length == 0) // 예외 처리
        {
            return;
        }

        // 임시 리스트를 사용해 거리 조건에 맞는 몬스터만 저장
        List<GameObject> validMonsters = new List<GameObject>();

        foreach (GameObject monster in monsters)
        {
            if (monster == null) continue; // 예외 처리

            float disMonster = Vector3.Distance(gameObject.transform.position, monster.transform.position);
            if (disMonster <= 50) // 거리 조건
            {
                validMonsters.Add(monster);
            }
        }

        // 거리 조건에 맞는 몬스터들로 배열 초기화
        monsterGravity = validMonsters.ToArray();

    }

    // Update is called once per frame
    void Update()
    { 
        // 몬스터 위치 저장
        for (int i = 0; i < monsterGravity.Length; i++)
        {
            monsterGravity[i].transform.position = Vector3.MoveTowards(gameObject.transform.position, monsterGravity[i].transform.position, 10);
        }
    }
}
