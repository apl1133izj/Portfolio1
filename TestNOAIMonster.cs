using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNOAIMonster : MonoBehaviour
{
    public Transform[] points; // �̵� ����Ʈ �迭
    public float speed = 3.0f; // �̵� �ӵ�
    private int currentPointIndex = 0;

    void Update()
    {
        // ����Ʈ�� ������ �������� ����
        if (points.Length == 0)
            return;

        // ���� ��ġ�� ���� ����Ʈ ������ �Ÿ� ���
        Transform targetPoint = points[currentPointIndex];
        Vector3 targetPosition = targetPoint.position;
        float step = speed * Time.deltaTime;

        // MoveTowards�� ����Ͽ� ĳ���� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // ��ǥ ������ ���� �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ���� ����Ʈ�� �̵�
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }
}
