using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOnOff : MonoBehaviour
{
    public Button button; // 버튼을 Unity 에디터에서 할당해야 합니다.
    public GameObject debugconsole;
    //public Text countText; // 카운트 표시를 위한 텍스트
    public int count = 0;
    private float lastClickTime = 0f;
    private float resetTime = 2f; // 2초 동안 클릭이 없으면 카운트 초기화

    private bool isOn = false;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        OnOffDebug();
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
            isOn = !isOn;
            OnOffDebug();
        }
    }



    void OnOffDebug()
    {
        if(!isOn) 
            debugconsole.GetComponent<Canvas>().sortingOrder = -999;
        else
            debugconsole.GetComponent<Canvas>().sortingOrder = 999;
    }
}
