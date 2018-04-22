using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour {

    public HumanWatchTower tower;

    public void gameOverButtonOnClick()
    {
        tower = GameObject.FindWithTag("tower").GetComponent<HumanWatchTower>();
        tower.currentHealth = tower.maximumHealth;
        SceneManager.LoadScene("Wizard's Lab");
    }

}
