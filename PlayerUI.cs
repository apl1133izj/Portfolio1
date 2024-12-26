using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public GameObject[] postionTypeImage;//�����̹��� �ٲٱ�
    public Image[] potionImageA;//���� �̹���
    public Image[] previewBar;

    public HP hPS;
    public Stamina staminaS;
    public Potion potionS;
    public SkillTree skillTreeS;
    public PutitemBag putitemBagS;

    public Text textTypingSpeedText;//Ÿ���� �ӵ� ���� �ӵ� �ȳ� �� �����̽��ٸ� �������� �ƴ����� UI�� ǥ��
    public Text announcementText;//��ȭ�� �ȳ�����
    public float announcementTime;//�����ð��� ������ �ȳ����ڰ� �ٲ�ϴ�

    //���콺 ����
    [Header("���콺")]
    public Texture2D customCursor;
    public Texture2D clikCursor;
    public Vector2 hotSpot = Vector2.zero; // Ŀ���� �ֽ��� ��ġ
    public bool uiOpen; //���콺 Ŀ���� ����Ҽ��ְ� �˴ϴ�
    public CursorMode cursorMode = CursorMode.Auto;


    int menuCount;
    public int bagCount;
    public GameObject menuGameObject;
    public GameObject bagGameObject;
    public GameObject dataUIGameObject;
    //���� ����
    public int[] _savePoint;
    public TextMeshProUGUI[] savePointText;//�ð� ���� ������ �Ҽ��ִ� ����Ʈ�� ����
    public TextMeshProUGUI[] savePointTimeText;//�ð� ���� ������ �Ҽ��ִ� ����Ʈ�� �ð��� �����ִ� UI
    public bool[] savePointSaveFile;
    public bool[] savePointTimeSaveFile;
    public TextMeshProUGUI[] staminatext;
    public TextMeshProUGUI[] hptext;
    public TextMeshProUGUI[] skillPointtext;
    public TextMeshProUGUI[] killedbosstext;
    public TextMeshProUGUI[] positionHptext;
    public TextMeshProUGUI[] positionStaminatext;
    //����,ȿ����
    public AudioSource[] backgroundAudio;
    public Slider backgroundSlider;
    public AudioSource templeMapAudio;
    public AudioSource playerAudio;
    public Slider playerSlider;
    public AudioClip buttonClip;
    public GameObject startMenu;
    //�þ� ����(������)
    public Camera camera;
    public Slider playerVisualrangeSlider;
    public TextMeshProUGUI performance;
    //����
    public SkillItemFusion skillItemFusion;
    public GameObject curseGameObject;//���ְ� ���� ������ ���� ���ӿ�����Ʈ �� ��Ȱ��ȭ �ϱ� ���� ���ӿ�����Ʈ
    public Image Image_CurseProgression;//����
    public TextMeshProUGUI[] curseText;
    public float f_CurseProgression;
    public float f_Maxf_CurseProgression = 5000;
    public float f_Curse = 1;
    public bool curseRemoval = false;
    //���� óġ
    public int boseKillNum;

    //����� ���� tip
    //������
    public bool b_tip;//ó�� ���Ϳ�(����� ����) ���� �������� ���п��� Ǯ���� ����� �˷���
    public GameObject g_tipUI;
    public TextMeshProUGUI spaceCount;

    public GameObject g_cheat;
    private bool isCheatEnabled = false; // ġƮ ��� ���¸� �����ϴ� ����

    //����
    public GameObject[] potionSlot;
    public AudioClip bagSound;

    public GameObject playerDie;
    private void Start()
    {
        Application.targetFrameRate = 90; // 60 FPS ����
        QualitySettings.vSyncCount = 1;  // VSync�� ������ �ӵ� ����
        performance.text = "����";
        /*        for (int r = 0; r < 3; r++)
                {
                    if (File.Exists(DataManager.instance.path + $"{r}"))	// �����Ͱ� �ִ� ���
                    {
                        Debug.Log("O");
                        DataManager.instance.nowSlot = r; // ���� ���� ����
                        DataManager.instance.LoadData(); // �����͸� �ش� ���Կ��� �ҷ���
                        //f_CurseProgression = DataManager.instance.nowPlayer.curseProgressionData;
                    }
                    else
                    {
                        f_CurseProgression = 5000;
                    }
                }*/
        Cursor.SetCursor(customCursor, hotSpot, cursorMode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadGameData();
    }
    //������ ĭ�� ������ �����������
    void PotionBagIn()
    {
        //HP
        if (potionSlot[0].transform.childCount > 1)
        {
            potionImageA[0].gameObject.SetActive(true);
        }
        else
        {
            potionImageA[0].gameObject.SetActive(false);
        }
        //Stamina
        if (potionSlot[1].transform.childCount > 1)
        {
            potionImageA[1].gameObject.SetActive(true);
        }
        else
        {
            potionImageA[1].gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.nowPlayer.curseProgressionData = f_CurseProgression;
        }
        if (!startMenu.activeSelf)
        {
            templeMapAudio.enabled = true;
        }
        if (!hPS.isDie)
        {
            Posion();
            PotionBagIn();
            MenuOpen();
            BagOpen();
            ConversationStatus();
            Mouse();
            Curse();
        }


        g_cheat.SetActive(CheateOpen());
    }
    void Curse()
    {
        if (!curseRemoval)//f_Maxf_CurseProgression�� 0�� �Ǹ� ���ְ� ������f_CurseProgression0�� �Ǹ� �����
        {
            if (f_CurseProgression >= 0)
            {
                float curseReductionRate = 0.5f + f_Curse + skillItemFusion.curseHap.Take(6).Sum();
                f_CurseProgression -= curseReductionRate * Time.fixedDeltaTime;
            }

            int curseProgress = Mathf.CeilToInt(f_CurseProgression);
            int curseMaxProgress = Mathf.CeilToInt(f_Maxf_CurseProgression);
            curseText[0].text = curseProgress.ToString();
            curseText[1].text = curseMaxProgress.ToString();
            Image_CurseProgression.fillAmount = f_CurseProgression / f_Maxf_CurseProgression;
            curseGameObject.SetActive(true);
        }
        else
        {
            curseGameObject.SetActive(false);
        }

        if (f_CurseProgression <= 0)
        {
            hPS.isDie = true;
        }
        else
        {
            hPS.isDie = false;
        }
        if (f_Maxf_CurseProgression > 0)
        {
            curseRemoval = false;
        }
        else
        {
            curseRemoval = true;
        }
    }
    void Mouse()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(clikCursor, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(customCursor, hotSpot, cursorMode);
        }
    }

    public void LoadGameData()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savePointSaveFile[i] = true;
                savePointTimeSaveFile[i] = true;
                _savePoint[i] = DataManager.instance.nowPlayer.savePoint;
                savePointText[i].text = DataManager.instance.nowPlayer.savePoint.ToString();
                DataManager.instance.nowPlayer.backgroundVolume = backgroundSlider.value;
                DataManager.instance.nowPlayer.playerVolume = playerSlider.value;
                staminatext[i].text = staminaS.stamina.ToString();
                hptext[i].text = hPS.hp.ToString();
                skillPointtext[i].text = skillTreeS.skillPoint.ToString();
                killedbosstext[i].text = boseKillNum.ToString();
                positionHptext[i].text = potionS.hpPotion.ToString();
                positionStaminatext[i].text = potionS.staminaPotion.ToString();
            }
            else
            {
                savePointText[i].text = "1";
            }
        }
    }
    public void Posion()
    {
        if (putitemBagS.itemCount[0] != 0)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                postionTypeImage[0].SetActive(true);
                postionTypeImage[1].SetActive(false);
                StartCoroutine(PreviewBar("Hp"));
            }
        }
        if (putitemBagS.itemCount[1] != 0)
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                postionTypeImage[0].SetActive(false);
                postionTypeImage[1].SetActive(true);
                StartCoroutine(PreviewBar("Stamina"));
            }
        }
    }

    //���� ���� ���� ������ �̸�����
    IEnumerator PreviewBar(string previewBarName)
    {
        if (previewBarName == "Hp")
        {
            putitemBagS.itemCount[0] -= 1;
            previewBar[0].fillAmount += 0.2f;
            //previewBar[0].fillAmount = (hPS.hp + potionS.hpPostionRecovery) / hPS.hpMax;
        }
        else if (previewBarName == "Stamina")
        {
            putitemBagS.itemCount[1] -= 1;
            //previewBar[1].fillAmount = (staminaS.stamina + potionS.staminaPostionRecovery) / staminaS.staminaMax;
            previewBar[1].fillAmount += 0.2f;
        }
        yield return new WaitForSeconds(3.8f);
        if (previewBarName == "Hp")
        {
            previewBar[0].fillAmount = hPS.hp / hPS.hpMax;
        }
        else if (previewBarName == "Stamina")
        {
            previewBar[1].fillAmount = staminaS.stamina / staminaS.staminaMax;
        }
    }
    bool CheateOpen()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return isCheatEnabled = true;

        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return isCheatEnabled = false;
        }
        return isCheatEnabled;
    }
    public void MenuOpen()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCount += 1;
        }

        if (menuCount == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            menuGameObject.SetActive(true);
            uiOpen = true;
        }
        else if (menuCount == 2)
        {
            menuGameObject.SetActive(false);
            uiOpen = false;
            menuCount = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    public void BagOpen()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.PlaySound(bagSound);
            bagCount += 1;
        }

        if (bagCount == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            bagGameObject.transform.localScale = Vector3.one;
            uiOpen = true;
        }
        else if (bagCount == 2)
        {

            bagGameObject.transform.localScale = Vector3.zero;
            uiOpen = false;
            bagCount = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
    void ConversationStatus()
    {
        if (uiOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public void Audio(int _type)
    {
        if (_type == 1)
        {
            for (int i = 0; i <= 5; i++)
                backgroundAudio[i].volume = backgroundSlider.value;
        }

        if (_type == 2) playerAudio.volume = playerSlider.value;
    }
    public void VisualRange()
    {

        camera.farClipPlane = playerVisualrangeSlider.value * 800;

        if (camera.farClipPlane >= 800)
        {
            performance.text = "�ſ� ����";
        }
        else if (camera.farClipPlane >= 400)
        {
            performance.text = "����";
        }
        else if (camera.farClipPlane >= 200)
        {
            performance.text = "����";
        }
        else if ((camera.farClipPlane >= 100))
        {
            performance.text = "����";
        }
    }

    public void DataUI()
    {
        dataUIGameObject.SetActive(true);
        SoundManager.PlaySound(buttonClip);
    }
    public void DeDataUI()
    {
        SoundManager.PlaySound(buttonClip);
        menuCount = 2;
    }


}
