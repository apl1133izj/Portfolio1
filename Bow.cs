using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator a_bowanimator;
    UndeadKnight undeadKnight;
    public GameObject[] g_Animation_InstBow_Shot;
    public Transform startPoint;
    public Transform targetPoint;
    float firingAngle = 45f;
    float gravity = 9.8f;

    float lerpRotation1;
    float lerpRotation2;

    GameObject player;
    public AudioClip[] skillClip;
    private void Awake()
    {
        undeadKnight = GetComponentInParent<UndeadKnight>();
        a_bowanimator = GetComponent<Animator>();
        player = GameObject.Find("Player");

    }
    private void Update()
    {
        if (undeadKnight.b_ShotBool)
        {
            a_bowanimator.enabled = true;
        }
        else
        {
            a_bowanimator.enabled = false;
        }
    }
    public IEnumerator SimulateProjectile()
    {
        // �߻��� ��ü ����
        GameObject projectile = Instantiate(g_Animation_InstBow_Shot[UndeadKnight.randomArrow], startPoint.position, Quaternion.identity) as GameObject;
       
        // �������� ��ǥ�� ������ �Ÿ� ���
        float target_Distance = Vector3.Distance(startPoint.position, targetPoint.position);
        float target_Distance_half = Mathf.Lerp(startPoint.position.x, targetPoint.position.x, 0.5f);
        // �ʱ� �ӵ� ���
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // XZ ��鿡���� �ӵ� ���
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // ���� �ð� ���
        float flightDuration = target_Distance / Vx;

        // �߻� ���� ����
        projectile.transform.rotation = Quaternion.LookRotation(targetPoint.position - startPoint.position);

        // ���� �ð� ���� �̵�
        float elapse_time = 0;

        lerpRotation1 = 0;
        lerpRotation2 = 0;
        while (elapse_time < flightDuration)
        {

            // projectile�� null�� �ƴ��� Ȯ��
            if (projectile != null)
            {
                if (projectile.transform.position.x > target_Distance_half)//���� �������� �߰� ��������
                {
                    Debug.Log("�߰� �������� �� ��������");
                    //lerpRotation2 = Mathf.Lerp(lerpRotation1, -40, lerpspeed += Time.deltaTime * 2);
                    projectile.transform.rotation = Quaternion.Euler(0f, -90f, lerpRotation2);
                }
                else//�߰� �������� �� ��������
                {
                    Debug.Log("���� �������� �߰� ��������");
                    //lerpRotation1 = Mathf.Lerp(0, -20, lerpspeed += Time.deltaTime * 2);
                    projectile.transform.rotation = Quaternion.Euler(0f, -90f, lerpRotation1);
                }
                projectile.transform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
            }
            else
            {
                // projectile�� �̹� �ı��Ǿ��ٸ� �ڷ�ƾ�� ����
                yield break;
            }
            elapse_time += Time.deltaTime;
            yield return null;
        }
        // �߻��� ��ü �ı�
        if (projectile != null)
        {
            Destroy(projectile);
        }
    }
    public void ShotBool()
    {
        SoundManager.PlaySound(skillClip[UndeadKnight.randomArrow]);
        undeadKnight.b_Load = false;
        GameObject projectile = Instantiate(g_Animation_InstBow_Shot[UndeadKnight.randomArrow], startPoint.position, Quaternion.identity) as GameObject;
        Vector3 dir = player.transform.position - projectile.transform.position; dir.y = 0f;

        //������ ���ʹϾ� �� ���ϱ�
        //���ʹϾ� �� = ���ʹϾ� ���� ��(���� ����)
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        rot *= Quaternion.Euler(0, -90, 0);
        //���� ������
        projectile.transform.rotation = rot;
        projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, player.transform.position, 40 * Time.deltaTime);
        undeadKnight.g_Animation_bow_Load[UndeadKnight.randomArrow].gameObject.SetActive(false);

    }
    public void BowAnimatorEnabel()
    {
        undeadKnight.b_ShotBool = false;
        undeadKnight.b_Load = false;
    }
}
