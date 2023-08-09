/*
 * Author: Hamza Iftikhar
 * Version: 1.0.0
 */

using System;
using UnityEngine;

namespace Respeller
{
    /// <summary>
    /// Respeller SDK Listener Class for Web Extension Interaction in Browser.
    /// To listen events being called from the browser
    /// </summary>
    public class RespellerSDKListener : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Event called from web browser on game session started
        /// </summary>
        /// <param name="data">String data of current Dictation Type to start session</param>
        public void respeller_onGameStarted(string data)
        {
            RespellerSDK.Respeller_OnSessionStarted?.Invoke(JsonUtility.FromJson<DictationType>(data));
        }
    }
}