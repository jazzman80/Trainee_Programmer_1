using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
public class NetManager : MonoBehaviour
{
    private const string loginAddress = "https://test.loy.am/oauth/token";
    private const string collectionListAddress = "https://test.loy.am/api/sets";
    private const string registrateAddress = "https://test.loy.am/api/users";
    private const string refreshAddress = "https://test.loy.am/oauth/token";
    private const string collectionElementsAddress = "https://test.loy.am/api/subjects";
    private const string favoriteElementAddressStart = "https://test.loy.am/api/subjects/";
    private const string favoriteElementAddressEnd = "/actions/favorite";
    private const string unFavoriteElementAddressEnd = "/actions/unfavorite";
    private const string clientSecret = "0IcbmorPNeuEcywxvaGQzznSd3pIl8BF12hT8eeExuZ2G9XYJH7YHeQh";

    private SignInResponse signInResponse;
    private string accessToken;
    private string refreshToken;

    public List<CollectionListResponse> collectionList = new List<CollectionListResponse>();
    public List<CollectionElementResponse> elementsList = new List<CollectionElementResponse>();

    [Header("System Components")]
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Database database;
    [SerializeField] private SpawnManager spawnManager;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AccessToken")) uIManager.ActivateSignInScreen();
        else
        {
            LoadTokens();
            StartCoroutine(RefreshToken());
        }
    }

    public void OnSignInButtonClick()
    {
        if(!uIManager.signInScreen.CheckError()) StartCoroutine(SignIn());
    }

    public void OnSignUpButtonClick()
    {
        if (!uIManager.signUpScreen.CheckError()) StartCoroutine(Registrate());
    }

    private void SaveTokens()
    {
        PlayerPrefs.SetString("AccessToken", accessToken);
        PlayerPrefs.SetString("RefreshToken", refreshToken);
        PlayerPrefs.Save();
    }

    private void LoadTokens()
    {
        accessToken = PlayerPrefs.GetString("AccessToken");
        refreshToken = PlayerPrefs.GetString("RefreshToken");
    }

    private IEnumerator GetCollectionList()
    {
        UnityWebRequest www = UnityWebRequest.Get(collectionListAddress);
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        collectionList = JsonConvert.DeserializeObject<List<CollectionListResponse>>(www.downloadHandler.text);

        StartCoroutine(GetCollectionElements());
    }

    private IEnumerator SignIn()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("grant_type", "password"));
        formData.Add(new MultipartFormDataSection("client_id", "loyam_test"));
        formData.Add(new MultipartFormDataSection("client_secret", clientSecret));
        formData.Add(new MultipartFormDataSection("username", uIManager.signInScreen.username.text));
        formData.Add(new MultipartFormDataSection("password", uIManager.signInScreen.password.text));

        UnityWebRequest www = UnityWebRequest.Post(loginAddress, formData);
        
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            uIManager.errorScreen.Alert(www.error);
        }
        else
        {
            signInResponse = JsonConvert.DeserializeObject<SignInResponse>(www.downloadHandler.text);

            accessToken = signInResponse.access_token;
            refreshToken = signInResponse.refresh_token;
            SaveTokens();

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

        UnityWebRequest www = UnityWebRequest.Post(registrateAddress, formData);

        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            if (www.responseCode == 422) uIManager.errorScreen.Alert("Username or e-mail has already been taken");
            else uIManager.errorScreen.Alert(www.error);
        }
        else uIManager.SuccessRegistration();

    }

    private IEnumerator GetCollectionElements()
    {
        UnityWebRequest www = UnityWebRequest.Get(collectionElementsAddress);
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        elementsList = JsonConvert.DeserializeObject<List<CollectionElementResponse>>(www.downloadHandler.text);

        database.BuildDatabase(collectionList, elementsList);
        spawnManager.BuildCollectionListScreen();
        uIManager.ActivateCollectionListScreen();
    }

    private IEnumerator RefreshToken()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("grant_type", "refresh_token"));
        formData.Add(new MultipartFormDataSection("client_id", "loyam_test"));
        formData.Add(new MultipartFormDataSection("client_secret", clientSecret));
        formData.Add(new MultipartFormDataSection("refresh_token", refreshToken));

        UnityWebRequest www = UnityWebRequest.Post(refreshAddress, formData);

        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            signInResponse = JsonConvert.DeserializeObject<SignInResponse>(www.downloadHandler.text);

            accessToken = signInResponse.access_token;
            refreshToken = signInResponse.refresh_token;
            SaveTokens();

            StartCoroutine(GetCollectionList());
        }
        else uIManager.ActivateSignInScreen();

    }

    private IEnumerator FavCollectionElement(int elementIndex)
    {
        byte[] bodyData = null;
        UnityWebRequest www = UnityWebRequest.Put(favoriteElementAddressStart + elementIndex.ToString() + favoriteElementAddressEnd, bodyData);
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();
    }

    public void FavoriteElement(int _elementIndex)
    {
        StartCoroutine(FavCollectionElement(_elementIndex));
    }

    private IEnumerator UnFavCollectionElement(int elementIndex)
    {
        byte[] bodyData = null;
        UnityWebRequest www = UnityWebRequest.Put(favoriteElementAddressStart + elementIndex.ToString() + unFavoriteElementAddressEnd, bodyData);
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();
    }

    public void UnFavoriteElement(int _elementIndex)
    {
        StartCoroutine(UnFavCollectionElement(_elementIndex));
    }

    //avoid "put request with empty body" error 
    public static UnityWebRequest Put(string uri, byte[] bodyData)
    {
        return new UnityWebRequest(uri, "PUT", (DownloadHandler)new DownloadHandlerBuffer(), bodyData == null || bodyData.Length == 0 ? null : (UploadHandler)new UploadHandlerRaw(bodyData));
    }

}
