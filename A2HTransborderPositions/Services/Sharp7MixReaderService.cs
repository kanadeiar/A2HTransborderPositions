﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                #region Получение данных с контроллера
                GetValueFromPLC(values, 2400, 0);
                GetValueFromPLC(values, 102, 1);
                GetValueFromPLC(values, 103, 2);
                GetValueFromPLC(values, 104, 3);
                GetValueFromPLC(values, 105, 4);
                GetValueFromPLC(values, 106, 5);
                GetValueFromPLC(values, 108, 6);
                GetValueFromPLC(values, 2210, 7);
                GetValueFromPLC(values, 110, 8);
                GetValueFromPLC(values, 2200, 9);
                GetValueFromPLC(values, 112, 10);
                GetValueFromPLC(values, 114, 11);
                GetValueFromPLC(values, 115, 12);
                GetValueFromPLC(values, 116, 13);
                GetValueFromPLC(values, 2300, 14);
                GetValueFromPLC(values, 118, 15);
                GetValueFromPLC(values, 119, 16);
                GetValueFromPLC(values, 120, 17);
                GetValueFromPLC(values, 121, 18);
                GetValueFromPLC(values, 122, 19);
                GetValueFromPLC(values, 123, 20);
                GetValueFromPLC(values, 124, 21);
                GetValueFromPLC(values, 125, 22);
                GetValueFromPLC(values, 126, 23);
                GetValueFromPLC(values, 127, 24);
                GetValueFromPLC(values, 128, 25);
                GetValueFromPLC(values, 129, 26);
                GetValueFromPLC(values, 130, 27);
                GetValueFromPLC(values, 131, 28);
                GetValueFromPLC(values, 132, 29);
                GetValueFromPLC(values, 133, 30);
                GetValueFromPLC(values, 134, 31);
                #endregion
            }
            else
            {
                error = r;
                result = false;
            }
            _client.Disconnect();
            return result;

            void GetValueFromPLC(int[] values, int numbBlock, int ind)
            {
                var r0 = _client.DBRead(numbBlock, 14, 4, _bufferDb);
                if (r0 != 0) throw new ApplicationException($"Ошибка обновления данных внутреннего буфера.\nНе удалось прочитать блок данных  с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r0}");
                values[ind] = _bufferDb.GetIntAt(0);
            }
        }

        /// <summary> Получение текущих актуальных данных парома </summary>
        /// <param name="error">Ошибка</param> <param name="position">Поцизия по энкодеру</param> <param name="target">Номер цель место</param>
        /// <param name="left">Фиксатор слева включен</param> <param name="right">Фиксатор справа включен</param> <returns>Данные успешно получены</returns>
        public bool GetActualValues(out int error, out int position, out int target, out int left, out int right)
        {
            position = target = 0;
            left = right = 0;
            var r = _client.ConnectTo(_address, 0, 2);
            if (r != 0)
            {
                error = r;
                return false;
            }
            byte[] buffer = new byte[4];
            var r0 = _client.DBRead(60, 130, 4, buffer);
            if (r0 != 0) throw new ApplicationException($"Ошибка получения данных.\nНе удалось прочитать данные с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r0}");
            position = buffer.GetDIntAt(0);

            r0 = _client.DBRead(201, 100, 4, buffer);
            if (r0 != 0) throw new ApplicationException($"Ошибка получения данных.\nНе удалось прочитать данные с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r0}");
            target = buffer.GetIntAt(0);

            r0 = _client.ReadArea(S7Area.MK, 0, 1162, 1, S7WordLength.Byte, buffer);
            if (r0 != 0) throw new ApplicationException($"Ошибка получения данных.\nНе удалось прочитать данные с контроллера с адресом {_client.PLCIpAddress}, ошибка = {r0}");
            var bits = new BitArray(buffer);
            var leftClose = bits[1];
            var leftOpen = bits[2];
            var rightClose = bits[4]; 
            var rightOpen = bits[5]; 
            
            if (leftOpen)
                left = 1;
            else if (leftClose)
                left = 2;
            else
                left = 0;
            if (rightOpen)
                right = 1;
            else if (rightClose)
                right = 2;
            else
                right = 0;

            error = 0;
            return true;
        }
    }
}
