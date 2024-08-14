using CodeBase.Configs.Level;
using CodeBase.Configs.WindowsConfig;
using CodeBase.Services;
using CodeBase.UI.MainUI;

namespace CodeBase.Configs.Interface
{
    public interface IConfigsProvider : IService
    {
        void Load();
        //EnemyConfig GetEnemyConfig(EnemyId id);

        public LevelConfig GetLevelConfig(string name);
        public LevelConfig GetLevelConfig(int index);
        int LevelAmount { get; }

        public WindowConfig GetWindowConfig(WindowMainUIId id);
    }
}