using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReaderPort : MonoBehaviour
{
    public class Data
    {
        public string portNumber;
    }
    public Data jsonData = new Data();

    SerialPort serialport;
    private float ledOnOffTime=0.2f;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
        OpenSerialPort();

        StartCoroutine(DelayedLedRGB("255,255,255"));
    }

    // Update is called once per frame
    void Update()
    {
        Update_SerialPort();
    }
    private void Update_SerialPort()
    {
       
        if (serialport.IsOpen/*&&GameObject.Find("Canvas_AnimUI").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ReadyTag")*/)
        {
            try
            {
                string data = serialport.ReadLine(); // ������ �б�
                //ebug.Log(data);
                if(!data.Contains(",")&&data!="")
                {
                    AudioManager.instance.playSfx(AudioManager.Sfx.TAG_OK);
                    StartCoroutine(LED_Off_Time(2, "255,255,255"));//led����
                    string nickname = GameObject.Find("Text_NickName").GetComponent<Text>().text;
                    GetComponent<APICallURLFunction>().Data_Save_UID(data,nickname);
                    GameObject.Find("Canvas_AnimUI").GetComponent<AnimFunction>().SetTrigger_OKTag();
                }
            }
            catch
            {

            }
        }


    }
    //Start
    private void OpenSerialPort()
    {
        serialport = new SerialPort(jsonData.portNumber, 9600, Parity.None, 8, StopBits.One);
        serialport.ReadTimeout = 10;

        try
        {
            serialport.Open();
            Debug.Log("Open Port : " + jsonData.portNumber);
        }
        catch
        {
            Debug.Log("Fail Port : " + jsonData.portNumber);
        }
    }

    private void LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Json_Files", "config.json");

        if (System.IO.File.Exists(filePath))
        {
            string dataAsJson = System.IO.File.ReadAllText(filePath);
            jsonData = JsonUtility.FromJson<Data>(dataAsJson);

            Debug.Log("config: " + jsonData.portNumber);
        }
        else
        {
            Debug.Log("config : " + filePath);
        }
    }
    public IEnumerator LED_Off_Time(int time, string ledcolor)
    {
        if (time == 0)
        {
            LedRGB(ledcolor);
        }
        else
        {
            for (int i = 0; i < time; i++)
            {
                LedRGB("0,0,0");
                yield return new WaitForSeconds(ledOnOffTime);
                LedRGB(ledcolor);
                yield return new WaitForSeconds(ledOnOffTime);
            }
        }


        yield break; // �ڷ�ƾ ����
    }

    public void LedRGB(string ledColor)
    {
        if (serialport == null)
            return;
        if (serialport.IsOpen)
        {
            try
            {
                // Unity���� �����͸� �ø��� ����Ϳ� ����
                serialport.WriteLine(ledColor);
                serialport.BaseStream.Flush();
            }
            catch
            {
                Debug.Log("Arduino Send color massege Fail!");
            }
        }
    }
        //End
    private void CloseSerialPort()
    {
        if (serialport != null)
        {
            if (serialport.IsOpen)
            {
                LedRGB("0,0,0");
                serialport.Close();
            }
        }

    }
    public void ResetPort()
    {        
        StartCoroutine(PortReset());
    }
    private IEnumerator PortReset()
    {
        Debug.Log("��Ʈ ����");
        CloseSerialPort();
        yield return new WaitForSeconds(3);
        OpenSerialPort(); 
        StartCoroutine(DelayedLedRGB("255,255,255"));

    }
    IEnumerator DelayedLedRGB(string ledColor)
    {
        yield return new WaitForSeconds(3); // 1�� ���
        LedRGB(ledColor); // LedRGB �Լ� ȣ��
    }
    void OnDestroy()
    {
        CloseSerialPort();
    }
    public void DeletBuffer()
    {
        serialport.DiscardInBuffer();
    }

}
