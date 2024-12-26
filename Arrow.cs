using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum ArrowType { Arrow_1, Arrow_2, Arrow_3, Arrow_4, Arrow_5, Arrow_6 }
    public ArrowType type;//1:���� 2:�� 3:���� 4:���� 5: Ǯ 6: ������
    GameObject player;
    GameObject UndeadKnight;
    Vector3 playerPos;
    public GameObject skillArrow;
    public AudioClip[] skillSound;
    private void Awake()
    {
        UndeadKnight = GameObject.Find("Realistic Undead Knight");
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        playerPos = player.transform.position;
    }
    void Update()
    {
        //���� ���ϱ�
        //���� ���Ͱ� = ��ǥ ���� - ���� ����
        Vector3 dir = player.transform.position - transform.position; dir.y = 0f;

        //������ ���ʹϾ� �� ���ϱ�
        //���ʹϾ� �� = ���ʹϾ� ���� ��(���� ����)
        if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            rot *= Quaternion.Euler(0, -90, 0);
            //���� ������
            transform.rotation = rot;
        }

        transform.position = Vector3.MoveTowards(transform.position, playerPos, 40 * Time.deltaTime);
    }

    void Skill(Transform targetPos)
    {
        switch (type)
        {
            case ArrowType.Arrow_1:
                SoundManager.PlaySound(skillSound[0]);
                GameObject skill1 = Instantiate(skillArrow, targetPos.transform);
                skill1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Destroy(skill1,3);
                break;
            case ArrowType.Arrow_2:
                SoundManager.PlaySound(skillSound[1]);
                GameObject skill2 = Instantiate(skillArrow, targetPos.transform);
                skill2.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                Destroy(skill2, 3);
                break;
            case ArrowType.Arrow_3:
                SoundManager.PlaySound(skillSound[2]);
                GameObject skill3 = Instantiate(skillArrow, targetPos.transform);
                skill3.transform.position = transform.position - new Vector3(0,0.3f,0);

                break;
            case ArrowType.Arrow_4:
                SoundManager.PlaySound(skillSound[3]);
                GameObject skill4 = Instantiate(skillArrow, targetPos.transform);
                skill4.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case ArrowType.Arrow_5:
                SoundManager.PlaySound(skillSound[4]);
                GameObject skill5 = Instantiate(skillArrow, targetPos.transform);
                skill5.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case ArrowType.Arrow_6:
                SoundManager.PlaySound(skillSound[5]);
                GameObject skill6 = Instantiate(skillArrow, targetPos.transform);
                skill6.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;

        }
    }
    void HitSkill(GameObject targetPos)
    {
        switch (type)
        {
            case ArrowType.Arrow_1:
                GameObject skill1 = Instantiate(skillArrow, targetPos.transform);
                Destroy(skill1, 3);
                break;
            case ArrowType.Arrow_2:
                GameObject skill2 = Instantiate(skillArrow, targetPos.transform);
                Destroy(skill2, 3);
                break;
            case ArrowType.Arrow_3:
                GameObject skill3 = Instantiate(skillArrow, targetPos.transform);
                break;
            case ArrowType.Arrow_4:
                GameObject skill4 = Instantiate(skillArrow, targetPos.transform);
                break;
            case ArrowType.Arrow_5:
                GameObject skill5 = Instantiate(skillArrow, targetPos.transform);
                break;
            case ArrowType.Arrow_6:
                GameObject skill6 = Instantiate(skillArrow, targetPos.transform);
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitSkill(other.gameObject);
            HP getHp = other.GetComponent<HP>();
            getHp.hp -= 50;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Skill(gameObject.transform);
            Destroy(gameObject, 6);
        }
    }
}
