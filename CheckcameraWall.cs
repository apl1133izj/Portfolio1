using UnityEngine;

public class CheckcameraWall : MonoBehaviour
{

/*    [SerializeField] private GameObject player;
    private bool isRightWallHit = false;
    private bool isLeftWallHit = false;
    private bool b_CurrentstatusRight = false;
    private bool b_CurrentstatusLeft = false;

    private void LateUpdate()
    {
        // ����� ���� �׸��� (������)
        Debug.DrawRay(player.transform.position + new Vector3(0, 0.9f, 0), transform.right * 1, Color.yellow);
        // ����� ���� �׸��� (����)
        Debug.DrawRay(player.transform.position + new Vector3(0,0.9f,0), -transform.right * 1, Color.red);

        // ������ ���� ����ĳ��Ʈ: ���� �ε��ƴ��� Ȯ��
        isRightWallHit = Physics.Raycast(player.transform.position + new Vector3(0, 0.9f, 0), transform.right, 1);

        // ���� ���� ����ĳ��Ʈ: ���� �ε��ƴ��� Ȯ��
        isLeftWallHit = Physics.Raycast(player.transform.position + new Vector3(0, 0.9f, 0), -transform.right, 1);

        // ���� �������� �� ���� ����
        b_CurrentstatusRight = isRightWallHit;
        b_CurrentstatusLeft = isLeftWallHit;

        // �����ʰ� ������ ���°� �ٸ� �� (���ʿ��� ���� �ִ� ���)
        if (b_CurrentstatusRight != b_CurrentstatusLeft)
        {
            transform.localPosition = new Vector3(0f, 3.42f, -2.32f);
            Debug.Log("���ʿ��� ���� �ֽ��ϴ�.");
        }
        // ���� ���ʿ� ��� ���� ��
        else if (!b_CurrentstatusRight && !b_CurrentstatusLeft)
        {
            transform.localPosition = new Vector3(0f, 3.42f, -4.32f);  // �⺻ ��ġ�� �ʱ�ȭ
            Debug.Log("���ʿ� ���� �����ϴ�.");
        }


        // ����� �޽��� ���
        if (b_CurrentstatusRight)
        {
            Debug.Log("�����ʿ� ���� �ֽ��ϴ�.");
        }

        if (b_CurrentstatusLeft)
        {
            Debug.Log("���ʿ� ���� �ֽ��ϴ�.");
        }
    }

    float CoolTime()
    {
        return Time.deltaTime;
    }*/
}
