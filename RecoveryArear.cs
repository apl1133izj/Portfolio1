using UnityEngine;
public class RecoveryArear : MonoBehaviour
{
    public GameObject RecoveryArearIntypingText;
    public typing[] RecoveryArearTyping;

    public HP hpS;
    public Stamina staminaS;
    public PlayerUI playerUI;
    public bool smallsanctum;
    public GameObject sanctumEffect;
    private void Update()
    {
        if (smallsanctum)
        {
            sanctumEffect.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("RecoveryArear�� ����");
            RecoveryArearIntypingText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hpS.hp <= hpS.hpMax)//�÷��̾� ���׹̳ʰ� maximumSteminerRecovery ���� Ŭ��� �Ʒ� �ڵ� ����
            {

                if (sanctumEffect)//���� ���� ȸ��
                {
                    hpS.hp += Time.deltaTime * 10;
                }
                else//ū���� ȸ��
                {
                    hpS.hp += Time.deltaTime * 20;
                }
            }
            if (staminaS.stamina <= staminaS.staminaMax)//�÷��̾� ���׹̳ʰ� maximumSteminerRecovery ���� Ŭ��� �Ʒ� �ڵ� ����
            {
                if (sanctumEffect)
                {
                    staminaS.stamina += Time.deltaTime * 10;
                }
                else
                {
                    staminaS.stamina += Time.deltaTime * 20;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("RecoveryArear�� ����");
            RecoveryArearIntypingText.SetActive(false);
            RecoveryArearTyping[0].count = 0;
            RecoveryArearTyping[1].count = 0;
        }
    }
}
