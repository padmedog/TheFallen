using System;
using System.Collections.Generic;
using SharpServer.Sockets;
using SharpServer.Buffers;

namespace GMS_Server
{
    public class GameWorld
    {
        public Dictionary<int, GameEntity> entityMap { get; private set; }
        public Dictionary<int, GameClient> clientMap;
        public Dictionary<int, GameObject> objectMap;
        private TcpServerHandler gameServer;
        public int maxEntities, maxObjects;
        public GameWorld(TcpServerHandler _gameServer)
        {
            gameServer = _gameServer;
            maxEntities = 32;
            maxObjects = 64;
            entityMap = new Dictionary<int, GameEntity>();
            clientMap = new Dictionary<int, GameClient>(gameServer.MaxConnections);
            objectMap = new Dictionary<int, GameObject>();
            createObject(new GamePoint3D(0d, 0d, -32d), new GamePoint3D(1024d, 1024d, 32d));
        }

        public bool createEntity(GamePoint3D position)
        {
            return createEntity(position, new GamePoint2D(16d,64d));
        }
        public bool createEntity(GamePoint3D position, GamePoint2D size)
        {
            //returns if successful
            if (entityMap.Count < maxEntities)
                for(int i = 0; i < maxEntities; i += 1)
                    if(!entityMap.ContainsKey(i))
                    {
                        entityMap.Add(i, new GameEntity(position, size, i, this));
                        return true;
                    }
            return false;
        }
        public bool removeEntity(int Id)
        {
            //returns if successful
            if(entityMap.ContainsKey(Id))
            {
                entityMap.Remove(Id);

                BufferStream buff = new BufferStream(1024, 1);
                buff.Write((ushort)5);
                buff.Write((uint)Id);
                foreach(KeyValuePair<int,GameClient> pair in clientMap)
                    PacketStream.SendAsync(pair.Value.clientHandler, buff);
                buff.Deallocate();
                return true;
            }
            return false;
        }
        public bool createObject(GamePoint3D position, GamePoint3D size)
        {
            if(objectMap.Count < maxObjects)
                for(int i = 0; i < maxObjects; i += 1)
                    if(!objectMap.ContainsKey(i))
                    {
                        objectMap.Add(i, new GameObject(position, size, i, this));
                        return true;
                    }
            return false;
        }
        public bool removeObject(int id)
        {
            if(objectMap.ContainsKey(id))
            {
                objectMap.Remove(id);

                BufferStream buff = new BufferStream(1024, 1);
                buff.Write((ushort)2);
                buff.Write((uint)id);
                foreach (KeyValuePair<int, GameClient> pair in clientMap)
                    PacketStream.SendAsync(pair.Value.clientHandler, buff);
                buff.Deallocate();
                return true;
            }
            return false;
        }
        public int createPlayer(GamePoint3D position, TcpClientHandler client)
        {
            return createPlayer(position, new GamePoint2D(16d, 64d), client);
        }
        public int createPlayer(GamePoint3D position, GamePoint2D size, TcpClientHandler client)
        {
            //this will ignore the max entity limit, however cannot ignore the max connection limit
            //returns if successful
            int i = 0;
            if (clientMap.Count < gameServer.MaxConnections)
            {
                while (entityMap.ContainsKey(i)) //looks to find the lowest open slot to make an entity
                    i += 1;
                entityMap.Add(i, new GameEntity(position, size, i, this));
                clientMap.Add(i, new GameClient(client, i, this));
            }
            return i;
        }
        public void update()
        {
            if (clientMap.Count <= 0) return;
            foreach(KeyValuePair<int,GameClient> pair in clientMap)
                pair.Value.update(this);
            List<GameEntity> updateList = new List<GameEntity>();
            foreach(KeyValuePair<int,GameEntity> pair in entityMap)
                if(pair.Value.update(this))
                    updateList.Add(pair.Value);
            BufferStream buff = new BufferStream(12 + (updateList.Count*52),1);
            buff.Seek(0);
            buff.Write((ushort)0);
            buff.Write(updateList.Count);
            foreach(GameEntity entity in updateList)
            {
                buff.Write(entity.id);
                buff.Write(entity.pos.X);
                buff.Write(entity.pos.Y);
                buff.Write(entity.pos.Z);
                buff.Write(entity.size.X);
                buff.Write(entity.size.Y);
                buff.Write(entity.direction);
                buff.Write(entity.pitch);
            }
            foreach(KeyValuePair<int,GameClient> client in clientMap)
                PacketStream.SendAsync(client.Value.clientHandler,buff);
            buff.Deallocate();
        }
        public int getPlayer(int socket)
        {
            foreach(KeyValuePair<int,GameClient> client in clientMap)
                if(socket == client.Value.clientHandler.Socket)
                    return client.Value.entityId;
            return -1;
        }
        public TcpClientHandler getClient(int id)
        {
            foreach(KeyValuePair<int,GameClient> client in clientMap)
                if(id == entityMap[client.Key].id)
                    return client.Value.clientHandler;
            return null;
        }
        public GameEntity getEntity(int id)
        {
            if (entityMap.ContainsKey(id))
                return entityMap[id];
            return null;
        }
    }
    public class GameEntity
    {
        public GamePoint3D pos;
        private GamePoint3D ppos;
        public GamePoint3D spd;
        public GamePoint3D base_spd;
        public GamePoint3D frc;
        public GamePoint2D size;
        public float direction, pitch, precision, pdir = 0f, ppit = 0f;
        public int id { get; private set; }
        public GameEntity(GamePoint3D Position, GamePoint2D Size, int Id, GameWorld gameWorld)
        {
            pos = Position;
            id = Id;
            spd = new GamePoint3D();
            frc = new GamePoint3D(1.4d,1.4d,1.05d); //friction system needs to be changed to support different areas, like just going through air 
            base_spd = new GamePoint3D(1d, 0.5d, 0.75d); //or walking on something, which have different frictions; you won't be able to directly set it
            direction = 0f;
            pitch = 0f;
            size = Size;
            precision = 1; //higher number = chunkier collision checks (faster but crappier)

            BufferStream buff = new BufferStream(1024, 1);
            buff.Write((ushort)4);
            buff.Write(id);
            buff.Write(pos.X);
            buff.Write(pos.Y);
            buff.Write(pos.Z);
            buff.Write(size.X);
            buff.Write(size.Y);
            buff.Write(direction);
            buff.Write(pitch);
            foreach (KeyValuePair<int, GameClient> pair in gameWorld.clientMap)
            {
                PacketStream.SendAsync(pair.Value.clientHandler, buff);
            }
            buff.Deallocate();
        }
        public bool update(GameWorld gameWorld)
        {
            /*foreach(KeyValuePair<int,GameObject> pair in gameWorld.objectMap)
            {
                while (GameGeometry.cube_in_cube(pair.Value.position, GamePoint3D.Add(pair.Value.position, pair.Value.size),
                    GamePoint3D.Subtract(pos, new GamePoint3D(size.X - spd.X, size.X, size.Y)), GamePoint3D.Add(pos, new GamePoint3D(size.X + spd.X, size.X, size.Y))))
                {
                    if (spd.X >= precision)
                        spd.X -= precision * Math.Sign(spd.X);
                    else
                    {
                        spd.X = 0;
                        break;
                    }
                }
                while (GameGeometry.cube_in_cube(pair.Value.position, GamePoint3D.Add(pair.Value.position, pair.Value.size),
                    GamePoint3D.Subtract(pos, new GamePoint3D(size.X, size.X - spd.Y, size.Y)), GamePoint3D.Add(pos, new GamePoint3D(size.X, size.X + spd.Y, size.Y))))
                {
                    if (spd.Y >= precision)
                        spd.Y -= precision * Math.Sign(spd.Y);
                    else
                    {
                        spd.Y = 0;
                        break;
                    }
                }
                while (GameGeometry.cube_in_cube(pair.Value.position, GamePoint3D.Add(pair.Value.position, pair.Value.size),
                    GamePoint3D.Subtract(pos, new GamePoint3D(size.X, size.X, size.Y - spd.Z)), GamePoint3D.Add(pos, new GamePoint3D(size.X, size.X, size.Y + spd.Z))))
                {
                    if (spd.Z >= precision)
                        spd.Z -= precision * Math.Sign(spd.Z);
                    else
                    {
                        spd.Z = 0;
                        break;
                    }
                }
            }*/
            pos.Add(spd);
            spd.Divide(frc);
            while (direction < 0f) direction += 360f; direction %= 360f;
            if (pitch >= 89f) pitch = 89f;
            else if (pitch <= -89f) pitch = -89f;
            //Console.WriteLine("x" + pos.X.ToString() + ",y" + pos.Y.ToString() + ",z" + pos.Z.ToString()+",d"+direction.ToString());
            bool ret_ = false;
            if(pos != ppos || pdir != direction || ppit != pitch)
            {
                ret_ = true;
            }
            ppos = pos;
            return ret_;
        }
    }
    public class GameClient
    {
        public int entityId;
        public TcpClientHandler clientHandler;
        public InputMap inputMap;
        public GameClient(TcpClientHandler tcpclient, int entityid, GameWorld gameWorld)
        {
            entityId = entityid;
            clientHandler = tcpclient;
            inputMap = new InputMap();
        }
        public void update(GameWorld gameWorld)
        {
            if (gameWorld.entityMap.ContainsKey(entityId))
            {
                float dir = gameWorld.entityMap[entityId].direction;
                if (inputMap.getInput("left") == 1f) //strafe left
                {
                    gameWorld.entityMap[entityId].spd.Add(GameGeometry.lengthdir(Convert.ToSingle(gameWorld.entityMap[entityId].base_spd.Y), dir - 90));
                }
                if (inputMap.getInput("right") == 1f) //strafe right
                {
                    gameWorld.entityMap[entityId].spd.Add(GameGeometry.lengthdir(Convert.ToSingle(gameWorld.entityMap[entityId].base_spd.Y), dir + 90));
                }
                if (inputMap.getInput("forward") == 1f) //forward
                {
                    gameWorld.entityMap[entityId].spd.Add(GameGeometry.lengthdir(Convert.ToSingle(gameWorld.entityMap[entityId].base_spd.X), dir));
                }
                if (inputMap.getInput("backward") == 1f) //backward
                {
                    gameWorld.entityMap[entityId].spd.Add(GameGeometry.lengthdir(Convert.ToSingle(gameWorld.entityMap[entityId].base_spd.Z), dir + 180));
                }
                if (inputMap.getInput("up") == 1f) //jump
                {
                    gameWorld.entityMap[entityId].spd.Z -= 4d;
                }
                gameWorld.entityMap[entityId].direction += inputMap.getInput("view_x"); inputMap.setInput("view_x", 0f);
                gameWorld.entityMap[entityId].pitch -= inputMap.getInput("view_y"); inputMap.setInput("view_y", 0f);
            }
        }
    }
    public class InputMap
    {
        public Dictionary<string, float> map { get; private set; }
        public InputMap()
        {
            map = new Dictionary<string, float>();
        }
        public void setInput(string key, bool state)
        {
            setInput(key, state ? 1f : 0f);
        }
        public bool setInput(string key, float state)
        {
            //returns if successful
            while (map.ContainsKey(key))
                map.Remove(key);
            try
            {
                map.Add(key, state);
            }
            catch
            {
                Console.WriteLine("error setting input");
                return false;
            }
            return true;
        }
        public float getInput(string key)
        {
            if (map.ContainsKey(key))
                return map[key];
            return 0f;
        }
    }
    public class GameObject
    {
        public GamePoint3D position;
        public GamePoint3D size;
        public int id;
        public GameObject(GamePoint3D Position, GamePoint3D Size, int Id, GameWorld gameWorld)
        {
            position = Position;
            size = Size;
            id = Id;

            BufferStream buff = new BufferStream(64,1);
            buff.Write((ushort)1);
            buff.Write((uint)id);
            buff.Write(position.X);
            buff.Write(position.Y);
            buff.Write(position.Z);
            buff.Write(size.X);
            buff.Write(size.Y);
            buff.Write(size.Z);
            foreach(KeyValuePair<int,GameClient> pair in gameWorld.clientMap)
            {
                PacketStream.SendAsync(pair.Value.clientHandler, buff);
            }
            buff.Deallocate();
        }
    }
}
