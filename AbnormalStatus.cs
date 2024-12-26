using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalStatus : MonoBehaviour
{
    public enum abnormalStatusType {Slow , Curse}
    public abnormalStatusType abnormalStatus;
    public DemoCharacter demoCharacter;
    void Start()
    {
        demoCharacter = GetComponentInParent<DemoCharacter>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(abnormalStatus == abnormalStatusType.Slow)
        {
            demoCharacter.speed -= 3;
        }else if(abnormalStatus == abnormalStatusType.Curse)
        {
            demoCharacter.speed -= 3;
        }
    }
}
