using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cradle : MonoBehaviour
{

    public ElementData element;
    public float spawnTime;
    public Transform elementContainer;

    ElementInstance _lastSpawnedInstance;
    private float _currentSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnElement();
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastSpawnedInstance == null)
        {
            _currentSpawnTime += Time.deltaTime;
            if (_currentSpawnTime > spawnTime)
            {
                SpawnElement();
            }
        }
    }

    void SpawnElement()
    {
        if (element != null)
        {
            _lastSpawnedInstance = Instantiate(Director.GetManager<CombinationManager>().emptyElementPrefab, transform.position, Quaternion.identity, elementContainer);
            _lastSpawnedInstance.data = element;
        }
        _currentSpawnTime = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_lastSpawnedInstance != null && collision.GetComponent<ElementInstance>() == _lastSpawnedInstance)
        {
            _lastSpawnedInstance = null;
        }
    }
}
