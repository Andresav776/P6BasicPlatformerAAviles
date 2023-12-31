using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody playerRb;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public float horizontalInput;
    public float verticalInput;
    public float speed = 7;
    public TextMeshProUGUI youWonText;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(-4.5f, -3.5f, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            gameOver = true;
            youWonText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
