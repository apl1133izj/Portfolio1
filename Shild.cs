
using UnityEngine;

public class Shild : MonoBehaviour
{
    public float damageHap;
    public float f_shildTime;
    CapsuleCollider capsuleCollider;
    public GameObject hitPaticle;
    bool b_attack;
    Skill skill;
    private void Awake()
    {

        skill = GetComponentInParent<Skill>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        f_shildTime += Time.deltaTime;
        if (f_shildTime >= 4)
        {
            if (Input.GetMouseButtonDown(1))
            {
                b_attack = true;

            }
            if (f_shildTime >= 8)
            {
                f_shildTime = 0f;
            }
        }
        if (b_attack)
        {
            gameObject.transform.localScale += Vector3.one * Time.deltaTime * 3;
            capsuleCollider.isTrigger = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageHap += 150;
        }

        if (collision.gameObject.layer == 10)
        {
            Debug.Log("³Ë¹é");
            damageHap += 150;
        }

        if (collision.gameObject.layer == 11)
        {
            damageHap += 250;

        }
        if (collision.gameObject.layer == 12)
        {
            damageHap += 100;
        }
        //¾óÀ½¿ë
        if (collision.gameObject.CompareTag("BoneDragon"))
        {
            damageHap += 50;
        }
        if (collision.gameObject.CompareTag("Desh"))
        {
            damageHap += 200;
        }
        //Á×À½ÀÇ ±â»ç
        if (collision.gameObject.CompareTag("Arrow"))
        {
            damageHap += 100;
        }
        if (collision.gameObject.CompareTag("Ax"))
        {
            damageHap += 100;
        }

        if (collision.gameObject.layer == 30)
        {
            damageHap += 40;
        }
        //¾îµÒÀÇ ¸Á·É
        if (collision.gameObject.CompareTag("MonsterAttack"))
        {
            damageHap += 10;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        int hitCount = 0;
        if (other.gameObject.CompareTag("Monster"))
        {
            hitCount += 1;
            if (hitCount == skill.skillEnhanceCount[4])
            {
                f_shildTime = 0;
                b_attack = false;
                capsuleCollider.isTrigger = false;
                gameObject.transform.localScale = Vector3.one;
                GameObject hit = Instantiate(hitPaticle, other.transform);
                Destroy(hit, 1);
                gameObject.SetActive(false);
            }

        }
    }
}
