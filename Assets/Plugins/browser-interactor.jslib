mergeInto(LibraryManager.library, {

    respeller_checkRespellerVariable: function()
    {
        if(window.RespellrSDK)
            return true;
        else
            return false;
    },

    respeller_enableTestMode: function(status)
    {
        window.RespellrSDK.setTestMode(true);
    },

    respeller_startGame: function()
    {
      window.RespellrSDK.startGame((data) => {
          SendMessage('RespellerSDKListener', 'respeller_onGameStarted', JSON.stringify(data));
      });
    },

    respeller_wordWin: function(dataPtr)
    {
      window.RespellrSDK.wordWin(JSON.parse(UTF8ToString(dataPtr)));
    },

    respeller_skipit: function(dataPtr)
    {
      window.RespellrSDK.skipIt(JSON.parse(UTF8ToString(dataPtr)));
    },

    respeller_finishGame: function()
    {
      window.RespellrSDK.finishGame();
    },

    respeller_closeGame: function()
    {
      window.RespellrSDK.closeGame();
    }
});