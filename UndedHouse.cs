using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UndedHouse : MonoBehaviour
{
    [Range(0f, 40000f)]
    public float f_undedHouseHp = 40000f;
    public float f_MaxundedHouseHp = 40000f;
    public GameObject ui;
    public TextMeshProUGUI undedHouseHpUI;
    public TextMeshProUGUI undedMaxHouseHpUI;
    public Image hpbarImage;

    Animator animator;
    GameObject player;
    DemoCharacter character;
    UndeadKnight undeadKnight;
    AttackAndCombo andCombo;


    //"Idel0","Wark1","Run2","BackWark-1"}
    public bool b_speed, b_Direction, b_Attack, b_Jump;
    [Range(-1.0f, 2.0f)]
    public float f_speed, f_speedTime;
    [Range(-3.0f, 3.0f)]
    public float f_Direction;
    public float f_Attack, f_AttackCoolTime;
    //private bool canAttack = true; // ���� ���� ����
    //float attackCooldown = 2.5f; // ��Ÿ�� �ð� (2.5��)
    public GameObject[] g_hitBox; //0: �� 1:��
    public bool b_straight = false;
    bool b_Rotation = false;
    //���� ���� 
    public BoxCollider runHitBox;//�¾Ҵ���

    //���� ���
    public PlayerUI playerUI;
    public GameObject[] g_ClearItem;
    public GameObject boseClearPotal;
    public QuestManager questManager;

    public AudioClip[] stepClip;//0:�� �����ٸ� 1:�� �޴ٸ� 2:�� ���� �ٸ� 3:�� �޴ٸ� 
    public AudioClip damageClip;

    //�÷��̾ ���� ���� �̻�
    public bool b_Freeze;
    public float f_FreezeTime;
    public GameObject g_FreezePaticle;
    public Skill skill;
    public Shild shild;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        undeadKnight = GetComponentInChildren<UndeadKnight>();
    }
    void Start()
    {
        player = GameObject.Find("Player");
        character = player.GetComponent<DemoCharacter>();
        andCombo = player.GetComponent<AttackAndCombo>();
        StartCoroutine(MoveToPosition(player.transform.position));

    }
    void Update()
    {
        undedHouseHpUI.text = f_undedHouseHp.ToString() + "HP";
        undedMaxHouseHpUI.text = f_MaxundedHouseHp.ToString() + "HP";
        ThisHp();
        Die();

        if (!b_Freeze)
        {
            AnimatorControll();
            if (f_speed >= 0.2f)
            {
                f_speedTime += Time.deltaTime;
                if (f_speedTime >= 15)
                {
                    f_speedTime = 0;
                    Jump();
                }
            }
            if (Dis() < 200)
            {
                CurseAbsorption();
                ui.gameObject.SetActive(true);

            }
            if (b_Jump) LookAtPlayer(player);
            if (!undeadKnight.b_Load || !undeadKnight.b_Ax)
            {
                if (!b_straight) Jump();
            }
            if (!undeadKnight.attackBool) Attack();
        }
        else
        {
            f_speed = 0f;
            animator.enabled = false;
            f_FreezeTime += Time.deltaTime;
            g_FreezePaticle.gameObject.SetActive(true);
            //��ų�� ���׷��̵� �Ǳ������� 2�� + ��ȭ �ɶ����� 1�ʾ�����
            if (f_FreezeTime >= 2 + skill.skillEnhanceCount[3])
            {
                f_speed = 1f;
                g_FreezePaticle.gameObject.SetActive(false);
                f_FreezeTime = 0;
                b_Freeze = false;
            }
        }
    }
    public void Resurrection()
    {
        f_MaxundedHouseHp = 4000f;
        f_undedHouseHp = 4000f;
        playerUI.f_Maxf_CurseProgression = 5000;
        playerUI.f_CurseProgression = 5000;
        ui.gameObject.SetActive(false);
    }

    void ThisHp()
    {
        hpbarImage.fillAmount = f_undedHouseHp / f_MaxundedHouseHp;
    }
    void CurseAbsorption()
    {
        playerUI.f_Maxf_CurseProgression -= Time.deltaTime * 30;
    }
    public void Die()
    {
        if (f_undedHouseHp <= 0)
        {
            float resurrection = 0;
            resurrection += Time.deltaTime;
            if (resurrection > 300)
            {
                f_undedHouseHp = f_MaxundedHouseHp;
            }
            animator.SetTrigger("DieTrigger");
            Invoke("InvokeSetActive", 3);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public void DieClip()
    {
        questManager.i_questMosterCount[7] = 0;
        gameObject.SetActive(false);
        boseClearPotal.gameObject.SetActive(true);
        GameObject item1 = Instantiate(g_ClearItem[0], gameObject.transform.position + new Vector3(1, 2, 0), Quaternion.identity);
        item1.name = g_ClearItem[0].name;
        GameObject item2 = Instantiate(g_ClearItem[1], gameObject.transform.position + new Vector3(-1, 2, 0), Quaternion.identity);
        item2.name = g_ClearItem[1].name;
        GameObject item3 = Instantiate(g_ClearItem[2], gameObject.transform.position + new Vector3(3, 2, 0), Quaternion.identity);
        item3.name = g_ClearItem[2].name;
        GameObject item4 = Instantiate(g_ClearItem[3], gameObject.transform.position + new Vector3(0, 2, 1), Quaternion.identity);
        item4.name = g_ClearItem[3].name;
        GameObject item5 = Instantiate(g_ClearItem[4], gameObject.transform.position + new Vector3(0, 2, -1), Quaternion.identity);
        item5.name = g_ClearItem[4].name;
        GameObject item6 = Instantiate(g_ClearItem[5], gameObject.transform.position + new Vector3(2, 2, -1), Quaternion.identity);
        item6.name = g_ClearItem[5].name;



        Vector3 randomForce1 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item1.GetComponent<Rigidbody>().AddForce(randomForce1);
        Vector3 randomForce2 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item2.GetComponent<Rigidbody>().AddForce(randomForce2);
        Vector3 randomForce3 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item3.GetComponent<Rigidbody>().AddForce(randomForce3);
        Vector3 randomForce4 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item4.GetComponent<Rigidbody>().AddForce(randomForce4);
        Vector3 randomForce5 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item5.GetComponent<Rigidbody>().AddForce(randomForce5);
        Vector3 randomForce6 = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)) * 300f;
        item6.GetComponent<Rigidbody>().AddForce(randomForce6);
    }
    void InvokeSetActive()
    {
        gameObject.SetActive(false);
    }

    void AnimatorControll()
    {
        animator.SetBool("Run", b_speed);
        animator.SetFloat("speedBlend", f_speed);
        if (!undeadKnight.b_Ax) animator.SetBool("Direction", b_Direction);

        animator.SetFloat("DirectionFloat", f_Direction);
        animator.SetBool("AttackBool", b_Attack);
        animator.SetFloat("AttackFloat", f_Attack);
        animator.SetBool("JumpBool", b_Jump);
    }
    void LookAtPlayer(GameObject playerRotation)
    {
        Vector3 direction = playerRotation.transform.position - transform.position;
        direction.y = 90;
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }
    void Jump()
    {

        b_Jump = false; // Jump �ʱ�ȭ
        if (Dis() > 45 && Dis() < 50)
        {
            if (!playerUI.curseGameObject.activeSelf)
            {
                f_undedHouseHp -= 100;
            }
            b_Jump = true;
        }

    }
    //�Ÿ�
    float Dis()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        return dis;
    }
    //�յ�
    float Dot()
    {
        Vector3 localPlayerPos = transform.InverseTransformPoint(player.transform.position);
        float dot = Vector3.Dot(Vector3.forward, localPlayerPos);
        return dot;
    }
    void Attack()
    {
        Vector3 localPlayerPos = transform.InverseTransformPoint(player.transform.position);
        float dot = Vector3.Dot(Vector3.forward, localPlayerPos);
        //Debug.Log(Dis());
        // b_Attack = false; // ���� ���� �ʱ�ȭ
        float attackRange = 3.5f; // ���� ������ �Ÿ� ����
        //f_AttackCoolTime += Time.deltaTime;
        if (dot > 0f && Dis() > 0 && Dis() < attackRange) // �÷��̾ �տ� ���� ��
        {
            if (!playerUI.curseGameObject.activeSelf)
            {
                f_undedHouseHp -= 100;
            }
            f_Attack = 0;
            b_Attack = true;
        }
        else if (dot < 0f && Dis() > 0 && Dis() < attackRange) // �÷��̾ �ڿ� ���� ��
        {
            if (!playerUI.curseGameObject.activeSelf)
            {
                f_undedHouseHp -= 100;
            }
            f_Attack = 1;
            b_Attack = true;
        }
        else
        {
            b_Attack = false;
        }
    }
    public void DeAttack()
    {
        Debug.Log("DeAttack");
        StartCoroutine(HitBox((int)f_Attack));
    }

    public void AttackAnimatorEvent()
    {
        b_Attack = true;
        f_Attack = 1;
    }
    IEnumerator HitBox(int frontback)
    {
        b_Attack = false;
        g_hitBox[frontback].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.78f);
        g_hitBox[frontback].gameObject.SetActive(false);
    }


    float Angle()
    {
        // �÷��̾�� ������ ���� ���͸� ���
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // ������ ���� ����
        Vector3 monsterForward = transform.forward;

        // ������ ������ ����
        Vector3 monsterRight = transform.right;

        // ������ ������ �������� ���� ��� (y���� �������� ȸ��)
        float angle = Vector3.Angle(monsterForward, directionToPlayer);

        // ������ ��ȣ ��� (�����̸� ���, �������̸� ������ ����)
        float sign = Mathf.Sign(Vector3.Dot(monsterRight, directionToPlayer)) * -1;

        // ��ȣ�� ������ ���� ����
        float signedAngle = angle * sign;


        // ���� ���
        //Debug.Log(signedAngle);

        return signedAngle;
    }


    IEnumerator MoveToPosition(Vector3 target)
    {
        // ���� ��ġ
        float progress = 0f;
        Vector3 localPlayerPos = transform.InverseTransformPoint(player.transform.position);
        Vector3 crose = Vector3.Cross(Vector3.forward, localPlayerPos.normalized);
        b_straight = false;
        b_Rotation = false;
        yield return null;
        // ������ ���� �̵� ��� ����
        if (Angle() > -10.5 && Angle() < 10.5f)
        {
            b_straight = true;
        }
        else
        {
            b_straight = false;
            b_Rotation = true;
        }
        yield return null;
        if (b_Rotation)
        {
            StartCoroutine(Rotation(progress, crose));
        }
        if (b_straight)
        {
            StartCoroutine(Straight(progress));
        }
        StartCoroutine(MoveToPosition(player.transform.position));
    }
    //�����̵�
    IEnumerator Straight(float progress)
    {

        undeadKnight.weponchangeNot = true;
        runHitBox.enabled = true;
        while (progress < 3.0f && Dis() < 20)
        {
            progress += Time.deltaTime * 1;
            f_speed = 2f;
            b_speed = true;
            if (!playerUI.curseGameObject.activeSelf)
            {
                f_undedHouseHp -= 0.5f;
            }
            yield return null;
        }
        undeadKnight.weponchangeNot = false;
        runHitBox.enabled = false;
        b_speed = false;
        f_speed = 2f;
    }
    //ȸ��
    IEnumerator Rotation(float progress, Vector3 crose)
    {
        // �ִϸ��̼��� ����Ǵ� ���� ����
        while (progress < 1.0f)
        {
            b_Direction = true;
            // �����Ӵ� ���൵ ����
            progress += Time.deltaTime * 1;

            // ���� �����ӱ��� ���
            if (crose.y > 0) //������
            {
                f_Direction = 1;
                if (Dot() > 7.5f)//��
                {
                    b_Direction = false;
                }
            }
            else if (Dot() < 0f) // ��
            {
                f_Direction = -1;
                if (Dot() > 7.5f)//��
                {
                    b_Direction = false;
                }
            }
            yield return null;
        }
    }
    public void HorseHoof(int step)
    {
        if (step == 0)
        {
            SoundManager.PlaySound(stepClip[0]);
        }
        else if (step == 1)
        {
            SoundManager.PlaySound(stepClip[1]);
        }
        else if (step == 2)
        {
            SoundManager.PlaySound(stepClip[2]);
        }
        else if (step == 3)
        {
            SoundManager.PlaySound(stepClip[3]);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("R"))
        {
            f_undedHouseHp -= 5;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandAttack"))
        {
            SoundManager.PlaySound(damageClip);
            undedHouseHpUI.text = f_undedHouseHp.ToString() + "HP";
            ThisHp();
            switch (AttackAndCombo.comboAttakccount)
            {
                case 0:
                    f_undedHouseHp -= andCombo.attackDamage[0];
                    break;
                case 1:
                    f_undedHouseHp -= andCombo.attackDamage[1];
                    break;
                case 2:
                    f_undedHouseHp -= andCombo.attackDamage[2];
                    break;
                case 3:
                    f_undedHouseHp -= andCombo.attackDamage[3];
                    break;
            }
        }
        if (other.gameObject.CompareTag("ShildAttack"))
        {
            f_undedHouseHp -= shild.damageHap;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("freeze"))
        {
            b_Freeze = true;
        }
    }
}
