using System.Collections;
using UnityEngine;

public class SpawEnemy : MonoBehaviour
{

    /*
    ��� ����  Goblin Warrior
    ����� ���� Dark Wolf
    ���̷��� �ü� Skeleton Archer
    ��Ÿ�� ���� Burning Zombie
    ��ħ ���� Poison Stinger Scorpion
    �̳�Ÿ��ν� Minotaur
    ���� �Ź� Frost Spider
    ���� ��� Wraith Knight
    ���� �� Stone Golem
    �͵� ������ Venomous Slime
    ���ֹ��� �������� Cursed Treant
    ���� ���� Bloodstain Bat
    �ͼ� ����  Savage Harpy
    �׸��� ���� Savage Harpy
    �߼� �Ź� Beast Spider
    ��� ���� Lava Elemental
    ���� ���� Ice Giant
    �������� ����� Mutant Ogre
    ����ϴ� ���� Wandering Zombie
    ���� ���ܺ� Undead Legionnaire
    */



    public enum enemyType { Zombi, DarkWolf, SkeletonArcher };//
    public enemyType enemySpawType;
    public GameObject[] enemySpawIns;//��ȯ ������ ���� ����
    public int enemyMaxSpaw;  //�ʿ� �ִ�� ��ȯ ������ ������ ��
    private bool isSpawning;
    [SerializeField]
    float spawTimeDelay;
    float spawPos;
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemySpawType.ToString());
        if (enemies.Length <= enemyMaxSpaw && !isSpawning)
        {
            StartCoroutine(EnemySpawCoroutine(spawTimeDelay));
        }
    }

    IEnumerator EnemySpawCoroutine(float spawTime)
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawTime);
        EnemySpawIns();
        isSpawning = false;
    }
    void EnemySpawIns()
    {
        Instantiate(enemySpawIns[(int)enemySpawType], new Vector3(transform.position.x + spawPos,
                    transform.position.y, transform.position.z + spawPos), Quaternion.identity);
        spawPos = Random.Range(3, 5f);
    }
}
