using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FindLoad : MonoBehaviour
{
    public GameObject playerPos;//�÷��̾� ��ġ
    public GameObject destination;//���� ��ġ
    Vector3 meter;               //���� ��ġ������ �Ÿ�
    public Text meterText;
    public SpriteRenderer arrowImage;//��ã�� ȭ��ǥ �̹��� �� ����
    typing typingS;
    bool effectBool = true;
    public static bool findLoadBool; //���� ã������
    public bool questStartBool;//����Ʈ�� ����������
    private void Start()
    {
        typingS = GetComponent<typing>();
    }
    void Update()
    {
        if (questStartBool)
        {
            Color color = arrowImage.color;
            color.a = 1f;
            arrowImage.color = color;
            LookAtPlayer(destination);

            //�÷��̾�� ���� ��ġ������ �Ÿ� ���
            meter.x = playerPos.transform.position.x - destination.transform.position.x;


            if (Mathf.Round(meter.x) == 0)
            {
                meterText.color = Color.red;
                meterText.text = "����Ʈ ��� ����";
                
                if (effectBool)
                {
                    StartCoroutine(TextEffect());
                }
            }
            else
            {
                if (!findLoadBool)
                {
                    typingS.enabled = false;
                    meterText.text = "����Ʈ ��ұ���" + Mathf.Round(meter.x) + "M";
                }
            }
        }else
        {
            Color color = arrowImage.color;
            color.a = 0f;
            arrowImage.color = color;
        }
    }
    //���� ����Ʈ(�̹���,����)
    IEnumerator TextEffect()
    {
        yield return new WaitForSeconds(0.5f);
        meterText.enabled = false;
        gameObject.SetActive(false);
        effectBool = false;
        typingS.enabled = true;
        yield return new WaitForSeconds(4.05f);
        typingS.enabled = false;
        meterText.enabled = false;
        findLoadBool = true;
        gameObject.SetActive(false);
        yield return null;
    }
    //�÷��̾� ���� ȭ��ǥ
    void LookAtPlayer(GameObject loadRotation)
    {
        Vector3 direction = loadRotation.transform.position - transform.position;
        direction.y = 0;
        {
            //
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(81.55f, rotation.eulerAngles.y, 74.481f);
        }
    }


}
