using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class MonsterAgents : MonoBehaviour
{
    public enum aiType { moster, serch }
    public aiType type;
    public Transform t_StartPos;
    public enum MonsterType { nul, Creep2, Creep3, Creep4, Demon_damaged };
    public MonsterType monsterType;
    public NavMeshAgent agent;
    public float resurrectionTime = 0;
    //ó�� ����
    public bool startserchs = true;

    // ������ ����
    GameObject player;
    public Transform target;
    public GameObject p_serchPaticle;//��ƼŬ
    public GameObject g_smell;
    public Moster monster;
    RangedMonster rangedMonster;
    public GameObject demo;
    bool b_attackCoroutine = false;
    bool b_Arrival; //���� ����
    bool b_RediscoveringMaxDistanceCoroutine;
    //��Ȱ ���� ����
    private bool isDead = false; // ���Ͱ� �׾����� ����
    public bool isResurrecting = false; // ��Ȱ ������ ����
    public float f_ResurrectingFloat = 0;
    public float f_MaxResurrectingFloat = 60;
    public GameObject[] g_ClearItem;

    void Start()
    {
        // NavMeshAgent�� �Ҵ���� ���� ��� �ڵ����� �Ҵ�
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is not attached to this GameObject.");
            }
        }
        // target�� null�� ���, Player ������Ʈ�� ã�� �ڵ����� �Ҵ�
        // Ensure target is initialized
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player"); // Find the player by its tag
            if (player != null)
            {
                target = player.transform; // Assign the player's transform to target
                Debug.Log("Target assigned automatically to player.");
            }
        }
        if (type == aiType.moster && monsterType != MonsterType.Demon_damaged)
        {
            monster = GetComponent<Moster>();
        }
        else
        {
            rangedMonster = GetComponentInChildren<RangedMonster>();
        }

    }
    void Update()
    {

        if (type == aiType.moster)
        {
            if (monsterType != MonsterType.Demon_damaged)
            {
                if (!monster.b_Freeze)
                {
                    if (monster != null && monster.b_SmellSearchComplete)
                    {
                        MoveAI();
                    }

                }
            }
            else
            {
                if (monsterType == MonsterType.Demon_damaged)
                {
                    if (!rangedMonster.b_Freeze)
                    {
                        if (!rangedMonster.ballAttack)
                        {
                            MoveAI();
                        }
                    }
                }
            }
        }
        else
        {
            Destroy(gameObject, 15);
        }
        if (b_RediscoveringMaxDistanceCoroutine)
        {
            b_RediscoveringMaxDistanceCoroutine = false; // �ڷ�ƾ�� �ߺ� ������� �ʵ��� �ٷ� false�� ����
            StartCoroutine(RediscoveringMaxDistance());
        }
        if (monsterType == MonsterType.Demon_damaged)
        {
            Resurrecting();
        }

    }

    void Resurrecting()
    {
        if (rangedMonster.f_Hp > 0)
        {
            demo.gameObject.SetActive(true);
            if (isResurrecting)
            {
                StartCoroutine(ResurrectingCoroutin());
            }
        }
        else
        {
            f_ResurrectingFloat += Time.deltaTime;
            if (!isDead)
            {
                StartCoroutine(Die());
            }
            if (f_ResurrectingFloat >= f_MaxResurrectingFloat)
            {
                isDead = false;
                rangedMonster.f_Hp = 700;
                f_ResurrectingFloat = 0;
            }
        }
    }
    IEnumerator Die()
    {
        isDead = true;
        rangedMonster.animator.SetTrigger("DieTrigger");
        isResurrecting = true;
        yield return new WaitForSeconds(3);
        ItemInst();
        demo.gameObject.SetActive(false);
    }
    public void ItemInst()
    {
        int rand = Random.Range(0, 3);
        GameObject item1 = Instantiate(g_ClearItem[rand], gameObject.transform.position + new Vector3(1, 2, 0), Quaternion.identity);
        item1.name = g_ClearItem[0].name;
        Vector3 randomForce1 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item1.GetComponent<Rigidbody>().AddForce(randomForce1);
    }
    IEnumerator ResurrectingCoroutin()
    {
        isResurrecting = false;
        agent.enabled = true; // NavMeshAgent ��Ȱ��ȭ
        agent.Warp(transform.position); // ���� ��ġ�� �̵� �ʱ�ȭ
        rangedMonster.ui.gameObject.SetActive(true); // UI Ȱ��ȭ
        rangedMonster.animator.SetLayerWeight(3, 1f); // ���̾� �ʱ�ȭ
        rangedMonster.ballAttack = false;
        rangedMonster.attackCoroutin = false;
        rangedMonster.launchAttackCoroutin = false;
        rangedMonster.b_isAttack = false;
        //rangedMonster.animator.SetTrigger("isResurrectingTrigger");
        yield return new WaitForSeconds(3);
        rangedMonster.animator.SetLayerWeight(0, 1f); // ���̾� �ʱ�ȭ
        rangedMonster.animator.Rebind(); // Animator ���� �ʱ�ȭ
    }
    float Distence()
    {
        // target�� null���� Ȯ��
        if (target == null)
        {
            return float.MaxValue; // �⺻�� ��ȯ
        }
        return Vector3.Distance(transform.position, target.position);
    }
    public IEnumerator SerchSmell()//�÷��̾� ���� �˻�(���Ͱ� �÷��̾��� ������ ã���ϴ�)
    {
        float serchTime = 0;// Ž���ð�
        float paticleTime = 0;
        bool searchComplete = false;//Ž�� �ð��� �� �Ǿ�����
        GameObject paticle = null;
        //g_smell.SetActive(true);

        Debug.Log(type + "�� �̵���");
        if (gameObject != null)
        {
            while (Distence() > 1 && !searchComplete)//�÷��̾���� �Ÿ��� 0�� �ɶ����� �÷��̾ ã�� �÷��̾ ã�������� �ݺ�
            {

                serchTime += Time.deltaTime;
                paticleTime += Time.deltaTime;
                if (paticle != null)//��ƼŬ�� �ִ� ��� ����
                {
                    Destroy(paticle, 1.25f);
                }
                if (paticleTime >= 1f)//0.25�ʿ� �ѹ��� ��ƼŬ ����
                {
                    paticle = Instantiate(p_serchPaticle, transform.position, Quaternion.identity);

                    paticleTime = 0;
                }
                if (serchTime > 13)
                {
                    searchComplete = true;//Ž���ð� ����
                }
                MoveAI();//ai�̵�
                yield return null;
            }

        }
        else
        {
            yield break;
        }
        yield return new WaitForSeconds(4);
        //g_smell.SetActive(false);
        // transform.position = monster.transform.position;//������ �ٽ� ���Ϳ��� �̵�
        /*if(monster.b_SmellSearchComplete) */
        StartCoroutine(SerchSmell());//�÷��̾ ������ �����ưų� ã�� ������ ��쿡�� ��Ž��
    }
    //���� �̵�
    public void MoveAI()
    {
        if (agent == null)
        {
            return; // Prevent any further operations if agent is not assigned
        }
        if (monsterType == MonsterType.Demon_damaged)
        {
            if (Distence() <= 35)
            {
                RangedMonster.lookat = true;
                agent.stoppingDistance = 25;
                if (target != null && !b_Arrival)
                {
                    // ���� �� ������ ����
                    agent.SetDestination(target.position);
                    if (!b_attackCoroutine && monsterType != MonsterType.Demon_damaged)
                    {
                        if (monster.b_SmellSearchComplete)
                        {
                            monster.animator.SetBool("SerchBool", false);
                            monster.animator.SetBool("WalkBool", true);
                        }
                        else
                        {
                            monster.animator.SetBool("WalkBool", false);
                            monster.animator.SetBool("SerchBool", true);
                        }
                    }
                    else
                    {
                        if (agent.isOnOffMeshLink)
                        {
                            rangedMonster.animator.SetBool("Run Bool", false);
                        }
                        else
                        {
                            rangedMonster.animator.SetBool("Run Bool", true);
                        }

                    }
                }

            }
            else
            {
                RangedMonster.lookat = false;
                agent.stoppingDistance = 0;
                agent.SetDestination(t_StartPos.position);
            }
        }
        else
        {
            if (target != null && !b_Arrival)
            {
                // ���� �� ������ ����
                agent.SetDestination(target.position);
                if (type == aiType.moster)
                {
                    if (!b_attackCoroutine && monsterType != MonsterType.Demon_damaged)
                    {
                        if (monster.b_SmellSearchComplete)
                        {
                            monster.animator.SetBool("SerchBool", false);
                            monster.animator.SetBool("WalkBool", true);
                        }
                        else
                        {
                            monster.animator.SetBool("WalkBool", false);
                            monster.animator.SetBool("SerchBool", true);
                        }
                    }
                    else
                    {
                        if (agent.isOnOffMeshLink)
                        {
                            rangedMonster.animator.SetBool("Run Bool", false);
                        }
                        else
                        {
                            rangedMonster.animator.SetBool("Run Bool", true);
                        }

                    }
                }
            }
        }

        // ������������ �Ÿ��� üũ�Ͽ� �������� �� �ൿ ����
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.isOnOffMeshLink)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                // �������� ������ ����� ����
                if (monsterType != MonsterType.Demon_damaged)
                {
                    OnDestinationReached();
                }
                else//MonsterType.Demon_damaged �ΰ��
                {
                    if (!rangedMonster.b_isAttack && Distence() <= 35) rangedMonster.RandomAttack();
                    rangedMonster.animator.SetBool("Run Bool", false);
                }

            }
        }
    }
    private void OnDestinationReached()
    {
        // ������ ���� �� ȣ��Ǵ� �޼���
        if (type == aiType.moster)
        {

            if (!b_attackCoroutine) StartCoroutine(Attack(4f));
        }
    }
    IEnumerator Attack(float attackTime)
    {
        b_attackCoroutine = true;
        b_Arrival = true;
        monster.animator.SetBool("WalkBool", false);
        monster.animator.SetTrigger("AttackTrriger");
        monster.eatAnimator.SetTrigger("AttackTrriger");
        yield return new WaitForSeconds(attackTime);
        b_Arrival = false;
        b_attackCoroutine = false;
    }
    // �� �������� �����ϴ� �޼���
    public void SetNewDestination(Vector3 newDestination)
    {
        agent.ResetPath();
        agent.SetDestination(newDestination);
    }
    IEnumerator RediscoveringMaxDistance()
    {
        monster.f_RediscoveringMaxDistance = 30;
        Debug.Log("�Ÿ� 30���� ����");
        yield return new WaitForSeconds(2);
        monster.f_RediscoveringMaxDistance = 15;
        Debug.Log("�Ÿ� 15���� �ʱ�ȭ");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (type == aiType.serch)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                target = other.transform; // �浹 �� target ����

                Debug.Log($"Collision detected with {other.gameObject.name}");
                Destroy(gameObject);
                if (monster != null)
                {
                    monster.isSearching = false;
                    monster.startSerch = true;
                    monster.b_SmellSearchComplete = true;
                }
            }
        }

        if (other.gameObject.CompareTag("Monster") || other.gameObject.name == "Creep")
        {
            Debug.Log("��ġ�� ���Ϳ� �ε�ħ");
            startserchs = false;
            monster = other.GetComponent<Moster>();
            if (monster == null)
            {
                Debug.LogError($"Monster component not found on {other.gameObject.name}");
                return;
            }

            if (type == aiType.serch && !monster.b_SmellSearchComplete)
            {
                Destroy(gameObject, 15);
                StartCoroutine(SerchSmell());
            }
        }
    }
}
