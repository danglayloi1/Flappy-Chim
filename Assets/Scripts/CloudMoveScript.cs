using UnityEngine;

public class CloudMoveScript : MonoBehaviour
{
    public LogicScript logic;
    public float moveSpeed;
    public float deadZone = -45;

    public GameObject menuPanel;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        menuPanel = GameObject.FindGameObjectWithTag("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < deadZone)
        {
            Debug.Log("Cloud Deleted");
            Destroy(gameObject);
        }

        if (logic.gameIsPlaying == false && menuPanel == null) return;

        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
    }
}
