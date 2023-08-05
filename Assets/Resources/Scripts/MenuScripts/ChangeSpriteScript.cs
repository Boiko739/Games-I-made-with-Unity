using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteScript : MonoBehaviour
{
    [field:SerializeField]
    public int CurrentBirdSprite { get; private set; } = 0;
    [field:SerializeField]
    public int CurrentHatSprite { get; private set; } = 0;
    [field:SerializeField]
    public int CurrentBackgroundSprite { get; private set; } = 0;
    [field:SerializeField]
    public int CurrentPipeSprite { get; private set; } = 0;

    public GameObject Bird, Hat, Background, Pipe;

    [SerializeField]
    private List<Sprite> _birdSprites, _hatSprites, _backgroundSprites, _pipeSprites;

    private void Start()
    {
        _birdSprites = Resources.LoadAll<Sprite>($"Sprites/Birds").ToList();
        _hatSprites = Resources.LoadAll<Sprite>("Sprites/Hats").ToList();
        _backgroundSprites = Resources.LoadAll<Sprite>("Sprites/Backgrounds").ToList();
        _pipeSprites = Resources.LoadAll<Sprite>("Sprites/Pipes").ToList();
    }

    public void SetByDefault()
    {
        CurrentBirdSprite = ChangeSprite(Bird, 0, 0, _birdSprites);
        CurrentHatSprite = ChangeSprite(Hat, 0, 0, _hatSprites);
        CurrentBackgroundSprite = ChangeSprite(Background, 0, 0, _backgroundSprites);
        CurrentPipeSprite = ChangeSprite(Pipe, 0, 0, _pipeSprites);
    }

    public void ChangeBirdSprite(int next)
    {
        CurrentBirdSprite = ChangeSprite(Bird, CurrentBirdSprite, next * 3, _birdSprites);
    }

    public void ChangeHatSprite(int next)
    {
        CurrentHatSprite = ChangeSprite(Hat, CurrentHatSprite, next, _hatSprites);
    }

    public void ChangeBackgroundSprite(int next)
    {
        CurrentBackgroundSprite = ChangeSprite(Background, CurrentBackgroundSprite, next, _backgroundSprites);
    }

    public void ChangePipeSprite(int next)
    {
        CurrentPipeSprite = ChangeSprite(Pipe, CurrentPipeSprite, next, _pipeSprites);
    }

    private int ChangeSprite(GameObject objToChange, int currentSprite, int next, List<Sprite> sprites)
    {
        int nextSprite = currentSprite + next;
        int lastSpriteIndex = sprites.Count - 1;

        if (nextSprite < 0)
        {
            if (objToChange.Equals(Bird))
                nextSprite = lastSpriteIndex - 2;
            else
                nextSprite = lastSpriteIndex;
        }
        else if (nextSprite > lastSpriteIndex) nextSprite = 0;

        objToChange.GetComponent<Image>().sprite = sprites[nextSprite];

        return nextSprite;
    }

    public List<Sprite> GetAllSprites()
    {
        var list = _birdSprites.GetRange(CurrentBirdSprite, 3);
        list.Add(_hatSprites[CurrentHatSprite]);
        list.Add(_backgroundSprites[CurrentBackgroundSprite]);
        list.Add(_pipeSprites[CurrentPipeSprite]);
        return list;
    }
}