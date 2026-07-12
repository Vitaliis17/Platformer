using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUnlocker : MonoBehaviour, ILevelUnlocker
{
    [SerializeField] private Button[] _buttons;

    private Dictionary<int, Button> _levelButtons;

    private void Awake()
    {
        FillLevelDictionary();
        LockAll();
    }

    public void UnlockLevels(int lastLevelIndex)
    {
        const bool UnlockValue = true;
        const int MinLevelIndex = 0;

        if (lastLevelIndex < MinLevelIndex)
            return;

        if (lastLevelIndex >= _levelButtons.Count)
            lastLevelIndex = _levelButtons.Count - 1;

        for (int i = 0; i < lastLevelIndex + 1; i++)
            _levelButtons[i].interactable = UnlockValue;
    }

    private void FillLevelDictionary()
    {
        _levelButtons = new();

        for (int i = 0; i < _buttons.Length; i++)
            _levelButtons.Add(i, _buttons[i]);
    }

    private void LockAll()
    {
        const bool LockValue = false;

        for (int i = 0; i < _levelButtons.Count; i++)
            _levelButtons[i].interactable = LockValue;
    }
}