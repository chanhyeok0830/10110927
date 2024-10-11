using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class DebugButton_Close : MonoBehaviour
{
    public Button button; // ��ư�� Unity �����Ϳ��� �Ҵ��ؾ� �մϴ�.
    //public Text countText; // ī��Ʈ ǥ�ø� ���� �ؽ�Ʈ
    public int count = 0;
    private float lastClickTime = 0f;
    private float resetTime = 2f; // 2�� ���� Ŭ���� ������ ī��Ʈ �ʱ�ȭ

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        // ������ Ŭ�� ���� 2�ʰ� ������ ī��Ʈ �ʱ�ȭ
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
        // �����Ϳ��� ���� ���� ���
        EditorApplication.isPlaying = false;
#else
        // ����� ���ӿ��� ���� ���� ���
        Application.Quit();
#endif
    }
}
