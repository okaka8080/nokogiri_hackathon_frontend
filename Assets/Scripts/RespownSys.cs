using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class RespownSys : MonoBehaviour
{
    [SerializeField] private Transform _resetPoint;
    GameManager _gameManager;
    Rigidbody _rb;
    MultipleTargetCamera camerascript;

    private void Start()
    {
        camerascript = GameObject.Find("Main Camera").GetComponent<MultipleTargetCamera>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeadArea")
        {
            
            _rb.velocity = Vector3.zero;
            if (gameObject.tag == "P1")
            {
                _gameManager.PlayerDamage[0] = 0;
                _gameManager.ReduceStock(0);
                if (_gameManager.PlayerStocks[0] < 0)
                {
                    camerascript.enabled= false;
                    transform.position = new Vector3(0, -20, -20);

                } else
                {
                    transform.position = _resetPoint.position;
                }
                
            }
            else
            {
                _gameManager.PlayerDamage[1] = 0;
                _gameManager.ReduceStock(1);
                if (_gameManager.PlayerStocks[1] < 0)
                {
                    camerascript.enabled = false;
                    transform.position = new Vector3(0, -20, -20);
                }
                else
                {
                    transform.position = _resetPoint.position;
                }
                
            }
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "DeadArea")
        {
            transform.position = _resetPoint.position;
            _rb.velocity = Vector3.zero;
            if (gameObject.tag == "P1")
            {
                _gameManager.PlayerDamage[0] = 0;
                _gameManager.ReduceStock(0);
                if (_gameManager.PlayerStocks[0] < 0)
                {
                    //Destroy(gameObject);
                    camerascript.enabled = false;
                    transform.position = new Vector3(0, -20, -20);
                }
                else
                {
                    transform.position = _resetPoint.position;
                }
            }
            else
            {
                _gameManager.PlayerDamage[1] = 0;
                _gameManager.ReduceStock(1);
                if (_gameManager.PlayerStocks[1] < 0)
                {
                    //Destroy(gameObject);
                    camerascript.enabled = false;
                    transform.position = new Vector3(0, -20, -20);
                }
                else
                {
                    transform.position = _resetPoint.position;
                }
            }

        }
    }
}
