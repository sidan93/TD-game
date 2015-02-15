using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TD.Core
{
    public class GameState
    {
        EGameState _gameState;      //  Текущая стадия игры
        public EGameState State
        {
            get
            {
                return _gameState;
            }
        }

        public GameState()
        {
            _gameState = EGameState.MainMenu;
        }
    }

    /// <summary>
    /// Все доступные стадии игры
    /// </summary>
    public enum EGameState
    {
        MainMenu,
        Game,
        GameMenu,
        MainSettings,
        GameSettings
    }
}
