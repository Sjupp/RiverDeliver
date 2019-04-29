using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public Player playerRef;
    public float movementSpeed = 1.0f;
    public float lerpStrength = 0.3f;
    private Vector3 playerVector;
    private Vector3 inputVector;
    public float viewAngle;
    public float inputAngle;

    //fulkod nedanför (var disclaimern slutar är subjektivt)

    Rigidbody rb;

    public void Start()
    {
        rb = playerRef.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        playerVector = inputVector.normalized * movementSpeed;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            playerRef.Interact();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            playerRef.Broom();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, playerVector, lerpStrength);

        if (playerVector.sqrMagnitude > 0.1f)
        {
            inputAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
            viewAngle = Mathf.LerpAngle(viewAngle, inputAngle, lerpStrength);
            playerRef.transform.rotation = Quaternion.AngleAxis(viewAngle, Vector3.up);
        }

    }
}
