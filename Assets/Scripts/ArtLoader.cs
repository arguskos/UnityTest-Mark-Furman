using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

public class ArtLoader {
    private static string _url = "https://picsum.photos/";

    public static IEnumerator DownloadImage(RawImage texture, int width = 100, int height = 90) {
        var url = $"{_url}{width}/{height}";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.LogError(request.error);
        else
            texture.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
