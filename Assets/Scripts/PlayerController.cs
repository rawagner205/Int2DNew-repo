using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Controls forward motion speed
    [SerializeField] float forwardSpeed = 16f;
    //Controls side-to-side steering speed
    [SerializeField] float originalLateralSpeed, lateralSpeed = 10f;

    //Variables for restart button
    public UIDocument uiDocument;
    private Button restartButton;

    //Variables for boost 
    private Label boostLabel;
    private float elapsedTime = 0f;
    private bool boostOn = false;
    [SerializeField] float boostTime = 5f;
    [SerializeField] float boostSpeed = 20f;

    public GameObject explosionEffect;

    //Text for win/lose game over conditions
    private Label winLabel;
    private Label loseLabel;

    void Start()
    {
        //Set up restart button
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;

        //Set up boost tracker
        boostLabel = uiDocument.rootVisualElement.Q<Label>("BoostLabel");
        boostLabel.style.display = DisplayStyle.None;

        //Set up win/lose messages
        winLabel = uiDocument.rootVisualElement.Q<Label>("WinLabel");
        winLabel.style.display = DisplayStyle.None;

        loseLabel = uiDocument.rootVisualElement.Q<Label>("LoseLabel");
        loseLabel.style.display = DisplayStyle.None;
    }

    void Update()
    {
        float steer = 0f;

        // Left and right steering
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            steer = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            steer = 1f;
        }

        //Apply movement and rotation 
        float steerAmount = steer * lateralSpeed * Time.deltaTime;
        float moveAmount = forwardSpeed * Time.deltaTime;
        transform.Translate(0, moveAmount, 0);
        transform.Translate(steerAmount, 0, 0);

        if (boostOn == true)
        {
            //Calculate and display time left for boost
            elapsedTime += Time.deltaTime;
            float timeLeft = Mathf.Round(boostTime - elapsedTime);
            boostLabel.text = "Steering Boost - " + timeLeft + "s";

            //When timer runs out, reset back to normal
            if (elapsedTime >= 5.0)
            {
                boostLabel.style.display = DisplayStyle.None;
                lateralSpeed = originalLateralSpeed;
                elapsedTime = 0;
                boostOn = false;
            } 
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If collided with Boost object, turn on boost function
        if (collision.CompareTag("Boost"))
        {
            lateralSpeed = boostSpeed;
            Destroy(collision.gameObject);

            //Display  boost timer
            boostLabel.style.display = DisplayStyle.Flex;

            //Turn on boost function
            boostOn = true;
        }

        //When finish line is crossed, stop all sprite movement and display Restart button
        if (collision.CompareTag("Finish"))
        {
            //Cut off player movement
            lateralSpeed = 0f;
            forwardSpeed = 0f;

            //Display end text and restart button
            winLabel.style.display = DisplayStyle.Flex;
            restartButton.style.display = DisplayStyle.Flex;

            //Turn off boost if boost is on
            if (boostOn == true)
            {
                boostOn = false;
                boostLabel.style.display = DisplayStyle.None;
            }
        }
    }

    //Destroy player sprite with explosion on collision
    void OnCollisionEnter2D(Collision2D collision)
    {  
        //Destroy sprite and create explosion
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);

        //Turn off boost text, and display end text and start button
        boostLabel.style.display = DisplayStyle.None;
        loseLabel.style.display = DisplayStyle.Flex;
        restartButton.style.display = DisplayStyle.Flex;
    }
}
