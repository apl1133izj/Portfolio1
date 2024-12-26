using UnityEngine;

public class CircleR : MonoBehaviour
{
    public bool b_circleR;
    public GameObject drogon;
    public float circleR; //������
    public  float deg; //����
    public  float f_randonDeg = 360;
    public float objSpeed; //��� �ӵ�
    int objSize = 5;
    void Update()
    {
        if (b_circleR)
        {
            deg += Time.deltaTime * objSpeed;
            if (deg < 360)
            {
                for (int i = 0; i < objSize; i++)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / objSize)));
                    var x = circleR * Mathf.Sin(rad);
                    var z = circleR * Mathf.Cos(rad);
                    drogon.transform.position = transform.position + new Vector3(x,0,z);
                    drogon.transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / objSize))) * -1);
                }

            }
            else
            {
                b_circleR = false;
                deg = 0;
            }
        }
    }
}
