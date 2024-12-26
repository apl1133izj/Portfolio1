/*using System.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MonsterAgent : Agent
{
    public GameObject player;  // �÷��̾� ĳ����
    private Rigidbody monsterRb;

    // �и� ���� ����
    private bool isParrying = false;
    private float parryWindow = 0.5f;  // �и� ������ �ð� â (��)
    private float lastParryTime;
    public Transform startpos;
    public Animator animator;

    // RayPerceptionSensor3D ����
   // private RayPerceptionSensor3D raySensor;

    private void Update()
    {
        //Debug.Log($"���� ��ġ: {transform.position}, �и� ����: {isParrying}"); // ���� ���� ���
    }

    public override void Initialize()
    {
        monsterRb = GetComponent<Rigidbody>();
       // raySensor = GetComponent<RayPerceptionSensor3D>(); // RayPerceptionSensor3D �ʱ�ȭ
    }

    public void EndParry()
    {
        isParrying = false; // �и� ���¸� false�� ����
        animator.SetBool("Parry", isParrying); // �и� �ִϸ��̼� ����
    }

    // ���� �����͸� �����ϴ� �޼���
    public override void CollectObservations(VectorSensor sensor)
    {
        // �÷��̾���� �Ÿ�
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        sensor.AddObservation(distanceToPlayer);

        // �÷��̾��� ���� ���� (1: ���� ��, 0: ���� �� ��)
        bool isPlayerAttacking = player.GetComponent<AttackAndCombo>().isAttacking;
        sensor.AddObservation(isPlayerAttacking);

        // �÷��̾��� ���� ����
        Vector3 playerDirection = player.transform.forward;
        sensor.AddObservation(playerDirection);

        // ������ �и� �غ� ���� (1: �غ� �Ϸ�, 0: �и� â�� �����)
        sensor.AddObservation(isParrying ? 1.0f : 0.0f);

        // RayPerceptionSensor3D�κ��� ������ ���� �߰�
*//*        var rayObservations = raySensor.GetRayPerceptionObservations();
        foreach (var observation in rayObservations)
        {
            sensor.AddObservation(observation);
        }*//*
    }

    // ������Ʈ�� ���� �ൿ�� ó���ϴ� �޼���
    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        Debug.Log($"���� �ൿ: {action}"); // �� �αװ� ��µǴ��� Ȯ��
        switch (action)
        {
            case 0:
                // ��� ����
                break;
            case 1:
                // �и� �õ�
                AttemptParry();
                break;
            case 2:
                // ��� �õ�
                Defend();
                break;
            case 3:
                // ȸ��
                Dodge();
                break;
        }
    }

    // �и� �õ� �޼���
    private void AttemptParry()
    {
        if (Time.time - lastParryTime < parryWindow)
        {
            if (IsParrySuccessful())
            {
                isParrying = true; // �и� ���¸� true�� ����
                animator.SetBool("Parry", isParrying);  // �и� �ִϸ��̼� Ʈ����
                AddReward(1.0f);  // �и� ���� �� ����
                Debug.Log("�и� ����! ���� �߰���."); // ���� �޽��� ���
            }
            else
            {
                isParrying = false; // �и� ���� �� false�� ����
                AddReward(-0.5f);  // �и� ���� �� ���Ƽ
                Debug.Log("�и� ����! ���Ƽ �����."); // ���� �޽��� ���
            }
        }
        lastParryTime = Time.time;  // �и� �õ� �ð� ����
        EndParry(); // �и� �õ� �� �и� ���� ����
    }

    // ��� �õ� �޼���
    private void Defend()
    {
        // ��� ���� ����
        AddReward(0.2f);  // ��� ���� �� ���� ����
        Debug.Log("��� ����! ���� �߰���."); // ��� ���� �޽��� ���
    }

    // ȸ�� �޼���
    private void Dodge()
    {
        // ȸ�� ���� ����
        AddReward(0.1f);  // ȸ�� ���� �� ���� ����
        Debug.Log("ȸ�� ����! ���� �߰���."); // ȸ�� ���� �޽��� ���
    }

    // �и� ���� ���� Ȯ�� �޼���
    private bool IsParrySuccessful()
    {
        // �и� ���� ���θ� �÷��̾��� ���� Ÿ�ְ̹� ���� ����
        return player.GetComponent<AttackAndCombo>().IsAttackTimingMatched();
    }

    // ȯ���� ���µ� �� ȣ��Ǵ� �޼���
    public override void OnEpisodeBegin()
    {
        // ������ ���� ���� (��ġ, ���� �ʱ�ȭ ��)
        transform.position = startpos.transform.position;
        isParrying = false; // ���Ǽҵ� ���� �� �и� ���� �ʱ�ȭ
        lastParryTime = 0; // �и� �õ� �ð� �ʱ�ȭ
        Debug.Log("���ο� ���Ǽҵ� ����!"); // ���Ǽҵ� ���� �޽��� ���
    }
}
*/