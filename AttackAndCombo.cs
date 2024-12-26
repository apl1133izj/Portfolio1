using System.Collections;
using System.IO;//�ܺ� ���� �������� json
using System.Linq;
using UnityEngine;
public class AttackAndCombo : MonoBehaviour
{
    private Animator animator;
    public QuestManager questManager;
    private HP playerHp;//�÷��̾� ü�� ������ ����
    public PlayerUI playerUI;
    private PutitemBag putitemBag;
    Skill skill;

    public Vector3 savePos;
    public Vector3 v_DefultPos;
    //�ָ�,������ ���� �� ���� �޺� , ���� ������ ������� �ñر� ��ų����
    private bool attackBool;
    static public int comboAttakccount = 0;
    public int[] attackDamage = { 50, 70, 60, 80 };
    public int[] stackAttackDamage = { 0, 0, 0, 0 };
    public float[] attackSpeed = { 0, 0, 0, 0 };
    private bool kickBool;
    static public int comboKickCount = 0;
    public GameObject attackCollider;//���� ����
    public SphereCollider attackSphereCollider;
    public float attackRange;
    public bool ultimateSkillunlock;
    public bool ultimateSkill;
    public float ultimateSkillCoolTime = 0;
    public float ultimateSkillCoolTimeMax = 20;
    public bool ultimateSkillCoroutin = true;
    //Ÿ�ݰ�
    private float feelinghitting;
    //���� �ӵ� ����,��� �Һ񰨼�
    public float skillattackSpeed = 0;
    public float staminaReductionRate = 0;
    //ȸ�� �G ����,�ݰ�
    public float defend = 1;
    public float stackdefend = 1;
    private bool rollingBool;
    private float rollingCoolTime = 0f;
    private float JumpTime = 0f;
    public bool invincibilityBool;//���� ���� �ƴ���
    public bool block;
    public float blockCoolTime = 0;
    public bool counterattack;
    public GameObject counterAttackGameObject;//counterAttackGameObject�� �´� Prefab�ֱ�
    //���� �G ���� ����
    Rigidbody rb;
    //private float junpAttackCoolTime = 0f;
    private bool jumpBool;
    // private bool junpAttackBool = false;
    public bool hitFlyBackBool;
    public bool cc; //�����̻�
    //�¿�,�� �̵� �ִϸ��̼� ���� Ȯ��
    private bool backWalkadnRunbool;
    private bool rightWalkandRun;
    private bool leftWalkandRun;


    public CharacterController charControl;
    private Vector3 velocity;

    //������ ����
    bool isGround;

    //�� ���� ã�� 
    GameObject darkKnightGameObject;
    Enemy enemy;
    GameObject undedHouse;
    //�⺻ ��Ʈ��
    DemoCharacter demoCharacter;

    //ü�� ����
    Stamina stamina;
    public bool potionDrinkBool;//���� �Դ� �ִϸ��̼�
    Potion potion;
    bool[] postionType = { false, false };

    //��ų ���׷��̵� 
    public SkillTree skillTree;
    //���� �ð�
    public bool InvincibleTime;

    SkillItemFusion skillItemFusion;

    public float hpPostionCoolTime;
    public float staminaPostionCoolTime;
    public GameObject startMenu;

    UndeadKnight undeadKnight;

    //����Ʈ(������� �Ÿ�)
    public GameObject g_CurseSource;
    public BoxCollider box_CurseSourcePotal;
    private void Awake()
    {
        skillItemFusion = GetComponent<SkillItemFusion>();
        rb = GetComponent<Rigidbody>();
        putitemBag = GetComponent<PutitemBag>();
        skill = GetComponent<Skill>();
    }

    void Start()
    {
        Debug.Log("����� �������� �ִ��� Ȯ��");
        gameObject.transform.position = savePos;
        string filePath = DataManager.instance.path + DataManager.instance.nowSlot.ToString();
        if (File.Exists(filePath))
        {
            // �����Ͱ� ������ ������ �ε� �� �ڷ�ƾ ����
            StartCoroutine(SavePosition());
        }
        else
        {
            gameObject.transform.position = v_DefultPos;
        }
        playerHp = GetComponent<HP>();
        charControl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        demoCharacter = GetComponent<DemoCharacter>();
        stamina = GetComponent<Stamina>();
        potion = GetComponent<Potion>();
    }

    void Update()
    {
        if (DataManager.instance.nowPlayer != null)
        {
            savePos = gameObject.transform.localPosition;
            DataManager.instance.nowPlayer.plyerPosition = savePos;
        }
        if (!startMenu.activeSelf)
        {
            if (!questManager.conversationStatus)
            {
                if (!playerUI.uiOpen)
                {
                    IsGround();
                    KeyControll();
                    AnimatorControll();
                    StartCoroutine(Feelinghitting());
                    darkKnightGameObject = GameObject.Find("DarkKnight");
                }
                if (darkKnightGameObject != null)
                {
                    enemy = darkKnightGameObject.GetComponent<Enemy>();
                }
            }
            AttackDamageItem();

            AttackSpeed(skillTree.skillattackSpeed + skill.skillAttackSpeed);


            AttackRange(skillTree.attackRange);
        }
        if (questManager.questBool[3])
        {
            CurseSourceDistence();
        }
        else if (questManager.questBool[4])
        {
            box_CurseSourcePotal.enabled = true;
        }

    }
    IEnumerator SavePosition()
    {
        Debug.Log("�÷��̾� ������ġ");
        charControl.enabled = false;
        yield return null;
        gameObject.transform.localPosition = DataManager.instance.nowPlayer.plyerPosition;
        yield return null;
        charControl.enabled = true;
    }
    void CurseSourceDistence()
    {
        float dis = Vector3.Distance(transform.position, g_CurseSource.transform.position);
        if (dis <= 20)
        {
            questManager.i_questMosterCount[3] = 0;
        }
    }

    void AttackDamageItem()
    {
        attackDamage[0] = 50 + skillItemFusion.attackDamageHap.Take(6).Sum() + stackAttackDamage[0];
        attackDamage[1] = 70 + skillItemFusion.attackDamageHap.Take(6).Sum() + stackAttackDamage[1];
        attackDamage[2] = 60 + skillItemFusion.attackDamageHap.Take(6).Sum() + stackAttackDamage[2];
        attackDamage[3] = 80 + skillItemFusion.attackDamageHap.Take(6).Sum() + stackAttackDamage[3];
    }

    public void AttackSpeed(float attspeed)
    {
        
        attackSpeed[0] = skillItemFusion.attackSpeedHap.Take(6).Sum();
        attackSpeed[1] = skillItemFusion.attackSpeedHap.Take(6).Sum();
        attackSpeed[2] = skillItemFusion.attackSpeedHap.Take(6).Sum();
        attackSpeed[3] = skillItemFusion.attackSpeedHap.Take(6).Sum();

        animator.SetFloat("AttackSpeed1", 1f + ((attspeed + attackSpeed[0]) / 100));
        animator.SetFloat("AttackSpeed2", 1f + ((attspeed + attackSpeed[1]) / 100));
        animator.SetFloat("AttackSpeed3", 1f + ((attspeed + attackSpeed[2]) / 100));
        animator.SetFloat("AttackSpeed4", 1f + ((attspeed + attackSpeed[3]) / 100));
    }
    void AttackRange(float range)
    {
        attackSphereCollider.radius = 0.65f + range;
    }
    void KeyControll()
    {
        //�ָ� ����
        if (Input.GetMouseButtonDown(0) && isGround && !demoCharacter.isInWater) attackBool = true;
        else attackBool = false;


        /*        //������ ����
                if (Input.GetMouseButtonDown(1) && isGround) kickBool = true;
                else kickBool = false;*/

        //������
        if (Input.GetKeyDown(KeyCode.E) && rollingCoolTime > 3f - skillTree.upgradeReduceCoolTime && stamina.stamina >= 100 && !demoCharacter.isInWater)
        {
            rollingBool = true;
            rollingCoolTime = 0;
            invincibilityBool = true;
            Debug.Log("��� �Ҹ�:" + (100f - skillTree.staminaReductionRate));
            stamina.stamina -= 100f - skillTree.staminaReductionRate;
        }
        else
        {
            if (rollingCoolTime <= 3) rollingCoolTime += Time.deltaTime;
        }

        //����
        if (Input.GetKey(KeyCode.Space) && JumpTime > 2f && isGround && !questManager.conversationStatus)
        {
            jumpBool = true;
            JumpTime = 0;
            rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
        else
        {
            jumpBool = false;
            if (JumpTime <= 2) JumpTime += Time.deltaTime;
        }

        //�ڷ� �̵�
        if (Input.GetKey(KeyCode.S))
        {
            backWalkadnRunbool = true;
        }
        else { backWalkadnRunbool = false; }
        //���������� �̵�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rightWalkandRun = true;
        }
        else { rightWalkandRun = false; }
        //�������� �̵�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) { leftWalkandRun = true; }
        else { leftWalkandRun = false; }

        /*        //����
                if (Input.GetKey(KeyCode.Q) && blockCoolTime > 4 - skillTree.upgradeReduceCoolTime && stamina.stamina >= 60)
                {
                    blockCoolTime = 0;
                    DemoCharacter.noneSpeed = true;
                    attackCollider.SetActive(true);
                    attackCollider.gameObject.tag = "Counter";
                    block = true;
                    invincibilityBool = true;
                    stamina.stamina -= 120f - skillTree.staminaReductionRate;
                }
                else
                {
                    attackCollider.gameObject.tag = "HandAttack";
                    if (blockCoolTime <= 4) blockCoolTime += Time.deltaTime;

                }*/
        //���� �Ա�
        if (Input.GetKey(KeyCode.Alpha1) && potion.hpPotion >= 0 &&
            playerHp.hp < playerHp.hpMax && hpPostionCoolTime >= 3) //hp
        {
            if (putitemBag.itemCount[81] > 0)
            {
                ItemType itemType = putitemBag.ItemSlot[81].gameObject.GetComponentInChildren<ItemType>();
                playerHp.hp += 200 + potion.staminaPotion;
                itemType.get_itemcount -= 1;
            }
            hpPostionCoolTime = 0;
            postionType[0] = true;
            potionDrinkBool = true;
        }
        else
        {
            if (hpPostionCoolTime <= 3.1f) hpPostionCoolTime += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Alpha2) && potion.staminaPotion >= 0 &&
            stamina.stamina < stamina.staminaMax && staminaPostionCoolTime >= 3)//stamina
        {
            if (putitemBag.itemCount[82] > 0)
            {
                ItemType itemType = putitemBag.ItemSlot[82].gameObject.GetComponentInChildren<ItemType>();
                itemType.get_itemcount -= 1;
            }
            staminaPostionCoolTime = 0;
            postionType[1] = true;
            potionDrinkBool = true;
        }
        else
        {
            if (staminaPostionCoolTime <= 3.1f) staminaPostionCoolTime += Time.deltaTime;
        }
        //�ñر�
        if (Input.GetKey(KeyCode.R) && ultimateSkillunlock && ultimateSkillCoolTime >= ultimateSkillCoolTimeMax - skillTree.upgradeReduceCoolTime)
        {
            if (ultimateSkillCoroutin)
            {
                ultimateSkillCoolTime = 0;
                DemoCharacter.noneSpeed = true;
                stamina.stamina -= 300f - skillTree.staminaReductionRate;
                StartCoroutine(UltimateSkill());
            }
        }
        else
        {
            if (ultimateSkillCoolTime <= ultimateSkillCoolTimeMax) ultimateSkillCoolTime += Time.deltaTime;
        }
    }

    IEnumerator UltimateSkill()
    {

        invincibilityBool = true;
        ultimateSkillCoroutin = false;
        counterAttackGameObject.gameObject.SetActive(true);
        animator.SetLayerWeight(3, 1f);
        animator.SetTrigger("isR");
        yield return new WaitForSeconds(4);
        DemoCharacter.noneSpeed = false;
        animator.SetLayerWeight(3, 0f);
        counterAttackGameObject.gameObject.SetActive(false);
        ultimateSkillCoroutin = true;
        invincibilityBool = false;
    }
    void IsGround()
    {
        if (demoCharacter.isInWater)
        {
            isGround = true;
        }
        else
        {
            if (charControl.isGrounded)
            {
                isGround = true;
            }
            else { isGround = false; }
        }

    }
    public void invincibilityFalse()
    {
        invincibilityBool = false;
        rollingBool = false;
    }
    void AnimatorControll()
    {
        animator.SetBool("AttackBool", attackBool);                 //�ָԱ⺻ ����
        animator.SetInteger("AttackInt", comboAttakccount);         //�ָ��޺� ����
        animator.SetBool("KickBool", kickBool);                     //�ٸ� ����
        animator.SetInteger("kinkInt", comboKickCount);             //�ٸ��޺� ����
        animator.SetBool("JumpBool", jumpBool);
        // animator.SetBool("JumpAttack", junpAttackBool);          //���� ����;
        animator.SetBool("RollingBool", rollingBool);               //������
        animator.SetBool("BlockBool", block);
        animator.SetBool("PlayerCounterBreathBool", counterattack); //ī���� ����
        animator.SetBool("PotionDrinkBool", potionDrinkBool);       //���� �Ա�

        animator.SetBool("backRun Bool", backWalkadnRunbool);       //�ڷ� �ȱ�
        animator.SetBool("rightRun Bool", rightWalkandRun);         //���������� �ȱ�
        animator.SetBool("leftRun Bool", leftWalkandRun);           //�������� �ȱ�

        animator.SetBool("HitFlyBack Bool", hitFlyBackBool);        //Ư�� ������ �¾����� �ڷ� ���ư��� ��Ʈ �ִϸ��̼�
    }
    IEnumerator Feelinghitting()
    {
        if (counterattack)
        {
            Time.timeScale = 0.4f;
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1;
        }
        if (rollingBool)
        {
            gameObject.layer = 0;
            invincibilityBool = true;
            yield return new WaitForSecondsRealtime(1);
            invincibilityBool = false;
            gameObject.layer = 8;
        }
    }
    public void AttackCollider()
    {
        attackCollider.gameObject.SetActive(true);
    }
    public void CountAttackFire()
    {
        counterAttackGameObject.gameObject.SetActive(true);
    }

    public void DeAttackCollider()
    {
        attackCollider.gameObject.SetActive(false);
    }
    public void Deblock()
    {
        DemoCharacter.noneSpeed = false;
        block = false;
        invincibilityBool = false;
        attackCollider.SetActive(false);
        attackCollider.gameObject.tag = "HandAttack";
    }

    public void Postion()
    {
        if (postionType[0])
        {
            potion.HpPotion();
        }
        else if (postionType[1])
        {
            potion.StaminaPotion();
        }
    }

    public void DeCountAttackFire()
    {
        counterAttackGameObject.gameObject.SetActive(false);

        block = false;
        counterattack = false;
        DemoCharacter.noneSpeed = false;
    }
    public void DePotionDrinkBool()
    {
        potionDrinkBool = false;

        if (postionType[0] && potion.hpPotion != 0)
        {
            potion.hpPotion -= 1;
            postionType[0] = false;
        }
        else if (postionType[1] && potion.staminaPotion != 0)
        {
            potion.staminaPotion -= 1;
            postionType[1] = false;
        }

    }
    public void HitFlyBackBool(int moveInt)
    {

        if (moveInt == 1) cc = true;
        else if (moveInt == 2) { Invoke("DemoCharacterenabled", 0.1f); }
    }
    void DemoCharacterenabled()
    {
        hitFlyBackBool = false;

        Debug.Log("demoCharacter");
        cc = false;
    }



    IEnumerator knoback(int i, GameObject undedHouse) // i: 0 -> ������ ������, 1 -> �ڷ� ������
    {
        // Debug.Log("�˹�:" + knoback(i));
        hitFlyBackBool = true;
        demoCharacter.enabled = false; // ĳ���� ��Ʈ�� ��Ȱ��ȭ
        charControl.enabled = false;

        Vector3 startPosition = transform.position; // ���� ��ġ
        Vector3 targetPosition;

        // ���� �÷��̾� ������ ����
        Vector3 directionToPlayer = (undedHouse.transform.position - transform.position).normalized; // �÷��̾�� �� ������ ���� ����

        // �˹� ���� ����
        Vector3 knockbackDirection;

        // i ���� ���� �˹� ���� ���� (0: ������, 1: �ڷ�)
        if (i == 0) // �չ�����
        {
            knockbackDirection = directionToPlayer; // �÷��̾ ���� ������ (������)
        }
        else if (i == 1) // �޹�����
        {
            knockbackDirection = -directionToPlayer; // �÷��̾� �ݴ� �������� ������ (�ڷ�)
        }
        else
        {
            yield break; // �߸��� �ε����� ��� �ڷ�ƾ ����
        }

        // ��ǥ ��ġ ��� (�Ÿ��� 3���� ���� 1/3 ��ŭ ������)
        float knockbackDistance = 60 / 3f;
        targetPosition = startPosition + knockbackDirection * knockbackDistance;

        float elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
        float duration = 0.35f; // �̵� �ð� ����

        // Lerp�� ����Ͽ� 0.35�� ���� �ε巴�� �̵�
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        transform.position = targetPosition; // ���� ��ġ ����
        demoCharacter.enabled = true; // ĳ���� ��Ʈ�� Ȱ��ȭ
        charControl.enabled = true;
        hitFlyBackBool = false; // �˹� ���� ����
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Flame"))
        {
            playerHp.hp -= (20 - (defend + stackdefend));
        }
        else if (other.gameObject.CompareTag("Flame2"))
        {
            playerHp.hp -= (1 - (defend + stackdefend));
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            playerHp.hp -= 200;
        }
        if (!InvincibleTime)
        {
            if (other.gameObject.CompareTag("Luncher") && !invincibilityBool)
            {
                playerHp.hp -= (200 - (defend + stackdefend));
                Destroy(other.gameObject);
            }
            for (int i = 21; i < 27; i++)
            {
                if (other.gameObject.layer == i && !invincibilityBool)
                {
                    //������ ��� ��Ÿ���� �� ��ų�� ���� ������(������ ���� ȿ���� ���� ����)
                    switch (other.gameObject.layer)
                    {
                        case 21:
                            Debug.Log("skill1");
                            playerHp.hp -= 10;
                            break;
                        case 22:
                            Debug.Log("skill2");
                            playerHp.hp -= 150;
                            break;
                        case 23:
                            Debug.Log("skill3");
                            playerHp.hp -= 150;
                            break;
                        case 24:
                            Debug.Log("skill4");
                            playerHp.hp -= 100;
                            break;
                        case 25:
                            Debug.Log("skill5");
                            playerHp.hp -= 50;
                            break;
                        case 26:
                            Debug.Log("skill6");
                            playerHp.hp -= 100;
                            break;
                    }
                }
            }

        }
    }
    float DefendItem()
    {
        return defend + skillItemFusion.defenseHap.Take(6).Sum();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!playerHp.isDie)
        {
            if (other.gameObject.CompareTag("Enemy") && !invincibilityBool)
            {
                Debug.Log("������ ����");
                if (enemy.attackcout == 5 || enemy.attackcout == 9)
                {
                    playerHp.hp -= enemy.attackDamage - (DefendItem() + stackdefend);
                    hitFlyBackBool = true;
                    DemoCharacter.noneSpeed = true;
                }
                else
                {
                    playerHp.hp -= enemy.attackDamage - (DefendItem() + stackdefend);
                }
            }
            if (!invincibilityBool)
            {
                if (other.gameObject.CompareTag("Enemy") && invincibilityBool)
                {
                    demoCharacter.attackSound.PlayOneShot(demoCharacter.attackClip[11], 1.0f);
                    counterattack = true;
                    playerHp.hp -= 150 - (DefendItem() + stackdefend);
                }

                if (other.gameObject.layer == 10 && !invincibilityBool)
                {
                    Debug.Log("�˹�");
                    hitFlyBackBool = true;
                    other.gameObject.layer = 0;
                    playerHp.hp -= 150 - (DefendItem() + stackdefend);
                    //gameObject.transform.position += Vector3.forward * 50;
                }

                if (other.gameObject.layer == 11 && !invincibilityBool)
                {
                    playerHp.hp -= 250 - (DefendItem() + stackdefend);
                    StartCoroutine(knoback(0, other.gameObject));
                }
                if (other.gameObject.layer == 12 && !invincibilityBool)
                {
                    playerHp.hp -= 100 - (DefendItem() + stackdefend);
                    StartCoroutine((knoback(1, other.gameObject)));
                }
                //������
                if (other.gameObject.CompareTag("BoneDragon") && !invincibilityBool)
                {
                    playerHp.hp -= 50 - (DefendItem() + stackdefend); 
                    StartCoroutine(Feelinghitting());
                }
                if (other.gameObject.CompareTag("Desh"))
                {
                    hitFlyBackBool = true;
                    StartCoroutine((knoback(1, other.gameObject)));
                    playerHp.hp -= 200 - (DefendItem() + stackdefend);
                }

                //������ ���
                if (other.gameObject.CompareTag("Arrow") && !invincibilityBool)
                {
                    Debug.Log("ȭ�� ����");
                    GameObject undead = GameObject.Find("Undead Horse");
                    UndedHouse undedHouse = undead.GetComponent<UndedHouse>();
                    undedHouse.f_undedHouseHp += 500;
                    undedHouse.f_MaxundedHouseHp = undedHouse.f_undedHouseHp;
                    playerHp.hp -= 100 - (DefendItem() + stackdefend);
                    Destroy(other.gameObject);
                }
                if (other.gameObject.CompareTag("Ax") && !invincibilityBool)
                {
                    GameObject undead = GameObject.Find("Undead Horse");
                    UndedHouse undedHouse = undead.GetComponent<UndedHouse>();
                    undedHouse.f_undedHouseHp += 500;
                    undedHouse.f_MaxundedHouseHp = undedHouse.f_undedHouseHp;
                    playerHp.hp -= 100 - (DefendItem() + stackdefend);
                    StartCoroutine((knoback(1, undead)));
                }

                if (other.gameObject.layer==30 && !invincibilityBool)
                {

                    GameObject undead = GameObject.Find("Undead Horse");
                    UndedHouse undedHouse = undead.GetComponent<UndedHouse>();
                    undedHouse.f_undedHouseHp += 500;
                    undedHouse.f_MaxundedHouseHp = undedHouse.f_undedHouseHp;
                    StartCoroutine((knoback(1, undead)));
                    playerHp.hp -= 40 - (DefendItem() + stackdefend);
                }
                //����� ����
                if (other.gameObject.CompareTag("MonsterAttack") && !invincibilityBool)
                {
                    playerHp.hp -= 10 - (DefendItem() + stackdefend);
                }
            }
        }
    }
}
