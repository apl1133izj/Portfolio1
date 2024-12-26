using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAIMonster : MonoBehaviour
{
    public Transform[] points; // �̵� ����Ʈ �迭
    private NavMeshAgent agent;
    private int destPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // �ڵ����� ȸ������ �ʵ��� ����
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // ����Ʈ�� ������ ��ȯ
        if (points.Length == 0)
            return;

        // ���� ��ġ ����
        agent.destination = points[destPoint].position;

        // ���� ����Ʈ�� �ε��� ����
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        // ������Ʈ�� �������� ���� �����ߴ��� Ȯ��
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
    
}
