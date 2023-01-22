using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor.PackageManager.Requests;
using System;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject Modal;
    [SerializeField] private GameObject P1Name;
    [SerializeField] private GameObject P2Name;

    public static string[] uuid = new string[2];

    class user
    {
        public string name;
        public string uuid;
    }

    [Serializable]
    public struct Request
    {
        public string param1;
    }
    public Request request;

    /// レスポンスデータ
    [Serializable]
    public struct Response
    {
        public string res;
    }
    public Response response;


    string url = "https://nokogiri-backend-xejr.onrender.com/api/v1";

    private SceneManager sceneManager;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !Modal.activeSelf)
        {
            Modal.SetActive(true);
        }
    }

    private void Start()
    {
        Modal.SetActive(false);
    }

    public void ModalChange(bool value)
    {
        Modal.SetActive(value);
    }
    public void PushButton()
    {
        InputField Input1 = P1Name.GetComponent<InputField>();
        InputField Input2 = P2Name.GetComponent<InputField>();

        string Name1 = Input1.text;
        var newguid1 = System.Guid.NewGuid();
        uuid[0] = newguid1.ToString();
        //Debug.Log(uuid[0]);

        string Name2 = Input2.text;
        var newguid2 = System.Guid.NewGuid();
        uuid[1] = newguid2.ToString();
        //Debug.Log(uuid[1]);

        //Debug.Log(Name1 + " " + uuid[0]+ " " + Name2 + " " + uuid[1]);
        //PostingData(Name1 + " " + uuid[0] + " " + Name2 + " " + uuid[1]);


        SceneManager.LoadScene("BattleScene");
    }
    public static string[] getIDs()
    {
        return uuid;
    }



    private void Post()
    {

    }

    public void Gettingdata()
    {
        StartCoroutine("GetData", url);
    }

    IEnumerator GetData(string URL)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);
        yield return response.SendWebRequest();
        switch (response.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(response.downloadHandler.text);
                break;
        }
    }

    public void PostingData(string data)
    {
        StartCoroutine("PostData", "data");
    }

    IEnumerator PostData(string Data)
    {
        UnityWebRequest response = UnityWebRequest.Post("https://nokogiri-backend-xejr.onrender.com/temp", "hoge");
        yield return response.SendWebRequest();
        switch (response.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(response.downloadHandler.text);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("プロトコルエラー");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("データぷろっシングエラー");
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("コネクションエラー");
                break;
        }
    }
}


