using UnityEngine;
using UnityEngine.UI;

public class SkillActivationLevel : MonoBehaviour
{
    //�÷��̾ ������ ������ �ÿ��� ��ų����Ʈ�ټ�

    public enum SkillpointActivationLevel { Skill0, Skill5, Skill15, Skill20, Skill30, Skill35}
    public SkillpointActivationLevel skillpointActivationLevel;
    Button skillpointActivationLevelButton;
    private void Start()
    {
        skillpointActivationLevelButton = GetComponent<Button>();
    }
    public void Buttoninteractable()
    {
        if(skillpointActivationLevel == SkillpointActivationLevel.Skill0)
        {
            skillpointActivationLevelButton.interactable = true;
        }
    }
}
