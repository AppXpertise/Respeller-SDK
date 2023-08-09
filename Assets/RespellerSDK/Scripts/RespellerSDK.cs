/*
 * Author: Hamza Iftikhar
 * Version: 1.0.0
 */


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Respeller
{
    /// <summary>
    /// Respeller SDK Class for Web Extension Interaction in Browser.
    /// </summary>
    public class RespellerSDK
    {
        /// <summary>
        /// Callback for Session Start Event
        /// </summary>
        public static Action<DictationType> Respeller_OnSessionStarted;

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool respeller_checkRespellerVariable();

        [DllImport("__Internal")]
        private static extern void respeller_enableTestMode(bool status);

        [DllImport("__Internal")]
        private static extern void respeller_startGame();

        [DllImport("__Internal")]
        private static extern void respeller_wordWin(string data);

        [DllImport("__Internal")]
        private static extern void respeller_skipit(string data);

        [DllImport("__Internal")]
        private static extern void respeller_finishGame();

        [DllImport("__Internal")]
        private static extern void respeller_closeGame();
#endif

        /// <summary>
        /// Checks if the SDK environment is setup and ready to use
        /// </summary>
        /// <returns>
        /// Returns true is SDK environment is ready to use.
        /// </returns>
        public static bool CheckSDKAvailable()
        {
            RespellerSDKListener listener = GameObject.FindObjectOfType<RespellerSDKListener>();
            if (listener)
            {
                listener.name = nameof(RespellerSDKListener);
                listener.gameObject.SetActive(true);
            }
            else
            {
                new GameObject(nameof(RespellerSDKListener), typeof(RespellerSDKListener));
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            return respeller_checkRespellerVariable();
#else
            Debug.LogError("Not available for this platform");
            return false;
#endif
        }

        /// <summary>
        /// Set the Test Mode for the SDK. (Only works if the SDK environment is ready.)
        /// </summary>
        /// <param name="status">The status for test mode (True or False)</param>
        public static void SetTestMode(bool status)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_enableTestMode(status);
#else
            Debug.LogError("Not available for this platform");
#endif
        }

        /// <summary>
        /// Start the game session in the SDK.
        /// Close a game session before starting a new one.
        /// </summary>
        /// <param name="OnSessionStarted">Callback called upon successful session start. Gives DictationType class instance</param>
        public static void StartGame(System.Action<DictationType> OnSessionStarted)
        {
            Respeller_OnSessionStarted = OnSessionStarted;
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_startGame();
#else
            Debug.LogError("Not available for this platform");
#endif
        }

        /// <summary>
        /// Submit upon selection of the word.
        /// </summary>
        /// <param name="wordWinType">WordWinType class instance containing information about the selected word.</param>
        public static void WordWin(WordWinType wordWinType)
        {
            string data = JsonUtility.ToJson(wordWinType);
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_wordWin(data);
#else
            Debug.LogError("Not available for this platform");
#endif
        }

        /// <summary>
        /// Skip the word
        /// </summary>
        /// <param name="dictation">Dictation class instance of current session</param>
        public static void SkipIt(DictationType dictation)
        {
            string data = JsonUtility.ToJson(dictation);
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_skipit(data);
#else
            Debug.LogError("Not available for this platform");
#endif
        }

        /// <summary>
        /// Finishes the game upon completion of words in the dictation
        /// </summary>
        public static void FinishGame()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_finishGame();
#else
            Debug.LogError("Not available for this platform");
#endif
        }

        /// <summary>
        /// Close the game session.
        /// </summary>
        public static void CloseGame()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_closeGame();
#else
            Debug.LogError("Not available for this platform");
#endif
        }
    }

    /// <summary>
    /// Dictation Type Data Class
    /// </summary>
    [Serializable]
    public class DictationType
    {
        /// <summary>
        /// Id from DB for dictation type
        /// </summary>
        public int id;

        /// <summary>
        /// List of dictations
        /// </summary>
        public List<string> dictation;

        /// <summary>
        /// List of words to find
        /// </summary>
        public List<Word> findWordsByUserInChallengeWords;

        /// <summary>
        /// Number of error words in dictation
        /// </summary>
        public int numberOfErrorWordInDictation;

        /// <summary>
        /// Number of correct words in dicatation
        /// </summary>
        public int numberOfWordInDictation;

        /// <summary>
        /// Points for this dictation
        /// </summary>
        public int points;
    }

    /// <summary>
    /// Word Type Data Class
    /// </summary>
    [Serializable]
    public class Word
    {
        /// <summary>
        /// Id of the Challenge Word in DB
        /// </summary>
        public int id;

        /// <summary>
        /// Word ID from DB
        /// </summary>
        public int wordId;

        /// <summary>
        /// User ID from DB
        /// </summary>
        public int userId;

        /// <summary>
        /// Actual Word String
        /// </summary>
        public string wordTemp;

        /// <summary>
        /// Date Time String for last misspelled
        /// </summary>
        public string lastTimeMisspelled;

        /// <summary>
        /// Date Time String for last mastered
        /// </summary>
        public string lastTimeMastered;

        /// <summary>
        /// The text of the word with spelling error
        /// </summary>
        public string misspelling;

        /// <summary>
        /// Price of word from DB
        /// </summary>
        public int priceOfWord;

        /// <summary>
        /// Number of times misspelled
        /// </summary>
        public int numberOfMisspelling;

        /// <summary>
        /// Number of times correct
        /// </summary>
        public int numberOfCorrect;

        /// <summary>
        /// Number of Letters in Word
        /// </summary>
        public int numberOfLetters;

        /// <summary>
        /// Number of Wrong letters in word
        /// </summary>
        public int numberOfMistakeLetters;

        /// <summary>
        /// The text of the sentence where the word was last encountered in dictation
        /// </summary>
        public string inSentenceLastTime;

        /// <summary>
        /// The index of word in sentence
        /// </summary>
        public int wordIndexInSentence;

        /// <summary>
        /// Is word mastered?
        /// </summary>
        public bool isMastered;

        /// <summary>
        /// Number of times user successfully completed a game with this word
        /// </summary>
        public int numberOfWin;

        /// <summary>
        /// Number of times user failed to complete a game with this word
        /// </summary>
        public int numberOfLose;

        /// <summary>
        /// The id of the google doc the word was taken from
        /// </summary>
        public string dictationId;

        /// <summary>
        /// Points that the user already received for "fixing" this word in games
        /// </summary>
        public int points;

        /// <summary>
        /// Created Log from DB
        /// </summary>
        public string createdAt;

        /// <summary>
        /// Updated Log from DB
        /// </summary>
        public string updatedAt;
    }

    /// <summary>
    /// Word Win Type Data Class
    /// </summary>
    [Serializable]
    public class WordWinType
    {
        /// <summary>
        /// Has user win?
        /// Always true
        /// </summary>
        public bool isWin;

        /// <summary>
        /// The challenge word ID from Word class (id)
        /// </summary>
        public int challengeWord_id;

        /// <summary>
        /// Current Dictation Type being played
        /// </summary>
        public DictationType dictation;
    }
}