using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class MasterGameManager : MonoBehaviour {

	[HideInInspector]
	public GestureGameManager gestureGameManager;

	[HideInInspector]
	public NVRPlayer nvrPlayer;


    public HumanWatchTower tower;

    public PortalSpawning portalSpawning;

  [HideInInspector]
	public SpellEngine spellEngine;

	public Transform rightHandTransform;

	[HideInInspector]
	public Transform LeftHandTransform;

	[HideInInspector]
	public AssembledSpellQuickBar assembledSpellQuickBar;

	public bool useQuickBarMechanics = false;

    public Text goldText;
    public int gold;

    public Text expText;
    public int exp;

    public int goldRequiredToUpgrade;
		public int towerMaximumHealth;

    public Dictionary<string, int> userSpellHistory;
    //Use this metric to decide what enemies to spawn next
    public string mostUsedSpellComponent = null;

    public int currentLevel;

    public bool debugger = false;

    public bool win; //Need a mechanism to see if all enemies have been defeated or not.

    public int[][] levelPopulation;

    public Canvas gameOverCanvas;
    public Button leaveWizardLabButton;
    public Canvas nextWaveCanvas;
    public Boolean nextWave;
    public Boolean expoMode;





    void Awake ()
	{
        int[] levelOne = new int[5] { 3, 0, 0, 0, 0 }; //small, medium, flyer, speed, large
        int[] levelTwo = new int[5] { 5, 2, 0, 0, 0 };
        int[] levelThree = new int[5] { 10, 5, 1, 0, 0 };
        int[] levelFour = new int[5] { 13, 10, 5, 3, 0 };
        int[] levelFive = new int[5] { 10, 13, 0, 7, 3 };

        levelPopulation = new int[][] { levelOne, levelTwo, levelThree, levelFour, levelFive };

        nvrPlayer =  GameObject.FindWithTag("Player").GetComponent<NVRPlayer>();
		gestureGameManager = GetComponent<GestureGameManager>();
		gestureGameManager.nvrPlayer = nvrPlayer;
		if ( GameObject.FindWithTag("tower") != null)
		{
			tower = GameObject.FindWithTag("tower").GetComponent<HumanWatchTower>();
		}

        if (GameObject.FindWithTag("GameController").GetComponentInChildren<PortalSpawning>() != null)
        {
            portalSpawning = GameObject.FindWithTag("GameController").GetComponentInChildren<PortalSpawning>();
            Debug.Log("Portal Spawning object has been found");
        } else
        {
            Debug.Log("Portal Spawning object has not been found.");
        }

        gestureGameManager.masterGameManager = this;
		assembledSpellQuickBar = GetComponent<AssembledSpellQuickBar>();

		spellEngine = GetComponent<SpellEngine>();
		spellEngine.masterGameManager = this;
	}

	// Use this for initialization
	void Start () {
        expoMode = true;
        nextWave = true;
	    if (SceneManager.GetActiveScene().name == "MainScene") {
        	gameOverCanvas = GameObject.FindWithTag("gameOverCanvas").GetComponent<Canvas>();
					gameOverCanvas.enabled = false;

            leaveWizardLabButton = GameObject.FindWithTag("leaveTowerButton").GetComponent<Button>();
            leaveWizardLabButton.interactable = false;

            nextWaveCanvas = GameObject.FindWithTag("nextWaveCanvas").GetComponent<Canvas>();
            nextWaveCanvas.enabled = false;

						nextWaveCanvas.gameObject.transform.GetChild(1).GetComponent<Text>().text = "You have cleared this wave of enemies. Click below to face the next wave or look behind and run away like a coward!";
						nextWaveCanvas.GetComponentInChildren<Button>().gameObject.SetActive(true);
		}

        if (gameOverCanvas == null)
        {
            Debug.Log("Canvas object could not be found.");
        }
        win = false;
	    if (SceneManager.GetActiveScene().name == "MainScene") {
            if (expoMode)
            {
                currentLevel = 0;
                instantiateLevel(currentLevel);
            } else
            {
                instantiateLevel(currentLevel);
            }
        }

        //gold = 100;
        goldText = GameObject.FindWithTag("canv").transform.GetChild(0).GetComponent<Text>();
        expText = GameObject.FindWithTag("canv").transform.GetChild(1).GetComponent<Text>();
        //exp = 0;
        //goldRequiredToUpgrade = 200;
				if (SceneManager.GetActiveScene().name == "Wizard's Lab")
				{
					GameObject.FindWithTag("GoldShop").GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = "Upgrade Tower : " + goldRequiredToUpgrade;
				}
        userSpellHistory = new Dictionary<string, int>();
        userSpellHistory.Add("FireV1", 0);
        userSpellHistory.Add("WaterV1", 0);
        userSpellHistory.Add("NatureV1", 0);
        userSpellHistory.Add("DamageV1", 0);
        userSpellHistory.Add("SizeV1", 0);
        userSpellHistory.Add("SpeedV1", 0);
        userSpellHistory.Add("GravityV1", 0);
        userSpellHistory.Add("MultiplierV1", 0);
        userSpellHistory.Add("PoisonV1", 0);
        userSpellHistory.Add("ExplosionV1", 0);
        userSpellHistory.Add("ParalyzeV1", 0);
        userSpellHistory.Add("ArrowV1", 0);
        userSpellHistory.Add("ShieldV1", 0);
        userSpellHistory.Add("BeamV1", 0);
        userSpellHistory.Add("LightningV1", 0);
        userSpellHistory.Add("WallV1", 0);
        userSpellHistory.Add("MeteorShowerV1", 0);
        userSpellHistory.Add("MagicBallV1", 0);
        userSpellHistory.Add("ShockWaveV1", 0);
        userSpellHistory.Add("SummonGolemsV1", 0);
		userSpellHistory.Add("SlowV1", 0);
    }

    public void instantiateLevel(int currentLevel)
    {
        win = false;
        Debug.Log("Instantiating level: " + currentLevel);
        // Change difficulty
        // Change number of spawn points in the first place (Here)
        // Change number of enemies spawning from each spawn point (Done in EnemySpawning class)
        // Change elements of enemies probabilistically depending on user history (Done in PortalSize class)
        portalSpawning.limit = levelPopulation[currentLevel].Sum();
        Debug.Log("Current Level enemy List");
        Debug.Log(portalSpawning.limit);

    }

    private void levelOver()
    {
        if (tower.currentHealth <= 0)
        {

            //instantiateLevel(currentLevel);
            Debug.Log("Game over. You could not go to the next level");
            List<GameObject> enemies = EnemyManager.getEnemies();
            win = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject enemy = enemies[i];
                Destroy(enemy);

            }
            gameOverCanvas.enabled = true; //Should bring out the game over canvas.
            portalSpawning.count = 0;

        } else
        {
            if (win)
            {
                leaveWizardLabButton.interactable = true;
                nextWaveCanvas.enabled = true;
                nextWave = false;
                currentLevel += 1;
                if (currentLevel == 5)

                {
                    nextWaveCanvas.gameObject.transform.GetChild(1).GetComponent<Text>().text = "You have mastered this game!! You can turn and go back to the wizrd lab.";
                    //nextWaveCanvas.GetComponentInChildren<Button>().enabled = false;
										nextWaveCanvas.GetComponentInChildren<Button>().gameObject.SetActive(false);
                }
                nextWaveCanvas.enabled = true;
                win = false;
                portalSpawning.count = 0;
            }
        }
    }

    // Update is called once per frame
    void Update () {


        //Checks if all enemies are dead and if all portals are spawned

        if (SceneManager.GetActiveScene().name == "MainScene" && EnemyManager.getEnemies().Count == 0 && portalSpawning.count >= portalSpawning.limit && portalSpawning.limit > 0)
        {
            win = true;
            print("you win");
        }

      //  Debug.Log(mostUsedSpellComponent);
        //Checks if the level is over or not. Fixes settings for the next level.
				if (SceneManager.GetActiveScene().name == "MainScene")
				{
					levelOver();
				}


		//since the controllers could be turned off and on, make sure we still got hands
		if (rightHandTransform == null)
		{
			rightHandTransform = nvrPlayer.transform.Find("RightHand");
		}

		if (LeftHandTransform == null)
		{
			LeftHandTransform = nvrPlayer.transform.Find("LeftHand");
		}
        goldText.text = "Gold: " + gold;
        expText.text = "Exp: " + exp;


    }

    public void addGold(int newGold)
    {
        gold += newGold;
    }


    public void addExp(int newExp)
    {
        exp += newExp;
    }

	public void leaveLab()
	{
		Debug.Log ("Transportation spell to Top of Tower");
		SceneManager.LoadScene("MainScene");
	}

	public void leaveTower()
	{
		Debug.Log ("Transportation spell to the Lab");
		SceneManager.LoadScene("Wizard's Lab");
	}
	public void loadTowerScene()
	{
		Debug.Log ("Transportation spell to Top of Tower");
		SceneManager.LoadScene("MainScene");
	}

	public void loadLabScene()
	{
		Debug.Log ("Transportation spell to the Lab");
		SceneManager.LoadScene("Wizard's Lab");
	}

	public void loadPracticeScene()
	{
		Debug.Log ("Transportation spell to the Practice zone");
		SceneManager.LoadScene("Practice Arena");
	}

    public void upgradeTower()
    {
        if (gold < goldRequiredToUpgrade)
        {
            print("You do not have enough gold.");
            return;
        }
        int newHealth = (int)(towerMaximumHealth * 1.3);
				towerMaximumHealth = newHealth;
        //tower.maximumHealth = newHealth;
        //tower.currentHealth = tower.maximumHealth;
        gold = gold - goldRequiredToUpgrade;
        goldRequiredToUpgrade = goldRequiredToUpgrade * 2;
				GameObject.FindWithTag("GoldShop").GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = "Upgrade Tower : " + goldRequiredToUpgrade;
    }

    public void repairTower()
    {
        int goldNeeded = 0;
        while ((goldNeeded <= gold) && (tower.currentHealth <= tower.maximumHealth))
        {
            tower.currentHealth += 50;
            goldNeeded += 1;
            if (tower.currentHealth > tower.maximumHealth)
            {
                tower.currentHealth = tower.maximumHealth;
                break;
            }
        }
        gold = gold - goldNeeded;
    }
}
