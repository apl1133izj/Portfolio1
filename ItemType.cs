using TMPro;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum ItemTypes
    {
        hpPosion, staminPostion,
        Helmet,       // �Ӹ�
        Torso,      // ��ü
        ArmsRight,  // ���� ��
        ArmsLeft,  // �� ��
        Legs,       // ��ü
        Feet,        // ��
        saveItem,
        Skill
    }
    public ItemTypes itemtype;
    public TextMeshProUGUI itemCount_Text;
    PutitemBag putitemBag;
    TMP_Text tmpText;  // TMP_Text ����
    public int get_itemcount;
    public string displayedText;
    public string itemName;
    Potion position;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        position = player.GetComponent<Potion>();
        putitemBag = FindAnyObjectByType<PutitemBag>();
        itemCount_Text = GetComponentInChildren<TextMeshProUGUI>();
        tmpText = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        // TMP_Text���� ���� �ؽ�Ʈ �� ��������
        itemCount_Text.text = get_itemcount.ToString();
        displayedText = itemCount_Text.text;
        itemName = gameObject.name;
        position.hpText.text = putitemBag.itemCount[81].ToString();
        position.staminaText.text = putitemBag.itemCount[82].ToString();
        if(get_itemcount == 0)
        {
            Destroy(gameObject);
        }

    }
    public int GetItemCount()
    {
        return get_itemcount;
    }
    public void DecreaseItemCount(int amount)
    {
        if (get_itemcount - amount >= 0) // 0 ���Ϸ� �������� �ʵ��� ����
        {
            get_itemcount -= amount;
            Debug.Log($"Item count decreased by {amount}. New count: {get_itemcount}");
        }
        else
        {
            Debug.LogWarning("Cannot decrease item count below 0.");
        }
    }
}
