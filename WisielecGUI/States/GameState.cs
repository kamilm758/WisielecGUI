﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisielecGUI.States
{
    class GameState
    {
        public static bool _IsShowMainMenuScene;
        public static bool _IsShowGameScene;
        public static bool _IsRankingScene;
        public static bool IsShowMainMenuScene { get { return _IsShowMainMenuScene; } set { if (value == true) _IsShowGameScene = false;_IsRankingScene = false; _IsShowMainMenuScene = value; } }
        public static bool IsShowGameScene { get { return _IsShowGameScene; } set { if (value == true) _IsShowMainMenuScene = false; _IsRankingScene=false ; _IsShowGameScene = value; } }
        public static bool IsRankingScene { get { return _IsRankingScene; } set { if (value == true) _IsShowMainMenuScene = false; _IsShowGameScene = false; _IsRankingScene = value; } }
    }
}
