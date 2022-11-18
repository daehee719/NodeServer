using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class SendDataObject : MonoBehaviour
{
    private InputField _inputScore;
    private InputField _inputUsername;

    private Button _sendBtn;

    private void Awake()
    {
        _inputScore = transform.Find("InputScore").GetComponent<InputField>();
        _inputUsername = transform.Find("InputUsername").GetComponent<InputField>();
        _sendBtn = transform.Find("Button").GetComponent<Button>();
        _sendBtn.onClick.AddListener(() =>
        {
            StartCoroutine(SendRecordData());
        });
    }

    IEnumerator SendRecordData()
    {
        int score = int.Parse(_inputScore.text);
        string username = _inputUsername.text;

        RecordVO vo = new RecordVO { score = score, username = username };
        string json = JsonUtility.ToJson(vo);


        UnityWebRequest req = new UnityWebRequest("http://localhost:50000/insert","POST");
        byte[] dataByte = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(dataByte);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-type", "application/json");
        yield return req.SendWebRequest();
        string result = req.downloadHandler.text;
        Debug.Log(result);
    }
}
