using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public GameObject healthIncreaseParticlePrefab;
    public GameObject healthDecreaseParticlePrefab;

    bool gameOver = false;

    public int fixedRobots = 0;
    public Text scoreText; // Declare scoreText variable

    public GameObject gameOverText;

    public AudioClip throwSound;
    public AudioClip hitSound;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found!");
        }
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (currentHealth == 0)
        {
            speed = 0;
            gameOverText.GetComponent<Text>().text = ("You Fainted! Press R to Restart!");
            gameOver = true;

        }

        if (fixedRobots == 4)
        {
            speed = 0;
            gameOverText.GetComponent<Text>().text = ("You Win! Game Created by: Group 24. Press R to Restart!");
            gameOver = true;
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            Instantiate(healthDecreaseParticlePrefab, transform.position, Quaternion.identity);

            PlaySound(hitSound);
        }

        if (amount > 0)
        {
            Instantiate(healthIncreaseParticlePrefab, transform.position, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    //Function created by Francisco
    public void ChangeSpeed(float amount)
    {
        speed = speed + amount;
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
             Debug.LogWarning("Attempting to play a null audio clip.");
            return;
        }

        audioSource.PlayOneShot(clip);
    }

    public void ChangeScore(int amount)
    {
        fixedRobots += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Fixed Robots: " + fixedRobots.ToString();
    }

    //This function was created by Kole
    public void GameOver()
    {
        speed = 0;
        gameOverText.GetComponent<Text>().text = "Game Over! Cat too sad! Press R to Restart!";
        gameOver = true;
    }
}