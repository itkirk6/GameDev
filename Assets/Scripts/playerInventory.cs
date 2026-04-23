using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    // public string currentEquippedItem = "None";
    // public string previouslyEquippedItem;
    public List<string> keys = new List<string>();
    public List<string> questItems = new List<string>();
    public int playerHealth = 100;
    public Image equippedItemUIBox;
    public GameObject currentEquippedItem;
    [SerializeField] private AudioClip pickupClip;
    public GameObject deathPanel;
    public GameObject gameOverPanel;

    private AudioSource audioSource;
    public Slider healthSlider;
    public int maxHealth = 100;
    private bool isDead = false;
    public GameObject pauseButton;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if(equippedItemUIBox != null)
            equippedItemUIBox.enabled = false;
        audioSource = GetComponent<AudioSource>();
        findHealthSlider();
        findEquippedItemUIBox();
        updateHealthSlider();
        findDeathPanel();
        findGameOverPanel();
    }


    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            isDead = true;
            handleDeath();
        }
        if(Input.GetMouseButtonDown(0) && currentEquippedItem != null)
        {
            IUsableItem usableItem = currentEquippedItem.GetComponent<IUsableItem>();
            if(usableItem != null)
            {
                usableItem.UseItem();
            }
        }
    }
    public void ProcessPickup( ItemPickup pickupInfo)
    {
        audioSource.PlayOneShot(pickupClip);
        switch (pickupInfo.itemType)
        {
            case ItemPickup.PickupType.Equippable:

                if(currentEquippedItem != null)
                {
                    currentEquippedItem.transform.SetParent(null);
                    currentEquippedItem.transform.localScale = currentEquippedItem.GetComponent<InteractableObject>().originalScale;
                    currentEquippedItem.transform.position = transform.position;
                    currentEquippedItem.GetComponent<SpriteRenderer>().enabled = true;
                    currentEquippedItem.GetComponent<Collider2D>().enabled = true;
                    currentEquippedItem.GetComponent<InteractableObject>().enabled = true;
                    currentEquippedItem.GetComponent<ItemPickup>().enabled = true;

                }

                currentEquippedItem = pickupInfo.gameObject;

                currentEquippedItem.transform.SetParent(transform);
                currentEquippedItem.transform.position = Vector3.zero;

                currentEquippedItem.GetComponent<SpriteRenderer>().enabled = false;
                currentEquippedItem.GetComponent<Collider2D>().enabled = false;

                currentEquippedItem.GetComponent<InteractableObject>().enabled = false;;
                currentEquippedItem.GetComponent<ItemPickup>().enabled = false;

                if(equippedItemUIBox != null)
                {
                    equippedItemUIBox.sprite = pickupInfo.GetComponent<SpriteRenderer>().sprite;
                    equippedItemUIBox.enabled = true;
                    equippedItemUIBox.color = Color.white;
                }

                Debug.Log($"Equipped: {currentEquippedItem}");
                break;
            
            case ItemPickup.PickupType.Consumable:
                playerHealth += pickupInfo.itemValue;
                playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
                updateHealthSlider();
                Debug.Log($"Restored {pickupInfo.itemValue} health.");
                Destroy(pickupInfo.gameObject);
                break;


            case ItemPickup.PickupType.Key:
                if(!keys.Contains(pickupInfo.itemName))
                {
                    keys.Add(pickupInfo.itemName);
                    Debug.Log($"Added {pickupInfo.itemName} key to inventory.");
                }
                Destroy(pickupInfo.gameObject);
                break;
            
            case ItemPickup.PickupType.QuestItem:
                //Debug.Log("Enterd here");
                if(!questItems.Contains(pickupInfo.itemName))
                {
                    questItems.Add(pickupInfo.itemName);
                    Debug.Log($"Added {pickupInfo.itemName} to inventory.");
                }
                Destroy(pickupInfo.gameObject);
                break;

        }
        if (pickupInfo.addToKeys && !keys.Contains(pickupInfo.itemName))
        {
            keys.Add(pickupInfo.itemName);
        }

        if (pickupInfo.addToQuestItems && !questItems.Contains(pickupInfo.itemName))
        {
            questItems.Add(pickupInfo.itemName);
        }

    }

    public bool HasKey(string requiredKey)
    {
        return keys.Contains(requiredKey);
    }
    private void updateHealthSlider()
    {
        if (healthSlider == null)
        {
            findHealthSlider();
        }

        if (healthSlider != null)
        {
            healthSlider.value = playerHealth;
        }
    }
    public void takeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
        updateHealthSlider();

        if (playerHealth <= 0)
        {
            handleDeath();
        }
    }
    private void findHealthSlider()
    {
        if (healthSlider != null)
            return;

        GameObject sliderObject = GameObject.FindGameObjectWithTag("HealthSlider");


        if (sliderObject != null)
        {
            healthSlider = sliderObject.GetComponent<Slider>();
        }

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        healthSlider = null;
        equippedItemUIBox = null;

        findHealthSlider();
        findEquippedItemUIBox();
        findDeathPanel();
        updateHealthSlider();
        updateEquippedItemUI();
    }

    private void findEquippedItemUIBox()
    {
        if (equippedItemUIBox != null)
            return;

        GameObject equippedItemObject = GameObject.FindGameObjectWithTag("EquippedItemBox");

        if (equippedItemObject != null)
        {
            equippedItemUIBox = equippedItemObject.GetComponent<Image>();
        }
    }
    private void updateEquippedItemUI()
    {
        if (equippedItemUIBox == null)
        {
            findEquippedItemUIBox();
        }

        if (equippedItemUIBox == null)
            return;

        if (currentEquippedItem == null)
        {
            equippedItemUIBox.enabled = false;
            return;
        }

        SpriteRenderer itemSpriteRenderer = currentEquippedItem.GetComponent<SpriteRenderer>();

        if (itemSpriteRenderer != null)
        {
            equippedItemUIBox.sprite = itemSpriteRenderer.sprite;
            equippedItemUIBox.enabled = true;
            equippedItemUIBox.color = Color.white;
        }
    }

    private void findDeathPanel()
    {
        if (deathPanel != null)
            return;

        GameObject panelObject = GameObject.FindGameObjectWithTag("DeathPanel");

        if (panelObject != null)
        {
            deathPanel = panelObject;
            deathPanel.SetActive(false);
        }
    }

    private void handleDeath()
    {
        findDeathPanel();
        findPauseButton();

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            deathPanel.transform.SetAsLastSibling();
        }

        Time.timeScale = 0f;
    }

    private void findGameOverPanel()
    {
        if (gameOverPanel != null)
            return;

        GameObject panelObject = GameObject.FindGameObjectWithTag("WinPanel");

        if (panelObject != null)
        {
            gameOverPanel = panelObject;
            gameOverPanel.SetActive(false);
        }
    }

    public void handleGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void findPauseButton()
    {
        if (pauseButton != null)
            return;

        GameObject pauseButtonObject = GameObject.Find("pauseButton");

        if (pauseButtonObject != null)
        {
            pauseButton = pauseButtonObject;
        }
    }



}