using UnityEngine;

public class SoundManager_yy : MonoBehaviour
{
    // シングルトンインスタンス
    private static SoundManager_yy instance;
    
    // AudioSourceコンポーネント
    private AudioSource audioSource;

    // シングルトンインスタンスへのアクセス用プロパティ
    public static SoundManager_yy Instance
    {
        get
        {
            if (instance == null)
            {
                // シーンにSoundManagerが存在しない場合、新しく作成
                GameObject go = new GameObject("SoundManager");
                instance = go.AddComponent<SoundManager_yy>();
                DontDestroyOnLoad(go); // シーンを跨いでも破棄されないようにする
            }
            return instance;
        }
    }

    private void Awake()
    {
        // 既存のインスタンスがある場合、このオブジェクトを破棄
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    // 効果音を再生するメソッド（AudioClipを指定）
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    // 効果音を再生するメソッド（クリップ名を指定）
    public void PlaySound(string clipName, float volume = 1.0f)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"Sound clip not found: {clipName}");
        }
    }

    // 音量を設定するメソッド
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}