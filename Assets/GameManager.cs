using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    enum GameState {
        Starting = 0,
        Playing = 1,
        Win = 2,
        Lose = 3
    }

    [SerializeField]
    private int bonusCash = 0;

    [SerializeField]
    private float warningCashDecreaseSpeed;

    private GameState state = GameState.Starting;
    [SerializeField]
    private Furnace furnace;
    [SerializeField]
    private float timer = 60 * 5; // 5 min

    [SerializeField]
    private float readyTimer = 3;
    [SerializeField]
    private string[] readyWords = {
        "Get Ready",
        "Almost time",
        "Go!"
    };
    [SerializeField]
    private TextMeshProUGUI readyTimerText;


    [SerializeField]
    private Image furnacePowerRepresentation;
    [SerializeField]
    private Image warningSign;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI bonusCashText;


    [SerializeField]
    private MMF_Player gameStartFeedback = null;
    [SerializeField]
    private MMF_Player gameReadyFeedback = null;
    [SerializeField]
    private MMF_Player gameWinFeedback = null;
    [SerializeField]
    private MMF_Player moneyChangeFeedback = null;
    [SerializeField]
    private MMF_Player timeoutFeedback = null;
    [SerializeField]
    private MMF_Player gameLoseFeedback = null;

    // Win:
    [SerializeField]
    private TextMeshProUGUI winBonusText;
    private int winBonusCashCounter;
    private float winBonusCashCounterTimerTotal = 1.2f;
    private float winBonusCashCounterTimerCurrent = 0f;
    [SerializeField]
    public bool startWinBonusCashCounter = false;


    // Lose:
    [SerializeField]
    private TextMeshProUGUI justificationText;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Starting)
        {
            readyTimer -= Time.deltaTime;
            if (readyTimer <= 0)
            {
                onCompleteIntro();
                return;
            }

            string word = readyWords[(int)(3 - readyTimer)];
            readyTimerText.text = word;

            if (!gameReadyFeedback.IsPlaying)
            {
                gameReadyFeedback.PlayFeedbacks();
            }
        }
        
        if (state == GameState.Playing)
        {
            bonusCashText.text = bonusCash.ToString("#,##0");
            // Update timer:
            timer = Mathf.Max(timer - Time.deltaTime, 0);
            int seconds = ((int)(Mathf.Ceil(timer % 60)));
            int minutes = ((int)timer / 60); 
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (minutes == 0 && seconds < 3)
            {
                timeoutFeedback.PlayFeedbacks();
            }

            if (seconds <= 0)
            {
                if (bonusCash > 0)
                {
                    state = GameState.Win; 
                    gameWinFeedback.PlayFeedbacks();
                    return;
                } else
                {
                    state = GameState.Lose;
                    gameLoseFeedback.PlayFeedbacks();
                    justificationText.text = $"You have cost the company {Mathf.Abs(bonusCash).ToString("#,##0")} bucks.";
                    return;
                }
            }

            // Update the graphics:
            float fillAmount = (furnace.currentPower / furnace.idealPower) / 2;
            furnacePowerRepresentation.fillAmount = fillAmount; // Divide by 2 because the ideal should go half-way.
            if (fillAmount < 0.25 || fillAmount > 0.75)
            {
                warningSign.enabled = true;
            } else
            {
                warningSign.enabled = false;
            }

            if (fillAmount <= 0 || fillAmount >= 1)
            {
                // Trigger game lost:
                state = GameState.Lose;
                gameLoseFeedback.PlayFeedbacks();
                justificationText.text = fillAmount <= 0
                    ? "You have let  the generator run too cold and it is now ruined"
                    : "You have let the generator run too hot and it has been ruined";
            }
        }

        if (state == GameState.Win)
        {
            if (startWinBonusCashCounter && winBonusCashCounter != bonusCash)
            {
                winBonusCashCounterTimerCurrent += Time.deltaTime;
                winBonusCashCounter = (int)Mathf.Lerp(0, bonusCash, winBonusCashCounterTimerCurrent / winBonusCashCounterTimerTotal);
            }

            winBonusText.text = winBonusCashCounter.ToString("#,##0");
        }



    }

    void onCompleteIntro()
    {
        gameStartFeedback.PlayFeedbacks();
        state = GameState.Playing;
    }

    public void AddBonusCash(int bonus) {
        bonusCash += bonus;
        moneyChangeFeedback.PlayFeedbacks();
    }
}
