using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Raisthemouse : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float lerpTime = 5f;
    public GameObject[] moveGameObject;
    float currentTime = 0;
    public float duration = 1.0f; // �̵��� �ɸ��� �ð�
    public GameObject exitGameObject;//���� �ε� �ϴ� ������ ��Ȱ��ȭ
    public GameObject savePageExitGameObject;//�����ϴ� ���� ������Ʈ ��Ȱ��ȭ
    public AudioClip buttonClip;
    private void Start()
    {
        for (int i = 0; i < moveGameObject.Length; i++)
        {
            moveGameObject[i].transform.position = startPosition.position;
        }
    }
    public void NextButton(int buttonN)
    {
        SoundManager.PlaySound(buttonClip);
        StartCoroutine(LerpNextPage(buttonN));
    }
    public void PreviousButton(int buttonN)
    {
        SoundManager.PlaySound(buttonClip);
        StartCoroutine(LerpPreviousPage(buttonN));
    }
    IEnumerator LerpNextPage(int buttonN)
    {
        currentTime = 0f; // �ʱ�ȭ

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            moveGameObject[buttonN].transform.position = Vector3.Lerp(startPosition.position, endPosition.position, t);
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ ��ġ ���� (��Ȯ�� endPosition�� ����)
        moveGameObject[buttonN].transform.position = endPosition.position;
    }

    IEnumerator LerpPreviousPage(int buttonN)
    {
        currentTime = 0f; // �ʱ�ȭ

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            moveGameObject[buttonN].transform.position = Vector3.Lerp(endPosition.position, startPosition.position, t);
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ ��ġ ���� (��Ȯ�� endPosition�� ����)
        moveGameObject[buttonN].transform.position = startPosition.position;
    }
    public void SaveUIExit()
    {
        SoundManager.PlaySound(buttonClip);
        exitGameObject.SetActive(false);
    }

    public void UIExit() 
    {
        SoundManager.PlaySound(buttonClip);
        savePageExitGameObject.SetActive(false);
    }
}
