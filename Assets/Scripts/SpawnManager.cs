using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject level1;
    Vector3 nextSpawnPoint;


    public void spawnLevel()
    {
      GameObject temp = Instantiate(level1,nextSpawnPoint,Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(2).transform.position;
        //Debug.Log(temp.transform.GetChild(2).name);
    }

    private void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            spawnLevel();
        }
    }

}
