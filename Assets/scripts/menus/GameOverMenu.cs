using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    static Text scoreText;

    [SerializeField]
    GameObject hud;

    HUD hudscript;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
        AudioManager.Play(AudioClipName.GameOverAudio);

        hud = GameObject.FindGameObjectWithTag("HUD");
        hudscript = hud.GetComponent<HUD>();
        score = hudscript.Score;
        scoreText = GameObject.FindGameObjectWithTag("GameOverScoreText").GetComponent<Text>();
        scoreText.text = "Score:" + score;
    }

    /// <summary>
    /// Handles the on click event from the Quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.MenuClick);
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
