using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class NineSplineFix : MonoBehaviour
{
    Sprite sprite;
    Image img;
    public Vector4 border = new Vector4(3.5f, 3.5f, 3.5f, 3.5f);
    Sprite cachedSprite;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.type = Image.Type.Sliced;
        sprite = img.sprite;
    }

    private void OnEnable()
    {
        if (cachedSprite == null)
        {
            Rect rect = new Rect(0, 0, sprite.texture.width, sprite.texture.height);
            Sprite newSprite = Sprite.Create(sprite.texture, rect, new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit, 1, SpriteMeshType.FullRect, border);
            cachedSprite = newSprite;
        }
        img.sprite = cachedSprite;
    }
}