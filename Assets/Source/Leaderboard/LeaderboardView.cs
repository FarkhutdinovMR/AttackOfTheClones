#pragma warning disable

using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private PlayerRankingView _playerRankingViewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private int MaxAmount = 9;
    [SerializeField] private PlayerRankingView _playerRank;

    public const string LeaderboardName = "Leaderboard";

    public void Show()
    {
        ShowPlayerScore();
        CreateLeaderboardList();
    }

    public void OnRankButtonClick()
    {
        gameObject.SetActive(true);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }

    private void ShowPlayerScore()
    {
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result != null)
            {
                string name = result.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                _playerRank.Init(result.rank + "." + name, result.score.ToString());
                _playerRank.Render();
                _playerRank.gameObject.SetActive(true);
            }
        });
    }

    private void CreateLeaderboardList()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            int rankingCount = 0;
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                PlayerRankingView newPlayerRankingView = Instantiate(_playerRankingViewTemplate, _container);
                newPlayerRankingView.Init(entry.rank + "." + name, entry.score.ToString());
                newPlayerRankingView.Render();

                rankingCount++;
                if (rankingCount >= MaxAmount)
                    break;
            }
        });
    }
}