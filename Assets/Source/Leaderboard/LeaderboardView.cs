#pragma warning disable

using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private PlayerRankingView _playerRankingViewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private int _topPlayersCount = 5;
    [SerializeField] private int _competingPlayersCount = 4;
    [SerializeField] private PlayerRankingView _playerRank;
    [SerializeField] private Character _character;
    [SerializeField] private GameObject _vkGamesLeaderboardButton;

    public const string LeaderboardName = "Leaderboard";
    public const string Anonymous = "Anonymous";

    public void Show()
    {
#if YANDEX_GAMES
        ShowPlayerScore();
        CreateLeaderboardList();
#endif
#if VK_GAMES
        _vkGamesLeaderboardButton.SetActive(true);
#endif
    }

    public void OnRankButtonClick()
    {
#if YANDEX_GAMES
        gameObject.SetActive(true);
#endif
#if VK_GAMES
        Agava.VKGames.Leaderboard.ShowLeaderboard((int)_character.Score.Value);
#endif
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
                    name = Anonymous;

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
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = Anonymous;
                PlayerRankingView newPlayerRankingView = Instantiate(_playerRankingViewTemplate, _container);
                newPlayerRankingView.Init(entry.rank + "." + name, entry.score.ToString());
                newPlayerRankingView.Render();
            }
        }, null, _topPlayersCount, _competingPlayersCount);
    }
}