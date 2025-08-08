using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_System : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    public static SR_System instance;
    SR_AudioManager audioManager => SR_AudioManager.instance;
    // Start is called before the first frame update

    [SerializeField] AudioClip GameOverClip_Bomb;

    [SerializeField] AudioClip NingenKorosu_Clip;
    [SerializeField] AudioClip GameStartClip;
    [SerializeField] Animator ShutterAnimator;

    [SerializeField] SR_ScoreManager scoreManager;
    [SerializeField] GameObject ScoreBoard;
    [SerializeField] float GameStartTime = 0;
    float GameStartCount = 0;

    public float DecreaseCoffee = 1;//一秒の減少量

    public enum GameMode 
    { 
    
        Before,
        GameStart,
        MainGame,
        After
    
    }
    public GameMode gameMode = GameMode.Before;

    void Start()
    {
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
        switch (gameMode) 
        {

            case GameMode.Before:

                break;
            case GameMode.GameStart:
                isGameStart();
                break;
            case GameMode.MainGame:

                break;
            case GameMode.After:
                isAfter();
                break;

        }
    }
    void isGameStart() 
    {

        audioManager.isPlaySE(GameStartClip);

        scoreManager.isClearScoreText();
        ShutterAnimator.Play("開く",0,0);
        gameMode = GameMode.MainGame;
    }
    void isAfter() 
    {

        audioManager.isPlaySE(GameOverClip_Bomb);

        StartCoroutine(Shake(0.3f,0.8f));

        scoreManager.SaveScore = scoreManager.AllScore;//スコア情報を念のため保存
        scoreManager.AllScore = 0;//ゲームの合計スコア
        ScoreBoard.SetActive(true);
        

        ShutterAnimator.Play("閉じる", 0, 0);
        gameMode = GameMode.Before;
    }

    public void NINGEN_KOROSU() 
    {
        StartCoroutine(Shake(0.1f,0.2f));
        audioManager.isPlaySE(NingenKorosu_Clip);
    }

    public void isGameOver() 
    {
        gameMode = GameMode.After;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        Debug.Log("A");
        //magnitude = 揺らす強さ
        //duration　= 揺らす時間
        Vector3 originalPos = Camera.transform.localPosition; // 元の位置を記憶
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // ランダムな位置にずらす
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            Camera.transform.localPosition = new Vector3(
                originalPos.x + offsetX,
                originalPos.y + offsetY,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }
        Camera.transform.localPosition = new Vector3(0,0,-10) ;
    }
}
