using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{

    public GameObject pipe;
    public float pipeDistance;
    public float heightOffset = 8;
    public LogicScript logic;

    private GameObject lastPipe;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        lastPipe = spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.gameIsPlaying == false) return;
        if (lastPipe == null || lastPipe.transform.position.x <= transform.position.x - pipeDistance)
        {
            lastPipe = spawnPipe();
        }
    }
    GameObject spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        return Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
