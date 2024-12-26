using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLearning : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ�� ������ ����
    public float attackInterval = 1.0f; // ���� ����
    private float nextAttackTime = 0f; // ���� ���� ���� �ð�
    private bool attackBool = false; // ���� �ִϸ��̼� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // Animator ������Ʈ�� �����ɴϴ�.
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ð����� ���� ���� ���� �ð��� ũ�� ���� ����
        if (Time.time >= nextAttackTime)
        {
            // ���� �ִϸ��̼� ����
            Attack();
            // ���� ���� ���� �ð� ����
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void Attack()
    {
        attackBool = true; // ���� ���¸� true�� ����
        animator.SetBool("AttackBool", attackBool); // �ָ� �⺻ ���� �ִϸ��̼� ����

        // ���� �� ��� �� ���� ���¸� false�� �����Ͽ� �ִϸ��̼��� ����
        StartCoroutine(ResetAttackBool());
    }

    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f); // ���� �ִϸ��̼��� ���� �ð��� ���� ���
        attackBool = false; // ���� ���¸� false�� ����
        animator.SetBool("AttackBool", attackBool); // �ִϸ��̼� ����
    }
}
