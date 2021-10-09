using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    [SerializeField] private Blob _basicBlob;
    [SerializeField] private float _minSpawnRate;
    [SerializeField] private float _maxSpawnRate;
    [SerializeField] private int _maxBlobCount; // Includes player blob
    private List<Blob> _blobList;
    private Blob _player;

    void Start()
    {
        _blobList = new List<Blob>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _blobList.Add(_player);
        StartCoroutine(SpawnCoroutine());
    }

    void Update() {}

    private IEnumerator SpawnCoroutine() {
        while (_blobList.Count < _maxBlobCount) {
            float time = Random.Range(_minSpawnRate, _maxSpawnRate);
            float y = Random.Range(0f, 10f);
            float x = Random.Range(-22f, 22f);
            float z = Random.Range(-15f, -5f);
            _basicBlob.transform.position = new Vector3(x, y, z);

            yield return new WaitForSeconds(time);
            Blob b = Instantiate(_basicBlob, new Vector3(x, y, z), Quaternion.Euler(0, 180, 0));
            _blobList.Add(b);
        }
    }

    public void AddBlob(Blob b) {
        _blobList.Add(b);
    }

    public void RemoveBlob(Blob b) {
        _blobList.Remove(b);
    }

    public Vector3 GetRandomBlobLocation() {
        int index = Random.Range(0, _blobList.Count);
        return _blobList[index].transform.position;
    }

    public Vector3 GetRandomBlobLocation(float playerFocus) {
        int index = Random.Range(0, _blobList.Count);
        return _blobList[index].transform.position;
    }

    private void OnValidate() {
        if (_minSpawnRate < 0) {
            _minSpawnRate = 0;
        }

        if (_maxSpawnRate < 0) {
            _maxSpawnRate = 0;
        }
        
        if (_maxBlobCount < 1) {
            _maxBlobCount = 1;
        }
    }
}
