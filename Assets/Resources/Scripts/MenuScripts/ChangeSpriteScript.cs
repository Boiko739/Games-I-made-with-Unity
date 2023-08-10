using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteScript : MonoBehaviour
{
    [field: SerializeField]
    public int CurrentBirdSprite { get; private set; } = 0;
    [field: SerializeField]
    public int CurrentHatSprite { get; private set; } = 0;
    [field: SerializeField]
    public int CurrentBackgroundSprite { get; private set; } = 0;
    [field: SerializeField]
    public int CurrentPipeSprite { get; private set; } = 0;

    public GameObject BirdImage, HatImage, BackgroundImage, PipeImage;

    [SerializeField]
    private List<Sprite> _birdSprites, _hatSprites, _backgroundSprites, _pipeSprites;
    public static List<Sprite> GameSprites { get; private set; }

    private void Start()
    {
        _birdSprites = Resources.LoadAll<Sprite>($"Sprites/Birds").ToList();
        _hatSprites = Resources.LoadAll<Sprite>("Sprites/Hats").ToList();
        _backgroundSprites = Resources.LoadAll<Sprite>("Sprites/Backgrounds").ToList();
        _pipeSprites = Resources.LoadAll<Sprite>("Sprites/Pipes").ToList();
        if (GameSprites == null) SaveAllSprites();
    }

    public void SetByDefault()
    {
        CurrentBirdSprite = ChangeSpriteImage(BirdImage, 0, 0, _birdSprites);
        CurrentHatSprite = ChangeSpriteImage(HatImage, 0, 0, _hatSprites);
        CurrentBackgroundSprite = ChangeSpriteImage(BackgroundImage, 0, 0, _backgroundSprites);
        CurrentPipeSprite = ChangeSpriteImage(PipeImage, 0, 0, _pipeSprites);
        SaveAllSprites();
    }

    public void ChangeBirdSpriteImage(int next)
    {
        CurrentBirdSprite = ChangeSpriteImage(BirdImage, CurrentBirdSprite, next * 3, _birdSprites);
        SaveAllSprites();
    }

    public void ChangeHatSpriteImage(int next)
    {
        CurrentHatSprite = ChangeSpriteImage(HatImage, CurrentHatSprite, next, _hatSprites);
        SaveAllSprites();
    }

    public void ChangeBackgroundSpriteImage(int next)
    {
        CurrentBackgroundSprite = ChangeSpriteImage(BackgroundImage, CurrentBackgroundSprite, next, _backgroundSprites);
        SaveAllSprites();
    }

    public void ChangePipeSpriteImage(int next)
    {
        CurrentPipeSprite = ChangeSpriteImage(PipeImage, CurrentPipeSprite, next, _pipeSprites);
        SaveAllSprites();
    }

    private int ChangeSpriteImage(GameObject imgToChange, int currentSprite, int next, List<Sprite> sprites)
    {
        int nextSprite = currentSprite + next;
        int lastSpriteIndex = sprites.Count - 1;

        if (nextSprite < 0)
            nextSprite = imgToChange.Equals(BirdImage) ? lastSpriteIndex - 2 : lastSpriteIndex;
        else if (nextSprite > lastSpriteIndex) nextSprite = 0;

        imgToChange.GetComponent<Image>().sprite = sprites[nextSprite];

        return nextSprite;
    }

    private void SaveAllSprites()
    {
        var list = _birdSprites.GetRange(CurrentBirdSprite, 3);
        list.Add(_hatSprites[CurrentHatSprite]);
        list.Add(_backgroundSprites[CurrentBackgroundSprite]);
        list.Add(_pipeSprites[CurrentPipeSprite]);
        GameSprites = list;
    }
}