using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Respeller;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text wordText;

    [SerializeField] private Button StartGameButton;
    [SerializeField] private Button SubmitWinButton;
    [SerializeField] private Button SkipButton;
    [SerializeField] private Button CloseGameButton;

    bool isSDKInit = false;
    public DictationType dictationData;

    int currentWordCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        wordText.text = "Checking SDK Availablity";

        StartGameButton.interactable = false;
        SubmitWinButton.interactable = false;
        SkipButton.interactable = false;
        CloseGameButton.interactable = false;

        InvokeRepeating(nameof(CheckSDKAvailable), 0f, 1f);
    }

    void CheckSDKAvailable()
    {
        bool status = RespellerSDK.CheckSDKAvailable();

        if (status && !isSDKInit)
        {
            wordText.text = "SDK Available. You can start game now";
            isSDKInit = true;
            RespellerSDK.SetTestMode(false);

            StartGameButton.interactable = true;
        }
    }

    public void StartGame()
    {
        RespellerSDK.StartGame((data) =>
        {
            StartGameButton.interactable = false;

            SkipButton.interactable = true;
            SubmitWinButton.interactable = true;

            dictationData = data;

            currentWordCount = 0;
            UpdateUI();
        });
    }

    void UpdateUI()
    {
        if(dictationData != null)
        {
            if(dictationData.findWordsByUserInChallengeWords != null && dictationData.findWordsByUserInChallengeWords.Count > currentWordCount)
            {
                wordText.text = "Word: " + dictationData.findWordsByUserInChallengeWords[currentWordCount].wordTemp;
            }
            else
            {
                SkipButton.interactable = false;
                SubmitWinButton.interactable = false;

                CloseGameButton.interactable = true;

                wordText.text = "End of game. You can close game now.";
                RespellerSDK.FinishGame();
            }
        }
        else
        {
            wordText.text = "No Data Found";

            SkipButton.interactable = false;
            SubmitWinButton.interactable = false;

            CloseGameButton.interactable = true;
        }
    }

    public void SubmitWin()
    {
        RespellerSDK.WordWin(new WordWinType()
        {
            challengeWord_id = dictationData.findWordsByUserInChallengeWords[currentWordCount].id,
            dictation = dictationData,
            isWin = true
        });

        currentWordCount++;
        UpdateUI();
    }

    public void SkipWord()
    {
        RespellerSDK.SkipIt(dictationData);

        currentWordCount++;
        UpdateUI();
    }

    public void CloseGame()
    {
        RespellerSDK.CloseGame();
        wordText.text = "Game Closed. You can start new game now";

        StartGameButton.interactable = true;
        SubmitWinButton.interactable = false;
        SkipButton.interactable = false;
        CloseGameButton.interactable = false;
    }
}
