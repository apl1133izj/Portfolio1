using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    GameObject player;
    HP hP;
    Stamina stamina;
    public bool postionCheck;
    public bool armerCheck;
    public int itemCount;
    // ���� ������ Ÿ��
    public ItemType.ItemTypes allowedItemType;
    public bool isEquipmentSlot = false;  // ��� �������� ���θ� �����ϴ� ����

    void Awake()
    {
        player = GameObject.Find("Player");
        hP = player.GetComponent<HP>();
        stamina = player.GetComponent<Stamina>();
    }
    public GameObject Item()
    {
        if (transform.childCount <= 1)//������ �ټ��� 1�� �̻��ϰ�� ���� ��ȯ
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            else
                return null;
        }

        return null;//���Կ� �������� ���� ��� �̵� ���� ���ϵ��� ��
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = ItemDrog.beginDraggedItem;

        if (draggedItem != null)
        {
            // �巡�׵� �������� Ÿ�� Ȯ��
            ItemType itemType = draggedItem.GetComponent<ItemType>();

            // ������ �ڽ� ���� Ȯ��
            if (transform.childCount >= 2) // �ڽ��� 2�� �̻��� ���
            {
                //���� �θ� ���Կ� �ǵ�����
                ItemDrog itemDrog = draggedItem.GetComponent<ItemDrog>();
                draggedItem.transform.SetParent(itemDrog.startParent);
                draggedItem.transform.position = itemDrog.startPosition;
                Debug.Log("������ �� á���ϴ�. �������� ���� �� �����ϴ�.");
                return; // �������� ���Կ� ���� �ʰ� ����
            }

            if (isEquipmentSlot)
            {
                if (itemType != null)
                {
                    // �巡�׵� �������� ���Կ� ��ġ
                    draggedItem.transform.SetParent(transform);
                    draggedItem.transform.position = transform.position;
                }
                else
                {
                    Debug.Log($"�� ���Կ��� {allowedItemType} Ÿ�Ը� ���� �� �ֽ��ϴ�.");
                }
            }
            else
            {
                if (itemType != null)
                {
                    // ������ �����̸� Ÿ�� ������� �ֱ�
                    draggedItem.transform.SetParent(transform);
                    draggedItem.transform.position = transform.position;
                }
            }
        }
    }

}


