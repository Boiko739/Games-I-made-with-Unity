using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteScript : MonoBehaviour
{
    public int currentHatSprite = 0, currentBirdSprite = 0;
    private List<Sprite> _hatSprites, _birdSprites;

    private void Start()
    {
        LoadHatSprites();
        LoadBirdSprites();
    }
    private void LoadBirdSprites()
    {
        var allBirdSprites = Resources.LoadAll<Sprite>($"Sprites/BirdSkins");

        _birdSprites = new List<Sprite>();
        for (int i = 0; i < allBirdSprites.Length; i++)
        {
            if (i % 3 == 0)
                _birdSprites.Add(allBirdSprites[i]);
        }
    }
    private void LoadHatSprites()
    {
        _hatSprites = Resources.LoadAll<Sprite>("Sprites/Hats").ToList();
    }
    public void ChangeHatSprite(int next)
    {
        currentHatSprite = ChangeSprite(currentHatSprite, next, _hatSprites);
    }
    public void ChangeBirdSprite(int next)
    {
        currentBirdSprite = ChangeSprite(currentBirdSprite, next, _birdSprites);
    }
    private int ChangeSprite(int currentSprite, int next, List<Sprite> sprites)
    {
        int nextSprite = currentSprite + next;
        int lastSpriteIndex = sprites.Count - 1;

        if (nextSprite < 0)
        {
            gameObject.GetComponent<Image>().sprite = sprites[lastSpriteIndex];
            return lastSpriteIndex;
        }
        else if (nextSprite > lastSpriteIndex)
        {
            gameObject.GetComponent<Image>().sprite = sprites[0];
            return 0;
        }
        gameObject.GetComponent<Image>().sprite = sprites[nextSprite];

        return nextSprite;
    }
}
