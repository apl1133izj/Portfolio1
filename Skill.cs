using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;//�ܺ� ���� �������� json
public class Skill : MonoBehaviour
{
    public int[] skillAbsorptionCount;//��ų�� ��µ� �ʿ��� �ټ�
    public int[] skillMaxAbsorptionCount;//��ų�� ��µ� �ʿ��� �ִ� �ټ�
    public int[] skillEnhanceCount;//��ȭ
    public string[] skillName;//��ų �̸� ��ų�̸����� ��ų ����
    public GameObject[] instSkill;//���濡 �����ϱ� ���� ������
    public GameObject[] useSkill;//��ų�� ��� ������ �� ���� ������Ʈ
    public GameObject useSkillPos;
    public GameObject skill3Effect;

    public bool[] b_SkillAttackCorutin;
    public bool[] b_CoolTime;
    PutitemBag putitemBag;
    public Image[] skillSloat1UI;
    public Image[] skillSloat2UI;
    public Animator skillUiAnimator;
    public int skillCount;
    public bool[] firstSloat;
    public Camera playerCamera;
    AttackAndCombo attackAndCombo;
    CharacterController character;
    public PlayerUI playerUI;
    Animator animator;
    public float skillAttackSpeed;
    Rigidbody rb;
    //����
    public AudioClip[] skillAudioClip;
    private void Awake()
    {
        string filePath = DataManager.instance.path + DataManager.instance.nowSlot.ToString();
        if (File.Exists(filePath))
        {
            skillEnhanceCount = DataManager.instance.nowPlayer.skillEnhanceCount;
        }
        putitemBag = GetComponent<PutitemBag>();
        character = GetComponent<CharacterController>();
        attackAndCombo = GetComponent<AttackAndCombo>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        firstSloat[0] = true;
    }
    void Update()
    {
        DataManager.instance.nowPlayer.skillEnhanceCount = skillEnhanceCount;
       SkillInst();
        SkillUI();
        SkillChange();
        SkillAttack();
    }
    void SkillAttack()
    {
        for (int i = 0; i < skillSloat1UI.Length; i++)
        {
            if (skillSloat1UI[i].gameObject.activeSelf && Input.GetKey(KeyCode.Mouse1) && firstSloat[0])
            {
                int firstsloat = 0;
                if (!b_SkillAttackCorutin[0] && !b_CoolTime[0]) SkilType(putitemBag.itemNameString[89], firstsloat);
                Debug.Log("��ų ����1" + skillSloat1UI[i].gameObject.name);
            }
            if (skillSloat2UI[i].gameObject.activeSelf && Input.GetKey(KeyCode.Mouse1) && firstSloat[1])
            {
                int secondsloat = 1;
                if (!b_SkillAttackCorutin[1] && !b_CoolTime[1]) SkilType(putitemBag.itemNameString[90], secondsloat);
                Debug.Log("��ų ����2" + skillSloat2UI[i].gameObject.name);
            }
        }
    }
    void SkilType(string skillname, int sloatPos)
    {
        switch (skillname)
        {
            case "����":
                StartCoroutine(Skill1Coroutin(sloatPos));
                break;
            case "ȭ����":
                StartCoroutine(Skill2Coroutin(sloatPos));
                break;
            case "�뽬":
                StartCoroutine(Skill3Coroutin(sloatPos));
                break;
            case "����":
                StartCoroutine(Skill4Coroutin(sloatPos));
                break;
            case "���� ����":
                StartCoroutine(Skill5Coroutin(sloatPos));
                break;
            case "��ź��":
                StartCoroutine(Skill6Coroutin(sloatPos));
                break;
        }
    }
    void UI(byte r, byte g, byte b, byte a, int sloatPos)
    {
        if (sloatPos == 0)
        {
            Color32 newColor = new Color32(r, g, b, a); // R, G, B, A (0~255)
            for (int i = 0; i < skillSloat1UI.Length; i++)
            {
                if (skillSloat1UI[i].gameObject.activeSelf) // Ȱ��ȭ ���� Ȯ��
                {
                    skillSloat1UI[i].color = newColor;
                }
            }
        }
        else if (sloatPos == 1)
        {
            Color32 newColor = new Color32(r, g, b, a); // R, G, B, A (0~255)
            for (int i = 0; i < skillSloat2UI.Length; i++)
            {
                if (skillSloat2UI[i].gameObject.activeSelf) // Ȱ��ȭ ���� Ȯ��
                {
                    skillSloat2UI[i].color = newColor;
                }
            }
        }

    }
    IEnumerator Skill1Coroutin(int sloatPos)
    {
        SoundManager.PlaySound(skillAudioClip[0]);
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        playerUI.f_CurseProgression -= 100f;
        skillAttackSpeed += 150 + (skillEnhanceCount[0]) * 10;
        yield return new WaitForSeconds(5); //���ӽð�
        skillAttackSpeed -= 150 + (skillEnhanceCount[0]) * 10;
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(10);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    IEnumerator Skill2Coroutin(int sloatPos)
    {
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        animator.SetLayerWeight(3, 1);
        animator.SetTrigger("Skil2Trigger");
        DemoCharacter.noneSpeed = true;
        yield return new WaitForSeconds(2);
        DemoCharacter.noneSpeed = false;
        animator.SetLayerWeight(3, 0);
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(3);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    IEnumerator Skill3Coroutin(int sloatPos)
    {
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        float deshTime = 0f;
        animator.SetLayerWeight(3, 1);
        animator.SetTrigger("Skill3Trigger");
        attackAndCombo.invincibilityBool = true;//����
        skill3Effect.gameObject.SetActive(true);
        gameObject.tag = "Desh";
        DemoCharacter.noneSpeed = true;
        yield return new WaitForSeconds(1.2f);
        while (deshTime <= 0.05f * skillEnhanceCount[2])
        {
            character.enabled = false;
            deshTime += Time.deltaTime;
            rb.AddForce(gameObject.transform.forward * 100f, ForceMode.Impulse); // 50f�� �߻� �ӵ�}
            DemoCharacter.noneSpeed = true;
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        DemoCharacter.noneSpeed = false;
        gameObject.tag = "Player";
        skill3Effect.gameObject.SetActive(false);
        attackAndCombo.invincibilityBool = false;//���� ����
        animator.SetLayerWeight(3, 0);
        character.enabled = true;
        DemoCharacter.noneSpeed = false;
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(10);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    IEnumerator Skill4Coroutin(int sloatPos)
    {
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        animator.SetLayerWeight(3, 1);
        //animator.SetTrigger("Skil2Trigger");
        DemoCharacter.noneSpeed = true;
        Instantiate(useSkill[1], useSkillPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        animator.SetLayerWeight(3, 0);
        DemoCharacter.noneSpeed = false;
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(10);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    IEnumerator Skill5Coroutin(int sloatPos)
    {
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        useSkill[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(10);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    IEnumerator Skill6Coroutin(int sloatPos)
    {
        b_SkillAttackCorutin[sloatPos] = true;
        b_CoolTime[sloatPos] = true;
        useSkill[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        b_SkillAttackCorutin[sloatPos] = false;
        UI(61, 61, 61, 255, sloatPos);
        yield return new WaitForSeconds(10);
        b_CoolTime[sloatPos] = false;
        UI(255, 255, 255, 255, sloatPos);
    }
    //���濡 ���� ���� �������� ����
    void SkillInst()
    {
        for (int i = 0; i < skillAbsorptionCount.Length; i++)
        {
            if (skillAbsorptionCount[i] == 0)
            {
                GameObject skill = Instantiate(instSkill[i], transform.position, Quaternion.identity);
                skill.name = skillName[i];
                skillAbsorptionCount[i] = skillMaxAbsorptionCount[i];
                skillEnhanceCount[i] += 1;//��ȭ
            }
        }

    }
    void SkillChange()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillCount += 1;

        }
        if (skillCount == 1)
        {
            skillUiAnimator.SetFloat("changefloat", 1);
            skillUiAnimator.SetBool("Change Bool", true);
            firstSloat[0] = false;
            firstSloat[1] = true;
        }
        else if (skillCount == 2)
        {
            skillUiAnimator.SetFloat("changefloat", -1);
            skillUiAnimator.SetBool("Change Bool", false);
            firstSloat[0] = true;
            firstSloat[1] = false;
            skillCount = 0;
        }

    }
    public void SkillInstClip(int skill)
    {
        switch (skill)
        {
            case 0:
                break;
            case 1:
                Debug.Log("ȭ����");
                StartCoroutine(UpgradeSkill());
                break;
            case 2:
                break;
            case 3:
                Instantiate(useSkill[1], useSkillPos.transform.position, Quaternion.identity);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }

    IEnumerator UpgradeSkill()
    {
        for (int count = 0; count <= skillEnhanceCount[1]; count++)
        {
            GameObject skill1 = Instantiate(useSkill[0], useSkillPos.transform.position, Quaternion.identity);
            SkillEuler(skill1);
            playerUI.f_CurseProgression -= 50f;
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void SkillEuler(GameObject skill)
    {
        skill.transform.rotation = Quaternion.LookRotation(-gameObject.transform.forward);
        // X���� 90���� ����
        Vector3 adjustedRotation = skill.transform.rotation.eulerAngles; // ���� ȸ���� ���Ϸ� ������ ������
        adjustedRotation.x = 90f; // X�� ���� 90���� ����
        skill.transform.rotation = Quaternion.Euler(adjustedRotation); // ���Ϸ� ������ Quaternion���� ��ȯ�Ͽ� ����
        Rigidbody rb = skill.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �÷��̾ �ٶ󺸴� �������� ���� ����.
            rb.AddForce(gameObject.transform.forward * 10f, ForceMode.Impulse); // 10f�� �߻� �ӵ�
            ApplyForceBasedOnCameraAngle(rb, playerCamera);


        }
        void ApplyForceBasedOnCameraAngle(Rigidbody rb, Camera playerCamera)
        {
            // ī�޶��� X�� ȸ�� ���� ��������
            float cameraXRotation = playerCamera.transform.rotation.eulerAngles.x;

            // X�� ������ �������� Y�� �� ���
            float yForce = Mathf.Sin((cameraXRotation - 35) * Mathf.Deg2Rad) * -10f; // 10f�� ���� ũ�� ������

            // �� ���� ���� (Y�࿡�� ���� ����)
            Vector3 force = new Vector3(0, yForce, 0);

            // Rigidbody�� �� ����
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
    void SkillUI()
    {

        for (int i = 0; i < skillAbsorptionCount.Length; i++)
        {
            if (putitemBag.itemNameString[89] == skillName[i] && putitemBag.itemNameString[89] != null)
            {
                skillSloat1UI[i].gameObject.SetActive(true);
            }
            else
            {
                skillSloat1UI[i].gameObject.SetActive(false);
            }
            if (putitemBag.itemNameString[90] == skillName[i] && putitemBag.itemNameString[90] != null)
            {
                skillSloat2UI[i].gameObject.SetActive(true);
            }
            else
            {
                skillSloat2UI[i].gameObject.SetActive(false);
            }
        }
    }
}
