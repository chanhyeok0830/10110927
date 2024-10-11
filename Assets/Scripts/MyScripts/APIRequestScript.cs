using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class ConfigData
{
    public string url;
    public string rfid;
    public string text;
}

public class APIRequestScript : MonoBehaviour
{
    public TextMeshProUGUI responseText;
    private string configFilePath;

    private void Start()
    {
        configFilePath = Path.Combine(Application.streamingAssetsPath, "Json_Files", "config.json");
        StartCoroutine(LoadConfigAndMakeRequest());
    }

    private IEnumerator LoadConfigAndMakeRequest()
    {
        string jsonText;

        // JSON 파일 읽기
        if (File.Exists(configFilePath))
        {
            jsonText = File.ReadAllText(configFilePath);
        }
        else
        {
            Debug.LogError("config.json 파일을 찾을 수 없습니다.");
            yield break;
        }

        // JSON 데이터 파싱
        ConfigData config = JsonUtility.FromJson<ConfigData>(jsonText);
        string url = config.url;
        string rfid = config.rfid;
        string text = config.text;

        // API 요청 URL 구성
        string finalUrl = url + "?UID=" + rfid + "&name=" + text;

        // API 호출
        yield return MakeApiRequest(finalUrl);
    }

    private IEnumerator MakeApiRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseData = request.downloadHandler.text;
                Debug.Log("Response received: " + responseData);
                responseText.text = responseData;
            }
            else
            {
                Debug.LogError("API 호출 실패: " + request.responseCode);
                responseText.text = "API 호출 실패: " + request.responseCode;
            }
        }
    }
}