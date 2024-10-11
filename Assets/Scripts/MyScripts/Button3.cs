using UnityEngine;
using UnityEngine.SceneManagement;

public class Button3 : MonoBehaviour
{
    private float timeElapsed;
    private bool isTimerActive;

    void Start()
    {
        // 타이머 시작
        isTimerActive = true;
        timeElapsed = 0f;
    }

    void Update()
    {
        // 타이머가 활성화된 경우 시간 누적
        if (isTimerActive)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 5f)
            {
                GoTosScene0();
            }
        }
    }

    public void GoTosScene0()
    {
        SceneManager.LoadScene(0);
    }
}