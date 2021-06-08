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
    private const string clientSecret = "0IcbmorPNeuEcywxvaGQzznSd3pIl8BF12hT8eeExuZ2G9XYJH7YHeQh";

    private SignInResponse signInResponse;
    public List<Collection> collectionList = new List<Collection>();

    [SerializeField] private UIManager uIManager;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TextMeshProUGUI errorText;
    
    public void OnSignInButtonClick()
    {
        StartCoroutine(SignIn());
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
        formData.Add(new MultipartFormDataSection("username", usernameInput.text));
        formData.Add(new MultipartFormDataSection("password", passwordInput.text));

        UnityWebRequest www = UnityWebRequest.Post(loginAdress, formData);
        
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
            errorText.text = www.error;
            errorText.enabled = true;
        }
        else
        {
            signInResponse = JsonConvert.DeserializeObject<SignInResponse>(www.downloadHandler.text);
            StartCoroutine(GetCollectionList());
        }

    }
}
