using System;
using System.Collections.Generic;

namespace Blablaland_Server.Net
{
    class Binary : List<byte>
    {
        public double bitLength;
        public double bitPosition;
        public static List<double> powList;
        public static bool __init = _init();

        public Binary()
        {
            this.bitLength = 0;
            this.bitPosition = 0;
            return;
        }

        public string bitReadString()
        {
            double _loc_4 = 0;
            string _loc_1 = "";
            double _loc_2 = this.bitReadUnsignedInt(16);
            int _loc_3 = 0;
            while (_loc_3 < _loc_2)
            {

                _loc_4 = this.bitReadUnsignedInt(8);
                if (_loc_4 == 255)
                {
                    _loc_4 = 8364;
                }
                _loc_1 = _loc_1 + unchecked((char)(byte)(_loc_4));
                _loc_3 = _loc_3 + 1;
            }
            return _loc_1;
        }

        public bool bitReadBoolean()
        {
            if (this.bitPosition == this.bitLength)
            {
                return false;
            }
            int _loc_1 = (int)Math.Floor((double)(this.bitPosition / 8));
            double _loc_2 = this.bitPosition % 8;
            Binary _loc_3 = this;
            double _loc_4 = this.bitPosition + 1;
            _loc_3.bitPosition = _loc_4;
            return ((int)this[_loc_1] >> (int)(7 - _loc_2) & 1) == 1;
        }

        public double bitReadUnsignedInt(double param1)
        {
            double _loc_4 = 0;
            double _loc_5 = 0;
            double _loc_6 = 0;
            double _loc_7 = 0;
            double _loc_8 = 0;
            if (this.bitPosition + param1 > this.bitLength)
            {
                this.bitPosition = this.bitLength;
                return 0;
            }
            double _loc_2 = 0;
            double _loc_3 = param1;
            while (_loc_3 > 0)
            {

                _loc_4 = (double)Math.Floor((double)(this.bitPosition / 8));
                _loc_5 = (double)(this.bitPosition % 8);
                _loc_6 = 8 - _loc_5;
                _loc_7 = Math.Min(_loc_6, _loc_3);
                _loc_8 = (double)((int)this[(int)_loc_4] >> (int)(_loc_6 - _loc_7) & (int)(powList[(int)_loc_7] - 1));
                _loc_2 = _loc_2 + _loc_8 * (double)powList[(int)(_loc_3 - _loc_7)];
                _loc_3 = _loc_3 - _loc_7;
                this.bitPosition = this.bitPosition + _loc_7;
            }
            return _loc_2;
        }

        public double bitReadSignedInt(double param1)
        {
            bool _loc_2 = this.bitReadBoolean();
            return this.bitReadUnsignedInt((double)((param1 - 1) * (_loc_2 ? (1) : (-1))));
        }

        public Binary bitReadBinaryData()
        {
            double _loc_1 = this.bitReadUnsignedInt(16);
            return this.bitReadBinary(_loc_1);
        }

        public Binary bitReadBinary(double param1)
        {
            double _loc_5 = 0;
            Binary _loc_2 = new Binary();
            double _loc_3 = this.bitPosition;
            while (this.bitPosition - _loc_3 < param1)
            {

                if (this.bitPosition == this.bitLength)
                {
                    return _loc_2;
                }
                _loc_5 = Math.Min(8, param1 - this.bitPosition + _loc_3);
                _loc_2.bitWriteUnsignedInt(_loc_5, this.bitReadUnsignedInt(_loc_5));
            }
            return _loc_2;
        }

        public void bitWriteString(string param1)
        {
            double _loc_4 = 0;
            double _loc_2 = (double)Math.Min(param1.Length, (powList[16] - 1));
            this.bitWriteUnsignedInt(16, _loc_2);
            double _loc_3 = 0;
            while (_loc_3 < _loc_2)
            {

                _loc_4 = unchecked((double)(param1[(int)_loc_3]));
                if (_loc_4 == 8364)
                {
                    _loc_4 = 255;
                }
                this.bitWriteUnsignedInt(8, _loc_4);
                _loc_3 = _loc_3 + 1;
            }
            return;
        }

        public void bitWriteSignedInt(double param1, double param2)
        {
            this.bitWriteBoolean(param2 >= 0);
            this.bitWriteUnsignedInt((param1 - 1), Math.Abs(param2));
            return;
        }

        public void bitWriteUnsignedInt(double param1, double param2)
        {
            double _loc_4 = 0;
            double _loc_5 = 0;
            double _loc_6 = 0;
            double _loc_7 = 0;
            param2 = (double)Math.Min((powList[(int)param1] - 1), param2);
            double _loc_3 = param1;
            while (_loc_3 > 0)
            {

                _loc_4 = this.bitLength % 8;
                if (_loc_4 == 0)
                {
                    Add(Convert.ToByte(false));
                }
                _loc_5 = 8 - _loc_4;
                _loc_6 = Math.Min(_loc_5, _loc_3);
                _loc_7 = this.Rshift(param2, (int)(_loc_3 - _loc_6));
                this[(this.Count - 1)] = unchecked((byte)(this[(this.Count - 1)] + _loc_7 * powList[(int)(_loc_5 - _loc_6)]));
                param2 = param2 - _loc_7 * powList[(int)(_loc_3 - _loc_6)];
                _loc_3 = _loc_3 - _loc_6;
                this.bitLength = this.bitLength + _loc_6;
            }
            return;
        }

        public void bitWriteBoolean(bool param1)
        {
            double _loc_2 = this.bitLength % 8;
            if (_loc_2 == 0)
            {
                Add(Convert.ToByte(false));
            }
            if (param1)
            {
                this[(this.Count - 1)] = unchecked((byte)(this[(this.Count - 1)] + powList[(int)(7 - _loc_2)]));
            }
            Binary _loc_3 = this;
            double _loc_4 = this.bitLength + 1;
            _loc_3.bitLength = _loc_4;
            return;
        }

        public void bitWriteBinaryData(Binary param1)
        {
            double _loc_2 = Math.Min(param1.bitLength, (powList[16] - 1));
            this.bitWriteUnsignedInt(16, _loc_2);
            this.bitWriteBinary(param1);
            return;
        }

        public void bitWriteBinary(Binary param1)
        {
            double _loc_3 = 0;
            double _loc_4 = 0;
            param1.bitPosition = 0;
            double _loc_2 = param1.bitLength;
            while (_loc_2 > 0)
            {

                _loc_3 = Math.Min(8, _loc_2);
                _loc_4 = param1.bitReadUnsignedInt(_loc_3);
                this.bitWriteUnsignedInt(_loc_3, _loc_4);
                _loc_2 = _loc_2 - _loc_3;
            }
            return;
        }

        public void bitCopyObject(Binary param1)
        {
            this.bitPosition = param1.bitPosition;
            this.bitLength = param1.bitLength;
            double _loc_2 = 0;
            while (_loc_2 < param1.Count)
            {

                Add(param1[(int)_loc_2]);
                _loc_2++;
            }
            return;
        }

        public double Rshift(double param1, int param2)
        {
            return Math.Floor(param1 / powList[param2]);
        }

        public double Lshift(double param1, int param2)
        {
            return param1 * powList[param2];
        }

        public void writeBytes(byte[] byteArray, int startIndex, int endIndex)
        {
            int index = startIndex;
            while (index < endIndex)
            {
                this.Add(byteArray[index]);
                index++;
            }
        }

        public static bool _init()
        {
            powList = new List<double>();
            int _loc_1 = 0;
            while (_loc_1 <= 32)
            {

                powList.Add(Math.Pow(2, _loc_1));
                _loc_1 = _loc_1 + 1;
            }
            return true;
        }

        public byte[] toByteArray()
        {
            byte[] array = new byte[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = unchecked(this[i]);
            }
            return array;
        }
    }
}
