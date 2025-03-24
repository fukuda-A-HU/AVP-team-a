using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PinchDetection_yy : MonoBehaviour
{
    private GameObject m_SelectedObject;

    void OnEnable()
    {
        // EnhancedTouchSupportを有効化
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        // EnhancedTouchSupportを無効化
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        var activeTouches = Touch.activeTouches;
        //Debug.Log(string.Format("{0}", activeTouches.Count));
        // タッチが2本以上ある場合、ピンチジェスチャーの可能性を検討
        if (activeTouches.Count > 0)
        {
            // タッチを取得
            SpatialPointerState touch1Data = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);

            // ピンチ開始時（どちらかのタッチがBeganフェーズ）の処理
            if (activeTouches[0].phase == TouchPhase.Began)
            {
                // ターゲットオブジェクトがある場合、それを選択
                if (touch1Data.targetObject != null)
                {
                    m_SelectedObject = touch1Data.targetObject;
                    Debug.Log($"Indirect Pinch detected on object: {m_SelectedObject.name}");
                    // ここで呼び出す
                }
            }

            // ピンチ終了時（どちらかのタッチがEndedまたはCanceledフェーズ）の処理
            if (activeTouches[0].phase == TouchPhase.Ended || activeTouches[0].phase == TouchPhase.Canceled)
            {
                m_SelectedObject = null;
            }
        }
        else
        {
            // タッチが2本未満の場合、選択を解除
            m_SelectedObject = null;
        }
    }
}