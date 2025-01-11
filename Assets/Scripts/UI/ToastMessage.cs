using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour
{
    public float lifeTime = 6;
    public float sinkSpeed = 0.5f;

    [SerializeField] private TextMeshProUGUI msgText;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Init(MessageSO msg)
    {
        msgText.text = msg.message;
        var rect = GetComponent<RectTransform>();
        var size = rect.sizeDelta;
        size.y = 30 * msg.lines;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    private void LateUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }

        var pos = transform.position;
        pos.y -= Time.deltaTime * sinkSpeed;
        transform.position = pos;
        if (lifeTime <= 2)
        {
            var col = image.color;
            col.a = lifeTime / 4;
            image.color = col;
        }
    }
}
