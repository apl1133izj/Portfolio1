using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public GameObject player;
    public int[] skillLevel;
    public int[] skillMaxLevel;
    public bool[] upgradeComplete;//��ų ���׷��̵� �ϷῩ��
    public Image[] enableSkill;//��ų ����Ʈ�� �̿��� ��ų ���׷��̵� ������ ������ �Ͼ������
    public Image[] skillConnection;
    public TextMeshProUGUI[] skillLevelTextMeshPro;

    //��ų ��ȭ
    public HP hPS;
    public Stamina staminaS;
    Potion potion;
    AttackAndCombo attackAndCombo;
    DemoCharacter demoCharacter;
    PlayerUI playerUI;
    PutitemBag putitemBag;


    public TextMeshProUGUI[] skillText;//0:���� ��ų ����Ʈ 1:����� ��ų ����Ʈ
    public int skillPoint; //��� ������ ��ų ����Ʈ
    public int usedSkillPoint;//����� ��ų ����Ʈ
    public bool[] skillUpGradeBool;
    int skillnum;

    public float upgradeReduceCoolTime;
    public Button[] skillpointActivationLevelButton;


    public int hpStack;
    public int staminaStack;
    public float hpRecoveryStack;
    public float staminaRecoveryStack;
    public float walkSpeedStack;
    public float runspeedStack;
    public float skillattackSpeed;
    public float staminaReductionRate;
    public float attackRange;
    public ToolTip[] toolTip;
    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        player = GameObject.Find("Player");
        if (player != null)
        {
            potion = player.GetComponent<Potion>();
            attackAndCombo = player.GetComponent<AttackAndCombo>();
            putitemBag = player.GetComponent<PutitemBag>();
            demoCharacter = player.GetComponent<DemoCharacter>();
        }
        string filePath = DataManager.instance.path + DataManager.instance.nowSlot.ToString();
        if (File.Exists(filePath))
        {
            skillPoint = DataManager.instance.nowPlayer.skillPointData;
            usedSkillPoint = DataManager.instance.nowPlayer.useSkillPointData;
            for (int i = 0; i < skillLevel.Length; i++)
            {

                skillLevel[i] = DataManager.instance.nowPlayer.skillLevelsData[i];
                //Debug.Log($"Initial skillLevel[{i}] = {skillLevel[i]}");
                skillLevelTextMeshPro[i].text = skillLevel[i].ToString();
                if (skillLevel[i] > 0)
                {
                    skillConnection[i].fillAmount = 1;
                    enableSkill[i].color = Color.white;
                }
            }
        }
        // DataManager���� �ҷ��� �����͸� �ʱ�ȭ

    }
    private void Update()
    {
        SKillTreeUI();
        UpdateSkillTreeUI();
    }

    void SKillTreeUI()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.nowPlayer.skillPointData = skillPoint;
            DataManager.instance.nowPlayer.useSkillPointData = usedSkillPoint;
            DataManager.instance.nowPlayer.skillTree = skillnum;
        }
        if (usedSkillPoint >= 15)
        {
            skillpointActivationLevelButton[2].interactable = true;
            skillpointActivationLevelButton[3].interactable = true;
        }
        if (usedSkillPoint >= 55)
        {
            skillConnection[14].fillAmount = 1;
            skillpointActivationLevelButton[4].interactable = true;
        }
        if (usedSkillPoint >= 70)
        {
            skillConnection[15].fillAmount = 1;
            skillConnection[16].fillAmount = 1;
            skillpointActivationLevelButton[5].interactable = true;
            skillpointActivationLevelButton[6].interactable = true;
        }
        if (usedSkillPoint >= 100)
        {
            skillpointActivationLevelButton[7].interactable = true;
            skillpointActivationLevelButton[8].interactable = true;
        }
        if (usedSkillPoint >= 130)
        {
            skillConnection[17].fillAmount = 1;
            skillpointActivationLevelButton[9].interactable = true;
            skillpointActivationLevelButton[10].interactable = true;
            skillpointActivationLevelButton[11].interactable = true;
        }
        if (usedSkillPoint >= 170)
        {
            skillConnection[18].fillAmount = 1;
            skillpointActivationLevelButton[12].interactable = true;
            skillpointActivationLevelButton[13].interactable = true;
        }
    }
    void UpdateSkillTreeUI()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.nowPlayer.skillPointData = skillPoint;
            DataManager.instance.nowPlayer.useSkillPointData = usedSkillPoint;
            for (int i = 0; i < skillLevel.Length; i++)
            {
                DataManager.instance.nowPlayer.skillLevelsData[i] = skillLevel[i];
            }
        }


        skillText[0].text = skillPoint.ToString();
        skillText[1].text = usedSkillPoint.ToString();
    }
    public void Skill(int skill)
    {
        //��ų ������ �ʿ��� ��ų ����Ʈ�� ����
        skillnum = skill;


        if (!upgradeComplete[skill])//���׷��̵� �� �Ϸ�� ���°� �ƴѰ��
        {

            //��ų ������ MAX�� ���
            if (skillLevel[skill] == skillMaxLevel[skill])
            {
                if (skillPoint >= skillLevel[skill])
                {
                    skillLevelTextMeshPro[skill].text = skillLevel[skill].ToString();
                    skillPoint -= skillLevel[skill];
                    usedSkillPoint += skillLevel[skill];
                    MaxSkillCommend(skill);
                    upgradeComplete[skill] = true;
                }
            }
            else//�ƴѰ��
            {
                if (skillPoint >= skillLevel[skill])
                {
                    skillConnection[skill].fillAmount = 1;//��ų�� Ȱ��ȭ �Ǹ� ���ἱ �� 0���� 1�� ä���

                    enableSkill[skill].color = Color.white;//��Ȱ��ȭ�� ����(ȸ��) Ȱ��ȭ�� ����(�Ͼ��)
                    skillLevelTextMeshPro[skill].text = skillLevel[skill].ToString();//��ų ������ ������
                    skillPoint -= skillLevel[skill];//��ų ����Ʈ�� ���� ��ŭ ���� ��Ŵ
                    usedSkillPoint += skillLevel[skill]; //����� ��ų ����Ʈ�� ��ų���� ��ŭ ����
                    skillLevel[skill] += 1; //��������
                    SkillCommend(skill);

                }
            }
        }
    }
    /// <summary>
    ///  skillcommend : ��ų ���� 
    ///  ��ų ������ ���� �ϰ� �ɰ�� �⺻ ���� �� * ��ų������ ������ ���� 
    /// </summary>
    /// <param name="skillcommend"></param>
    private void SkillCommend(int skillcommend)
    {
        switch (skillcommend)
        {
            case 0://ü������
                hpStack += 100 * skillLevel[skillcommend];
                break;
            case 1://���׹̳� ����
                staminaStack += 100 * skillLevel[skillcommend];
                break;
            case 2://ü�� ��� ȿ�� ����
                hpRecoveryStack = 1 * skillLevel[skillcommend];
                break;
            case 3://����� ���ð� ����
                upgradeReduceCoolTime = 0.2f * skillLevel[skillcommend];
                break;
            case 4://���� ȿ�� ����
                potion.stackPostionRecovery = 50 * skillLevel[skillcommend];
                break;
            case 5://���� ȿ�� ����
                playerUI.f_Curse -= 0.025f * skillLevel[skillcommend];
                break;
            case 6://��� ��� ȿ�� ����
                staminaRecoveryStack = 1 * skillLevel[skillcommend];
                break;
            case 7://���ݷ� ����
                attackAndCombo.stackAttackDamage[0] += (1 * skillLevel[skillcommend]);
                attackAndCombo.stackAttackDamage[1] += (1 * skillLevel[skillcommend]);
                attackAndCombo.stackAttackDamage[2] += (1 * skillLevel[skillcommend]);
                attackAndCombo.stackAttackDamage[3] += (1 * skillLevel[skillcommend]);
                break;
            case 8://���ط� ����
                attackAndCombo.defend += 2 * skillLevel[skillcommend];
                break;
            case 9://�̵� �ӵ� ����
                walkSpeedStack += 0.15f * skillLevel[skillcommend];
                runspeedStack += 0.15f * skillLevel[skillcommend];
                break;
            case 10://���� �ӵ� ����
                skillattackSpeed += 0.03f * skillLevel[skillcommend];
                break;
            case 11://��� ���� ��� ����
                staminaReductionRate += 5f * skillLevel[skillcommend];
                break;
            case 12://���� ���� ����
                attackRange += 0.025f * skillLevel[skillcommend];
                break;
            case 13: //�ñر� ����
                attackAndCombo.ultimateSkillunlock = true;
                attackAndCombo.ultimateSkillCoolTimeMax -= 1 * skillLevel[skillcommend];
                break;
        }
    }
    private void MaxSkillCommend(int skillcommend)
    {
        switch (skillcommend)
        {
            case 0:
                hPS.hpRegenerationBool = true;
                hPS.hpMax = 200 * skillLevel[skillcommend];
                hPS.hp = hPS.hpMax;
                break;
            case 1:
                staminaS.staminaRegenerationBool = true;
                staminaS.staminaMax = 300 * skillLevel[skillcommend];
                staminaS.stamina = staminaS.staminaMax;
                break;
            case 2:
                hPS.hpRecovery = 7;
                break;
            case 3:
                upgradeReduceCoolTime = 1.5f;
                break;
            case 4:
                potion.stackPostionRecovery = 700;
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                attackAndCombo.stackdefend = 15;
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
        }
    }
}
