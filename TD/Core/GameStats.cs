using SharpDX.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TD.Factory;

namespace TD.Core
{
    class GameStats
    {
        /** Фабрики */
        private PlayersFactory PlayersFactory;
        private MobsFactory MobsFactory;
        private BuildingsFactory BuildingsFactory;

        /** Провольстве */
        private int _money;
        private int _woods;

        private double _lastUpdateMoney;
        private double _lastUpdateWood;
        private double _timeUpMoney;
        private double _timeUpWood;

        /** Гетеры */
        public int PlayersCount { get { return PlayersFactory.FactorySize; } }
        public int BuildingsCount { get { return BuildingsFactory.FactorySize; } }
        public int MobsCount { get { return MobsFactory.FactorySize; } }
        public int Money { get { return _money; } set { _money = value; } }
        public int Woods { get { return _woods; } set { _woods = value; } }

        public GameStats(PlayersFactory PlayersFactory, BuildingsFactory BuildingsFactory, MobsFactory MobsFactory)
        {
            this.PlayersFactory = PlayersFactory;
            this.BuildingsFactory = BuildingsFactory;
            this.MobsFactory = MobsFactory;

            _timeUpMoney = 3;
            _timeUpWood = 2;
        }

        public void Update(DemoTime time)
        {
            if (time.ElapseTime - _lastUpdateMoney > _timeUpMoney)
            {
                _lastUpdateMoney = time.ElapseTime;
                _money++;
            }
            if (time.ElapseTime - _lastUpdateWood > _timeUpWood)
            {
                _lastUpdateWood = time.ElapseTime;
                _woods++;
            }
        }

        public bool CheckResources(int money, int woods)
        {
            if (Money > money && Woods > woods)
                return true;
             return false;
        }
    }
}
