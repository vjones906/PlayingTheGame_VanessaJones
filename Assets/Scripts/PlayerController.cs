using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float forSpeed;
    private GameObject focalPoint;
    private Renderer playerRend;
    public bool hasPowerUp = false;
    private float powerUpKick = 10.0f;
    private Animator anim;
    public GameObject powerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRend = GetComponent<Renderer>();
        focalPoint = GameObject.Find("Focal Point");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * verticalInput * forSpeed);

        if (verticalInput > 0)
        {
            playerRend.material.color = new Color(1 - verticalInput, 1, 1 - verticalInput);
        }
        else
        {
            playerRend.material.color = new Color(1 + verticalInput, 1, 1 + verticalInput);
        }
        powerUpIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            anim.SetBool("Has Power Up", true);
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown());
        }
    }

    private IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("Has Power Up", false);
        powerUpIndicator.SetActive(false);
        hasPowerUp = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with " + collision.gameObject + " with powerUp set to " + hasPowerUp);
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(dir * powerUpKick, ForceMode.Impulse);
        }
    }
}
