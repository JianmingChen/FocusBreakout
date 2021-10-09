using System.Collections;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _minFireRate;
    [SerializeField] private float _maxFireRate;
    private BlobManager _blobManager;
    private Vector3 _startPos;
    private RaycastHit hitData;

    void Start() {
        // Blob prefab won't let me drag BlobManager into its SerializeField
        // so to be consistent I used Find() here aswell.
        _blobManager = GameObject.Find("BlobManager").GetComponent<BlobManager>();
        _startPos = new Vector3(1, 0, -20);
        StartCoroutine(BulletCoroutine(_startPos));
    }

    private void Update() {
        Debug.DrawRay(_startPos, _bullet.Direction);
    }

    private IEnumerator BulletCoroutine(Vector3 startPos) {
        while (true) {
            float time = Random.Range(_minFireRate, _maxFireRate);

            yield return new WaitForSeconds(time);
            _bullet.Direction = _blobManager.GetRandomBlobLocation() - startPos;
            // Only shoot if Raycast hits a blob
            if (Physics.Raycast(startPos, _bullet.Direction, out hitData, Constants.HitRange)
                && hitData.collider.tag == "Blob") {
                Instantiate(_bullet, startPos, Quaternion.identity);
            }
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
