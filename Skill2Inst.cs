using System.Collections;
using UnityEngine;

public class Skill2Inst : MonoBehaviour
{
    public GameObject playerLooak;
    public GameObject skill2Arear;
    public Material skill2ArearMaterial;
    public GameObject[] skillPrefab;
    public float duration = 3f; // ���� ��ȭ�� �ɸ��� �ð�
    void Start()
    {
        playerLooak = GameObject.Find("Player");
        //��ų�����ø��� ���׸��� rpga�� �⺻ �ʱ�ȭ
        float r = 0f / 255f;
        float g = 0f / 255f;
        float b = 0f / 255f;
        float a = 120f / 255f;

        // Material�� ������ ����
        Color color = new Color(r, g, b, a);
        skill2ArearMaterial.color = color;
        StartCoroutine(skill2());

    }
    IEnumerator skill2()
    {
        Debug.Log("�巹�� ��ų ����");
        Vector3 plyerPos = playerLooak.transform.localPosition;
        GameObject skill2InstArear = Instantiate(skill2Arear, new Vector3(plyerPos.x, 23.8f, plyerPos.z), Quaternion.identity);

        float elapsedTime = 0f;
        Color startColor = skill2ArearMaterial.color;// ���� ����
        Color targetColor = new Color32(0x00, 0xC9, 0xFF, 0xFF);
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // ���� ����
            skill2ArearMaterial.color = Color.Lerp(startColor, targetColor, t);
            // �ð� ����
            elapsedTime += Time.deltaTime;
            // ���� �����ӱ��� ���
            yield return null;
        }
        skill2ArearMaterial.color = targetColor;
        Destroy(skill2InstArear);
        yield return null;
        GameObject skill2Inst = Instantiate(skillPrefab[2], new Vector3(plyerPos.x, 23.8f, plyerPos.z), Quaternion.identity);
    }
}
