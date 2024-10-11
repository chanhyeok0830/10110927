using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetRawImageToPath : MonoBehaviour
{
    public string imageName = "yourImage.png"; // StreamingAssets ������ �ִ� �̹��� �̸�
    private RawImage rawImage;

    void Start()
    {
        rawImage=transform.GetComponent<RawImage>();
        StartCoroutine(LoadPathImage());
    }

    IEnumerator LoadPathImage()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath,"UI", imageName);

        // �ٸ� �÷����� ���, File �б⸦ ����Ͽ� �̹��� �ε�
        if (System.IO.File.Exists(filePath))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            rawImage.texture = texture;
            // ���� �ؽ�ó ������ ����
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
