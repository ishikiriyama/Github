namespace Projects.Core.DomainEvents
{
    /// <summary>
    /// レベルが生成された瞬間の通知イベント
    /// </summary>
    public readonly struct OnLevelGeneratedEvent
    {
        // 生成されたレベルID
        public int LevelId { get; }

        public OnLevelGeneratedEvent(int levelId)
        {
            LevelId = levelId;
        }
    }

    /// <summary>
    /// レベル失敗（ゲームオーバー）時に発行されるイベント
    /// </summary>
    public readonly struct OnLevelFailedEvent
    {
        // 失敗レベルID
        public int LevelId { get; }

        public OnLevelFailedEvent(int levelId)
        {
            LevelId = levelId;
        }
    }

    /// <summary>
    /// レベルクリア時に発行されるイベント
    /// </summary>
    public readonly struct OnLevelClearedEvent
    {
        // クリアレベルID
        public int LevelId { get; }

        public OnLevelClearedEvent(int levelId)
        {
            LevelId = levelId;
        }
    }
}
