using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace InternetManager
{
    public class ServerInput
    {
        Server.SessionClient client;

        public void DoWord()
        {
            var context = new InstanceContext(new CallBack());
            client = new Server.SessionClient(context);
            client.Open();
            client.DoWork();
        }
    }

    public class CallBack : Server.ISessionCallback
    {
        public void MoveAllBullets(double[] x, double[] y, int[] id)
        {

        }
        public void MoveAllMobs(double[] x, double[] y, int[] id, double[] p)
        {

        }
        public void MoveAllHeroes(double[] x, double[] y, int[] id)
        {

        }
        public void MessageToMe(string a, string b)
        {

        }
        public void Print(string a)
        {

        }
    }
}
