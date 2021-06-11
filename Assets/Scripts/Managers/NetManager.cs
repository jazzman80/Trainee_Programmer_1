using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
public class NetManager : MonoBehaviour
{
    private const string loginAdress = "https://test.loy.am/oauth/token";
    private const string collectionListAdress = "https://test.loy.am/api/sets";
    private const string registrateAdress = "https://test.loy.am/api/users";
    private const string clientSecret = "0IcbmorPNeuEcywxvaGQzznSd3pIl8BF12hT8eeExuZ2G9XYJH7YHeQh";

    private SignInResponse signInResponse;
    public List<Collection> collectionList = new List<Collection>();

    [Header("System Components")]
    [SerializeField] private UIManager uIManager;

    public void OnSignInButtonClick()
    {
        if(!uIManager.signInScreen.CheckError()) StartCoroutine(SignIn());
    }

    public void OnSignUpButtonClick()
    {
        if (!uIManager.signUpScreen.CheckError()) StartCoroutine(Registrate());
    }

    private IEnumerator GetCollectionList()
    {
        UnityWebRequest www = UnityWebRequest.Get(collectionListAdress);
        www.SetRequestHeader("Authorization", "Bearer " + signInResponse.access_token);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        collectionList = JsonConvert.DeserializeObject<List<Collection>>(www.downloadHandler.text);
        uIManager.ActivateCollectionsScreen();
    }

    private IEnumerator SignIn()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("grant_type", "password"));
        formData.Add(new MultipartFormDataSection("client_id", "loyam_test"));
        formData.Add(new MultipartFormDataSection("client_secret", clientSecret));
        formData.Add(new MultipartFormDataSection("username", uIManager.signInScreen.username.text));
        formData.Add(new MultipartFormDataSection("password", uIManager.signInScreen.password.text));

        UnityWebRequest www = UnityWebRequest.Post(loginAdress, formData);
        
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            uIManager.errorScreen.Alert(www.error);
        }
        else
        {
            signInResponse = JsonConvert.DeserializeObject<SignInResponse>(www.downloadHandler.text);
            StartCoroutine(GetCollectionList());
        }

    }

    private IEnumerator Registrate()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("client_id", "loyam_test"));
        formData.Add(new MultipartFormDataSection("client_secret", clientSecret));
        formData.Add(new MultipartFormDataSection("username", uIManager.signUpScreen.username.text));
        formData.Add(new MultipartFormDataSection("password", uIManager.signUpScreen.password.text));
        formData.Add(new MultipartFormDataSection("email", uIManager.signUpScreen.email.text));

        UnityWebRequest www = UnityWebRequest.Post(registrateAdress, formData);

        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            if (www.responseCode == 422) uIManager.errorScreen.Alert("Username or e-mail has already been taken");
            else uIManager.errorScreen.Alert(www.error);
        }
        else uIManager.SuccessRegistration();

    }
}
