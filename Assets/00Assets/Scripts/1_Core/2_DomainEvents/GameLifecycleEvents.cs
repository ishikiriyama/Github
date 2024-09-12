using System;

namespace Projects.Core.DomainEvents
{

    /// <summary>
    /// ゲーム開始前の待機画面に入った時のイベント
    /// </summary>
    public readonly struct OnGameReadyEvent
    {
        // ゲームが初回起動時かどうかを示すbool値
        public bool IsFirstGame { get; }

        public OnGameReadyEvent(bool isFirstGame)
        {
            IsFirstGame = isFirstGame;
        }

    }

    /// <summary>
    /// ゲーム開始時に発行されるイベント
    /// </summary>
    public readonly struct OnGameStartEvent
    {
        // セッション時刻
        public DateTime SessionTime { get; }

        public OnGameStartEvent(DateTime sessionTime = default)
        {
            SessionTime = sessionTime;
        }
    }

    /// <summary>
    /// セーブ実行時に発行されるイベント
    /// </summary>
    public readonly struct OnSaveEvent
    {
        // セーブデータ
        public byte[] SaveData { get; }

        public OnSaveEvent(byte[] saveData)
        {
            SaveData = saveData;
        }
    }

}