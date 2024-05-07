using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;

public class PlayfabManager : MonoBehaviour
{

    public void OnSaveuser(string pUsername, string pPassword)
    {
        PlayerPrefs.SetString("username", pUsername);
        PlayerPrefs.SetString("password", pPassword);
        PlayerPrefs.Save();
    }



    public void OnServerInitialized()
    {
    }

    public void OnTryLogin(string pEmail, string pPassword, Action<string, string> pCallbackSuccess, Action<PlayFabError> pCallbackFailure)
    {
        LoginWithEmailAddressRequest mRequest = new LoginWithEmailAddressRequest
        {
            Email = pEmail,
            Password = pPassword
        };

        PlayFabClientAPI.LoginWithEmailAddress(mRequest,
        res =>
        {
            OnSaveuser(pEmail, pPassword);
        },
        err =>
        {
            pCallbackFailure(err);
        });
    }

    public void OnLogout()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

    public void OnLogoutForced()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

    public void OnTryRegisterNewAccount(string pEmail, string pPassword, Action pCallbackSuccess, Action<PlayFabError> pCallbackFailure)
    {
        RegisterPlayFabUserRequest mReqest = new RegisterPlayFabUserRequest
        {
            Email = pEmail,
            Password = pPassword,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(mReqest,
        res =>
        {
            OnSaveuser(pEmail, pPassword);
            pCallbackSuccess();
        },
        err =>
        {
            pCallbackFailure(err);
        });
    }

    public void InitiatePasswordRecovery(string pEmail, Action pCallbackSuccess, Action<PlayFabError> pCallbackFailure)
    {
        SendAccountRecoveryEmailRequest mRequest = new SendAccountRecoveryEmailRequest
        {
            Email = pEmail,
            TitleId = "B9E19"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(mRequest,
        result =>
        {
            Debug.Log("Password reset email sent successfully.");
            pCallbackSuccess();
        },
        error =>
        {
            Debug.LogError("Password reset failed: " + error.ErrorMessage);
            pCallbackFailure(error);
        });
    }

}
