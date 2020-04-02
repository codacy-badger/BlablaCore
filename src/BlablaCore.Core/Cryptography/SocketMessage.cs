// ,-----.  ,--.        ,--.   ,--.         ,-----.                     
// |  |) /_ |  | ,--,--.|  |-. |  | ,--,--.'  .--./ ,---. ,--.--. ,---. 
// |  .-.  \|  |' ,-.  || .-. '|  |' ,-.  ||  |    | .-. ||  .--'| .-. :
// |  '--' /|  |\ '-'  || `-' ||  |\ '-'  |'  '--'\' '-' '|  |   \   --.
// `------' `--' `--`--' `---' `--' `--`--' `-----' `---' `--'    `----'
// 
// Copyright (C) 2020 - BlablaCore
// 
// BlablaCore is a free software: you can redistribute it and/or modify
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

namespace BlablaCore.Core.Cryptography
{
    public class SocketMessage : Binary
    {
        public SocketMessage()
        {
            return;
        }

        public SocketMessage(byte[] buf)
        {
            for (int i = 0; i < buf.Length; i++)
            {
                Add(buf[i]);
            }
        }

        public SocketMessage Duplicate()
        {
            SocketMessage loc1 = new SocketMessage();
            loc1.WriteBytes(ToByteArray(), 0, Count);
            loc1.BitLength = BitLength;
            loc1.BitPosition = BitPosition;
            return loc1;
        }

        public void ReadMessage(byte[] param1)
        {
            int loc2 = 0;
            while (loc2 < param1.Length)
            {
                if (param1[loc2] == 1)
                {
                    loc2 = loc2 + 1;
                    Add((byte)(param1[loc2] == 2 ? (1) : (0)));
                }
                else
                {
                    Add(param1[loc2]);
                }
                loc2 = loc2 + 1;
            }
            BitLength = Count * 8;
        }

        public byte[] ExportMessage()
        {
            SocketMessage loc1 = new SocketMessage();
            int loc2 = 0;
            while (loc2 < Count)
            {
                
                if (this[loc2] == 0)
                {
                    loc1.Add(1);
                    loc1.Add(3);
                }
                else if (this[loc2] == 1)
                {
                    loc1.Add(1);
                    loc1.Add(2);
                }
                else
                {
                    loc1.Add(this[loc2]);
                }
                loc2 = loc2 + 1;
            }
            return loc1.ToByteArray();
        }
    }
}
