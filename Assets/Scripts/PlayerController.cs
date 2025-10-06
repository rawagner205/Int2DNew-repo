using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Xml.Linq;

public class PlayerController : MonoBehaviour
{
    // controls forward motion speed
    [SerializeField] float forwardSpeed = 8f;
    //controls side-to-side steering speed
    [SerializeField] float lateralSpeed = 10f;

    Rigidbody2D rb;

    // variables for restart button
    public UIDocument uiDocument;
    private Button restartButton;

    //variables for boost 
    private Label boostLabel;
    private float elapsedTime = 0f;
    private bool boostOn = false;

    public GameObject explosionEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //setup restart button
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;

        //setup boost tracker
        boostLabel = uiDocument.rootVisualElement.Q<Label>("BoostLabel");
        boostLabel.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {

        float steer = 0f;

        // Left and Right steering
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            steer = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            steer = 1f;
        }

        // Apply movement and rotation 

        float steerAmount = steer * lateralSpeed * Time.deltaTime;
        float moveAmount = forwardSpeed * Time.deltaTime;
        transform.Translate(0, moveAmount, 0);
        transform.Translate(steerAmount, 0, 0);
        

        //When boost is activated, increase steering speed for 5 seconds
        //Also updates boost countdown timer text
        if (boostOn == true)
        {
            elapsedTime += Time.deltaTime;
            float timeLeft = Mathf.Round(5 - elapsedTime);
            boostLabel.text = "Steering Boost - " + timeLeft + " seconds";
            if (elapsedTime >= 5.0)
            {
                boostOn = false;
                boostLabel.style.display = DisplayStyle.None;
                lateralSpeed = 10f;
            }
        }


    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If collided with Boost, increase maneuvering speed
        if (collision.CompareTag("Boost"))
        {
            lateralSpeed = 20f;
            Destroy(collision.gameObject);

            //update counter for boost timer
            boostLabel.style.display = DisplayStyle.Flex;
            boostOn = true;
        }

        //When finish line is crossed, stop all sprite movement and display Restart button
        if (collision.CompareTag("Finish"))
        {
            lateralSpeed = 0f;
            forwardSpeed = 0f;
            restartButton.style.display = DisplayStyle.Flex;

            //Turn off boost if boost is on
            if (boostOn == true)
            {
                boostOn = false;
                boostLabel.style.display = DisplayStyle.None;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {  
        //Destroy player sprite with explosion on collision
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);
        boostLabel.style.display = DisplayStyle.None;
        restartButton.style.display = DisplayStyle.Flex;
    }
}
