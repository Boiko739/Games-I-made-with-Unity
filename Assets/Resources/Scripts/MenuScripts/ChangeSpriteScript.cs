using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteScript : MonoBehaviour
{
    public int currentBirdSprite = 0, currentHatSprite = 0,
        currentBackgroundSprite = 0, currentPipeSprite = 0;
    public GameObject bird, hat, background, pipe;
    [SerializeField]
    private List<Sprite> _birdSprites, _hatSprites, _backgroundSprites, _pipeSprites;

    private void Start()
    {
        LoadHatSprites();
        LoadBirdSprites();
        LoadBackgroundSprites();
        LoadPipeSprites();
    }
    public void SetByDefault()
    {
        currentBirdSprite = ChangeSprite(bird, 0, 0, _birdSprites);
        currentHatSprite = ChangeSprite(hat, 0, 0, _hatSprites);
        currentBackgroundSprite = ChangeSprite(background, 0, 0, _backgroundSprites);
        currentPipeSprite = ChangeSprite(pipe, 0, 0, _pipeSprites);
    }
    private void LoadBirdSprites()
    {
        var allBirdSprites = Resources.LoadAll<Sprite>($"Sprites/Birds");

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
    private void LoadBackgroundSprites()
    {
        _backgroundSprites = Resources.LoadAll<Sprite>("Sprites/Backgrounds").ToList();
    }
    private void LoadPipeSprites()
    {
        _pipeSprites = Resources.LoadAll<Sprite>("Sprites/Pipes").ToList();
    }

    public void ChangeBirdSprite(int next)
    {
        currentBirdSprite = ChangeSprite(bird, currentBirdSprite, next, _birdSprites);
    }
    public void ChangeHatSprite(int next)
    {
        currentHatSprite = ChangeSprite(hat, currentHatSprite, next, _hatSprites);
    }
    public void ChangeBackgroundSprite(int next)
    {
        currentBackgroundSprite = ChangeSprite(background, currentBackgroundSprite, next, _backgroundSprites);
    }
    public void ChangePipeSprite(int next)
    {
        currentPipeSprite = ChangeSprite(pipe, currentPipeSprite, next, _pipeSprites);
    }

    private int ChangeSprite(GameObject objToChange, int currentSprite, int next, List<Sprite> sprites)
    {
        int nextSprite = currentSprite + next;
        int lastSpriteIndex = sprites.Count - 1;

        if (nextSprite < 0)
        {
            objToChange.GetComponent<Image>().sprite = sprites[lastSpriteIndex];
            return lastSpriteIndex;
        }
        else if (nextSprite > lastSpriteIndex)
        {
            objToChange.GetComponent<Image>().sprite = sprites[0];
            return 0;
        }
        objToChange.GetComponent<Image>().sprite = sprites[nextSprite];

        return nextSprite;
    }
}
