using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _ccontroller;
    [SerializeField] private Transform _start;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Volume _volume;
    private Vignette _vignette;
    private Coroutine _vignetteFlash;
    private float _vignetteFlashDuration = 0.5f;

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

    private void Start()
    {
        _volume.profile.TryGet(out _vignette);
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
            Die();
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

    public void Die()
    {
        _ccontroller.enabled = false;
        _waitingToRespawn = true;
        _points = 0;

        foreach (GameObject obj in _earnedCoins)
        {
            obj.SetActive(true);
        }
        _earnedCoins.Clear();

        if (_vignetteFlash != null)
        {
            StopCoroutine(_vignetteFlash);
        }

        _vignetteFlash = StartCoroutine(DamageVignette());
    }

    private IEnumerator DamageVignette()
    {
        float elapsedTime = 0;
        _vignette.color.value = Color.black;

        while (elapsedTime < (_vignetteFlashDuration / 2))
        {
            _vignette.color.value = Color.Lerp(Color.black, Color.red, elapsedTime / (_vignetteFlashDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        elapsedTime = 0;

        while (elapsedTime < (_vignetteFlashDuration / 2))
        {
            _vignette.color.value = Color.Lerp(Color.red, Color.black, elapsedTime / (_vignetteFlashDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        _vignette.color.value = Color.black;
    }
}
