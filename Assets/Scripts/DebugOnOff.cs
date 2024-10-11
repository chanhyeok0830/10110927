using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOnOff : MonoBehaviour
{
    public Button button; // ��ư�� Unity �����Ϳ��� �Ҵ��ؾ� �մϴ�.
    public GameObject debugconsole;
    //public Text countText; // ī��Ʈ ǥ�ø� ���� �ؽ�Ʈ
    public int count = 0;
    private float lastClickTime = 0f;
    private float resetTime = 2f; // 2�� ���� Ŭ���� ������ ī��Ʈ �ʱ�ȭ

    private bool isOn = false;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        OnOffDebug();
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
