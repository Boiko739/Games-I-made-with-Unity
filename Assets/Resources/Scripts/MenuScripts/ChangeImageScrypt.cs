using UnityEngine;
using UnityEngine.UI;

public class ChangeImageScrypt : MonoBehaviour
{
    public int currentImage = 0;
    public void ChangeImage(int next)
    {
        int nextImage = currentImage + next;
        int lastImageIndex = 0;
        var a = Resources.LoadAll($"Sprites/Hats")[1].GetType();//this is the place to work on the next time

        if (nextImage < 0)
        {
            gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load($"Sprites/Hats/{lastImageIndex}");

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
