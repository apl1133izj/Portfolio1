using System.Collections;
using UnityEngine;
public class UndeadKnight : MonoBehaviour
{
    GameObject player;

    public int weponChange; //0:���� 1:Ȱ
    public bool attackBool;

    public GameObject[] g_WeponChange;
    public bool b_ShotBool;//Ȱ����(ȭ���� ������)
    public Animator animator;
    public GameObject[] g_Animation_bow_Load;
    public static int randomArrow;

    public bool b_Knight_direct, b_Load, b_Idle, b_Ax, b_WeponChange;
    public bool b_Crosed; //������ ���� �� �ִ°�;
    public float f_direct, f_Ax;


    public float angle;
    Bow bow;
    public float[] attackCoolTime; // 0:���� 1:Ȱ
    public bool isSwitching;
    private int currentWeaponType = 0;
    Vector3 playerStartPos;
    public float loadboolTime = 0;
    public bool weponchangeNot;
    UndedHouse undedHouse;
    public PlayerBoseRoomCheck check;
    public AudioClip[] attackClip;

    //�÷��̾ ���� ���� �̻�
    public bool b_Freeze;
    public float f_FreezeTime;
    public GameObject g_FreezePaticle;
    public Skill skill;
    private void Awake()
    {
        undedHouse = GetComponentInParent<UndedHouse>();
        bow = GetComponentInChildren<Bow>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerStartPos = player.transform.position;
    }
    void Update()
    {
        if (!b_Freeze)
        {
            if (Distance() > 120)
            {
                /// check.playerCheck = false;
                undedHouse.enabled = false;
                undedHouse.Resurrection();
            }
            else
            {
                if (undedHouse.f_undedHouseHp > 0 /*&& check.playerCheck*/)
                {
                    WeponSwitch();
                    AnimatorControll();
                    Attack(weponChange);
                    undedHouse.enabled = true;
                }
            }
        }
        else
        {
            animator.enabled = false;
            f_FreezeTime += Time.deltaTime;
            g_FreezePaticle.gameObject.SetActive(true);
            //��ų�� ���׷��̵� �Ǳ������� 2�� + ��ȭ �ɶ����� 1�ʾ�����
            if (f_FreezeTime >= 2 + skill.skillEnhanceCount[3])
            {
                g_FreezePaticle.gameObject.SetActive(false);
                f_FreezeTime = 0;
                b_Freeze = false;
            }
        }
    }

    private void WeponSwitch()
    {
        if (!weponchangeNot)
        {
            if (Distance() < 26f)
            {
                weponChange = 0;
            }
            else if (Distance() > 26f)
            {
                weponChange = 1;
            }
            // �Ÿ��� ���� ���⸦ ��ü�ϴ� ����
            if (Distance() < 26f && currentWeaponType != 0 && !isSwitching)
            {
                // �ٰŸ� ����� ��ü
                StartCoroutine(WeaponChangeCoroutine(0));
            }
            else if (Distance() > 26f && currentWeaponType != 1 && !isSwitching)
            {
                // ���Ÿ� ����� ��ü

                StartCoroutine(WeaponChangeCoroutine(1));
            }
        }
    }
    float Distance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
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


    void AnimatorControll()
    {
        animator.SetBool("IdleBool", b_Idle);

        animator.SetBool("DirectionBool", b_Knight_direct);
        animator.SetFloat("DirectionBlend", Angle());

        animator.SetBool("LoadBool", b_Load);
        animator.SetFloat("LoadBlend", Angle());

        animator.SetBool("AxBool", b_Ax);
        animator.SetFloat("AxFloat", f_Ax);
    }

    void Attack(int typeChange)
    {
        Vector3 localPlayerPos = transform.InverseTransformPoint(player.transform.position);
        Vector3 crose = Vector3.Cross(Vector3.forward, localPlayerPos.normalized);
        float dis = Vector3.Distance(player.transform.position, transform.position);
        float dot = Vector3.Dot(Vector3.forward, localPlayerPos);
        //Debug.Log(dis);
        attackCoolTime[typeChange] += Time.deltaTime;

        //���� ���ӿ�����Ʈ�� Ȱ��ȭ ���ְų� ��Ÿ���� 2���� ũ�� ���� ��ȣ�� �������
        if (typeChange == 0 && g_WeponChange[0].gameObject.activeSelf && attackCoolTime[0] > 5f && attackBool)
        {
            if (crose.y > 0) //������ | 11.5���� �������
            {
                SoundManager.PlaySound(attackClip[0]);
                b_Ax = true;
                f_Ax = 0;
                attackCoolTime[0] = 0;
            }
            else if (crose.y < 0)
            {
                SoundManager.PlaySound(attackClip[0]);
                b_Ax = true;
                f_Ax = 2;
                attackCoolTime[0] = 0;
            }
        }
        else
        {
            b_Ax = false;
        }
        if (typeChange == 1 && g_WeponChange[2].gameObject.activeSelf && attackCoolTime[1] > 5f)
        {
            if (dot > 0f || dis > 18.5f) //�� 11.5���� �ְ��
            {
                SoundManager.PlaySound(attackClip[0]);
                attackCoolTime[1] = 0;
                b_Load = true;
            }
            else if (dot < 0f || dis > 18.5f)//��11.5���� �ְ��
            {
                SoundManager.PlaySound(attackClip[0]);
                attackCoolTime[1] = 0;
                b_Load = true;
            }
        }
        //Ȱ ���ӿ�����Ʈ�� Ȱ��ȭ ���ְų� ��Ÿ���� 2 ���� ũ�� ���� ��ȣ�� �������
        if (typeChange == 1 && g_WeponChange[2].gameObject.activeSelf && attackCoolTime[1] > 3f)
        {                // �÷��̾�� ������ ��ġ
                         // �ִϸ����Ϳ� ������ �����Ͽ� ���� Ʈ������ �ִϸ��̼��� �ڿ������� ��ȯ�ǵ��� ����
            
            attackCoolTime[1] = 0;
            b_Load = true;
        }



        if (b_Load)
        {
            loadboolTime += Time.deltaTime;
            if (loadboolTime > 4)
            {
                b_Load = false;
                loadboolTime = 0;
            }
        }
    }
    public IEnumerator WeaponChangeCoroutine(int weaponType)
    {
        Debug.Log("WeaponChange �ڷ�ƾ ����");
        isSwitching = true;
        // ���� ��ü �غ� ���� �ʱ�ȭ
        b_Knight_direct = false;
        b_ShotBool = false;
        b_Load = false;
        // 1.5�� ���
        animator.SetBool("WeponChangeBool", true);
        yield return new WaitForSeconds(0.5f);

        // ���� Ÿ�Կ� ���� ���� Ȱ��ȭ/��Ȱ��ȭ
        switch (weponChange)
        {
            case 0: // �ٰŸ� ����
                g_WeponChange[0].SetActive(true);  // �ٰŸ� ���� Ȱ��ȭ
                g_WeponChange[1].SetActive(false); // �ٰŸ� ���� �� ���� ��Ȱ��ȭ
                g_WeponChange[2].SetActive(false); // ���Ÿ� ���� ��Ȱ��ȭ
                g_WeponChange[4].SetActive(true); // Ȱ �� ���� Ȱ��ȭ
                break;

            case 1: // ���Ÿ� ����
                g_WeponChange[0].SetActive(false); // �ٰŸ� ���� ��Ȱ��ȭ
                g_WeponChange[1].SetActive(true);  // �ٰŸ� ���� �� ���� ��Ȱ��ȭ
                g_WeponChange[2].SetActive(true);  // ���Ÿ� ���� Ȱ��ȭ
                g_WeponChange[4].SetActive(false); // Ȱ �� ���� ��Ȱ��ȭ
                break;

            default:
                Debug.LogWarning("Unknown weapon type: " + weponChange);
                break;
        }
        yield return new WaitForSeconds(0.45f);
        b_Knight_direct = false;
        b_ShotBool = false;
        b_Load = false;

        // ���� ��ü �ִϸ��̼� ����
        currentWeaponType = weaponType;
        animator.SetBool("WeponChangeBool", false);
        isSwitching = false;
        Debug.Log("WeaponChange �ڷ�ƾ ��");
    }
    public void Load()//����(�ִϸ��̼� Ŭ��)
    {
        StartCoroutine(LoadC());
    }

    IEnumerator LoadC()
    {
        randomArrow = Random.Range(0, 6);
        yield return null;
        g_Animation_bow_Load[randomArrow].SetActive(true);
    }
    public void Pull(int pull)
    {
        if (pull == 0)
        {
            b_ShotBool = false;
        }
        else
        {
            loadboolTime = 0;
            b_ShotBool = true;
        }
    }
    public void LoadFalse()
    {
        b_Load = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackBool = false;
        }
    }
}
