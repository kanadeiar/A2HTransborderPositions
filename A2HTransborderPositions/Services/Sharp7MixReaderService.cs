using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using A2HTransborderPositions.Interfaces;
using Sharp7;

namespace A2HTransborderPositions.Services
{
    public class Sharp7MixReaderService : IMixReaderService
    {
        private readonly S7Client _client;
        private readonly string _address;
        private byte[] _bufferDb;

        public Sharp7MixReaderService(string address = "10.0.57.10")
        {
            _address = address;
            _client = new S7Client { ConnTimeout = 5_000, RecvTimeout = 5_000 };
            _bufferDb = new byte[455];
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
        /// <summary> Получение данных текущих позиций </summary>
        public bool GetCurrentPositions(out int error, int[] values)
        {
            var result = true;
            error = 0;

            var r = _client.ConnectTo(_address, 0, 2);
            if (r == 0)
            {
                byte[] buffer = new byte[4];
                var r0 = _client.DBRead(2400, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[0] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(102, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[1] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(103, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[2] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(104, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[3] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(105, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[4] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(106, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[5] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(108, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[6] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(2210, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[7] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(110, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[8] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(2200, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[9] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(112, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[10] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(114, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[11] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(115, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[12] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(116, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[13] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(2300, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[14] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(118, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[15] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(119, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[16] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(120, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[17] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(121, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[18] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(122, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[19] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(123, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[20] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(124, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[21] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(125, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[22] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(126, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[23] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(127, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[24] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(128, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[25] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(129, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[26] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(130, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[27] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(131, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[28] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(132, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[29] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(133, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[30] = _bufferDb.GetIntAt(0);
                r0 = _client.DBRead(134, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r}");
                values[31] = _bufferDb.GetIntAt(0);

            }
            else
            {
                error = r;
                result = false;
            }
            _client.Disconnect();
            return result;
        }
    }
}
