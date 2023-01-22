using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float[] PlayerDamage = {0,0};
    public int[] PlayerStocks = {1,1};
    public float BattleSec;
    public float BattleMin;
    public bool isGame;

    [SerializeField]private List<GameObject> stocksImages;
    [SerializeField] private TextMeshProUGUI Starttext;
    [SerializeField] private TextMeshProUGUI Gamesettext;
    [SerializeField] private TextMeshProUGUI Timer;

    private void Start()
    {
        
        stocksImages[0].SetActive(true);
        stocksImages[1].SetActive(true);
        stocksImages[2].SetActive(true);
        stocksImages[3].SetActive(true);
        Starttext.gameObject.SetActive(false) ;
        Gamesettext.gameObject.SetActive(false);
        StartCoroutine(Stating());

    }

    private void FixedUpdate()
    {
        
        if (isGame)
        {
            BattleSec += Time.deltaTime;
            if(BattleSec > 60)
            {
                BattleMin++;
                BattleSec = 0;
            }

            Timer.text = BattleMin.ToString("00") + ":" + BattleSec.ToString("00");
            if (PlayerStocks[0] == 0)
            {
                stocksImages[0].SetActive(false);
            }
            else if (PlayerStocks[0] == -1)
            {
                stocksImages[1].SetActive(false);
                StartCoroutine(Finish());
            }
            if (PlayerStocks[1] == 0)
            {
                stocksImages[2].SetActive(false);
            }
            else if (PlayerStocks[1] == -1)
            {
                stocksImages[3].SetActive(false);
                StartCoroutine(Finish());
            }
        }
    }

    public void DamaageTo(int attacker, int enemy, float damage)
    {
        PlayerDamage[attacker - 1] += damage;
    }
    public void ReduceStock(int num)
    {
        PlayerStocks[num]--;
    }

    IEnumerator Stating()
    {
        
        PlayerMovement _p1 = GameObject.Find("P1").GetComponent<PlayerMovement>();
        PlayerMovement _p2 = GameObject.Find("P2").GetComponent<PlayerMovement>();
        _p1.MoveManager(false);
        _p2.MoveManager(false);

        yield return new WaitForSeconds(1f);
        Debug.Log("3");
        Starttext.gameObject.SetActive(true);
        Starttext.text = "3";
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        Starttext.text = "2";
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        Starttext.text = "1";
        yield return new WaitForSeconds(1f);
        Debug.Log("Go");
        Starttext.text = "Go!!";
        isGame = true;
        _p1.MoveManager(true);
        _p2.MoveManager(true);
        yield return new WaitForSeconds(1f);
        Starttext.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator Finish()
    {
        isGame = false;
        PlayerMovement _p1 = GameObject.Find("P1").GetComponent<PlayerMovement>();
        PlayerMovement _p2 = GameObject.Find("P2").GetComponent<PlayerMovement>();
        _p1.MoveManager(false);
        _p2.MoveManager(false);

        Gamesettext.gameObject.SetActive(true);
        Gamesettext.text = "GameSet!!";
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");


    }

}
