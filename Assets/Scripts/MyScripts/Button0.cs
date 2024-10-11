using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.IO.Ports;
using System; // TimeoutException을 사용하기 위해 추가
using Newtonsoft.Json.Linq; // JSON 파싱을 위해 추가

public class Button0 : MonoBehaviour
{
    string portName;
    private SerialPort serialPort;

    void Start()
    {
        // JSON 파일에서 포트 이름 값을 읽어오기
        LoadConfig();

        // 시리얼 포트 설정 및 열기
        if (!string.IsNullOrEmpty(portName))
        {
            serialPort = new SerialPort(portName, 9600); // 보드레이트는 고정 값 사용
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 10;
                Debug.Log("시리얼 포트가 성공적으로 열렸습니다: " + portName);
            }
            catch (IOException ex)
            {
                Debug.LogError("시리얼 포트를 여는 도중 오류 발생: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("포트 이름 값이 잘못되었습니다.");
        }
    }

    void LoadConfig()
    {
        string configPath = Path.Combine(Application.streamingAssetsPath, "Json_Files/config.json");
        if (File.Exists(configPath))
        {
            try
            {
                string jsonContent = File.ReadAllText(configPath);
                JObject config = JObject.Parse(jsonContent);
                portName = config["portNumber"].ToString();
                Debug.Log("Config 파일에서 포트를 성공적으로 불러왔습니다: " + portName);
            }
            catch (Exception ex)
            {
                Debug.LogError("Config 파일을 읽는 도중 오류 발생: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Config 파일을 찾을 수 없습니다: " + configPath);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine().Trim();
                if (!string.IsNullOrEmpty(data))
                {
                    // 데이터가 올바르게 수신된 경우 JSON 파일에 저장
                    SaveRfidToConfig(data);
                    // 씬 전환
                    GoToScene1();
                }
            }
            catch (TimeoutException)
            {
                // 시리얼 포트에서 데이터가 없을 때 발생하는 예외
                // 별도의 처리가 필요하지 않음
            }
            catch (System.Exception ex)
            {
                Debug.LogError("시리얼 포트에서 읽는 도중 오류 발생: " + ex.Message);
            }
        }
    }

    void SaveRfidToConfig(string rfidValue)
    {
        // \u0002 제거
        rfidValue = rfidValue.Replace("\u0002", "");

        string configPath = Path.Combine(Application.streamingAssetsPath, "Json_Files/config.json");
        if (File.Exists(configPath))
        {
            try
            {
                string jsonContent = File.ReadAllText(configPath);
                JObject config = JObject.Parse(jsonContent);
                // 기존 RFID 필드 값 삭제 후 새로운 값 설정
                if (config.ContainsKey("rfid"))
                {
                    config.Remove("rfid");
                }
                config["rfid"] = rfidValue;
                File.WriteAllText(configPath, config.ToString());
                Debug.Log("RFID 값이 Config 파일에 성공적으로 저장되었습니다: " + rfidValue);
            }
            catch (Exception ex)
            {
                Debug.LogError("Config 파일에 RFID 값을 저장하는 도중 오류 발생: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Config 파일을 찾을 수 없습니다: " + configPath);
        }
    }

    void OnDestroy()
    {
        // 게임 오브젝트가 파괴될 때 시리얼 포트를 닫기
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("시리얼 포트가 닫혔습니다: " + portName);
        }
    }

    public void GoToScene1()
    {
        SceneManager.LoadScene(1);
    }
}