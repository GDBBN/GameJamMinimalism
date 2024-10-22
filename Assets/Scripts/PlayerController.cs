using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody _rb;
    private UIManager _uiManager;
    private CoinManager _coinManager;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _uiManager = FindFirstObjectByType<UIManager>();
        _coinManager = FindFirstObjectByType<CoinManager>();
    }

    private void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");

        var movement = new Vector3(moveX, 0, moveZ) * moveSpeed;

        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);

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
        _coinManager.AddCoin();
        _uiManager.UpdateCoinUI();
    }
}
