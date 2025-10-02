using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _ccontroller;
    [SerializeField] private Transform _start;
    [SerializeField] private TMP_Text _text;

    private int _points = 0;
    private List<GameObject> _earnedCoins = new List<GameObject>();
    private bool _waitingToRespawn;

    public static Player instance;

    private void Awake()
    {
        if (Player.instance == null)
        {
            Player.instance = this;
        }
        else
        {
            Debug.Log("More than one player is in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Points"))
        {
            _points += 1;
            _earnedCoins.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag.Equals("Hazard"))
        {
            _ccontroller.enabled = false;
            _waitingToRespawn = true;
            _points = 0;

            foreach (GameObject obj in _earnedCoins)
            {
                obj.SetActive(true);
            }
            _earnedCoins.Clear();
        }

        _text.text = "Points: " + _points;
    }

    private void LateUpdate()
    {
        if (_waitingToRespawn)
        {
            transform.position = _start.position;
            _waitingToRespawn = false;  
            _ccontroller.enabled = true; 
        }
    }
}
