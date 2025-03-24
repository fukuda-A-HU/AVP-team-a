using UnityEngine;

public class SECallerExample_yy : MonoBehaviour
{
    public AudioClip testSound; // Inspectorで設定可能なAudioClip

    [ContextMenu("SETest")]
    public void SETest()
    {
        // AudioClipを直接指定して再生
        SoundManager_yy.Instance.PlaySound(testSound);

        // 音量を指定して再生
        SoundManager_yy.Instance.PlaySound(testSound, 0.5f);

        // Resources/Soundsフォルダからクリップ名で再生
        //SoundManager_yy.Instance.PlaySound("GrowSE");

        // 全体の音量を設定
        SoundManager_yy.Instance.SetVolume(0.8f);
    }
}
