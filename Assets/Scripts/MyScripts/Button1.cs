using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용을 위해 추가
using System.IO; // 파일 입출력 사용을 위해 추가
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class Button1 : MonoBehaviour
{
    public Text inputField; // UnityEngine.UI.Text 사용 시 주석 해제
    private string jsonFilePath;

    [System.Serializable]
    public class JsonData
    {
        public string portNumber;
        public string text;
        public string rfid; // rfid 필드 추가
    }

    void Start()
    {
        // Assets/StreamingAssets/Json_Files 경로 설정
        jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Json_Files", "config.json");
        Debug.Log("JSON 파일 경로 설정 완료: " + jsonFilePath);
    }

    public void SaveTextFieldToJson()
    {
        Debug.Log("SaveTextFieldToJson 호출됨");

        // 입력 필드로부터 텍스트 가져오기
        if (inputField == null)
        {
            Debug.LogError("inputField가 설정되지 않았습니다. Unity 에디터에서 필드를 연결했는지 확인하세요.");
            return;
        }

        string inputText = inputField.text;
        Debug.Log("입력된 텍스트: " + inputText);

        // 기존 JSON 파일이 있는지 확인 후 불러오기
        JsonData jsonData = new JsonData();

        if (File.Exists(jsonFilePath))
        {
            Debug.Log("JSON 파일이 존재합니다: " + jsonFilePath);
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                jsonData = JsonUtility.FromJson<JsonData>(jsonContent);
                Debug.Log("JSON 파일 읽기 성공: " + jsonContent);

                // 기존 text 필드 값 삭제
                jsonData.text = string.Empty;
                Debug.Log("기존 text 필드 값 삭제 완료");
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON 파일을 읽는 도중 오류 발생: " + e.Message);
                return;
            }
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다. 새 파일을 생성합니다.");
        }

        // 새로운 text 필드 값 업데이트
        jsonData.text = inputText;
        Debug.Log("JSON 데이터의 text 필드 업데이트 완료: " + jsonData.text);

        // JSON 파일로 다시 저장
        try
        {
            string updatedJsonContent = JsonUtility.ToJson(jsonData, true);
            File.WriteAllText(jsonFilePath, updatedJsonContent);
            Debug.Log("JSON 파일 저장 성공: " + updatedJsonContent);
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON 파일을 저장하는 도중 오류 발생: " + e.Message);
        }
    }

    public void SaveRfidToJson(string rfidValue)
    {
        Debug.Log("SaveRfidToJson 호출됨");

        // 기존 JSON 파일이 있는지 확인 후 불러오기
        JsonData jsonData = new JsonData();

        if (File.Exists(jsonFilePath))
        {
            Debug.Log("JSON 파일이 존재합니다: " + jsonFilePath);
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                jsonData = JsonUtility.FromJson<JsonData>(jsonContent);
                Debug.Log("JSON 파일 읽기 성공: " + jsonContent);

                // 기존 rfid 필드 값 삭제
                jsonData.rfid = string.Empty;
                Debug.Log("기존 rfid 필드 값 삭제 완료");
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON 파일을 읽는 도중 오류 발생: " + e.Message);
                return;
            }
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다. 새 파일을 생성합니다.");
        }

        // 새로운 rfid 필드 값 업데이트
        jsonData.rfid = rfidValue;
        Debug.Log("JSON 데이터의 rfid 필드 업데이트 완료: " + jsonData.rfid);

        // JSON 파일로 다시 저장
        try
        {
            string updatedJsonContent = JsonUtility.ToJson(jsonData, true);
            File.WriteAllText(jsonFilePath, updatedJsonContent);
            Debug.Log("JSON 파일 저장 성공: " + updatedJsonContent);
            Debug.Log("추가된 RFID 값: " + jsonData.rfid); // 추가된 RFID 값을 로그로 출력
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON 파일을 저장하는 도중 오류 발생: " + e.Message);
        }
    }

    public void GoToScene2()
    {
        SceneManager.LoadScene(2);
    }
}