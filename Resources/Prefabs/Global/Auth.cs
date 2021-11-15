using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;


public class Auth : MonoBehaviour
{
    FirebaseAuth auth = null;
    FirebaseUser user = null;

    //void Awake() => DontDestroyOnLoad(this);

    void Start()
    {
        InitializeFirebase();
        InitializeGPGS();

        if (auth.CurrentUser == null)
        {
            AnonymousSignIn();
        }
    }



    private void InitializeGPGS()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }



    void InitializeFirebase()
    {
       auth = FirebaseAuth.DefaultInstance;
       auth.StateChanged += AuthStateChanged;
    }



    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            user = auth.CurrentUser;
        }
        if (user != null)
        {
            Debug.Log("Login: [" + user.UserId + "]");
            // user.TokenAsync(false).ContinueWith(task => {
            //     Debug.Log("token = " + task.Result);
            // });
        }
    }



    void AnonymousSignIn()
    {
       auth.SignInAnonymouslyAsync().ContinueWith(task => {
           if (task.IsCanceled)
           {
               Debug.LogError("SignInAnonymouslyAsync was canceled.");
               return;
           }
           if (task.IsFaulted)
           {
               Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
               return;
           }
       });
    }



    void GPGSingIn()
    {
        Social.localUser.Authenticate((bool success) => 
        {
            if (success)
            {
                string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);

                auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("SignInWithCredentialAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                        return;
                    }
                });
            }
        });
    }



    void SignOut()
    {
       auth.SignOut();
    }
}
