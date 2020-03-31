namespace BlablaCore.Core.Net
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
                this.Add(buf[i]);
            }
            return;
        }

        public SocketMessage duplicate()
        {
            SocketMessage _loc_1 = new SocketMessage();
            _loc_1.writeBytes(this.toByteArray(), 0, this.Count);
            _loc_1.bitLength = this.bitLength;
            _loc_1.bitPosition = this.bitPosition;
            return _loc_1;
        }

        public void readMessage(byte[] param1)
        {
            int _loc_2 = 0;
            while (_loc_2 < param1.Length)
            {
                if (param1[_loc_2] == 1)
                {
                    _loc_2 = _loc_2 + 1;
                    this.Add((byte)(param1[_loc_2] == 2 ? (1) : (0)));
                }
                else
                {
                    this.Add(param1[_loc_2]);
                }
                _loc_2 = _loc_2 + 1;
            }
            bitLength = Count * 8;
            return;
        }

        public byte[] exportMessage()
        {
            SocketMessage _loc_1 = new SocketMessage();
            int _loc_2 = 0;
            while (_loc_2 < this.Count)
            {
                
                if (this[_loc_2] == 0)
                {
                    _loc_1.Add(1);
                    _loc_1.Add(3);
                }
                else if (this[_loc_2] == 1)
                {
                    _loc_1.Add(1);
                    _loc_1.Add(2);
                }
                else
                {
                    _loc_1.Add(this[_loc_2]);
                }
                _loc_2 = _loc_2 + 1;
            }
            return _loc_1.toByteArray();
        }
    }
}
