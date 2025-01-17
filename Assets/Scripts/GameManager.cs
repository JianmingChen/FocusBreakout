using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _keyCollected;
    private GameObject _keyObject;
    private GameObject _keyUI;

    void Start() {
        _keyCollected = false;
        _keyObject = GameObject.Find("Glitch(Key)");
        _keyUI = GameObject.Find("UIKey");
    }

    void Update() {
        if (!_keyObject.activeSelf) {
            _keyCollected = true;
        }

        if (_keyCollected) {
            _keyUI.GetComponent<Image>().enabled = true;
        }
    }
}
