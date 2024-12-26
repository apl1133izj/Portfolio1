using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    CanvasGroup group;
    private GraphicRaycaster gr;
    private PointerEventData ped;
    GameObject onDragParentGameObject;
    [SerializeField] Transform onDragParent;
    [HideInInspector] public Transform startParent;
    public GameObject itemToolTip;
    public bool fillGauge;
    public bool rayHit;

    PutitemBag putItemBag;


    /*[0("ü��")]
      [1("���׹̳�")]
      [2("�ӵ�")]
      [3("ü�� ���")]
      [4("��� ���")]
      [5("����")]
      [6("���ݷ�")]
      [7("����")]
      [8("���� �ӵ�")]
    */
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI[] specText;
    public TextMeshProUGUI[] items;
    public TextMeshProUGUI postion;
    [Header("ü��")]
    public int itemSpec_1;
    [Header("���׹̳�")]
    public int itemSpec_2;
    [Header("�ӵ�")]
    public int itemSpec_3;
    [Header("ü�� ���")]
    public int itemSpec_4;
    [Header("��� ���")]
    public int itemSpec_5;
    [Header("����")]
    public float itemSpec_6;
    [Header("���ݷ�")]
    public int itemSpec_7;
    [Header("����")]
    public int itemSpec_8;
    [Header("���� �ӵ�")]
    public int itemSpec_9;
    [Header("������ ����")]
    public string description;
    void Start()
    {
        onDragParentGameObject = GameObject.Find("Storage space");
        onDragParent = onDragParentGameObject.GetComponent<Transform>();
        group = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        putItemBag = FindAnyObjectByType<PutitemBag>();
        gr = GetComponent<GraphicRaycaster>();
    }
    private void Update()
    {

        Ray();
        ToolTip();

    }
    void Ray()
    {
        var ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        // GraphicRaycaster�� ���� Raycast ����
        gr.Raycast(ped, results);
        itemToolTip.transform.position = ped.position;

        rayHit = false;
        // Raycast ��� Ȯ��
        foreach (var hitItem in results)
        {
            rayHit = true;
            // ���콺 Ŭ�� ���� Ȯ�� && ���̰� �� ���� ���� ������Ʈ�� layer�� �������� ���
            for (int i = 0; i < hitItem.gameObject.transform.childCount; i++)
            {
                if (hitItem.gameObject.transform.childCount > 1)
                {
                    Transform item = hitItem.gameObject.transform.GetChild(1);
                    ItemPerformance itemPerformance = item.GetComponent<ItemPerformance>();
                    ItemDrog itemDrog = item.GetComponent<ItemDrog>();
                    if (itemDrog != null)
                    {
                        if (itemDrog.f_itemclikTime >= 0.9f)
                        {
                            fillGauge = true;
                        }
                        else
                        {
                            fillGauge = false;
                        }
                    }
                    if (itemPerformance != null)
                    {
                        postion.gameObject.SetActive(false);
                        if (itemPerformance.transform.GetChild(1))
                        {
                            itemName.text = itemPerformance.name;
                            object[] itemSpecs = new object[9]
                            {
                                    itemPerformance.itemSpec_1, // int
                                    itemPerformance.itemSpec_2, // int
                                    itemPerformance.itemSpec_3, // int
                                    itemPerformance.itemSpec_4, // int
                                    itemPerformance.itemSpec_5, // int
                                    itemPerformance.itemSpec_6, // float
                                    itemPerformance.itemSpec_7, // int
                                    itemPerformance.itemSpec_8, // int
                                    itemPerformance.itemSpec_9,  // int
                            };
                            Vector2 startPosition = new Vector2(0, 0); // �ʱ� ���� ��ġ
                            float verticalSpacing = 0.1f; // �� ��� ���� ���� ����

                            int activeIndex = 0; // Ȱ��ȭ�� ��Ҹ� �����ϱ� ���� �ε���
                            for (int r = 0; r < itemSpecs.Length; r++)
                            {
                                if (itemSpecs[r] is int intValue && intValue > 0) // int ���� 0 �̻��� �� ó��
                                {
                                    specText[activeIndex].text = intValue.ToString();
                                    specText[activeIndex].transform.localPosition = startPosition - new Vector2(-60, verticalSpacing * activeIndex);
                                    specText[activeIndex].gameObject.SetActive(true);
                                    items[activeIndex].gameObject.SetActive(true);
                                    activeIndex++;
                                }
                                else if (itemSpecs[r] is float floatValue && floatValue > 0) // float ���� 0 �̻��� �� ó��
                                {
                                    specText[activeIndex].text = floatValue.ToString("F3");
                                    specText[activeIndex].transform.localPosition = startPosition - new Vector2(-60, verticalSpacing * activeIndex);
                                    specText[activeIndex].gameObject.SetActive(true);
                                    items[activeIndex].gameObject.SetActive(true);
                                    activeIndex++;
                                }
                                else
                                {
                                    specText[r].gameObject.SetActive(false); // ��Ȱ��ȭ
                                    items[r].gameObject.SetActive(false);
                                }
                            }

                            // ������ ��Ȱ��ȭ
                            for (int s = activeIndex; s < specText.Length; s++)
                            {
                                specText[s].gameObject.SetActive(false);
                                items[s].gameObject.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        for (int r = 0; r < items.Length; r++)
                        {
                            items[r].gameObject.SetActive(false);
                        }
                        postion.gameObject.SetActive(true);
                        if (item.gameObject.name == "�������� ����")
                        {

                            itemName.text = item.name;
                            postion.text = "������ 5 ������ �ִ� ���� �Դϴ�." +
                                            "���콺 ������ ��ư�� ������ ��� ���� �մϴ�.";
                        }
                        else if (item.gameObject.name == "HpPotion")
                        {
                            itemName.text = item.name;
                            postion.text = "ü���� ȸ�� �ϴ� ���� �Դϴ�. ���� ĭ�� �ڵ� ���� �ʴϴ�";
                        }
                        else if (item.gameObject.name == "StaminaPotion")
                        {
                            itemName.text = item.name;
                            postion.text = "����� ȸ�� �ϴ� ���� �Դϴ�. ���� ĭ�� �ڵ� ���� �ʴϴ�";
                        }
                        else if (item.gameObject.name == "CompensationHpPotion")
                        {
                            itemName.text = item.name;
                            postion.text = "ü���� ȸ�� �ϴ� ���� �Դϴ�. ���� ĭ�� �ڵ� ���� �ʴϴ�";
                        }
                        else if (item.gameObject.name == "��ȥ�� ��ȯ��")
                        {
                            itemName.text = item.name;
                            postion.text = "���� Ư���� ���� ������ �ִµ� �׾ ����� ���� ä �ٽ� �¾ �� �ֽ��ϴ�.[������������ ����Ҽ� �����ϴ�]";
                        }
                        else
                        {
                            for (int r = 0; r < items.Length; r++)
                            {
                                items[r].gameObject.SetActive(true);
                            }
                            postion.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    void ToolTip()
    {
        if (rayHit)
        {
            itemToolTip.gameObject.SetActive(true);
        }
        else
        {
            itemToolTip.gameObject.SetActive(false);
        }
    }
}
