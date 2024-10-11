using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonImageToPath : MonoBehaviour
{

    public string imageName_Default = "default.png"; // �⺻ �̹��� �̸�
    public string imageName_Select = "select.png"; // ���õ� �̹��� �̸�
    private Image image;

    public bool isON = false;

    private Sprite sprite_Default;
    private Sprite sprite_Select;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(LoadSprites());
    }

    IEnumerator LoadSprites()
    {
        // �⺻ �̹��� �ε�
        yield return StartCoroutine(LoadSprite(imageName_Default, sprite => sprite_Default = sprite));

        // ���õ� �̹��� �ε�
        yield return StartCoroutine(LoadSprite(imageName_Select, sprite => sprite_Select = sprite));

        // �ʱ� �̹��� ����
        SetImage();
    }

    IEnumerator LoadSprite(string imageName, System.Action<Sprite> callback)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "UI", imageName);

        if (System.IO.File.Exists(filePath))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            // �ؽ�ó�� ��������Ʈ�� ��ȯ
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(texture, rect, pivot);

            callback(sprite);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
    }

    void SetImage()
    {
        if (isON)
        {
            image.sprite = sprite_Select;
            if (sprite_Select != null)
            {
                RectTransform rectTransform = image.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(sprite_Select.texture.width, sprite_Select.texture.height);
            }
        }
        else
        {
            image.sprite = sprite_Default;
            if (sprite_Default != null)
            {
                RectTransform rectTransform = image.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(sprite_Default.texture.width, sprite_Default.texture.height);
            }
        }
    }


    public void ToggleImage()
    {
        isON = !isON;
        SetImage();
    }
}
