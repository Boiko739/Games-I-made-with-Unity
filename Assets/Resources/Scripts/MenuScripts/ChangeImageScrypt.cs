using UnityEngine;
using UnityEngine.UI;

public class ChangeImageScrypt : MonoBehaviour
{
    public int currentImage = 0;

    public void ChangeImage(int next)
    {
        var inst = Resources.LoadAll($"Sprites/Hats")[1];
        var sprites = Resources.LoadAll($"Sprites/Hats", inst.GetType());

        int nextImage = currentImage + next;
        int lastImageIndex = sprites.Length - 1;

        if (nextImage < 0)
        {
            gameObject.GetComponent<Image>().sprite = sprites[lastImageIndex] as Sprite;

            currentImage = lastImageIndex;

            return;
        }
        else if (nextImage > lastImageIndex)
        {
            gameObject.GetComponent<Image>().sprite = sprites[0] as Sprite;

            currentImage = 0;

            return;
        }
        gameObject.GetComponent<Image>().sprite = sprites[nextImage] as Sprite;
        
        currentImage = nextImage;
    }
}
