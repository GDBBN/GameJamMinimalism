using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    public void AddCoin()
    {
        CoinManager.Instance.AddCoin();
        uiManager.UpdateCoinUI();
    }
}
