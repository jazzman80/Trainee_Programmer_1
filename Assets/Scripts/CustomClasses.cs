
[System.Serializable]
public class Collection
{
    public int id;
    public string name;
    public string catalogName;
    public string haveItems;
    public int totalItems;
    public string previewImage;
    public string backgroundImage;
    public string collectionIcon;
}

[System.Serializable]
public class SignInResponse
{
    public string token_type;
    public int expires_in;
    public string access_token;
    public string refresh_token;
}
