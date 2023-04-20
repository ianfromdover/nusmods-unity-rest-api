using System.Collections;
using TMPro;
using UnityEngine;
using wr = UnityEngine.Networking.UnityWebRequest;

public class RestAPI : MonoBehaviour
{
    public TextMeshProUGUI modTextField; // case sensitive
    public TextMeshProUGUI output;
    public string key = "description";
    
    private string addr = "https://api.nusmods.com/v2/2022-2023/modules/";
    private const string JsonEnd = ".json";
    /// <summary>
    /// Called by the button to start the coroutine of getting the data
    /// </summary>
    public void StartGettingData()
    {
        StartCoroutine(GetData());
    }

    /// <summary>
    /// Uses UnityWebRequest to GET the desc of a mod from nusmods
    /// </summary>
    /// <returns></returns>
    IEnumerator GetData()
    {
        // using (UnityWebRequest req = UnityWebRequest.Get(addr))
        string combinedAddr = addr + modTextField.text + JsonEnd;
        using wr req = wr.Get(combinedAddr); // the using keyword disposes of unused resources when operation is completed
        {
            yield return req.SendWebRequest();
            if (req.result == wr.Result.ConnectionError)
            {
               Debug.LogError(req.error); 
            }
            else
            {
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSONNode.Parse(json);

                output.text = stats[key];
            }
        }
    }
}
