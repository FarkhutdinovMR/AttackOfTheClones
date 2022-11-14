using TMPro;
using UnityEngine;

public class PlayerRankingView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;

    private string _name;
    private string _score;

    public void Init(string name, string score)
    {
        _name = name;
        _score = score;
    }

    public void Render()
    {
        _nameText.SetText(_name);
        _scoreText.SetText(_score);
    }
}