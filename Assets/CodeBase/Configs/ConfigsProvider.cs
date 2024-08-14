using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs.Interface;
using CodeBase.Configs.Level;
using CodeBase.Configs.WindowsConfig;
using CodeBase.UI.MainUI;
using UnityEngine;

namespace CodeBase.Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private const string EnemiesConfigsPath = "Configs/Enemies";
        private const string LevelsConfigsPath = "Configs/Levels";
        private const string WindowsConfigsPath = "Configs/Windows";
        
       // private Dictionary<EnemyId, EnemyConfig> _enemies;
        private Dictionary<string, LevelConfig> _levels;
        private Dictionary<WindowMainUIId, WindowConfig> _windows;
        
        private LevelConfig[] _levelsList;
        
        public int LevelAmount => _levelsList.Length;
        
        public void Load()
        {
            //_enemies = Resources.LoadAll<EnemyConfig>(EnemiesConfigsPath).ToDictionary(x => x.EnemyId, x => x);
            _windows = Resources.LoadAll<WindowConfig>(WindowsConfigsPath).ToDictionary(x => x.windowId, x => x);
            
            _levelsList = Resources.LoadAll<LevelConfig>(LevelsConfigsPath).ToArray();
            _levels = _levelsList.ToDictionary(x => x.LevelName, x => x);
        }
        
        /*public EnemyConfig GetEnemyConfig(EnemyId id)
        {
            return _enemies[id];
        }*/

        public LevelConfig GetLevelConfig(int index)
        {
            return _levelsList[index];
        }
        
        public LevelConfig GetLevelConfig(string name)
        {
            return _levels[name];
        }

        public WindowConfig GetWindowConfig(WindowMainUIId id)
        {
            return _windows[id];
        }
    }
}

