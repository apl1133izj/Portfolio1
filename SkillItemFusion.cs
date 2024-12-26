using System.Collections;
using UnityEngine;
//�����۰� ���� ���� ���� ����
public class SkillItemFusion : MonoBehaviour
{
    HP hpScript;
    Stamina staminaScript;
    AttackAndCombo attackAndComboScript;
    DemoCharacter demoCharacterScript;
    PutitemBag putitemBagScript;
    public float[] hpHap;
    public float[] staminaHap;
    public int[] speedHap;
    public int[] healthRegenerationHap;
    public int[] staminaRegenerationHap;
    public float[] defenseHap;
    public int[] attackDamageHap;
    public int[] curseHap;
    public int[] attackSpeedHap;
    public int itemNotNull;
    public int itemNull;
    public bool corutinitem;

    public bool healthRegenerationBool;
    void Start()
    {
        hpScript = GetComponent<HP>();
        staminaScript = GetComponent<Stamina>();
        attackAndComboScript = GetComponent<AttackAndCombo>();
        demoCharacterScript = GetComponent<DemoCharacter>();
        putitemBagScript = GetComponent<PutitemBag>();
    }

    private void Update()
    {
        NullArmor();
        if (corutinitem)
        {
            StartCoroutine(ItemCoroutin());
        }
        for (int i = 83; i <= 88; i++)
        {
            if (healthRegenerationHap[i - 83] > 0)
            {
                healthRegenerationBool = true;
            }

        }
    }
    IEnumerator ItemCoroutin()
    {
        corutinitem = false;
        /*        armorStatus(1, hpHap, staminaHap, speedHap, healthRegenerationHap, staminaRegenerationHap,
                          defenseHap, attackDamageHap, curseHap);*/
        ItemPluse();
        yield return null;
    }
    void NullArmor()
    {
        for (int i = 83; i <= 88; i++)
        {
            Transform slotTransform = putitemBagScript.ItemSlot[i].transform;
            if (slotTransform.childCount > 0) // �ڽ��� �ּ� 2�� �̻� �ִ��� Ȯ��
            {

                itemNotNull = i - 83;
            }
            else
            {
                itemNull = i - 83;
                hpHap[itemNull] = 0;
                staminaHap[itemNull] = 0;
                speedHap[itemNull] = 0;
                healthRegenerationHap[itemNull] = 0;
                staminaRegenerationHap[itemNull] = 0;
                defenseHap[itemNull] = 0;
                attackDamageHap[itemNull] = 0;
                curseHap[itemNull] = 0;
                attackSpeedHap[itemNull ] = 0;
            }
        }

    }
    public void ItemPluse()
    {
        // 83~88�� ���� ������ Ȯ��
        for (int i = 83; i <= 88; i++)
        {
            if (putitemBagScript.ItemSlot[i] != null)
            {

                if (putitemBagScript.ItemSlot[i].transform.childCount > 1)
                {   // ������ ���� �� ��� (�����)

                    Transform item = putitemBagScript.ItemSlot[i].transform.GetChild(1);
                    ItemPerformance[] itemPerformance = item.GetComponents<ItemPerformance>();
                    if (itemPerformance.Length == 0)
                    {
                        continue; // ���� �������� ����
                    }

                    for (int j = 0; j < itemPerformance.Length; j++)
                    {
                        //Debug.Log($"Slot {i}, Performance Index {j}: ");
                        // hpHap[1]�� ���� �̹� ����ִٸ� �ش� �������� �ǳʶٱ�
                        if (hpHap[i - 83] > 0 || staminaHap[i - 83] > 0 || speedHap[i - 83] > 0 || healthRegenerationHap[i - 83] > 0
                         || staminaRegenerationHap[i - 83] > 0 || defenseHap[i - 83] > 0 || attackDamageHap[i - 83] > 0 || curseHap[i - 83] > 0 || attackSpeedHap[i - 83] > 0)
                        {
                            continue; // �̹� ���� ����ִٸ� �� �������� �ǳʶݴϴ�.
                        }

                        hpHap[i - 83] += itemPerformance[j].itemSpec_1;
                        staminaHap[i - 83] += itemPerformance[j].itemSpec_2;
                        speedHap[i - 83] += itemPerformance[j].itemSpec_3;
                        healthRegenerationHap[i - 83] += itemPerformance[j].itemSpec_4;
                        staminaRegenerationHap[i - 83] += itemPerformance[j].itemSpec_5;
                        defenseHap[i - 83] += itemPerformance[j].itemSpec_6;
                        attackDamageHap[i - 83] += itemPerformance[j].itemSpec_7;
                        curseHap[i - 83] += itemPerformance[j].itemSpec_8;
                        attackSpeedHap[i - 83] += itemPerformance[j].itemSpec_9;
                    }
                }

            }

        }
    }
}
