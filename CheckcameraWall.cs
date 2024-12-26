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
        // 디버그 레이 그리기 (오른쪽)
        Debug.DrawRay(player.transform.position + new Vector3(0, 0.9f, 0), transform.right * 1, Color.yellow);
        // 디버그 레이 그리기 (왼쪽)
        Debug.DrawRay(player.transform.position + new Vector3(0,0.9f,0), -transform.right * 1, Color.red);

        // 오른쪽 방향 레이캐스트: 벽에 부딪쳤는지 확인
        isRightWallHit = Physics.Raycast(player.transform.position + new Vector3(0, 0.9f, 0), transform.right, 1);

        // 왼쪽 방향 레이캐스트: 벽에 부딪쳤는지 확인
        isLeftWallHit = Physics.Raycast(player.transform.position + new Vector3(0, 0.9f, 0), -transform.right, 1);

        // 현재 프레임의 벽 상태 저장
        b_CurrentstatusRight = isRightWallHit;
        b_CurrentstatusLeft = isLeftWallHit;

        // 오른쪽과 왼쪽의 상태가 다를 때 (한쪽에만 벽이 있는 경우)
        if (b_CurrentstatusRight != b_CurrentstatusLeft)
        {
            transform.localPosition = new Vector3(0f, 3.42f, -2.32f);
            Debug.Log("한쪽에만 벽이 있습니다.");
        }
        // 벽이 양쪽에 모두 없을 때
        else if (!b_CurrentstatusRight && !b_CurrentstatusLeft)
        {
            transform.localPosition = new Vector3(0f, 3.42f, -4.32f);  // 기본 위치로 초기화
            Debug.Log("양쪽에 벽이 없습니다.");
        }


        // 디버그 메시지 출력
        if (b_CurrentstatusRight)
        {
            Debug.Log("오른쪽에 벽이 있습니다.");
        }

        if (b_CurrentstatusLeft)
        {
            Debug.Log("왼쪽에 벽이 있습니다.");
        }
    }

    float CoolTime()
    {
        return Time.deltaTime;
    }*/
}
