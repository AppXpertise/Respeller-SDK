# Respeller SDK

Call CheckSDKAvailable() function at the very beginning of the game to verify the environment has been ready. Rest of the functions will not work unless the environment is ready.
This function also adds the necessary object to hear the callbacks from the browser.

Call SetTestMode(bool status) to enable/disable the test mode before making any further calls after environment ready.

Once set, call the StartGame(Action<DictationType> OnSessionStarted) to request for starting the game session in the browser extenstion. Once done, your callback provided in this function will trigger.

Use the WordWin(WordWinType wordWinType) function to submit the word selected by the user.

Use the SkipIt(DictationType dictation) function to skip the current word

Use the FinishGame() function to finish the game when all words are finished.

Use the CloseGame() function to close the current session of the game.

Don't forget to close the current session before starting a new one.

Detailed documentation regarding classes and functions can be found in the respective files.

To enable using the browser extension, you must create the WEBGL Build with the following configuration:
Build Settings -> Switch to WEBGL Platform
Player Settings -> Resolution and Presentation -> Select Respeller Package WebView as WebGL Template