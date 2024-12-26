using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterPlayerSmellSearch : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent�� NavMesh�� �ְ� Ȱ��ȭ�Ǿ� �ִ��� ���� Ȯ��
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
            StartCoroutine(PlayerSearch());
        }
        else
        {
            Debug.LogError("NavMeshAgent�� Ȱ��ȭ���� �ʾҰų� NavMesh�� ��ġ���� �ʾҽ��ϴ�.");
        }
    }

    private IEnumerator PlayerSearch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (agent != null && agent.isOnNavMesh && agent.isActiveAndEnabled)
            {
                agent.SetDestination(player.position);
                float remainingDistance = agent.remainingDistance;
                Debug.Log("���� �Ÿ�: " + remainingDistance);

                if (remainingDistance < 2f)
                {
                    Debug.Log("�÷��̾ ������");
                    // �߰� ���� �ۼ�
                }
            }
            else
            {
                Debug.LogWarning("������Ʈ�� NavMesh�� �����ϴ�.");
            }
        }
    }
}
