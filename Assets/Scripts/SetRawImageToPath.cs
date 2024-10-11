using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetRawImageToPath : MonoBehaviour
{
    public string imageName = "yourImage.png"; // StreamingAssets 폴더에 있는 이미지 이름
    private RawImage rawImage;

    void Start()
    {
        rawImage=transform.GetComponent<RawImage>();
        StartCoroutine(LoadPathImage());
    }

    IEnumerator LoadPathImage()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath,"UI", imageName);

        // 다른 플랫폼의 경우, File 읽기를 사용하여 이미지 로드
        if (System.IO.File.Exists(filePath))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            rawImage.texture = texture;
            // 원본 텍스처 사이즈 유지
            RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(texture.width, texture.height);

        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
    }
}
