using System;
using SharpServer.Buffers;
using SharpServer.Sockets;
using System.Collections.Generic;
using System.Net;
using System.Threading;
//using System.ComponentModel;
using System.Threading.Tasks;

namespace GMS_Server
{
    class mainProgram
    {
        /* alright lets get this out of the way
        0 - update entities
        1 - create object
        2 - destroy object
        3 - on player join
        4 - entity created
        5 - entity destroyed
        6 - ping
        7 - we got disconnect request
        8 - server wants updated input
        */

        //global game variables
        static int port = 5524, maxConnections = 8, alignment = 4;
        static uint timeout = 4096;
        static GameWorld gameWorld;
        static IPAddress address;
        static TcpServerHandler game_server;
        public static void Main()
        {
            //make the socketbinder
            SocketBinder binder = new SocketBinder();
            //lets make the server too
            //first we need to get the server options from the host user
            Console.WriteLine("Enter the IP your server runs on (leave an invalid ip for null):");
            string addr_ = Console.ReadLine();
            try
            {
                address = IPAddress.Parse(addr_);
            }
            catch
            {
                Console.WriteLine("Probably an invalid ip, using null");
                address = null;
            }
            while (true)
            {
                Console.WriteLine("Enter the port the server runs on:");
                string in_ = Console.ReadLine();
                if (Convert.ToInt32(in_).ToString() == in_)
                {
                    port = Convert.ToInt32(in_);
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("Enter the maximum amount of connections:");
                string in_ = Console.ReadLine();
                if (Convert.ToInt32(in_).ToString() == in_)
                {
                    maxConnections = Convert.ToInt32(in_);
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("Enter the maximum timeout for clients:");
                string in_ = Console.ReadLine();
                if (Convert.ToInt32(in_).ToString() == in_)
                {
                    timeout = Convert.ToUInt32(in_);
                    break;
                }
            }
            game_server = new TcpServerHandler(binder, port, maxConnections, alignment, timeout, address);
            //start the server
            game_server.Start();
            //now lets set the methods for event stuff
            game_server.StartedEvent += event_started;
            game_server.ClientCreatedEvent += event_clientCreated;
            game_server.ClosedEvent += event_closed;
            game_server.ReceivedEvent += event_received;
            game_server.ConnectedEvent += event_connected;
            game_server.DisconnectedEvent += event_disconnected;
            game_server.AttemptReconnectEvent += event_attemptReconnect;
            game_server.ClientOverflowEvent += event_clientOverflow;
            //now for the game world
            gameWorld = new GameWorld(game_server);
            
            //and now for the main game loop
            int sleepJump = 0;
            //int sleepNum = 0;
            //anything after here is not accessible until the server ends
            while (game_server.Status)
            {
                /*Thread.Sleep(1);
                if (sleepJump == 2 && sleepNum == 16)
                {
                    sleepJump = 0;
                    sleepNum = 0;
                    event_step();
                }
                else if(sleepNum != 2 && sleepNum == 17)
                {
                    sleepJump += 1;
                    sleepNum = 0;
                    event_step();
                }
                sleepNum += 1;
                */
                if(sleepJump == 2)
                {
                    sleepJump = 0;
                    event_step();
                    Thread.Sleep(16);
                }
                else
                {
                    sleepJump++;
                    event_step();
                    Thread.Sleep(17);
                }
                sleepJump++;
            }
            //whatever after the loop is triggered here when the server is disabled
            Thread.Sleep(1000);
               
        }
        
        private static void event_clientOverflow(TcpClientHandler client)
        {
            Console.WriteLine("Connection attempt from " + getIp(client).ToString() + ", socket " + client.Socket.ToString() + ", however the server is full");
        }

        private static void event_attemptReconnect(TcpClientHandler client)
        {
            Console.WriteLine("Client " + client.Socket.ToString() + "'s connection is unknown and unknowably unrecoverable (ip " + getIp(client).ToString() + ")");
            //no code ahead because I have no idea how to reconnect a client
        }

        private static void event_disconnected(TcpClientHandler client)
        {
            Console.WriteLine("Client " + client.Socket.ToString() + " disconnected from " + getIp(client).ToString());
            int id_ = gameWorld.getPlayer(client.Socket);
            gameWorld.removeEntity(id_);
            gameWorld.clientMap.Remove(id_);
        }

        private static void event_connected(TcpClientHandler client)
        {
            Console.WriteLine("Client connected from " + getIp(client).ToString());
            //we'll put our code to initiate the player client here
            int entid_ = gameWorld.createPlayer(new GamePoint3D(0d, 0d, 64d), client);
            int sz_ = 16 + (gameWorld.entityMap.Count * 52) + (gameWorld.objectMap.Count * 52);
            BufferStream buff = new BufferStream(sz_, 1);
            buff.Write((ushort)3);
            buff.Write(entid_);
            buff.Write((uint)gameWorld.entityMap.Count);
            foreach(KeyValuePair<int,GameEntity> pair in gameWorld.entityMap)
            {
                buff.Write(pair.Value.id);
                buff.Write(pair.Value.pos.X);
                buff.Write(pair.Value.pos.Y);
                buff.Write(pair.Value.pos.Z);
                buff.Write(pair.Value.size.X);
                buff.Write(pair.Value.size.Y);
                buff.Write(pair.Value.direction);
                buff.Write(pair.Value.pitch);
            }
            buff.Write((uint)gameWorld.objectMap.Count);
            foreach(KeyValuePair<int,GameObject> pair in gameWorld.objectMap)
            {
                buff.Write(pair.Value.id);
                buff.Write(pair.Value.position.X);
                buff.Write(pair.Value.position.Y);
                buff.Write(pair.Value.position.Z);
                buff.Write(pair.Value.size.X);
                buff.Write(pair.Value.size.Y);
                buff.Write(pair.Value.size.Z);
            }
            PacketStream.SendAsync(client, buff);
            buff.Deallocate();
            Console.WriteLine("World and client data sent to socket " + client.Socket.ToString());
        }

        private static void event_received(TcpClientHandler client, BufferStream readBuffer)
        {
            /*
                C#'s equivalent of GMS's buffer types:
                bool = boolean
                byte = u8
                sbyte = s8
                ushort = u16
                short = s16
                uint = u32
                int = s32
                float = f32
                double = f64
                string = string
                byte[] has no equivalent. Instead read out the number of bytes individually.
            */
            //Console.WriteLine("Received packet from " + getIp(client).ToString() + ", socket" + client.Socket.ToString());
            int plid = gameWorld.getPlayer(client.Socket);

            //this following line is probably absolutely essential to deal with the GMS packets
            readBuffer.Seek(12);
            BufferStream buff_ = null;
            //get the message id, so we know what to do with the packet
            ushort msgid; readBuffer.Read(out msgid);
            switch(msgid)
            {
                case 0: //client controls update
                    /*
                        0000000X - forward
                        000000X0 - backward
                        00000X00 - strafe left
                        0000X000 - strafe right
                        000X0000 - up ~jump
                        00X00000 - down ~crouch
                    */
                    byte in_; readBuffer.Read(out in_);
                    bool[] actions = GameGeometry.parse_binary(in_);
                    //Console.WriteLine(in_.ToString() + "-" + actions[5].ToString() + actions[4].ToString() + actions[3].ToString() + actions[2].ToString() + actions[1].ToString() + actions[0].ToString());
                    if (plid >= 0)
                    {
                        gameWorld.clientMap[plid].inputMap.setInput("forward", actions[0]);
                        gameWorld.clientMap[plid].inputMap.setInput("backward", actions[1]);
                        gameWorld.clientMap[plid].inputMap.setInput("left", actions[2]);
                        gameWorld.clientMap[plid].inputMap.setInput("right", actions[3]);
                        gameWorld.clientMap[plid].inputMap.setInput("up", actions[4]);
                        gameWorld.clientMap[plid].inputMap.setInput("down", actions[5]);
                    }
                    break;
                case 1: //client view update
                    //we should be getting two floats, one for direction and one for pitch
                    float dir_; readBuffer.Read(out dir_);
                    float pit_; readBuffer.Read(out pit_);
                    if(plid >= 0)
                    {
                        gameWorld.clientMap[plid].inputMap.setInput("view_x", dir_);
                        gameWorld.clientMap[plid].inputMap.setInput("view_y", pit_);
                    }
                    break;
                case 2: //client requested a ping
                    buff_ = new BufferStream(3, 1);
                    buff_.Write((ushort)6);
                    PacketStream.SendAsync(client, buff_);
                    break;
                case 3: //client is disconnecting
                    buff_ = new BufferStream(2, 1);
                    buff_.Write((ushort)7);
                    PacketStream.SendSync(client.Stream, buff_);
                    client.Connected = false;
                    break;
                default:
                    Console.WriteLine("invalid packet received");
                    break;
            }
            if(buff_ != null)
                buff_.Deallocate();
        }

        private static void event_closed(TcpServerHandler host)
        {
            if(host.ClientMap.Count > 0)
            {
                Console.WriteLine("Closing the server with " + host.ClientMap.Count.ToString() + " clients");
            }
            else
            {
                Console.WriteLine("Closing the server with no players");
            }
            
        }

        private static TcpClientHandler event_clientCreated(SocketBinder binder, TcpServerHandler server, uint clientTimeout)
        {
            TcpClientHandler tmp_ = new TcpClientHandler(binder, server, clientTimeout);
            Console.WriteLine("Client " + tmp_.Socket.ToString() + " has been created");
            return tmp_;
        }

        private static void event_started(TcpServerHandler host)
        {
            Console.WriteLine("Server is now running");
        }

        private static async void event_step()
        {
            /*BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                gameWorld.update();
            });
            bw.RunWorkerAsync();*/
            if(game_server.ClientMap.Count > 0)
                await Task.Run(() =>
                {
                    gameWorld.update();
                });
        }

        static IPAddress getIp(TcpClientHandler client)
        {
            return getIPEndPoint(client).Address;
        }
        static int getPort(TcpClientHandler client)
        {
            return getIPEndPoint(client).Port;
        }
        static IPEndPoint getIPEndPoint(TcpClientHandler client)
        {
            return ((IPEndPoint)client.Receiver.Client.RemoteEndPoint);
        }
    }
}