using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimFunction : MonoBehaviour
{
    public Animator animator;
    
    private Canvas canvas;
    private SetButtonImageToPath agreeCheck;
    [SerializeField]
    private ReaderPort m_readerPort;
    private void Start()
    {
        agreeCheck = GameObject.Find("Button_CheckAgree").GetComponent<SetButtonImageToPath>();
        canvas= GetComponent<Canvas>();
        m_readerPort = FindFirstObjectByType<ReaderPort>();
    }
    public void SetTrigger_ReadyTag()
    {
        if(agreeCheck.isON&& GameObject.Find("Text_NickName").GetComponent<Text>().text!="")
        {
            animator.SetTrigger("ReadyTag");
        }
    }
    public void SetTrigger_OKTag()
    {
        animator.SetTrigger("OKTag");
    }
    public void ResetOKTagTrigger()
    {
        animator.ResetTrigger("OKTag");

    }
    public void DeletBuffer()
    {
        GameObject.Find("GameManager").GetComponent<ReaderPort>().DeletBuffer();
    }
    public void CanvasSortingOrder_On()
    {
        canvas.sortingOrder = 3;
    }
    public void CanvasSortingOrder_Off()
    {
        canvas.sortingOrder = -1;
    }
    public void ResetInfo()
    {
        GameObject.Find("GameManager").GetComponent<APICallURLFunction>().getdataInfo=new APICallURLFunction.GETDATAINFO();
        GameObject.Find("VurtualTextInputBox").GetComponent<VirtualTextInputBox>().Clear();
        GameObject.Find("Button_CheckAgree").GetComponent<SetButtonImageToPath>().ToggleImage();
        m_readerPort.ResetPort();
    }
}
