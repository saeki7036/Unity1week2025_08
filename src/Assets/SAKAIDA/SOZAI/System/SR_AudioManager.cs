using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_AudioManager : MonoBehaviour
{
    public static SR_AudioManager instance;
    [SerializeField] GameObject AudioPlayObj;
    [SerializeField] AudioClip BGM;
    AudioSource BgmSource;
    SR_AudioPlay audioPlay;

    public List<SR_AudioPlay> audioPlays = new List<SR_AudioPlay>();

    public Dictionary<AudioClip, float> lastPlayTimes = new Dictionary<AudioClip, float>();
    public float minInterval = 0.1f; // 効果音を再生する間隔（秒）

    void Start()
    {
        BgmSource = GetComponent<AudioSource>();
        if (BGM != null)
        {
            BgmSource.clip = BGM;
            BgmSource.Play();
        }

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayBGM(AudioClip BGMclip) 
    {
       
            BgmSource.clip = BGMclip;
            BgmSource.Play();
 
    }
    public void StopBGM() 
    {
        BgmSource.Stop();
    }
    public void isPlaySE(AudioClip Clip)
    {
        /*
        foreach (var audios in audioPlays)
        {
            if (audios != null)
            {
                if (audios.GetCurrentClip() == Clip && audios.IsPlaying())
                {
                    Debug.Log("sound");
                    return; // 同じクリップが再生中なら新規再生を中断
                }
            }
        }*/
        if (lastPlayTimes.ContainsKey(Clip))
        {
            float lastPlayTime = lastPlayTimes[Clip];
            if (Time.time - lastPlayTime < minInterval)
            {
                //Debug.Log("再生間隔中：スキップ");
                return; // 最低間隔を満たしていない場合、再生しない
            }
        }

        GameObject CL_AudioPlay = Instantiate(AudioPlayObj);
        SR_AudioPlay audio = CL_AudioPlay.GetComponent<SR_AudioPlay>();

        audio.isCL_PlaySE(Clip);
        CL_AudioPlay.SetActive(true);

        audioPlays.Add(audio);

        // 再生時間を記録
        lastPlayTimes[Clip] = Time.time;

    }

    public void StopAllAudio()
    {
        // すべての再生中の音を停止する
        foreach (var audio in audioPlays)
        {
            audio.Stop(); // AudioPlayクラスにStopメソッドがあると仮定
        }
        audioPlays.Clear(); // リストをクリアして再利用可能に
    }
}
