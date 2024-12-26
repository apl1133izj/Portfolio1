using System.IO;//�ܺ� ���� �������� json
using UnityEngine;
//�����ϴ� ���
//1.������ �����Ͱ� ����
//2.�����͸� ���̽����� ��ȯ
//3. ���̽��� �ܺξ� ����

//�ҷ����� ���
//1. �ܺο� ����� ���̽��� ������
//2. ���̽��� ������ ���·� ��ȯ
//3. �ҷ��� �����͸� ���


//������
//�߾����� ����� �� ������ ���� �������� �����(����: ���� �ϸ鼭 �ϰ� �־� �����ϸ鼭 �ϰ� �;) 
public class PlayerData
{
    
    //�̸�,�÷��̾� ����,����Ʈ �������  
    #region �÷��̾� ����
    public string name;
    public Vector3 plyerPosition;
    public float hpData;
    public float hpMaxData;
    public float staminaData;
    public float staminaMaxData;
    public int hpPositionData;
    public int staminaPositionData;
    public int skillPointData;
    public int useSkillPointData;
    public int skillTree;
    public int[] skillLevelsData = new int[14];
    #endregion
    #region ���� UI����
    public int savePoint;
    public string saveTime;
    #endregion
    #region ����Ʈ �������
    public int questProgressData;
    #endregion
    #region ���� ������
    public int[] itemCountData = new int[91];  // ���� 3��, ���� 91���� �������� ������ �� ����
    public string[] itemnameData = new string[91];
    public int[] skillEnhanceCount = new int[6];
    #endregion
    #region ����
    public float curseProgressionData;
    #endregion
    #region �Ҹ�
    public float backgroundVolume;
    public float playerVolume;
    #endregion
    #region Ʃ�丮��
    public bool movetip;
    public bool startmenu;
    #endregion
}
public class DataManager : MonoBehaviour
{
    //�̱���
    public static DataManager instance;
    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot; // ���� ���Թ�ȣ
    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
        path = Application.persistentDataPath + "/save";
    }
    public void SaveData()
    {
        //���� ����  
        string data = JsonUtility.ToJson(nowPlayer); //���� �����
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("Data saved: " + data); // ����� ������ Ȯ��
    }
    public void LoadData()
    {
        // ���� ���
        string filePath = path + nowSlot.ToString();

        // ���� ���� ���� Ȯ��
        if (File.Exists(filePath))
        {
            // ������ �����ϸ� �����͸� �ε�
            string data = File.ReadAllText(filePath);
            Debug.Log(nowSlot.ToString() + ": Data �ε� �� Ȯ��: " + data); // ����� ������ Ȯ��
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
        else
        {
            // ������ ������ ��� �޽��� ���
            Debug.Log($"�÷��̷��� ���������� ����: ����� ������ ������ �������� �ʽ��ϴ�. ���: {filePath}");
            nowPlayer = new PlayerData(); // ������ �ʱ�ȭ
        }
    }
    public void DataClear()
    {
        //nowSlot = -1;
        nowPlayer = new PlayerData(); //�ʱⰪ
    }
}
