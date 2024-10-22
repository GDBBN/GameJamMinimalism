using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private UIManager uiManager;
    private CoinManager coinManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        uiManager = FindObjectOfType<UIManager>();
        coinManager = FindObjectOfType<CoinManager>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level_Menu");
        }
    }

    public void AddCoin()
    {
        coinManager.AddCoin();
        uiManager.UpdateCoinUI();
    }
}
