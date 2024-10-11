using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class DebugButton_Close : MonoBehaviour
{
    public Button button; // 버튼을 Unity 에디터에서 할당해야 합니다.
    //public Text countText; // 카운트 표시를 위한 텍스트
    public int count = 0;
    private float lastClickTime = 0f;
    private float resetTime = 2f; // 2초 동안 클릭이 없으면 카운트 초기화

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        // 마지막 클릭 이후 2초가 지나면 카운트 초기화
        if (Time.time - lastClickTime > resetTime && count > 0)
        {
            count = 0;
        }
    }

    void OnButtonClick()
    {
        count++;
        lastClickTime = Time.time;

        if (count >= 20)
        {
            EndGame();
        }
    }

 

    void EndGame()
    {
#if UNITY_EDITOR
        // 에디터에서 실행 중인 경우
        EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 실행 중인 경우
        Application.Quit();
#endif
    }
}
