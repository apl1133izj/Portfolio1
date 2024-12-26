using System.Collections;
using UnityEngine;
public class DrogonSkill : MonoBehaviour
{
    GameObject player;
    Transform hitPos;
    Camera cameraEffect;
    public float maxEffect = 10f;  // �ִ� ������
    public float minEffect = 0;   // �ּ� ������
    public float effectRange = 10f; // ȿ�� ����
    public float shakeDuration = 0.5f; // ��鸲 ���� �ð�
    public float shakeMagnitude = 0.1f; // ��鸲 ����
    Rigidbody playerRb;
    MeshCollider meshCollider;
    [SerializeField]
    public int posInt;//�ν����� â���� ����
    public AudioClip audio;
    void Start()
    {
        // �÷��̾� ������Ʈ�� ã�� ������ �Ҵ�
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();
        cameraEffect = player.GetComponentInChildren<Camera>();
        // ���ο� GameObject�� �����ϰ� �� ��ġ�� hitPos�� ���
        meshCollider = GetComponent<MeshCollider>();
        GameObject hitPosObject = new GameObject("HitPosition");
        hitPos = hitPosObject.transform;

        // hitPos�� ��ġ�� �÷��̾��� ���� ��ġ�� ����
        if (posInt == 0)
        {
            hitPos.position = player.transform.position;
        }
        else if (posInt == 1)
        {
            hitPos.position = new Vector3(player.transform.position.x + -3, player.transform.position.y, player.transform.position.z);
        }
        else if (posInt == 2)
        {
            hitPos.position = new Vector3(player.transform.position.x + -5, player.transform.position.y, player.transform.position.z);
        }
        else if (posInt == 3)
        {
            hitPos.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 3);
        }
        else if (posInt == 4)
        {

            hitPos.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 5);
        }


    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 60);
        transform.position = Vector3.MoveTowards(transform.position, hitPos.position, 55 * Time.deltaTime);
        if (transform.position.y <= hitPos.position.y)
        {

            //Destroy(gameObject);
        }
    }

    // �Ÿ��� ���� ī�޶� ��鸲�� ����ϴ� �޼���
    IEnumerator CameraShake()
    {
        meshCollider.enabled = false;
        Vector3 originalPosition = cameraEffect.transform.localPosition;
        float elapsed = 0.0f;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        float effect = CalculateEffect(distance);

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-0.8f, 0.8f) * shakeMagnitude * effect;
            float y = Random.Range(-0.8f, 0.8f) * shakeMagnitude * effect;

            cameraEffect.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraEffect.transform.localPosition = originalPosition;
    }

    // �Ÿ��� ���� ī�޶� ��鸲�� ����ϴ� �޼���
    float CalculateEffect(float distance)
    {
        // �Ÿ��� �������� ī�޶� ��鸲�� Ŀ������ ������ ���
        float distanceFactor = Mathf.Clamp01(1 - (distance / effectRange));

        // �ּ� ��鸲���� �ִ� ��鸲 ������ ���� ��ȯ
        return Mathf.Lerp(minEffect, maxEffect, distanceFactor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            SoundManager.PlaySound(audio);
            StartCoroutine(CameraShake());
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(CameraShake());
        }
    }
}
