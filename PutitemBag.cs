using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class PutitemBag : MonoBehaviour
{
    public GameObject[] ItemSlot;
    GameObject item;
    public Transform[] itemPos;
    public GameObject[] insItem;
    private bool isCoroutineRunning = false;
    public bool ispluseItem = false;
    public string itemTag;                   //������ �ױ� Ȯ��
    public string[] itemNameString;
    public int itemTypeNum;
    public int[] itemCount;          //������ �ټ�
    public string[] s_itemCount;
    string itemnameUpdate;
    public Select select;
    public int bagSize = 90;
    public TextMeshProUGUI bagSizeUI;
    public Canvas blocksRaycastsCheckCanvas;
    public int firstStackPostionCount;
    public int firstSavePostionCount;
    public AudioClip itemEatSound;
    private void Start()
    {
        if (DataManager.instance != null)
        {
            Debug.Log("����� �������� �ִ��� Ȯ��");
            itemNameString = DataManager.instance.nowPlayer.itemnameData;
            itemCount = DataManager.instance.nowPlayer.itemCountData;
            for (int j = 0; j < itemNameString.Length; j++)
            {
                //����) 0�Է� ������ ����ɰ�찡 ���� 
                //������� ���� ���
                if (!string.IsNullOrEmpty(itemNameString[j]) && itemNameString[j] != "0")
                {
                    // ������ ������Ʈ ����
                    GameObject armorPrefab = Resources.Load<GameObject>($"Item/{itemNameString[j]}");
                    BagSize();
                    GameObject item = Instantiate(armorPrefab, ItemSlot[j].transform);
                    item.name = itemNameString[j];
                    ItemType itemType = item.GetComponent<ItemType>();
                    itemType.get_itemcount = itemCount[j];
                    // ������ �ν��Ͻ� ����
                    /*                        InsItem(itemNameString[j], j);
                                            item.name = "Item";
                                            // ����� �α� ���
                                            Debug.Log(j + ": " + itemNameString[j]);*/
                }
            }
        }

    }
    //�����ۻ���(������ �ױ� ����,������ ��ġ ����)
    public void InsItem(string _itemTag, int _itemPos)
    {

        if (_itemTag == "HpPosion")
        {
            item = Instantiate(insItem[0], itemPos[_itemPos]);
        }
        else if (_itemTag == "StaminaPosion")
        {
            item = Instantiate(insItem[1], itemPos[_itemPos]);
        }
        else if (_itemTag == "Helmet")
        {
            item = Instantiate(insItem[2], itemPos[_itemPos]);
        }
        else if (_itemTag == "Torso")
        {
            item = Instantiate(insItem[3], itemPos[_itemPos]);
        }
        else if (_itemTag == "ArmsRight")
        {
            item = Instantiate(insItem[4], itemPos[_itemPos]);
        }
        else if (_itemTag == "ArmsLeft")
        {
            item = Instantiate(insItem[5], itemPos[_itemPos]);
        }
        else if (_itemTag == "Legs")
        {
            item = Instantiate(insItem[6], itemPos[_itemPos]);
        }
        else if (_itemTag == "Feet")
        {
            item = Instantiate(insItem[7], itemPos[_itemPos]);
        }
    }
    private void Update()
    {
        ItemNameUpdate();
        ItemUpdate();
        if (DataManager.instance != null)
        {
            DataManager.instance.nowPlayer.itemCountData = itemCount;
            DataManager.instance.nowPlayer.itemnameData = itemNameString;
        }
    }


    public void ItemNameUpdate()
    {
        for (int i = 0; i < ItemSlot.Length; i++) // ��� ���� ��ȸ
        {
            // ���Կ� �ڽ� ��ü�� �ִ� ���
            if (ItemSlot[i].transform.childCount > 1)
            {
                Transform child1 = ItemSlot[i].transform.GetChild(1); // �ڽ� �ε��� 1 ��������
                if (child1.gameObject.layer == 18) // �ùٸ� ���̾� Ȯ��
                {
                    ItemType itemType = child1.GetComponent<ItemType>(); ;
                    if (itemType != null)
                    {
                        itemNameString[i] = itemType.itemName;   // ������ �̸� ����ȭ
                    }
                }
            }
            else // ������ ��� �ִ� ���
            {

                itemNameString[i] = "";        // �̸� ���� �ʱ�ȭ
                itemCount[i] = 0;
            }
        }
    }

    public void ItemUpdate()
    {
        for (int i = 0; i < ItemSlot.Length; i++) // ���� ��ȸ
        {
            if (ItemSlot[i].transform.childCount > 1)
            {
                Transform childName = ItemSlot[i].transform.GetChild(1);

                // ���� ���Կ� ������ �������� �ִ��� Ȯ��
                if (itemNameString[i] == childName.name)
                {
                    //Debug.Log("������ġ:" + i + childName.name);
                    itemCount[i] = childName.GetComponent<ItemType>().get_itemcount;
                }
                if (itemNameString[i] == "")
                {

                    itemCount[i] = 0;
                }
            }
        }
    }
    /// <summary>
    /// Beta1 : EmptyItemSlot  : �������� ���� �Ǿ� �ִ����� Ȯ��, ���Կ� �������� �ִ°�� �̸��� �ױ׸� Ȯ����
    ///                          ��� ���� ���, ������ �ټ��� ���� ��Ű�� ���� ��ġ���� �ʴ� ��� �������� 
    ///                          ������ �������̸��� ���� �ߴٰ� Resources�������� �����̸��� prefab�� ���Կ�
    ///                          ���� ��ŵ�ϴ�.
    /// final : EmptyItemSlot2 : beta1�� ����� ��������� itemNameString �� ���Կ� ������ �������̸���
    ///                          itemNameString�迭�� ������ beta1�� ���� ������� �̸��� �ױ׸� Ȯ����
    ///                          ��� ���� ���, ������ �ټ��� ���� ��Ű�� ���� ��ġ���� �ʴ� ��� ��������
    ///                          ���� �ϵ��� �մϴ�.
    /// </summary>
    IEnumerator EmptyItemSlot(string Itemname)
    {
        // itemCheck();//������ ������ �ױ׿� �´� ������ ���濡 ����
        if (isCoroutineRunning)//�����۰ټ� 2������ ����
            yield break;

        isCoroutineRunning = true;
        //162~186 ���Կ� �������� �ִ��� Ȯ�� �ϰ� ������� ������ �ټ��� ���� 
        for (int i = 0; i < ItemSlot.Length; i++) // ������ ��ȸ�ϸ� ó��
        {
            Transform itemSlotTransform = ItemSlot[i].gameObject.transform;
            bool isEmptySlot = (itemSlotTransform.childCount <= 1); // �� �������� Ȯ��
            bool isMatchingSlot = false; // ���� ������ ���ǿ� �´� �������� Ȯ��

            if (!isEmptySlot) // �� ������ �ƴ� ���, ������ ������ ������ �˻�
            {
                Transform child1 = itemSlotTransform.GetChild(1);

                if (child1.tag == itemTag) // �±װ� ������ ���
                {
                    if (child1.name == Itemname) // �̸��� �����ϸ� �ش� ���Կ��� ������ �� ����
                    {
                        int index = Array.IndexOf(itemNameString, Itemname);
                        if (index != -1)
                        {
                            itemCount[index] += 1; // ������ �̸��� ������ ���� ����
                            s_itemCount[index] += 1;
                        }
                        isMatchingSlot = true;
                        break; // ���ǿ� �´� ������ ã�����Ƿ� ��ȸ ����
                    }
                }
            }
            // �� ���Կ� ���ο� ������ �߰�
            if (isEmptySlot && !isMatchingSlot)
            {
                // ������ �ε� �� ����
                GameObject armorPrefab = Resources.Load<GameObject>("Armor/" + Itemname);
                GameObject newItem = Instantiate(armorPrefab, itemPos[i]);
                newItem.name = Itemname; // �̸� ���� (Clone ����)
                newItem.tag = itemTag;  // �±� ����

                // �迭 �� ī��Ʈ �ʱ�ȭ
                itemNameString[i] = Itemname; // ���� �̸� ���
                itemCount[i] = 1; // ���ο� �������� ������ 1�� ����
                s_itemCount[i] = "1";
                BagSize(); // ���� ũ�� ������Ʈ
                DataManager.instance.nowPlayer.itemCountData = itemCount; // ������ ����

                Debug.Log($"�� ���Կ� ���ο� ������ '{Itemname}'�� �߰��߽��ϴ�.");
                break; // �������� �߰������Ƿ� ��ȸ ����
            }
        }
        yield return null;

        isCoroutineRunning = false;

    }
    IEnumerator EmptyItemSlot2(string Itemname)
    {
        Debug.Log($"Trying to add item: {Itemname}");

        if (isCoroutineRunning) yield break; // �ߺ� �ڷ�ƾ ����
        isCoroutineRunning = true;

        bool itemUpdated = false;  // ���� �������� ������Ʈ�Ǿ����� Ȯ��
        bool itemAdded = false;    // �� �������� �߰��Ǿ����� Ȯ��

        try
        {
            // 1. ���� ���Կ��� ������ ������ ������Ʈ
            for (int i = 0; i < ItemSlot.Length; i++)
            {
                if (itemNameString[i] == Itemname)
                {
                    Transform childName = ItemSlot[i].transform.GetChild(1);
                    ItemType itemType = childName.GetComponent<ItemType>();
                    itemCount[i] += 1;   // ���� ����
                    s_itemCount[i] = itemCount[i].ToString(); // ���ڿ� ����ȭ
                    itemType.get_itemcount = itemCount[i];
                    BagSize();
                    Debug.Log($"[Update] Slot {i}: {Itemname}, Count: {itemCount[i]}");
                    itemUpdated = true;
                    itemTag = "";
                    break;
                }
            }

            // 2. �� ���Կ� �� ������ �߰�
            if (!itemUpdated)
            {
                for (int i = 0; i < ItemSlot.Length; i++)
                {
                    if (string.IsNullOrEmpty(itemNameString[i])) // �� ���� Ȯ��
                    {
                        itemNameString[i] = Itemname;
                        Debug.Log($"[Add] Slot {i}: {Itemname}, Count: {itemCount[i]}");
                        itemAdded = true;

                        // ������ ������Ʈ ����
                        GameObject armorPrefab = Resources.Load<GameObject>($"Item/{Itemname}");
                        Debug.Log("armorPrefab�̸�" + armorPrefab);
                        if (armorPrefab == null)
                        {
                            Debug.LogError($"Failed to load prefab for item: {Itemname}");
                            break;
                        }
                        BagSize();
                        GameObject item = Instantiate(armorPrefab, ItemSlot[i].transform);
                        item.name = Itemname;
                        ItemType itemType = item.GetComponent<ItemType>();
                        itemCount[i] += itemType.get_itemcount;   // ���� ����
                        s_itemCount[i] = itemCount[i].ToString(); // ���ڿ� ����ȭ
                        break;
                    }
                }
            }

            // 3. ������ ���� �� ���
            if (!itemUpdated && !itemAdded)
            {
                Debug.LogWarning($"No empty slots available. Cannot add item: {Itemname}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in EmptyItemSlot2: {ex.Message}\n{ex.StackTrace}");
        }
        finally
        {
            itemTag = "";  // �±� �ʱ�ȭ
            isCoroutineRunning = false; // �ڷ�ƾ �÷��� ����
        }

        yield break;
    }

    void BagSize()
    {
        bagSize -= 1;
        bagSizeUI.text = bagSize.ToString();
    }
    void itemCheck()
    {
        switch (itemTag)
        {
            case "HpPosion": itemTypeNum = 0; break;
            case "StaminaPosion": itemTypeNum = 1; break;
            case "Helmet": itemTypeNum = 2; break;
            case "Torso": itemTypeNum = 3; break;
            case "ArmsRight": itemTypeNum = 4; break;
            case "ArmsLeft": itemTypeNum = 5; break;
            case "Legs": itemTypeNum = 6; break;
            case "Feet": itemTypeNum = 7; break;
            case "SaveItem": itemTypeNum = 8; break;
            default: itemTypeNum = 9; break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 18 && bagSize != 0)
        {
            SoundManager.PlaySound(itemEatSound);
            itemTag = other.gameObject.tag;
            Destroy(other.gameObject);
            //StartCoroutine(EmptyItemSlot(other.gameObject.name));
            StartCoroutine(EmptyItemSlot2(other.gameObject.name));
        }
        //�������� ���� ���̵� ������ ���� 
        ItemID item = other.GetComponent<ItemID>();
        //null�� �ƴҰ�� Collect()�Լ� ����
        if (item != null)
        {
            item.Collect();
        }
    }
}
