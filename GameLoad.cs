using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public static bool gameLoad;
    public bool game;
    public GameObject StartMenu;
    public GameObject playerState;
    public GameObject tutorials;

    public HP GetHP;
    public Stamina GetStamina;
    public AttackAndCombo GetAttackAndCombo;
    public PutitemBag GetPutitemBag;
    public Potion GetPotion;
    public QuestManager GetQuestManager;
    public KeyTip GetKeyTip;
    public Skill GetSkill;
    private void Start()
    {
        Debug.Log("gameLoad" + gameLoad);

        if (gameLoad)
        {

            StartMenu.gameObject.SetActive(false);
            tutorials.gameObject.SetActive(false);
            playerState.gameObject.SetActive(true);
        }
        // ������ �ε�


        // ������ ����
        if (DataManager.instance.nowPlayer != null)
        {
            DataManager.instance.LoadData();
            ApplyPlayerData();
        }
    }
    private void Update()
    {
        game = gameLoad;
        if (gameLoad && GetHP != null)
        {

            StartMenu.gameObject.SetActive(false);
            tutorials.gameObject.SetActive(false);
            playerState.gameObject.SetActive(true);
        }

    }
    // �ε�� �����͸� ������� �÷��̾� ���¸� ����
    private void ApplyPlayerData()
    {
        // ����: �÷��̾� ��ġ, ü�� ����
        GetHP.hp = DataManager.instance.nowPlayer.hpData;
        GetHP.hpMax = DataManager.instance.nowPlayer.hpMaxData;
        GetStamina.stamina = DataManager.instance.nowPlayer.staminaData;
        GetStamina.staminaMax = DataManager.instance.nowPlayer.staminaMaxData;
        GetAttackAndCombo.savePos = DataManager.instance.nowPlayer.plyerPosition;
        GetPutitemBag.itemNameString = DataManager.instance.nowPlayer.itemnameData;
        GetPutitemBag.itemCount = DataManager.instance.nowPlayer.itemCountData;
        GetQuestManager.questProgress = DataManager.instance.nowPlayer.questProgressData;
        GetKeyTip.firstMove = DataManager.instance.nowPlayer.movetip;
        GetSkill.skillEnhanceCount = DataManager.instance.nowPlayer.skillEnhanceCount;
        // �߰��� �������̳� ����Ʈ ���� � ���� ����
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
