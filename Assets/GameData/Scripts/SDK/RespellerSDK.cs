using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Respeller
{
    public class RespellerSDK
    {
        public static System.Action<DictationType> Respeller_OnSessionStarted;

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
#endif

            return false;
        }

        public static void SetTestMode(bool status)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_enableTestMode(status);
#endif
        }

        public static void StartGame(System.Action<DictationType> OnSessionStarted)
        {
            Respeller_OnSessionStarted = OnSessionStarted;
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_startGame();
#endif
        }

        public static void WordWin(WordWinType wordWinType)
        {
            string data = JsonUtility.ToJson(wordWinType);
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_wordWin(data);
#endif
        }

        public static void SkipIt(DictationType dictation)
        {
            string data = JsonUtility.ToJson(dictation);
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_skipit(data);
#endif
        }

        public static void FinishGame()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_finishGame();
#endif
        }

        public static void CloseGame()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            respeller_closeGame();
#endif
        }
    }

    [System.Serializable]
    public class DictationType
    {
        public int id;
        public List<string> dictation;
        public List<Word> findWordsByUserInChallengeWords;
        public int numberOfErrorWordInDictation;
        public int numberOfWordInDictation;
        public int points;
    }

    [System.Serializable]
    public class Word
    {
        public int id;
        public int wordId;
        public int userId;
        public string wordTemp;
        public string lastTimeMisspelled;
        public string lastTimeMastered;
        public string misspelling;
        public int priceOfWord;
        public int numberOfMisspelling;
        public int numberOfCorrect;
        public int numberOfLetters;
        public int numberOfMistakeLetters;
        public string inSentenceLastTime;
        public int wordIndexInSentence;
        public bool isMastered;
        public int numberOfWin;
        public int numberOfLose;
        public string dictationId;
        public int points;
        public string createdAt;
        public string updatedAt;
    }

    [System.Serializable]
    public class WordWinType
    {
        public bool isWin;
        public int challengeWord_id;
        public DictationType dictation;
    }
}
