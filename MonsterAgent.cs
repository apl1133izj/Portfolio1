/*using System.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MonsterAgent : Agent
{
    public GameObject player;  // 플레이어 캐릭터
    private Rigidbody monsterRb;

    // 패링 관련 변수
    private bool isParrying = false;
    private float parryWindow = 0.5f;  // 패링 가능한 시간 창 (초)
    private float lastParryTime;
    public Transform startpos;
    public Animator animator;

    // RayPerceptionSensor3D 변수
   // private RayPerceptionSensor3D raySensor;

    private void Update()
    {
        //Debug.Log($"현재 위치: {transform.position}, 패링 상태: {isParrying}"); // 현재 상태 출력
    }

    public override void Initialize()
    {
        monsterRb = GetComponent<Rigidbody>();
       // raySensor = GetComponent<RayPerceptionSensor3D>(); // RayPerceptionSensor3D 초기화
    }

    public void EndParry()
    {
        isParrying = false; // 패링 상태를 false로 설정
        animator.SetBool("Parry", isParrying); // 패링 애니메이션 중지
    }

    // 관찰 데이터를 수집하는 메서드
    public override void CollectObservations(VectorSensor sensor)
    {
        // 플레이어와의 거리
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        sensor.AddObservation(distanceToPlayer);

        // 플레이어의 공격 상태 (1: 공격 중, 0: 공격 안 함)
        bool isPlayerAttacking = player.GetComponent<AttackAndCombo>().isAttacking;
        sensor.AddObservation(isPlayerAttacking);

        // 플레이어의 공격 방향
        Vector3 playerDirection = player.transform.forward;
        sensor.AddObservation(playerDirection);

        // 몬스터의 패링 준비 여부 (1: 준비 완료, 0: 패링 창이 종료됨)
        sensor.AddObservation(isParrying ? 1.0f : 0.0f);

        // RayPerceptionSensor3D로부터 감지된 정보 추가
*//*        var rayObservations = raySensor.GetRayPerceptionObservations();
        foreach (var observation in rayObservations)
        {
            sensor.AddObservation(observation);
        }*//*
    }

    // 에이전트가 받은 행동을 처리하는 메서드
    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        Debug.Log($"받은 행동: {action}"); // 이 로그가 출력되는지 확인
        switch (action)
        {
            case 0:
                // 대기 상태
                break;
            case 1:
                // 패링 시도
                AttemptParry();
                break;
            case 2:
                // 방어 시도
                Defend();
                break;
            case 3:
                // 회피
                Dodge();
                break;
        }
    }

    // 패링 시도 메서드
    private void AttemptParry()
    {
        if (Time.time - lastParryTime < parryWindow)
        {
            if (IsParrySuccessful())
            {
                isParrying = true; // 패링 상태를 true로 설정
                animator.SetBool("Parry", isParrying);  // 패링 애니메이션 트리거
                AddReward(1.0f);  // 패링 성공 시 보상
                Debug.Log("패링 성공! 보상 추가됨."); // 성공 메시지 출력
            }
            else
            {
                isParrying = false; // 패링 실패 시 false로 설정
                AddReward(-0.5f);  // 패링 실패 시 페널티
                Debug.Log("패링 실패! 페널티 적용됨."); // 실패 메시지 출력
            }
        }
        lastParryTime = Time.time;  // 패링 시도 시간 갱신
        EndParry(); // 패링 시도 후 패링 상태 종료
    }

    // 방어 시도 메서드
    private void Defend()
    {
        // 방어 로직 구현
        AddReward(0.2f);  // 방어 성공 시 작은 보상
        Debug.Log("방어 성공! 보상 추가됨."); // 방어 성공 메시지 출력
    }

    // 회피 메서드
    private void Dodge()
    {
        // 회피 로직 구현
        AddReward(0.1f);  // 회피 성공 시 작은 보상
        Debug.Log("회피 성공! 보상 추가됨."); // 회피 성공 메시지 출력
    }

    // 패링 성공 여부 확인 메서드
    private bool IsParrySuccessful()
    {
        // 패링 성공 여부를 플레이어의 공격 타이밍과 비교해 판정
        return player.GetComponent<AttackAndCombo>().IsAttackTimingMatched();
    }

    // 환경이 리셋될 때 호출되는 메서드
    public override void OnEpisodeBegin()
    {
        // 리셋할 동작 정의 (위치, 상태 초기화 등)
        transform.position = startpos.transform.position;
        isParrying = false; // 에피소드 시작 시 패링 상태 초기화
        lastParryTime = 0; // 패링 시도 시간 초기화
        Debug.Log("새로운 에피소드 시작!"); // 에피소드 시작 메시지 출력
    }
}
*/