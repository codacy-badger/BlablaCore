using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BlablaCore.Core.Cryptography
{
    public class Client
    {
        public double Pid { get; private set; }

        public IPEndPoint EndPoint { get; private set; }

        public bool Disconected { get; private set; }

        internal Socket sck;
        public int inCmpt = 13;
        public int outCmpt = 12;

        public SocketMessage inBuffer;

        public Client(Socket accepted)
        {
            sck = accepted;
            EndPoint = (IPEndPoint)sck.RemoteEndPoint;
            sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            Disconected = false;
        }

        void callback(IAsyncResult AR)
        {
            try
            {
                if (!Disconected)
                {
                    sck.EndReceive(AR);

                    byte[] buf = new byte[8192];

                    int rec = sck.Receive(buf, buf.Length, 0);

                    if (rec < buf.Length)
                    {
                        Array.Resize<byte>(ref buf, rec);
                    }

                    if (Received != null)
                    {
                        string data = Encoding.Default.GetString(buf);
                        if (data.Length > 1)
                        {
                            if (Encoding.Default.GetString(buf).Substring(0, 1) == "<")
                            {
                                this.send("<?xml version=\"1.0\"?><cross-domain-policy><allow-access-from domain=\"*\" to-ports=\"*\" /></cross-domain-policy>\0");
                                this.Disconnect();
                                return;
                            }
                            Received(this, buf);
                        }
                        else
                        {
                            Close();
                        }
                    }


                    sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
                }
                else
                {
                    CloseSocket(AR);
                    if (Disconnected != null)
                    {
                        Disconnected(this);
                    }
                }
            }
            catch (Exception ex)
            {
                Close();
                Console.Write(ex.ToString());
                if (Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }


        public void send(string msg)
        {
            sck.BeginSend(System.Text.Encoding.Default.GetBytes(msg.ToCharArray()), 0, System.Text.Encoding.Default.GetBytes(msg.ToCharArray()).Length, SocketFlags.None, new AsyncCallback(SendCallback), sck);
        }

        public void sendMessage(SocketMessage msg)
        {
            SocketMessage message = new SocketMessage();
            this.outCmpt++;
            if (this.outCmpt >= 65530)
            {
                this.outCmpt = 12;
            }
            SocketMessage _loc_3 = new SocketMessage();
            _loc_3.bitWriteUnsignedInt(16, this.outCmpt);
            byte[] _loc_4 = _loc_3.exportMessage();
            message.writeBytes(_loc_4, 0, _loc_4.Length);
            _loc_4 = msg.exportMessage();
            message.writeBytes(_loc_4, 0, _loc_4.Length);
            message.Add(0);
            byte[] byteArray = message.toByteArray();
            sck.BeginSend(byteArray, 0, byteArray.Length, SocketFlags.None, new AsyncCallback(SendCallback), sck);
        }

        internal void SendCallback(IAsyncResult asyncResult)
        {
            int send = sck.EndSend(asyncResult);
        }

        public void Close()
        {
            /*sck.Close();
            sck.Dispose();
            GC.Collect();*/
            Disconected = true;
        }

        private void CloseSocket(IAsyncResult AR)
        {
            sck.EndSend(AR);
            sck.Close();
            sck.Dispose();
            GC.Collect();
        }

        public void Disconnect() => Close();

        public void update_id(double id) => this.Pid = id;

        public delegate void ClientReceiveHandler(Client sender, byte[] buf);
        public delegate void ClientDisconnecteHandler(Client sender);

        public event ClientReceiveHandler Received;
        public event ClientDisconnecteHandler Disconnected;
    }
}
