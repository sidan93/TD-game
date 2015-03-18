using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using InternetManager;


namespace TD.Internet
{
    public static class Connector
    {
        static ServerInput server;
        static bool _isConnect;
        static public bool isConnect 
        {
            get
            {
                return _isConnect;
            }
        }

        static Connector()
        {
            _isConnect = false;
            server = new ServerInput();
        }

        static public void ConnectWithServer()
        {
            server.DoWord();
        }
    }
}
