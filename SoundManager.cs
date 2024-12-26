using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // �̱��� ����
    public AudioSource audioSource; // ���� ���� ��� AudioSource

    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ���� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }

    // ���� �޼���� ���� ���
    public static void PlaySound(AudioClip clip)
    {
        if (Instance != null && Instance.audioSource != null && clip != null)
        {
            Instance.audioSource.PlayOneShot(clip);
        }
    }
}
