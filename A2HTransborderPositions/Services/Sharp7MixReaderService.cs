using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2HTransborderPositions.Interfaces;
using Sharp7;

namespace A2HTransborderPositions.Services
{
    public class Sharp7MixReaderService : IMixReaderService
    {
        private readonly S7Client _client;
        private readonly string _address;

        public Sharp7MixReaderService(string address = "10.0.57.10")
        {
            _address = address;
            _client = new S7Client { ConnTimeout = 5_000, RecvTimeout = 5_000 };
        }
        /// <summary> Тест соединения с контроллером </summary>
        public bool TestConnection(out int error)
        {
            var result = _client.ConnectTo(_address, 0, 2);
            _client.Disconnect();
            if (result != 0)
            {
                error = result;
                return false;
            }
            error = 0;
            return true;
        }
    }
}
