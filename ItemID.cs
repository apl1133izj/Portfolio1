using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID : MonoBehaviour
{
    public string itemID; // ������ ������ ID

    void Start()
    {
        // itemID �� ����� �� Ȯ�� (����׿�)
        Debug.Log($"Item ID: {itemID}, Saved Value: {PlayerPrefs.GetInt(itemID, 0)}");

        // ����� ���� 1�̸� ������ ��Ȱ��ȭ
        if (PlayerPrefs.GetInt(itemID, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        // �������� ���� ������ ǥ��
        Debug.Log($"Item Collected: {itemID}");
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();

        // ������ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

}
