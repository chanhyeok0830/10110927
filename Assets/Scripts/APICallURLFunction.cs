using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;
using System.Text.RegularExpressions;

public class APICallURLFunction : MonoBehaviour
{

    public class GETDATAINFO
    {
        public string IDX;
        public string UID;
        public string NICKNAME;
        public string STARTTIME;
        public string ENDTIME;
        public string EXHIBIT1;
        public string EXHIBIT2;
        public string EXHIBIT3;
        public string EXHIBIT4;
        public string EXHIBIT5;
        public string EXHIBIT6;
        public string EXHIBIT7;
        public string EXHIBIT8;
        public string EXHIBIT9;
        public string EXHIBIT10;
        public string EXHIBIT11;
        public string EXHIBIT12;
        public string EXHIBIT13;
        public string EXHIBIT14;
        public string EXHIBIT15;
    }
    public GETDATAINFO getdataInfo=new GETDATAINFO();
    public enum DATALIST { IDX,UID,NICKNAME,STARTTIME,ENDTIME,EXHIBIT1,EXHIBIT2,EXHIBIT3,EXHIBIT4,EXHIBIT5,EXHIBIT6,EXHIBIT7,EXHIBIT8,EXHIBIT9,EXHIBIT10,EXHIBIT11,EXHIBIT12,EXHIBIT13,EXHIBIT14,EXHIBIT15 }

    public void Data_Save_UID(string uid, string nickname)
    {

        string url = $"http://175.118.126.63/polar/start.cfm?uid={uid}&nickname={nickname}";
        StartCoroutine(Save_CallAPI(url));
    }
    public void Data_Save_Velue(string uid, DATALIST datalist,string value)
    {
        string url = "";
        switch(datalist)
        {
            case DATALIST.EXHIBIT1: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT1&contentValue={value}"; break;
            case DATALIST.EXHIBIT2: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT2&contentValue={value}"; break;
            case DATALIST.EXHIBIT3: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT3&contentValue={value}"; break;
            case DATALIST.EXHIBIT4: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT4&contentValue={value}"; break;
            case DATALIST.EXHIBIT5: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT5&contentValue={value}"; break;
            case DATALIST.EXHIBIT6: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT6&contentValue={value}"; break;
            case DATALIST.EXHIBIT7: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT7&contentValue={value}"; break;
            case DATALIST.EXHIBIT8: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT8&contentValue={value}"; break;
            case DATALIST.EXHIBIT9: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT9&contentValue={value}"; break;
            case DATALIST.EXHIBIT10: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT10&contentValue={value}"; break;
            case DATALIST.EXHIBIT11: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT11&contentValue={value}"; break;
            case DATALIST.EXHIBIT12: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT12&contentValue={value}"; break;
            case DATALIST.EXHIBIT13: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT13&contentValue={value}"; break;
            case DATALIST.EXHIBIT14: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT14&contentValue={value}"; break;
            case DATALIST.EXHIBIT15: url = $"http://175.118.126.63/polar/updateExhibit.cfm?idx={getdataInfo.IDX}&contentNo=EXHIBIT15&contentValue={value}"; break;
        }
        StartCoroutine(Save_CallAPI(url));
    }
    IEnumerator Save_CallAPI(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // ��û�� ������ ������ ���ƿ� ������ ���
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("SaveAPI_Success : " + url);
            }
            else
            {
                Debug.Log("SaveAPI_Fail : " + url);
            }
        }
    }
    public IEnumerator GETIDX(string myuid)
    {
        UnityWebRequest www = UnityWebRequest.Get($"http://175.118.126.63/polar/getIDX.cfm?uid={myuid}");

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Json string�� �޾ƿ�
            string jsondata = www.downloadHandler.text;
            getdataInfo.IDX = jsondata;
        }
        else
        {
            Debug.Log($"{myuid} : ������ �ҷ����� ����");
        }
    }
    public IEnumerator GETDATA_LoadURL(string myuid)
    {
        yield return StartCoroutine(GETIDX(myuid));

        UnityWebRequest www = UnityWebRequest.Get($"http://175.118.126.63/polar/getData.cfm?idx={getdataInfo.IDX}");

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Json string�� �޾ƿ�
            string jsondata = www.downloadHandler.text;
            // ��ųʸ� ����
            Dictionary<string, object> UserData;
            // ��ųʸ� ��ȯ
            UserData = Json.Deserialize(jsondata) as Dictionary<string, object>;
            // �� �κ� ����
            List<object> columnsData = UserData["COLUMNS"] as List<object>;
            // DATA�� ����
            List<object> userData = UserData["DATA"] as List<object>;
            List<object> dataArray = userData[0] as List<object>;

            for (int i = 0; i < (int)DATALIST.EXHIBIT15+1; i++)
            {
                string result = null;
                if (dataArray[i].ToString() != null)
                {
                    result = dataArray[i].ToString();
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = "";
                }
                if (IsUrlEncoded(result))
                {
                    result = CallSearchPanelWeb(result);
                }
                switch(i)
                {
                    case (int)DATALIST.IDX:         getdataInfo.IDX        = result; break;
                    case (int)DATALIST.UID:         getdataInfo.UID        = result; break;
                    case (int)DATALIST.NICKNAME:    getdataInfo.NICKNAME   = result; break;
                    case (int)DATALIST.STARTTIME:   getdataInfo.STARTTIME  = result; break;
                    case (int)DATALIST.ENDTIME:     getdataInfo.ENDTIME    = result; break;
                    case (int)DATALIST.EXHIBIT1:    getdataInfo.EXHIBIT1   = result; break;
                    case (int)DATALIST.EXHIBIT2:    getdataInfo.EXHIBIT2   = result; break;
                    case (int)DATALIST.EXHIBIT3:    getdataInfo.EXHIBIT3   = result; break;
                    case (int)DATALIST.EXHIBIT4:    getdataInfo.EXHIBIT4   = result; break;
                    case (int)DATALIST.EXHIBIT5:    getdataInfo.EXHIBIT5   = result; break;
                    case (int)DATALIST.EXHIBIT6:    getdataInfo.EXHIBIT6   = result; break;
                    case (int)DATALIST.EXHIBIT7:    getdataInfo.EXHIBIT7   = result; break;
                    case (int)DATALIST.EXHIBIT8:    getdataInfo.EXHIBIT8   = result; break;
                    case (int)DATALIST.EXHIBIT9:    getdataInfo.EXHIBIT9   = result; break;
                    case (int)DATALIST.EXHIBIT10:   getdataInfo.EXHIBIT10  = result; break;
                    case (int)DATALIST.EXHIBIT11:   getdataInfo.EXHIBIT11  = result; break;
                    case (int)DATALIST.EXHIBIT12:   getdataInfo.EXHIBIT12  = result; break;
                    case (int)DATALIST.EXHIBIT13:   getdataInfo.EXHIBIT13  = result; break;
                    case (int)DATALIST.EXHIBIT14:   getdataInfo.EXHIBIT14  = result; break;
                    case (int)DATALIST.EXHIBIT15:   getdataInfo.EXHIBIT15  = result; break;
                }
            }

            Debug.Log(getdataInfo);

        }
        else
        {
            Debug.Log($"{myuid} : ������ �ҷ����� ����");
        }
    }



    //���� �ҷ��� ���� ���ڵ� ������ �˱� �����Լ�
    public bool IsUrlEncoded(string str)
    {
        // URL ���ڵ� ������ ���� ǥ�������� ����
        Regex urlEncodePattern = new Regex(@"%[0-9a-fA-F]{2}");

        // ���� ǥ������ ����Ͽ� ���ڿ� ���� ���� ��ġ ���� Ȯ��
        return urlEncodePattern.IsMatch(str);
    }
    //���ڵ��� �ѱ۷� ��ȯ�ϱ�
    public string CallSearchPanelWeb(string incoding)
    {
        string decoding = "";
        string encodedString = incoding;
        string decodedString = UnityWebRequest.UnEscapeURL(encodedString);
        decoding = decodedString;

        Debug.Log(decodedString);

        return decoding;
    }
}
