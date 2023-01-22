using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class NetWorkManager : MonoBehaviour
{
    [SerializeField] private int[] cheerNum = {0,0 };
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    public GameManager gameManager;
    public float[] cheerPower = {0,0};
    public float[] max = {6,6};



    private string baseurl = "https://functionsnokogiri.azurewebsites.net/api";
    


    public float nowTime;
    private string[] url = {"", ""};
    private string[] uuids = new string[2];
    private int[] id = { 1,0 };

    void Start()
    {
        uuids = TitleScene.getIDs();

        P1.transform.localScale = new Vector3(1, 1, 1);
        P2.transform.localScale = new Vector3(1, 1, 1);
        nowTime = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        cheerPower[0] = cheerNum[0] * 0.03f + 1;
        cheerPower[1] = cheerNum[1] * 0.03f + 1;
        P1.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * cheerPower[0] / 6);
        P2.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * cheerPower[1] / 6);

        if (cheerPower[0] > max[0])
        {
            cheerPower[0] = max[0];
            P1.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * max[0] / 6);
        }
        else
        {
            P1.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * cheerPower[0] / 6);
        }
        if (cheerPower[1] > max[1])
        {
            cheerPower[1] = max[1];
            P2.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * max[1] / 6);
        }
        else
        {
            P2.transform.localScale = new Vector3(1, 1, 1) + (new Vector3(1, 1, 1) * cheerPower[1] / 6);
        }

        if (gameManager.isGame)
        {
            nowTime += Time.deltaTime;
            if (nowTime > 15)
            {
                nowTime = 0;
                Debug.Log("Update");
                Gettingdata();
            }
        }
    }


    public void Gettingdata()
    {
        url[0] = baseurl + "/getPoint/" + uuids[0];
        url[1] = baseurl + "/getPoint/" + uuids[1];
        StartCoroutine("GetData", 0);
        StartCoroutine("GetData", 1);
    }

    IEnumerator GetData(int num)
    {
        UnityWebRequest response = UnityWebRequest.Get(url[num]);
        yield return response.SendWebRequest();
        switch (response.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(response.downloadHandler.text);
                cheerNum[num] = Int32.Parse(response.downloadHandler.text);
                break;
        }
    }


}
