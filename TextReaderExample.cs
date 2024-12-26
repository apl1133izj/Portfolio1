using System.Collections;
using System.IO;
using UnityEngine;

public class TextReaderExample : MonoBehaviour
{

    private TextAsset textFile;  // �ؽ�Ʈ ������ ������ ����
    private StringReader stringReader;  // ���ڿ��� �б� ���� StringReader
    private string currentLine;  // ���� �а� �ִ� ���� ������ ����
    public UnityEngine.UI.Text textDi;
    public float typingSpeed;
    private bool isTyping = false;
    public QuestManager questManager;
    int buttonCount = 0;
    public PlayerUI playerUI;
    public int spaceCount = 0;
    public bool b_endQuest;
    public RecoveryArear recoveryArear;
    public GameObject recoveryArearEffect;
    public AudioClip questOnSound;
    public GameObject lastBosePotal;
    public void TextRead()
    {
        Debug.Log("TextRead ���� ��");

        // questName �� �α� ���
        Debug.Log($"���� questName: {QuestManager.questName}");

        // ���� StringReader �ݱ�
        if (stringReader != null)
        {
            stringReader.Close();
            stringReader = null;
        }

        // �ؽ�Ʈ ���� �ε�
        textFile = Resources.Load<TextAsset>(QuestManager.questName);
        if (textFile != null)
        {
            stringReader = new StringReader(textFile.text);
            playerUI.uiOpen = true;

            // ��ȭ ������Ʈ Ȱ��ȭ Ȯ��
            if (!questManager.dialogueGameObject.activeSelf)
            {
                questManager.dialogueGameObject.SetActive(true);
            }

            // ù �� �о����
            ReadNextLine();
        }
        else
        {
            Debug.LogError("�ؽ�Ʈ ������ ã�� �� �����ϴ�.");
        }
    }
    IEnumerator co()
    {
        QuestArear.b_textReadPlay = false;
        yield return null;
        TextRead();
    }
    void Update()
    {
        if (QuestArear.b_textReadPlay)
        {
            StartCoroutine(co());
        }
        // ��ư�� ������ ���� �ٷ� �Ѿ���� ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonCount += 1;

            if (buttonCount == 3)
            {
                buttonCount = 1;
            }
            Debug.Log(buttonCount);
        }
        if (Input.GetKeyDown(KeyCode.Space) && buttonCount == 1)
        {
            playerUI.textTypingSpeedText.text = "ON";
            typingSpeed = 0.001f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && buttonCount == 2)
        {
            playerUI.textTypingSpeedText.text = "OFF";
            typingSpeed = 0.06f;
        }

    }

    public void NextDialogueButton()
    {
        if (!isTyping)
        {
            ReadNextLine();
        }
    }
    public void ReadNextLine()
    {
        // ������ ���� ���� �ִٸ� ó�� �� ����
        if (!string.IsNullOrEmpty(currentLine))
        {
            textDi.text = "";
            Debug.Log("���� ��: " + currentLine);
        }

        // ���� �� �б�
        currentLine = stringReader.ReadLine();

        // ���� ���� null�̸� ��� ���� �� �о��ٴ� ��
        if (currentLine == null)
        {
            StartCoroutine(TextPosEnd());
            questManager.questBool[questManager.questProgress] = true;
            questManager.questCountBool[questManager.questProgress] = true;
            questManager.questGameObject[questManager.questProgress].SetActive(true);
            b_endQuest = true;//����Ʈ �̺�Ʈ ����
            DemoCharacter.noneSpeed = false;//�÷��̾� �̵� ����
            playerUI.uiOpen = false;//UI��ȭ��ȭ
            questManager.conversationStatus = false;
            questManager.dialogueGameObject.SetActive(false);//��ȭâ ��Ȱ��ȭ
            QuestArear.b_textReadPlay = true;
            questManager.Compensation();
            SoundManager.PlaySound(questOnSound);
            playerUI.f_CurseProgression = 5000f;
            Invoke("InvokeFlase", 3);
            Debug.Log("�� �̻� ���� ���� �����ϴ�.");
            if (questManager.questProgress == 1)
            {
                questManager.uiElement[0].gameObject.SetActive(false);
            }
            if (questManager.questProgress > 1)
            {
                questManager.uiElement[questManager.questProgress - 1].gameObject.SetActive(false);
            }
            if (questManager.questProgress == 3)
            {
                questManager.uiElement[questManager.questProgress - 1].gameObject.SetActive(false);
                recoveryArear.enabled = true;
                recoveryArear.smallsanctum = true;
                recoveryArearEffect.SetActive(true);
            }
            if(questManager.questProgress == 6) 
            {
                lastBosePotal.SetActive(true);
            }
            return;
        }
        if (!questManager.dialogueGameObject.activeSelf)
        {
            questManager.dialogueGameObject.SetActive(true);
        }
        StartCoroutine(TypingTextCoroutine());
    }

    IEnumerator TypingTextCoroutine()
    {
        isTyping = true;  // Ÿ���� �� �÷��� ����
        textDi.text = "";  // �ؽ�Ʈ UI �ʱ�ȭ
        foreach (char letter in currentLine)
        {
            textDi.text += letter;

            if (letter == ',')
            {
                textDi.text += "\n";  // '/' ���� ��� �ٹٲ� �߰�
            }
            yield return new WaitForSeconds(typingSpeed);  // �� ���ھ� ����ϴ� �ð��� ���� ����.
        }

        isTyping = false;  // Ÿ���� �Ϸ� �� �÷��� ����
    }
    IEnumerator TextPosEnd()
    {
        //Ŭ���� üũ�� �ϴ� ���� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ� ���
        if (questManager.questClearMark[questManager.questProgress].activeSelf)
        {
            //��Ż�� Ż��� ���� ��Ȱ��ȭ �Ǳ� ������ "������ ����"�� ���� ������� �Ʒ��ڵ� ���� 
            if (!questManager.questArea)
            {
                Debug.Log("����");
                questManager.g_Quest_situation_Arear_Icon[0].gameObject.SetActive(true);
                questManager.g_Quest_situation_Arear_Icon[1].gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("����");
                questManager.g_Quest_situation_Arear_Icon[3].gameObject.SetActive(true);
                questManager.g_Quest_situation_Arear_Icon[4].gameObject.SetActive(false);
            }

            questManager.f_textPos = -303f;
        }
        else//��Ȱ��ȭ �Ǿ� �ִ°��
        {
            if (!questManager.questArea)
            {
                Debug.Log("����");
                questManager.g_Quest_situation_Arear_Icon[1].gameObject.SetActive(true);
                questManager.g_Quest_situation_Arear_Icon[0].gameObject.SetActive(false);
                questManager.g_Quest_situation_Arear_Icon[2].gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("����");
                questManager.g_Quest_situation_Arear_Icon[4].gameObject.SetActive(true);
                questManager.g_Quest_situation_Arear_Icon[3].gameObject.SetActive(false);
                questManager.g_Quest_situation_Arear_Icon[5].gameObject.SetActive(false);
            }
            questManager.quest[questManager.questProgress].SetActive(true);//����Ʈ ����
            questManager.f_textPos = -359f;
        }
        yield return null;
        questManager.dialogueGameObject.SetActive(false);
    }
}

