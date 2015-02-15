using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TD.Internet
{
    public static class Connector
    {
        static bool _isConnect;
        static public bool isConnect 
        {
            get
            {
                return _isConnect;
            }
        }
        static Int32 Server;

        static Connector()
        {
            _isConnect = false;
        }

        static public void ConnectWithServer()
        {
        }
    }
}
