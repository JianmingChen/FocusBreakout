using System.Collections;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _minFireRate;
    [SerializeField] private float _maxFireRate;
    private BlobManager _blobManager;

    void Start() {
        // Blob prefab won't let me drag BlobManager into SerializeField so to be consistent 
        // I used Find() here aswell.
        _blobManager = GameObject.Find("BlobManager").GetComponent<BlobManager>(); ;
        StartCoroutine(BulletCoroutine(new Vector3(1, 0, -20)));
    }

    void Update() {}

    private void UpdateBlobList() {}

    // Uses Unity's Random.Range(), values are swapped if _maxFireRate is less than _minFireRate
    private IEnumerator BulletCoroutine(Vector3 startPos) {
        while (true) {
            float time = Random.Range(_minFireRate, _maxFireRate);

            yield return new WaitForSeconds(time);
            _bullet.Direction = _blobManager.GetRandomBlobLocation() - startPos;
            Instantiate(_bullet, startPos, Quaternion.identity);
        }
    }

    private void OnValidate() {
        if (_minFireRate < 0) {
            _minFireRate = 0;
        }

        if (_maxFireRate < 0) {
            _maxFireRate = 0;
        }
    }
}
