using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Contracts
{
    public interface IRoomRepository: IRepositoryBase<Room>
    {
        ShapedEntity GetRoomById(Guid roomId, string fields);
        Room GetRoomById(Guid roomId);
        void CreateRoom(Room room);
        void UpdateRoom(Room dbRoom, Room room);
        void DeleteRoom(Room room);
    }
}
