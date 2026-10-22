using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class LevelUnlocker : IInitializable, ILevelUnlocker
{
    private Dictionary<int, Button> _levelButtons;

    public LevelUnlocker(Button[] buttons)
        => FillLevelDictionary(buttons);

    public void Initialize()
        => LockAll();

    public void UnlockLevels(int lastLevelIndex)
    {
        const bool UnlockValue = true;
        const int MinLevelIndex = 1;

        if (lastLevelIndex < MinLevelIndex)
            return;

        if (lastLevelIndex > _levelButtons.Count)
            lastLevelIndex = _levelButtons.Count;

        for (int i = 0; i < lastLevelIndex; i++)
            _levelButtons[i].interactable = UnlockValue;
    }

    private void FillLevelDictionary(Button[] buttons)
    {
        if (buttons == null)
            return;

        _levelButtons = new();

        for (int i = 0; i < buttons.Length; i++)
            _levelButtons.Add(i, buttons[i]);
    }

    private void LockAll()
    {
        const bool LockValue = false;

        for (int i = 0; i < _levelButtons.Count; i++)
            _levelButtons[i].interactable = LockValue;
    }
}