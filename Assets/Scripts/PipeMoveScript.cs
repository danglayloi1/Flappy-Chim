using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    public LogicScript logic;
    public float baseSpeed;
    public float deadZone = -45;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.gameIsPlaying == false) return;
        int playerScore = logic.playerScore;
        float moveSpeed = baseSpeed + (playerScore * 0.3f);
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}
