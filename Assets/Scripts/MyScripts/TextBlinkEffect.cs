using TMPro;
using UnityEngine;
using System.Collections; // 추가된 using directive

public class TextBlinkEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 깜빡이는 효과를 적용할 TextMeshPro 객체
    public float blinkInterval = 0.5f;   // 깜빡이는 간격 (초 단위)

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        
        // 코루틴을 사용해 텍스트 깜빡임 시작
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            // 텍스트의 알파 값을 0으로 설정하여 숨김
            textMeshPro.alpha = 0;
            yield return new WaitForSeconds(blinkInterval);

            // 텍스트의 알파 값을 1로 설정하여 보이게 함
            textMeshPro.alpha = 1;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}