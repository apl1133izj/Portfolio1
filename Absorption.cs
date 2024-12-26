using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Absorption : MonoBehaviour
{
    GameObject playerAbsorptionPos;
    GameObject playerPos;
    public GameObject instSkill;
    public string skillName;
    public bool b_Absorption;
    public GameObject[] skillDis;

    Skill skillCount;
    public GameObject g_playerUI;
    PlayerUI playerUI;
    public AudioClip absorptionClip;
    float speed = 2;

    // SerializedObject ���� ���� (UnityEditor������ ���)
#if UNITY_EDITOR
    SerializedObject serializedObject;
    // �ʿ��� SerializedObject�� ����ϴ� ��� �߰� �ڵ�
#endif

    void Start()
    {
        playerPos = GameObject.Find("Player");
        playerAbsorptionPos = GameObject.Find("Fighter_10_Armors_Mesh");
        float speedDistance = Vector3.Distance(gameObject.transform.position, playerAbsorptionPos.transform.position);
        if (speedDistance >= 3)
        {
            speed = speedDistance / 2; Debug.Log(speed);
        }
    }
    void Update()
    {
        // �÷��̾��� ��� ��ų�� ����� ��Ʋ������ �̵�
        if (Input.GetKeyDown(KeyCode.Tab)&&Distance(playerAbsorptionPos) <= 10)
        {
            skillDis = GameObject.FindGameObjectsWithTag("Skill");
            skillCount = playerPos.GetComponent<Skill>();
            g_playerUI = GameObject.Find("Player UI");
            playerUI = g_playerUI.GetComponent<PlayerUI>();
            b_Absorption = true;
        }
        if (b_Absorption && Distance(playerAbsorptionPos) <= 10)
        {
            ArearSkillAbsorptionCount();
        }
        if (Distance(playerAbsorptionPos) >= 10)
        {
            b_Absorption = false;
        }
    }

    // Distance �Լ� �̸� ����
    float Distance(GameObject player)
    {
        return Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    /// <summary>
    /// ���� ����� ��ȥ���� ����ϴ� �ý���
    /// </summary>
    void ArearSkillAbsorptionCount()
    {
        // ���� ����� ��ų ã��
        GameObject closestSkill = null;
        Vector3 startPos = Vector3.zero;
        float closestDistance = float.MaxValue;
        

        foreach (var skillObj in skillDis)
        {
            if (skillObj == null) continue; // Null üũ
            float distance = Vector3.Distance(skillObj.transform.position, playerAbsorptionPos.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSkill = skillObj.gameObject;
                startPos = closestSkill.transform.position;
            }
        }
        // ���� ����� ��ų�� ������ �Լ� ����
        if (closestSkill == null)
        {
            Debug.Log("����� ��ų ������Ʈ�� ����");
            return;
        }
        // ��� �Ϸ� ����: ��ġ ���̰� ���� ���
        if (Vector3.Distance(closestSkill.transform.position, playerAbsorptionPos.transform.position) <= 0.1f)
        {
            UpdateSkillDisArray(closestSkill);
        }
        else
        {
            closestSkill.transform.position = Vector3.Lerp(startPos, playerAbsorptionPos.transform.position,speed  * Time.deltaTime);
        }
    }

    // skillDis �迭�� ������Ʈ�ϴ� ���� �Լ�
    void UpdateSkillDisArray(GameObject removedSkill)
    {
        List<GameObject> updatedList = new List<GameObject>(skillDis);

        // ������ ������Ʈ ����
        updatedList.Remove(removedSkill);

        skillDis = updatedList.ToArray();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && b_Absorption)
        {
            Debug.Log("�÷��̾� �� �ε�ħ");
            for (int i = 0; i < skillCount.skillName.Length; i++)
            {
                if (skillCount.skillName[i] == gameObject.name)//�̸��� ���� ���
                {
                    if (playerUI.f_CurseProgression >= playerUI.f_Maxf_CurseProgression - 99)
                    {
                        float curseRecovery = playerUI.f_Maxf_CurseProgression - playerUI.f_CurseProgression;
                        playerUI.f_CurseProgression += curseRecovery;
                    }
                    else
                    {
                        playerUI.f_CurseProgression += 100;
                    }
                    SoundManager.PlaySound(absorptionClip);
                    skillCount.skillAbsorptionCount[i] -= 1;               
                    gameObject.SetActive(false); 
                    Destroy(gameObject,3);
                }
            }
        }
    }
#if UNITY_EDITOR
    // SerializedObject �޸� ���� �� ������ ���
    private void OnDestroy()
    {
        if (serializedObject != null)
        {
            serializedObject.Dispose(); // �޸� ����
            serializedObject = null;     // ���� ����
        }
    }
#endif
}
