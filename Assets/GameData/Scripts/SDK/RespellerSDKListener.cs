using UnityEngine;
using Respeller;

public class RespellerSDKListener : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void respeller_onGameStarted(string data)
    {
        RespellerSDK.Respeller_OnSessionStarted.Invoke(JsonUtility.FromJson<DictationType>(data));
    }
}
