// ,-----.  ,--.        ,--.   ,--.         ,-----.                     
// |  |) /_ |  | ,--,--.|  |-. |  | ,--,--.'  .--./ ,---. ,--.--. ,---. 
// |  .-.  \|  |' ,-.  || .-. '|  |' ,-.  ||  |    | .-. ||  .--'| .-. :
// |  '--' /|  |\ '-'  || `-' ||  |\ '-'  |'  '--'\' '-' '|  |   \   --.
// `------' `--' `--`--' `---' `--' `--`--' `-----' `---' `--'    `----'
// 
// Copyright (C) 2020 - BlablaCore
// 
// NosCore is a free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace BlablaCore.Core.Cryptography
{
    public class Binary : List<byte>
    {
        protected double BitLength;
        protected double BitPosition;
        private static List<double> _powList;
        public static bool Init = _init();

        protected Binary()
        {
            BitLength = 0;
            BitPosition = 0;
        }

        public string BitReadString()
        {
            double loc4 = 0;
            string loc1 = "";
            double loc2 = BitReadUnsignedInt(16);
            int loc3 = 0;
            while (loc3 < loc2)
            {

                loc4 = BitReadUnsignedInt(8);
                if (loc4 == 255)
                {
                    loc4 = 8364;
                }
                loc1 = loc1 + unchecked((char)(byte)(loc4));
                loc3 = loc3 + 1;
            }
            return loc1;
        }

        private bool BitReadBoolean()
        {
            if (this.BitPosition == this.BitLength)
            {
                return false;
            }
            int loc1 = (int)Math.Floor((double)(BitPosition / 8));
            double loc2 = BitPosition % 8;
            Binary loc3 = this;
            double loc4 = BitPosition + 1;
            loc3.BitPosition = loc4;
            return ((int)this[loc1] >> (int)(7 - loc2) & 1) == 1;
        }

        private double BitReadUnsignedInt(double param1)
        {
            double loc4 = 0;
            double loc5 = 0;
            double loc6 = 0;
            double loc7 = 0;
            double loc8 = 0;
            if (BitPosition + param1 > BitLength)
            {
                BitPosition = BitLength;
                return 0;
            }
            double loc2 = 0;
            double loc3 = param1;
            while (loc3 > 0)
            {

                loc4 = (double)Math.Floor((double)(BitPosition / 8));
                loc5 = (double)(BitPosition % 8);
                loc6 = 8 - loc5;
                loc7 = Math.Min(loc6, loc3);
                loc8 = (double)((int)this[(int)loc4] >> (int)(loc6 - loc7) & (int)(_powList[(int)loc7] - 1));
                loc2 = loc2 + loc8 * (double)_powList[(int)(loc3 - loc7)];
                loc3 = loc3 - loc7; 
                BitPosition = BitPosition + loc7;
            }
            return loc2;
        }

        public double BitReadSignedInt(double param1)
        {
            bool loc2 = BitReadBoolean();
            return BitReadUnsignedInt((double)((param1 - 1) * (loc2 ? (1) : (-1))));
        }

        public Binary BitReadBinaryData()
        {
            double loc1 = BitReadUnsignedInt(16);
            return BitReadBinary(loc1);
        }

        private Binary BitReadBinary(double param1)
        {
            double loc5 = 0;
            Binary loc2 = new Binary();
            double loc3 = BitPosition;
            while (BitPosition - loc3 < param1)
            {

                if (BitPosition == BitLength)
                {
                    return loc2;
                }
                loc5 = Math.Min(8, param1 - BitPosition + loc3);
                loc2.BitWriteUnsignedInt(loc5, BitReadUnsignedInt(loc5));
            }
            return loc2;
        }

        public void BitWriteString(string param1)
        {
            double loc4 = 0;
            double loc2 = (double)Math.Min(param1.Length, (_powList[16] - 1));
            BitWriteUnsignedInt(16, loc2);
            double loc3 = 0;
            while (loc3 < loc2)
            {
                loc4 = unchecked((double)(param1[(int)loc3]));
                if (loc4 == 8364)
                {
                    loc4 = 255;
                }
                BitWriteUnsignedInt(8, loc4);
                loc3 = loc3 + 1;
            }
        }

        public void BitWriteSignedInt(double param1, double param2)
        {
            BitWriteBoolean(param2 >= 0);
            BitWriteUnsignedInt((param1 - 1), Math.Abs(param2));
        }

        public void BitWriteUnsignedInt(double param1, double param2)
        {
            double loc4 = 0;
            double loc5 = 0;
            double loc6 = 0;
            double loc7 = 0;
            param2 = (double)Math.Min((_powList[(int)param1] - 1), param2);
            double loc3 = param1;
            while (loc3 > 0)
            {
                loc4 = this.BitLength % 8;
                if (loc4 == 0)
                {
                    Add(Convert.ToByte(false));
                }
                loc5 = 8 - loc4;
                loc6 = Math.Min(loc5, loc3);
                loc7 = Rshift(param2, (int)(loc3 - loc6));
                this[(Count - 1)] = unchecked((byte)(this[(Count - 1)] + loc7 * _powList[(int)(loc5 - loc6)]));
                param2 = param2 - loc7 * _powList[(int)(loc3 - loc6)];
                loc3 = loc3 - loc6;
                BitLength = BitLength + loc6;
            }
        }

        public void BitWriteBoolean(bool param1)
        {
            double loc2 = BitLength % 8;
            if (loc2 == 0)
            {
                Add(Convert.ToByte(false));
            }
            if (param1)
            {
                this[(Count - 1)] = unchecked((byte)(this[(Count - 1)] + _powList[(int)(7 - loc2)]));
            }
            Binary loc3 = this;
            double loc4 = BitLength + 1;
            loc3.BitLength = loc4;
        }

        public void BitWriteBinaryData(Binary param1)
        {
            double loc2 = Math.Min(param1.BitLength, (_powList[16] - 1));
            BitWriteUnsignedInt(16, loc2);
            BitWriteBinary(param1);
        }

        public void BitWriteBinary(Binary param1)
        {
            double loc3 = 0;
            double loc4 = 0;
            param1.BitPosition = 0;
            double loc2 = param1.BitLength;
            while (loc2 > 0)
            {

                loc3 = Math.Min(8, loc2);
                loc4 = param1.BitReadUnsignedInt(loc3);
                BitWriteUnsignedInt(loc3, loc4);
                loc2 = loc2 - loc3;
            }
        }

        public void BitCopyObject(Binary param1)
        {
            BitPosition = param1.BitPosition;
            BitLength = param1.BitLength;
            double loc2 = 0;
            while (loc2 < param1.Count)
            {
                Add(param1[(int)loc2]);
                loc2++;
            }
        }

        public double Rshift(double param1, int param2)
        {
            return Math.Floor(param1 / _powList[param2]);
        }

        public double Lshift(double param1, int param2)
        {
            return param1 * _powList[param2];
        }

        public void WriteBytes(byte[] byteArray, int startIndex, int endIndex)
        {
            int index = startIndex;
            while (index < endIndex)
            {
                Add(byteArray[index]);
                index++;
            }
        }

        public static bool _init()
        {
            _powList = new List<double>();
            int loc1 = 0;
            while (loc1 <= 32)
            {
                _powList.Add(Math.Pow(2, loc1));
                loc1 = loc1 + 1;
            }
            return true;
        }

        public byte[] ToByteArray()
        {
            byte[] array = new byte[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = unchecked(this[i]);
            }
            return array;
        }
    }
}
