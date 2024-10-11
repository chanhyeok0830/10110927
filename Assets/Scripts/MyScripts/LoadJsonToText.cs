using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 사용을 위해 추가
using System.IO; // 파일 입출력 사용을 위해 추가

public class LoadJsonToText : MonoBehaviour
{
    public TMP_Text textMeshProField; // TextMeshPro 텍스트 필드

    private string jsonFilePath;

    [System.Serializable]
    public class JsonData
    {
        public string portNumber;
        public string text;
    }

    void Start()
    {
        // JSON 파일 경로 설정
        jsonFilePath = Path.Combine("C:\\Users\\HULIAC\\10110927\\Assets\\StreamingAssets\\Json_Files", "config.json");
        Debug.Log("JSON 파일 경로 설정 완료: " + jsonFilePath);

        // JSON 파일 읽기 및 TextMeshPro에 값 반영
        LoadJsonAndSetText();
    }

    void LoadJsonAndSetText()
    {
        if (File.Exists(jsonFilePath))
        {
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonContent);
                Debug.Log("JSON 파일 읽기 성공: " + jsonContent);

                // JSON 데이터의 text 필드를 TextMeshPro에 반영
                if (textMeshProField != null)
                {
                    textMeshProField.text = jsonData.text+"님,\n입구 앞에서 대기해 주세요.";
                    Debug.Log("TextMeshPro 필드에 텍스트 설정 완료: " + jsonData.text);
                }
                else
                {
                    Debug.LogError("TextMeshPro 필드가 연결되지 않았습니다. Unity 에디터에서 필드를 연결했는지 확인하세요.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON 파일을 읽는 도중 오류 발생: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("JSON 파일이 존재하지 않습니다: " + jsonFilePath);
        }
    }
}
