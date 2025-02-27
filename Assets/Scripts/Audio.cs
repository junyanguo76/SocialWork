using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������ֵĹ�����
/// </summary>
public class AudioManager : MonoBehaviour
{
    //��Ҫ�������ŵ�������
    public AudioClip[] audioGroup;

    //��ǰ���ŵ���˭
    private int playingIndex;

    //�Ƿ�����������
    private bool canPlayAudio;

    //AudioSource���
    private AudioSource audioSource;

    //-----------------------------------------------------

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        canPlayAudio = true;

        playingIndex = 0;
    }

    //-----------------------------------------------------

    void Update()
    {
        if (canPlayAudio)
        {
            PlayAudio();

            canPlayAudio = false;
        }

        if (!audioSource.isPlaying)
        {
            playingIndex++;

            if (playingIndex >= audioGroup.Length)
            {
                playingIndex = 0;
            }

            canPlayAudio = true;
        }
    }

    //-----------------------------------------------------

    private void PlayAudio()
    {
        audioSource.clip = audioGroup[playingIndex];
        audioSource.Play();
    }

    //-----------------------------------------------------
}