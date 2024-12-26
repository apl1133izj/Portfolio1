using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Moster : MonoBehaviour
{
    //ü��
    [SerializeField]
    private float hp = 1200;

    public Material hide_Alpha;                //���� ���� ���� ������ ��Ƽ����
    public Image[] hide_Hp;                    //���� ü�¹� ���� ���� ������ ��������Ʈ
    private Color originalColor;
    private GameObject target;                 //�÷��̾� ������Ʈ
    public GameObject rotationEat;             //�Դ¾ִϸ��̼� ����� ȸ���Ǵ� ���ӿ�����Ʈ
    public float maxDistance = 10f;            // �ִ� �Ÿ� (�� �Ÿ� �̻󿡼��� ������ ��������)
    public float f_RediscoveringMaxDistance;   //��Ž�� �� �� �� �ִ� ��Ÿ��� �þ (����: MonsterAgents Ʈ���ſ� �ڸ�ƾ
    public Animator animator;
    public Animator eatAnimator;
    //�÷��̾� ��ȣ�ۿ� ���� �޶�ٱ�,�����̻�
    [SerializeField]
    public bool startSerch = false;                  //���Ͱ� ������ �÷��̾ ã���� ���� ��������
    public bool stayRush;                     //�������ΰ�쿡�� ��� ������ �����
    public bool b_wall;                              //�������� ���� �ε�ĥ��� ������ ����
    public bool b_SmellSearchComplete;        //������ �÷��̾�� ���� ������쿡 ���Ͱ� ������
    public bool b_cling;                      //�÷��̾�� �޶� �ٱ����� ���� �� �ߴ���(MonsterAgents���� �����:�÷��̾ serch�� �ɷ������(�ݶ��̴��� �ε�ģ ���))
    public bool b_catch;                      //�÷��̾ �޶� �پ�����
    public bool isSearching = false;          // �ڷ�ƾ ���� ���θ� �����ϴ� ����
    public int i_escape;
    public float coolTime = 0f;
    public GameObject g_SmellSerch;
    public bool b_Freeze;
    public float f_FreezeTime;
    public GameObject g_FreezePaticle;
    [Range(-10f, 10f)]
    public float y;
    [Range(-10f, 10f)]
    public float z;
    //ī�޶� ����
    public Camera defaultCamera;             //���Ͱ� �޶� �ٱ� ��
    public Camera clingAttackCamera;         //��
    public GameObject tip;
    PlayerUI playerUI;
    bool isCatching = false;                 // �ڷ�ƾ ���� ���¸� Ȯ���ϴ� �÷��� ����
    AttackAndCombo player;
    MonsterAgents monsterAgents;
    public QuestManager questManager;
    HP playerHp;
    public GameObject[] die_SetActive;
    float f_ResurrectionTime = 0f;
    public GameObject[] g_ClearItem;
    public AudioClip runSound;
    public AudioClip damageSound;
    public Skill skill;
    public Shild shild;
    void Start()
    {
        StartCoroutine(SmellSerchInst());
        originalColor = hide_Alpha.color;
        target = GameObject.Find("Player");
        player = target.GetComponent<AttackAndCombo>();
        playerHp = target.GetComponent<HP>();
        tip = GameObject.Find("Player UI");
        playerUI = tip.GetComponent<PlayerUI>();
        monsterAgents = GetComponentInChildren<MonsterAgents>();
        f_RediscoveringMaxDistance = 15f;
        //questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        //animator.Play("Creep_Idle1_Action");
    }

    private void FixedUpdate()
    {
        if (!b_Freeze)
        {
            if (ClingCooltime() && b_SmellSearchComplete)//��Ÿ��
            {
                if (Distance() >= 5)
                {
                    StartCoroutine(ClingAttack());
                    animator.SetFloat("animationspeed", 1f);
                }
                else
                {
                    animator.SetFloat("animationspeed", 3f);
                }
            }
            LostPlayer();

        }
        else
        {
            f_FreezeTime += Time.deltaTime;
            monsterAgents.agent.speed = 0f;
            g_FreezePaticle.gameObject.SetActive(true);
            //��ų�� ���׷��̵� �Ǳ������� 2�� + ��ȭ �ɶ����� 1�ʾ�����
            if (f_FreezeTime >= 2 + skill.skillEnhanceCount[3])
            {
                g_FreezePaticle.gameObject.SetActive(false);
                monsterAgents.agent.speed = 3f;
                f_FreezeTime = 0;
                b_Freeze = false;
            }
        }
        if (Die())//�������
        {
            //monsterAgents.agent.isStopped = true;
            //����Ʈ ���� ���� Ƚ�� ����
            eatAnimator.SetTrigger("DieTrigger");
            animator.SetTrigger("DieTrigger");
            DieMonster();
        }

        /*        //�÷��̾ ����(��ȣ�ۿ� ����)
                if (b_catch)
                {
                    StartCoroutine(Catch());
                }*/

    }
    void DieMonster()
    {
        die_SetActive[0].SetActive(false);
        die_SetActive[1].SetActive(false);
        die_SetActive[2].SetActive(false);
        hide_Hp[0].enabled = false;
        f_ResurrectionTime += Time.deltaTime;
        monsterAgents.agent.speed = 0;
        if (f_ResurrectionTime >= 60)
        {
            monsterAgents.agent.speed = 3;
            f_ResurrectionTime = 0;
            Resurrection();
        }
    }
    void Resurrection()
    {
        hide_Hp[0].enabled = true;
        die_SetActive[0].SetActive(true);
        die_SetActive[1].SetActive(true);
        die_SetActive[2].SetActive(true);
        hp = 1200;
        monsterAgents.agent.isStopped = false;
    }
    bool Die() //�׾����� Ȯ��
    {
        if (hp < 0f)
        {

            if (die_SetActive[0].activeSelf || die_SetActive[1].activeSelf || die_SetActive[2].activeSelf)
            {
                questManager.i_questMosterCount[questManager.questProgress]--;
                ItemInst();
            }
            return true;
        }
        else
        {
            Hp();

            return false;
        }
    }
    public void ItemInst()
    {
        int rand = Random.Range(0, 3);
        GameObject item1 = Instantiate(g_ClearItem[rand], gameObject.transform.position + new Vector3(1, 2, 0), Quaternion.identity);
        item1.name = g_ClearItem[0].name;
        Vector3 randomForce1 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item1.GetComponent<Rigidbody>().AddForce(randomForce1);
    }
    void Hp()
    {
        hide_Hp[0].fillAmount = hp / 1200;
    }
    void LostPlayer()//���Ͱ� �÷��̾ ���� ��Ÿ����� ��ħ
    {
        //ó���� �÷��̾ ������ ã�ƾ� �ϱ⶧���� �Ÿ��� ������� Ž��
        //�׷��� ������ startSerch�� true�� �Ǳ��������� ���ڵ尡 ����Ǹ� �ȵ�
        //���Ͱ� ������ startSerch �⺻ ���� false
        if (startSerch)
        {
            //�÷��̿��� �Ÿ��� 15�̻��� ��� ������ �÷��̾� ��Ž��
            //������ ��Ž���� �ٽ� �÷��̾ ã���� ��� �Ͻ������� ��Ÿ��� 30���� �þ
            if (Distance() >= f_RediscoveringMaxDistance)
            {
                if (!isSearching && !Die()) StartCoroutine(SmellSerchInst());
                animator.SetBool("SerchBool", true);

                monsterAgents.agent.isStopped = true;//���� ����
                b_SmellSearchComplete = false;//���� ã�� ����
            }
            else
            {
                animator.SetBool("SerchBool", false);
                monsterAgents.agent.isStopped = false;//���� �̵�
                b_SmellSearchComplete = true;//�� ã�� ����
            }
        }
    }
    bool ClingCooltime()//���� ��Ÿ��
    {
        if (isSearching) return false; // Ž�� ���� ���� ��Ÿ�� ����

        coolTime += Time.deltaTime;

        if (coolTime >= 12f)
        {
            coolTime = 0f; // ��Ÿ�� �ʱ�ȭ
            Debug.Log("ClingCooltime �Ϸ�!"); // ����� �α�
            return true;
        }

        //Debug.Log($"ClingCooltime ���� ��: {coolTime:F2}s"); // ���� �ð� �α�
        return false;
    }
    float AlphaValueAccordingToDistance() // �Ÿ� ��� ���� �� ���
    {
        // �Ÿ��� �ּ��� ����������, ����������� �������������� 1���� 0���� ���� ���� ����
        float alpha = 1 - Mathf.Clamp01(Distance() / maxDistance);
        return alpha;
    }
    float Distance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //Debug.Log(distance);
        return distance;
    }
    IEnumerator SmellSerchInst()//�÷��̾� ã��
    {
        if (isSearching) yield break; // �̹� Ž�� ���̸� ����
        isSearching = true;

        // Ž�� ������Ʈ ����
        GameObject instSerch = Instantiate(g_SmellSerch, transform.position, Quaternion.identity);
        float serchTime = 0;

        // Ž�� ���� ����
        while (instSerch != null)
        {
            yield return new WaitForSeconds(1); // �ʴ� 1ȸ üũ
            serchTime += 1;

            if (serchTime >= 18)
            {
                // 18�� ���� �÷��̾ ã�� ���� ���
                Debug.Log("18�� ���� �÷��̾ ã�� ���߽��ϴ�. ��Ž�� ��...");
                Destroy(instSerch);
                instSerch = null;
            }
        }

        // Ž�� ���� �� ���� �ʱ�ȭ
        isSearching = false;
        Debug.Log("Ž�� ����");
    }
    IEnumerator ClingAttack() // �÷��̾� ��ġ�� ����
    {
        Debug.Log("ClingAttack ������");

        b_cling = false;
        stayRush = true;
        b_wall = false;
        // ���İ� ����
        Color colorWithUpdatedAlpha = originalColor;
        colorWithUpdatedAlpha.a = 1f;
        hide_Alpha.color = colorWithUpdatedAlpha;
        SoundManager.PlaySound(runSound);
        // ��ǥ ��ġ ����
        float rushTime = 0f;
        // jumpTime�� 2�� ������ ���� �ݺ�
        while (rushTime < 2 && !b_wall)
        {

            rushTime += Time.deltaTime;
            monsterAgents.agent.speed = 12;
            animator.SetFloat("animationspeed", 4);
            colorWithUpdatedAlpha = originalColor;
            colorWithUpdatedAlpha.a = 1f;
            hide_Alpha.color = colorWithUpdatedAlpha;

            eatAnimator.SetBool("RushBool", true);
            animator.SetBool("RushBool", true);

            yield return null;
        }
        animator.SetFloat("animationspeed", 1);
        monsterAgents.agent.speed = 3;
        // ���� ���� �� �ʱ�ȭ
        eatAnimator.SetBool("RushBool", false);
        animator.SetBool("RushBool", false);
        startSerch = true;
        stayRush = false;
    }
    IEnumerator Catch()
    {
        // �̹� ���� ���� ��� �ߺ� ���� ����
        if (isCatching) yield break;

        isCatching = true;

        if (!playerUI.b_tip)
        {
            playerUI.g_tipUI.SetActive(true);
        }

        while (i_escape <= 10)
        {
            eatAnimator.SetTrigger("EatTrigger");
            b_catch = false;
            Vector3 playerPosition = target.transform.position + new Vector3(0, y, z);
            transform.position = playerPosition;
            rotationEat.transform.rotation = Quaternion.Euler(-48f, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            clingAttackCamera.gameObject.SetActive(true);
            defaultCamera.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� �Է� üũ
            {
                i_escape += 1;
                if (!playerUI.b_tip)
                {
                    playerUI.spaceCount.text = i_escape.ToString();
                }
                if (i_escape >= 10)
                {
                    if (!playerUI.b_tip)
                    {
                        playerUI.g_tipUI.SetActive(false);
                    }
                    Debug.Log("���");
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0);
                    //rb.useGravity = true;//�߷� Ȱ��ȭ
                    animator.SetTrigger("JumpTrigger");
                    StartCoroutine(Escape());//���� �ʱ�ȭ
                    isCatching = false; // �ڷ�ƾ ���� ���·� ����
                    yield break;
                }
                yield return new WaitForSeconds(0.1f); // �Է� �� 0.1�� ���
            }

            yield return null;
        }

        playerUI.b_tip = true;
        isCatching = false; // �ڷ�ƾ ���� ���·� ����
    }
    IEnumerator Escape()
    {
        Debug.Log("Escape");
        clingAttackCamera.gameObject.SetActive(false);
        defaultCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        i_escape = 0;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("R"))
        {
            hp -= 5;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandAttack"))
        {
            SoundManager.PlaySound(damageSound);
            Debug.Log("Combo Attack Count: " + AttackAndCombo.comboAttakccount);
            switch (AttackAndCombo.comboAttakccount)
            {
                case 0:
                    Debug.Log("����1");
                    hp -= player.attackDamage[0];
                    break;
                case 1:
                    Debug.Log("����2");
                    hp -= player.attackDamage[1];
                    break;
                case 2:
                    Debug.Log("����3");
                    hp -= player.attackDamage[2];
                    break;
                case 3:
                    Debug.Log("����4");
                    hp -= player.attackDamage[3];
                    break;
            }
        }
        if (other.gameObject.CompareTag("ShildAttack"))
        {
            hp -= shild.damageHap;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<AttackAndCombo>();
            //�������ΰ�쿡�� ��ȣ�ۿ������ Ȱ��ȭ
            if (i_escape < 10 && stayRush && !player.invincibilityBool)
            {
                b_catch = true;
            }
            b_wall = true;//���̳� �÷��̾ �ε�ģ��� ���� �ڸ�ƾ���� ���ư��� �ִ� while���� ���ᰡ ��
        }
        if (other.gameObject.layer == 13 && stayRush)
        {
            b_wall = true;//���̳� �÷��̾ �ε�ģ��� ���� �ڸ�ƾ���� ���ư��� �ִ� while���� ���ᰡ ��
        }
        if (other.gameObject.CompareTag("freeze"))
        {
            b_Freeze = true;
        }
    }
}
