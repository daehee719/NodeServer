using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private bool _isLoading = false;
    [SerializeField]
    public Image _targetImage;

    private void Start()
    {
        //UnityWebRequest webReq = UnityWebRequestTexture.GetTexture("http://localhost:50000/imagelist");
        //string msgList = webReq.downloadHandler.text;
        //ImageListVO vo = JsonUtility.FromJson<ImageListVO>(msgList);
        //for (int i = 0; i < vo.imageCount; i++)
        //{
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_isLoading) return;
            _isLoading = true;
            Debug.Log("Loading Data From Server");
            StartCoroutine(GetDataFromServer());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_isLoading) return;
            _isLoading = true;
            Debug.Log("Loading Data From Server");
            StartCoroutine(GetImageFromServer());
        }
    }

    private IEnumerator GetImageFromServer()
    {
        UnityWebRequest webReq = UnityWebRequestTexture.GetTexture("http://localhost:50000/image");
        yield return webReq.SendWebRequest();
        //if (webReq.result == UnityWebRequest.Result.Success)
        //{
            Texture2D texture = ((DownloadHandlerTexture)webReq.downloadHandler).texture;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            Debug.Log(texture);
            _targetImage.sprite = sprite;
        //}
        _isLoading = false;
        Debug.Log("Loading complete");
    }

    private IEnumerator GetDataFromServer()
    {
        UnityWebRequest webReq = UnityWebRequest.Get("http://localhost:50000");

        yield return webReq.SendWebRequest();

        //if (webReq.result == UnityWebRequest.Result.Success)
        //{
            string msg = webReq.downloadHandler.text;
            Debug.Log(msg);
            DataVO vo = JsonUtility.FromJson<DataVO>(msg);
            // 들어온 데이터를 파싱해서
            // 3명을 순서대로 출력
            foreach (User user in vo.users)
                Debug.Log(user.name);
        //}

        _isLoading = false;
        Debug.Log("Loading complete");
    }
}
