using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSetup : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject collectiblePrefab;
    public int numberOfCollectibles = 5;
    public TMP_Text timerText;
    public TMP_Text collectedText;

    private GameObject player;

    void Start()
    {
        CreateGround();
        CreateWalls();
        SpawnPlayer();
        SpawnCollectibles();
    }

    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Circuit";
        ground.transform.localScale = new Vector3(5, 1, 5);
        ground.GetComponent<Renderer>().material.color = Color.gray;
    }

    void CreateWalls()
    {
        float size = 50f;
        float height = 3f;

        Vector3[] positions = {
            new Vector3(0, height / 2, -size / 2), 
            new Vector3(0, height / 2, size / 2),  
            new Vector3(-size / 2, height / 2, 0), 
            new Vector3(size / 2, height / 2, 0),  
        };

        foreach (Vector3 pos in positions)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = pos;
            wall.transform.localScale = new Vector3(
                pos.x == 0 ? size : 1,
                height,
                pos.z == 0 ? size : 1
            );
            wall.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }

    void SpawnCollectibles()
    {
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), 0.5f, Random.Range(-20, 20));
            GameObject item = Instantiate(collectiblePrefab, pos, Quaternion.identity);
            item.tag = "Collectible";
        }

        var collector = player.GetComponent<PlayerCollector>();
        collector.totalItems = numberOfCollectibles;
        collector.timerText = timerText;
        collector.collectedText = collectedText;
    }
}
