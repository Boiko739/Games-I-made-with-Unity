using UnityEngine;
using UnityEngine.UI;

public class ChangeImageScrypt : MonoBehaviour
{
    public int currentImage = 0;

    public void ChangeImage(int next)
    {
        var arr = Resources.LoadAll($"Sprites/Hats");
        var inst = arr[1];
        var t = inst.GetType();
        var a = Resources.LoadAll($"Sprites/Hats", t);
        int nextImage = currentImage + next;
        int lastImageIndex = a.Length - 1;

        if (nextImage < 0)
        {
            gameObject.GetComponent<Image>().sprite = a[lastImageIndex] as Sprite;

            currentImage = lastImageIndex;

            return;
        }
        else if (nextImage > lastImageIndex)
        {
            gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load($"Sprites//Hats//0");

            currentImage = 0;

            return;
        }
        gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load($"Sprites//Hats//{nextImage}");
        
        currentImage = nextImage;
    }
}
