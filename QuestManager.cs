using System.Collections;
using UnityEngine;
public class QuestManager : MonoBehaviour
{
    public bool[] questCountBool; //����Ʈ�� �����:false �������� ���:true
    public bool[] questBool;//����Ʈ�� �̹� �޾Ҵٸ�
    public GameObject[] questGameObject;
    public float f_textPos;
    public GameObject[] g_Quest_situation_Arear_Icon;//����Ʈ ���¸� ������ �� ������ ���� Ȯ��
    public GameObject dialogueGameObject;
    public GameObject[] quest;
    float transformY;
    public int questProgress;  //����Ʈ ���൵
    public bool conversationStatus = false;// ��ȭ�� ���� �Ǿ������(1:��ȭ ���� , 0: ��ȭ ����)

    public static string questName;
    public static string questClearName;
    public int[] i_questMosterCount; //0:creep ,1:��� 
    public bool[] b_questClearProgress;//Ŭ���� ��������
    public GameObject[] questClearMark;//����Ʈ�� Ŭ���� ������ üũ��ũ �� Ȱ��ȭ
    public TextReaderExample textReader;
    [Range(-1000, 1000)]
    public float textpos;
    public RectTransform[] uiElement; // �̵��� UI ���

    public GameObject[] compensationGameObject;//����
    public Transform[] itemInstPos;
    public bool smallsanctum;

    public GameObject[] arearEffect;
    //�Ҹ�
    public AudioClip questClearSound;
    public BoxCollider bose1BoxCollider;

    public bool questArea; //false:���� true:����
    private void Start()
    {
        if (questProgress > 0)
        {
            for (int j = 0; j < questProgress; j++)
            {
                questBool[j] = true;
            }
        }
        questCountBool[questProgress] = false;
        uiElement[questProgress].anchoredPosition = new Vector2(uiElement[questProgress].anchoredPosition.x, -303f);
        //questProgress = 1;
    }
    void Update()
    {

        DataManager.instance.nowPlayer.questProgressData = questProgress;
        Quest(questProgress);
        QuestClear(questProgress);
        PostionText(f_textPos);
    }
    public void Compensation()
    {
        if (questBool[1])
        {
            GameObject item = Instantiate(compensationGameObject[0], itemInstPos[0].position + Vector3.up, Quaternion.identity);
            item.name = compensationGameObject[0].name;
        }
        else if (questBool[2])
        {
            GameObject item2 = Instantiate(compensationGameObject[1], itemInstPos[0].position + Vector3.up, Quaternion.identity);
            item2.name = compensationGameObject[1].name;
            GameObject item3 = Instantiate(compensationGameObject[2], itemInstPos[1].position + Vector3.up, Quaternion.identity);
            item3.name = compensationGameObject[2].name;
        }
        else if (questBool[5])
        {
            GameObject item4 = Instantiate(compensationGameObject[4], itemInstPos[0].position + Vector3.up, Quaternion.identity);
            item4.name = compensationGameObject[4].name;
        }
    }

    public void QuestClear(int inputQuestProgress)
    {
        if (i_questMosterCount[inputQuestProgress] <= 0)
        {
            questCountBool[questProgress] = true;
            StartCoroutine(QuestGameObjectFalse());
            Debug.Log($"����Ʈ {inputQuestProgress} Ŭ����");
            SoundManager.PlaySound(questClearSound);
            // ����Ʈ ���� ���� �ʱ�ȭ
            textReader.b_endQuest = false;
            b_questClearProgress[inputQuestProgress + 1] = true;
            questClearMark[inputQuestProgress].gameObject.SetActive(true);
            if (arearEffect[0].activeSelf)
            {
                g_Quest_situation_Arear_Icon[0].gameObject.SetActive(false);
                g_Quest_situation_Arear_Icon[1].gameObject.SetActive(false);
                g_Quest_situation_Arear_Icon[2].gameObject.SetActive(true);
            }
            else if (arearEffect[1].activeSelf)
            {
                g_Quest_situation_Arear_Icon[3].gameObject.SetActive(false);
                g_Quest_situation_Arear_Icon[4].gameObject.SetActive(false);
                g_Quest_situation_Arear_Icon[5].gameObject.SetActive(true);
            }

            // ���൵ ����
            questProgress++;
        }
    }
    IEnumerator QuestGameObjectFalse()
    {
        f_textPos = -303f;
        yield return new WaitForSeconds(4);
        questGameObject[questProgress].gameObject.SetActive(false);
    }
    //����Ʈ ���൵ ���� �о���� ����Ʈ text������ �޶���
    public void Quest(int questS)//Ȧ���� ����Ʈ ¦���� Ŭ����
    {
        if (conversationStatus)
        {
            // questProgress = questS;
            switch (questS)
            {
                case 0:
                    questName = "Q1";
                    dialogueGameObject.SetActive(true);
                    i_questMosterCount[questS] = 2;
                    break;
                case 1:
                    questName = "C1";
                    dialogueGameObject.SetActive(true);
                    break;
                case 2:
                    questName = "Q2";
                    dialogueGameObject.SetActive(true);
                    i_questMosterCount[questS] = 4;
                    break;
                case 3:
                    questName = "C2";
                    dialogueGameObject.SetActive(true);
                    break;
                case 4:
                    questName = "Q3";
                    bose1BoxCollider.enabled = true;
                    dialogueGameObject.SetActive(true);
                    break;
                case 5:
                    questName = "C3";
                    compensationGameObject[3].SetActive(true);
                    dialogueGameObject.SetActive(true);
                    break;
                case 6:
                    questName = "Q4";
                    dialogueGameObject.SetActive(true);
                    break;
                case 7:
                    questName = "C4";
                    dialogueGameObject.SetActive(true);
                    break;
                case 8:
                    questName = "Q5";
                    dialogueGameObject.SetActive(true);
                    break;
                case 9:
                    questName = "C5";
                    dialogueGameObject.SetActive(true);
                    break;
                case 10:
                    questName = "Q6";
                    dialogueGameObject.SetActive(true);
                    break;
                case 11:
                    questName = "C6";
                    dialogueGameObject.SetActive(true);
                    break;
                case 12:
                    questName = "Q7";
                    dialogueGameObject.SetActive(true);
                    break;
                case 13:
                    questName = "C7";
                    dialogueGameObject.SetActive(true);
                    break;
                default:
                    Debug.LogError($"���ǵ��� ���� questProgress: {questProgress}");
                    break;
            }
        }
    }
    public void PostionText(float pos)
    {
        if (questCountBool[questProgress])
        {
            // t ��(����)�� ���� ����ؼ� ���
            float currentY = uiElement[questProgress].anchoredPosition.y;

            //���� Y ��ġ ���� pos���� �ε巴�� �̵�
            float newY = Mathf.MoveTowards(currentY, pos, 90 * Time.deltaTime);

            // ��ġ ������Ʈ
            uiElement[questProgress].anchoredPosition = new Vector2(uiElement[questProgress].anchoredPosition.x, newY);
        }
    }
}
