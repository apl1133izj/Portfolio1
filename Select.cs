using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Select : MonoBehaviour
{
    public GameObject playerPos;
    public HP hP;
    public PlayerUI playerUI;
    public GameObject creat;	// �÷��̾� �г��� �Է�UI
    public TextMeshProUGUI[] slotText;		// ���Թ�ư �Ʒ��� �����ϴ� Text��
    public TextMeshProUGUI[] saveTimeText;
    public TextMeshProUGUI newPlayerName;  // ���� �Էµ� �÷��̾��� �г���

    public TMP_InputField playerNameInput;
    public string[] saveSlotName;
    public string[] saveTimeLoad;
    public bool[] savefile = new bool[3];	// ���̺����� �������� ����
    public static int slaotArr;
    public DataManager dataManager;
    public PutitemBag putitemBag;
    public GameObject saveGameObject;
    //��ũ����
    public float screenShotTime;
    public Canvas saveCanvas;//����� ȭ��ĸ�� Canvas ��Ȱ��ȭ ��Ű��
    public Image[] screenShotImage;
    public Sprite[] screenShotSprite;
    public Image writingImage;
    public GameObject playerState;
    public GameObject startMenu;

    void Start()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
        UpdateFile();
    }

    void UpdateFile()
    {

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
            {
                Debug.Log(DataManager.instance.path + i);
                savefile[i] = true; //������ ���� �Ѵ� = true
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();    // �ش� ���� ������ �ҷ���
                string filePath = $"{Application.persistentDataPath}/save{DataManager.instance.nowSlot}.png";
                screenShotImage[i].gameObject.SetActive(true);
                slotText[i].text = DataManager.instance.nowPlayer.name;
                saveTimeText[i].text = DataManager.instance.nowPlayer.saveTime;
                // Texture2D�� ���� �ý��ۿ��� �ε�(������ �÷����ϴ� �÷��̾��� ��ǻ�� ��ο� �´� ��� ����
                byte[] fileData = File.ReadAllBytes(filePath);
                playerPos.transform.position += DataManager.instance.nowPlayer.plyerPosition;
                Texture2D texture = new Texture2D(2, 2);

                if (texture.LoadImage(fileData))
                {
                    screenShotSprite[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    screenShotImage[i].sprite = screenShotSprite[i];
                }
                else
                {
                    Debug.LogError($"Failed to load texture from file data at '{filePath}'.");
                }
            }
            else	// �����Ͱ� ���� ���
            {
                Debug.Log(DataManager.instance.path);
                slotText[i].text = "�� �����";
                saveTimeText[i].text = "";
            }
        }

        // �ҷ��� �����͸� �ʱ�ȭ��Ŵ.(��ư�� �г����� ǥ���ϱ������̾��� ����)
        DataManager.instance.DataClear();
    }
    public void DestoryFile(int sloat)
    {
        Debug.Log("���� ��ư ����");
        string fullPath = Application.persistentDataPath + "/save" + sloat.ToString();
        string relativePath = Application.persistentDataPath + "/save" + sloat + ".png";
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            File.Delete(relativePath);
            DataManager.instance.nowPlayer = new PlayerData();
            screenShotImage[sloat].sprite = null;
            screenShotImage[sloat].gameObject.SetActive(false);
            playerUI.hptext[sloat].text = "0";
            playerUI.staminatext[sloat].text = "0";
            playerUI.skillPointtext[sloat].text = "0";
            playerUI.killedbosstext[sloat].text = "0";
            playerUI.positionHptext[sloat].text = "0";
            playerUI.positionStaminatext[sloat].text = "0";
            playerUI.savePointText[sloat].text = "0";
            saveTimeText[sloat].text = "";
            savefile[sloat] = false;
            slotText[sloat].text = "����� ����� ����";
            Debug.Log("���� ����: " + fullPath);
            Debug.Log("���� ����: " + relativePath);
        }
        else
        {
            Debug.Log("������ �����Ͱ� ����: " + fullPath);
        }

    }
    public void Slot(int slaot)	// ������ ��� ����
    {
        if (playerUI._savePoint[0] >= 1 || playerUI._savePoint[1] >= 1 ||
            playerUI._savePoint[2] >= 1)
        {
            DataManager.instance.nowSlot = slaot;   // ������ ��ȣ�� ���Թ�ȣ�� �Է���.
            slaotArr = slaot;
            if (savefile[slaot])    // bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
            {
                Debug.Log("����");
                startMenu.gameObject.SetActive(false);
                playerState.gameObject.SetActive(true);
                DataManager.instance.nowSlot = slaot;
                DataManager.instance.LoadData();    // �����͸� �ε��ϰ�

                GoGame();   // ���Ӿ����� �̵�
            }
            else    // bool �迭���� ���� ���Թ�ȣ�� false��� �����Ͱ� ���ٴ� ��
            {
                Creat();    // ���� �̸� �Է� UI Ȱ��ȭ
            }
        }
    }

    public void Creat()	// �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        creat.gameObject.SetActive(true);
    }

    public void DeCreat()	// �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        saveGameObject.gameObject.SetActive(false);
    }

    public void GoGame()	// ���Ӿ����� �̵�
    {
        if (!savefile[DataManager.instance.nowSlot])	// ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            DataManager.instance.nowPlayer.itemnameData = putitemBag.itemNameString;
            DataManager.instance.nowPlayer.itemCountData = putitemBag.itemCount;
            DataManager.instance.nowPlayer.name = newPlayerName.text; // �Է��� �̸��� �����ؿ�
            DataManager.instance.nowPlayer.plyerPosition = playerPos.transform.position; //�÷��̾� ��ġ ����  
            DateTime now = DateTime.Now;
            saveTimeLoad[slaotArr] = now.ToString("yyyy-MM-dd HH:mm:ss");                //�ð� ����
            DataManager.instance.nowPlayer.saveTime = saveTimeLoad[slaotArr]; // �Է��� �̸��� �����ؿ�
            playerUI._savePoint[0] -= 1;
            playerUI._savePoint[1] -= 1;
            playerUI._savePoint[2] -= 1;
            DataManager.instance.nowPlayer.savePoint--;
            DataManager.instance.SaveData(); // ���� ������ ������.
            StartCoroutine(Screenshot());
            GameLoad.gameLoad = true;
        }
        else
        {
            playerState.gameObject.SetActive(true);
            GameLoad.gameLoad = true;
            startMenu.gameObject.SetActive(false);
            SceneManager.LoadScene(1); // ���Ӿ����� �̵�  
        }
    }

    IEnumerator Screenshot()
    {
        saveCanvas.enabled = false;
        //string relativePath = $"Assets/Resources/Sceenshot/save{DataManager.instance.nowSlot}.png";
        string relativePath = $"{Application.persistentDataPath}/save{DataManager.instance.nowSlot}.png";
        ScreenCapture.CaptureScreenshot(relativePath);

        yield return new WaitForSeconds(2f);
        saveCanvas.enabled = true;
        writingImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        writingImage.gameObject.SetActive(false);
        SceneManager.LoadScene(1);

    }
}
