using UnityEngine;
using static UnityEngine.Audio.ProcessorInstance;

public class CloudSpawnScript : MonoBehaviour
{

    public GameObject[] clouds;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 8;
    public LogicScript logic;

    public GameObject menuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.gameIsPlaying == false && menuPanel.activeSelf == false) return;
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnCloud();
            timer = 0;
        }
    }

    void spawnCloud()
    {
        int randomIndex = Random.Range(0, clouds.Length);
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(clouds[randomIndex], new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
