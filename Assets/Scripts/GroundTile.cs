using UnityEngine;



public class GroundTile : MonoBehaviour
{
    SpawnManager spawnManager;


    private void Start()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>(); 
    }

    private void OnTriggerExit(Collider other)
    {
        spawnManager.spawnLevel();
        Destroy(gameObject,1);
    }
}
