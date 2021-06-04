using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class NetManager : MonoBehaviour
{
    private SignInResponse signInResponse;
    public List<Collection> collectionList = new List<Collection>();

    [SerializeField] private UIManager uIManager;
    [SerializeField] private TextMeshProUGUI usernameInput;
    [SerializeField] private TextMeshProUGUI passwordInput;
    [SerializeField] private TextMeshProUGUI errorText;
    
    public void OnSignInButtonClick()
    {
        StartCoroutine(SignIn());
    }

    private IEnumerator GetCollectionList()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://test.loy.am/api/sets");
        www.SetRequestHeader("Authorization", "Bearer " + signInResponse.access_token);
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        collectionList = JsonUtility.FromJson<CollectionList>("{ \"collections\": " + www.downloadHandler.text + "}").collections;
        uIManager.ActivateCollectionsScreen();
    }

    private IEnumerator SignIn()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("grant_type", "password"));
        formData.Add(new MultipartFormDataSection("client_id", "loyam_test"));
        formData.Add(new MultipartFormDataSection("client_secret", "0IcbmorPNeuEcywxvaGQzznSd3pIl8BF12hT8eeExuZ2G9XYJH7YHeQh"));
        formData.Add(new MultipartFormDataSection("username", usernameInput.text));
        formData.Add(new MultipartFormDataSection("password", passwordInput.text));

        UnityWebRequest www = UnityWebRequest.Post("https://test.loy.am/oauth/token", formData);
        
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
            errorText.text = www.error;
            errorText.enabled = true;
        }
        else
        {
            signInResponse = JsonUtility.FromJson<SignInResponse>(www.downloadHandler.text);
            StartCoroutine(GetCollectionList());
        }

    }
}
