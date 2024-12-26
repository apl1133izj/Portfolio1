using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum skillTreeTooltip
    {
        Hp, Stamina, RecoverySpeed, ReduceCoolTime, PotionEffect, CurseReduction, StaminaRecovery, AttackPower,
        DamageReduction, MoveSpeed, AttackSpeed, StaminaCostReduction, AttackRange, UltimateSkill
    };
    public skillTreeTooltip skillname;
    int skilLevel;
    string tname;
    string tLevel;
    string tNextLevel;
    string tCommend;
    string tDetailsCommend;
    private TooltipController tooltipController;
    SkillTree skillTree;
    void Start()
    {
        tooltipController = GetComponent<TooltipController>();
        skillTree = GetComponentInParent<SkillTree>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillLevel();
        tooltipController.ShowTooltip(tname, tLevel, tNextLevel, tCommend, tDetailsCommend);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipController.HideTooltip();
    }
    /// <summary>
    /// ��ų������ ���� ��������
    /// </summary>
    public void SkillLevel()
    {
        if (skillname == skillTreeTooltip.Hp)
        {
            switch (skillTree.skillLevel[0])
            {
                case 1:
                    tname = "ü�� ����\n";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ü���� �����մϴ�." +
                             "\n\t\t ü�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "1000    ->     1200\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "ü�� ����\n";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ü���� �����մϴ�." +
                             "\n\t\t ü�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->      Lv3\n"
                                     + "1200    ->      1500\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "ü�� ����\n";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ü���� �����մϴ�." +
                             "\n\t\t ü�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->      Lv4\n"
                                     + "1500    ->      1900\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "ü�� ����\n";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ü���� �����մϴ�." +
                             "\n\t\t ü�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5      ->     �ְ� ����\n"
                                     + "1900     ->        2400\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "ü�� ����\n";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����\n";
                    tCommend = " ü���� �����մϴ�.\n" +
                             "\n\t\t ü�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�\n";
                    tDetailsCommend = "�ְ� ���� �޼�\n"
                                     + "ü�� ���� :2400        \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }

        }
        if (skillname == skillTreeTooltip.Stamina)
        {
            switch (skillTree.skillLevel[1])
            {
                case 1:
                    tname = "��� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ����� �����մϴ�." +
                             "\n\t\t ��� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "1000    ->     1200\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "��� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ����� �����մϴ�." +
                             "\n\t\t ��� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "1200    ->     1500\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "��� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ����� �����մϴ�." +
                             "\n\t\t ��� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "1500    ->     1900\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "��� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ����� �����մϴ�." +
                             "\n\t\t ��� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "1900    ->        2400\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "��� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ����� �����մϴ�." +
                             "\n\t\t ��� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = "�ְ� ���� �޼�\n"
                                     + "��� ���� : 2400       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�\n";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.RecoverySpeed)
        {
            switch (skillTree.skillLevel[2])
            {
                case 1:
                    tname = "ü�� ��� ȿ�� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "ü�� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ü�� ��� ȿ�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + " 1      ->      3\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "ü�� ��� ȿ�� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "ü�� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ü�� ��� ȿ�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + " 3      ->      6\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "ü�� ��� ȿ�� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "ü�� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ü�� ��� ȿ�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + " 6      ->      10\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "ü�� ��� ȿ�� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ü�� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ü�� ��� ȿ�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "10      ->        15\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "ü�� ��� ȿ�� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ü�� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ü�� ��� ȿ�� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "ü�� ��� ȿ�� : 15                 \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.ReduceCoolTime)
        {
            switch (skillTree.skillLevel[3])
            {
                case 1:
                    tname = "���� ���ð� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ���� ���ð��� ���� �մϴ�." +
                             "\n\t\t ���� ���ð� ���Ҵ� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "0.2��   ->     0.6��\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���� ���ð� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "���� ���ð��� ���� �մϴ�." +
                             "\n\t\t ���� ���ð� ���Ҵ� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2      ->     Lv3\n"
                                     + "0.6��    ->     1.2��\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���� ���ð� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "���� ���ð��� ���� �մϴ�." +
                             "\n\t\t ���� ���ð� ���Ҵ� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "1.2��   ->     2.0��\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���� ���ð� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = "���� ���ð��� ���� �մϴ�." +
                             "\n\t\t ���� ���ð� ���Ҵ� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "2.0��   ->     3.0��\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���� ���ð� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "���� ���ð��� ���� �մϴ�." +
                             "\n\t\t ���� ���ð� ���Ҵ� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "���� ���ð� : 3.0��       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.PotionEffect)
        {
            switch (skillTree.skillLevel[4])
            {
                case 1:
                    tname = "���� ȿ�� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ���� ȿ���� �����մϴ�." +
                             "\n\t\t ���� ȿ���� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->      Lv2\n"
                                     + "50      ->      150\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���� ȿ�� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ���� ȿ���� �����մϴ�." +
                             "\n\t\t ���� ȿ���� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "150     ->     300\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���� ȿ�� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ���� ȿ���� �����մϴ�." +
                             "\n\t\t ���� ȿ���� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "300     ->     500\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���� ȿ�� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ���� ȿ���� �����մϴ�." +
                             "\n\t\t ���� ȿ���� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "500     ->     750\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���� ȿ�� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ���� ȿ���� �����մϴ�." +
                             "\n\t\t ���� ȿ���� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "���� ȿ�� ���� : 750       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.CurseReduction)
        {
            switch (skillTree.skillLevel[5])
            {
                case 1:
                    tname = "���� ȿ�� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "���� ȿ�� ���Ұ� �����մϴ�." +
                             "\n\t\t ���� ȿ�� ���Ұ����� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "0.025   ->     0.075\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���� ȿ�� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "���� ȿ�� ���Ұ� �����մϴ�." +
                             "\n\t\t ���� ȿ�� ���Ұ����� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "0.075   ->     0.15\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���� ȿ�� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "���� ȿ�� ���Ұ� �����մϴ�." +
                             "\n\t\t ���� ȿ�� ���Ұ����� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "0.15    ->     0.25\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���� ȿ�� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = "���� ȿ�� ���Ұ� �����մϴ�." +
                             "\n\t\t ���� ȿ�� ���Ұ����� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "0.25    ->     0.375\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���� ȿ�� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "���� ȿ�� ���Ұ� �����մϴ�." +
                             "\n\t\t ���� ȿ�� ���Ұ����� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "����ȿ�� ���� : 0.375       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.StaminaRecovery)
        {
            switch (skillTree.skillLevel[6])
            {
                case 1:
                    tname = "��� ��� ȿ�� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "��� ��� ȿ�� ������ �մϴ�." +
                             "\n\t\t ��� ��� ȿ�� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "1       ->      3\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "��� ��� ȿ�� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "��� ��� ȿ�� ������ �����մϴ�." +
                             "\n\t\t ��� ��� ȿ�� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "3       ->     6\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "��� ��� ȿ�� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "��� ��� ȿ�� ������." +
                             "\n\t\t ��� ��� ȿ�� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "6       ->      10\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "��� ��� ȿ�� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = "��� ��� ȿ�� ������." +
                             "\n\t\t ��� ��� ȿ�� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "10      ->         15\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "��� ��� ȿ�� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "��� ��� ȿ�� ������." +
                             "\n\t\t ��� ��� ȿ�� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �缺          \n"
                                     + "��� ��� : 15       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.AttackPower)
        {
            switch (skillTree.skillLevel[7])
            {
                case 1:
                    tname = "���ݷ� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ���ݷ��� ���� �մϴ�." +
                             "\n\t\t ���ݷ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "1       ->      3\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���ݷ� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ���ݷ��� �����մϴ�." +
                             "\n\t\t ���ݷ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "3       ->      6\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���ݷ� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ���ݷ��� �����մϴ�." +
                             "\n\t\t ���ݷ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "6       ->      10\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���ݷ� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ���ݷ��� �����մϴ�." +
                             "\n\t\t ���ݷ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "10      ->         15\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���ݷ� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "���ݷ��� �����մϴ�" +
                             "\n\t\t ���ݷ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�          \n"
                                     + "���ݷ� : 15       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.DamageReduction)
        {
            switch (skillTree.skillLevel[8])
            {
                case 1:
                    tname = "���ط� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "���ط� ���Ұ� �����մϴ�." +
                             "\n\t\t ���ط� ���Ұ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "2       ->      6\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���ط� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ���ط� ���Ұ� �����մϴ�." +
                             "\n\t\t ���ط� ���Ұ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->      Lv3\n"
                                     + "6       ->       12\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���ط� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ���ط� ���Ұ� �����մϴ�." +
                             "\n\t\t ���ط� ���Ұ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "12      ->     20\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���ط� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ���ط� ���Ұ� �����մϴ�." +
                             "\n\t\t ���ط� ���Ұ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "20      ->        30\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���ط� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ���ط� ���Ұ� �����մϴ�." +
                             "\n\t\t ���ط� ���Ұ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "���� ���� : 30       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.MoveSpeed)
        {
            switch (skillTree.skillLevel[9])
            {
                case 1:
                    tname = "�̵� �ӵ� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " �̵� �ӵ��� �����մϴ�." +
                             "\n\t\t �̵� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "0.15    ->     0.45\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "�̵� �ӵ� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " �̵� �ӵ��� �����մϴ�." +
                             "\n\t\t �̵� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "0.45    ->     0.9\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "�̵� �ӵ� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " �̵� �ӵ��� �����մϴ�." +
                             "\n\t\t �̵� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "0.9     ->     1.5\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "�̵� �ӵ� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " �̵� �ӵ��� �����մϴ�." +
                             "\n\t\t �̵� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "1.5     ->        2.25\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "�̵� �ӵ� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " �̵� �ӵ��� �����մϴ�." +
                             "\n\t\t �̵� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "�̵��ӵ� : 2.25       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.AttackSpeed)
        {
            switch (skillTree.skillLevel[10])
            {
                case 1:
                    tname = "���� �ӵ� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "���� �ӵ��� �����մϴ�." +
                             "\n\t\t ���� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "0.03    ->     0.09\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���� �ӵ� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ���� �ӵ��� �����մϴ�." +
                             "\n\t\t ���� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "0.09    ->     0.18\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���� �ӵ� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ���� �ӵ��� �����մϴ�." +
                             "\n\t\t ���� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "0.18    ->     0.3\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���� �ӵ� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ���� �ӵ��� �����մϴ�." +
                             "\n\t\t ���� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "0.3     ->        0.45\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���� �ӵ� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ���� �ӵ��� �����մϴ�." +
                             "\n\t\t ���� �ӵ� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "���� �ӵ� : 0.45       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.StaminaCostReduction)
        {
            switch (skillTree.skillLevel[11])
            {
                case 1:
                    tname = "��� ��� ��� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "��� ��� ����� ���� �մϴ�." +
                             "\n\t\t ��� ��� ����� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "5       ->     15\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "��� ��� ��� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "��� ��� ����� ���� �մϴ�." +
                             "\n\t\t ��� ��� ����� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "15      ->     30\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "��� ��� ��� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "��� ��� ����� ���� �մϴ�." +
                             "\n\t\t ��� ��� ����� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "30      ->     50\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "��� ��� ��� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = "��� ��� ����� ���� �մϴ�." +
                             "\n\t\t ��� ��� ����� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->    �ְ� ����\n"
                                     + "50      ->        75\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "��� ��� ��� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "��� ��� ����� ���� �մϴ�." +
                             "\n\t\t ��� ��� ����� �������� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "��� ��� ��� : 75       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.AttackRange)
        {
            switch (skillTree.skillLevel[12])
            {
                case 1:
                    tname = "���� ���� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = " ���� ������ �����մϴ�." +
                             "\n\t\t ���� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "1       ->      2\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "���� ���� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = " ���� ������ �����մϴ�." +
                             "\n\t\t ���� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "2       ->     3\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "���� ���� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = " ���� ������ �����մϴ�." +
                             "\n\t\t ���� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->     Lv4\n"
                                     + "3       ->      4\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "���� ���� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = " ����� ���������� �����մϴ�." +
                             "\n\t\t ���� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->     �ְ� ����\n"
                                     + "4       ->         5\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "���� ���� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = " ���� ������ �����մϴ�." +
                             "\n\t\t ���� ������ Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " �ְ� ���� �޼�         \n"
                                     + "��Ÿ� : 5       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
        if (skillname == skillTreeTooltip.UltimateSkill)
        {
            switch (skillTree.skillLevel[12])
            {
                case 1:
                    tname = "�ñر� ����";
                    tLevel = "1";
                    tNextLevel = "2";
                    tCommend = "R�� ���� �ñر� �� ��� �� �� �ֽ��ϴ�." +
                             "\n\t\t ���� ���ð��� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv1     ->     Lv2\n"
                                     + "20    ->       19\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 2:
                    tname = "�ñر�[R] ��Ÿ�� ����";
                    tLevel = "2";
                    tNextLevel = "3";
                    tCommend = "�ñر��� ���� ���ð��� �����մϴ�" +
                             "\n\t\t ���� ���ð��� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv2     ->     Lv3\n"
                                     + "19     ->      17\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 3:
                    tname = "�ñر�[R] ��Ÿ�� ����";
                    tLevel = "3";
                    tNextLevel = "4";
                    tCommend = "�ñر��� ���� ���ð��� �����մϴ�" +
                             "\n\t\t ���� ���ð��� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv3     ->    Lv4\n"
                                     + "17    ->      14\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 4:
                    tname = "�ñر�[R] ��Ÿ�� ����";
                    tLevel = "4";
                    tNextLevel = "5";
                    tCommend = "�ñر��� ���� ���ð��� �����մϴ�" +
                             "\n\t\t ���� ���ð��� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = " Lv5     ->    �ְ� ����\n"
                                     + "14    ->         9\n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
                case 5:
                    tname = "�ñر�[R] ��Ÿ�� ����";
                    tLevel = "5";
                    tNextLevel = "           �ְ� ����";
                    tCommend = "�ñر��� ���� ���ð��� �����մϴ�" +
                             "\n\t\t ���� ���ð��� Level�� ���� ���� ���� \n\t\t �ϸ� Level�� MAX�ϰ�� �߰� ȿ����\n" +
                             "\t\t ȹ���մϴ�";
                    tDetailsCommend = "�ְ� ���� �޼�         \n"
                                     +"����� ��� �ð� : 9       \n" +
                                      "SkillLevel�� ���� SkillPoint��뷮\n�� �����մϴ�";
                    break;
            }
        }
    }
}
