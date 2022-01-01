using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities;
using LocationManagementSystem.Entities.Extensions;
using LocationManagementSystem.Entities.Helpers;
using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.Parameters;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Repository
{
    public class RoomRepository: RepositoryBase<Room>, IRoomRepository
    {
        private readonly ISortHelper<Room> _sortHelper;

        private readonly IDataShaper<Room> _dataShaper;

        public RoomRepository(RepositoryContext repositoryContext, ISortHelper<Room> sortHelper, IDataShaper<Room> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public void CreateRoom(Room room)
        {
            Create(room);
        }

        public void DeleteRoom(Room room)
        {
            Delete(room);
        }

        public PagedList<ShapedEntity> GetAllRooms(RoomParameters roomParameters)
        {
            var rooms = FindAll();

            SearchByName(ref rooms, roomParameters.Name);

            var sortedRooms = _sortHelper.ApplySort(rooms, roomParameters.OrderBy);
            var shapedRooms = _dataShaper.ShapeData(sortedRooms, roomParameters.Fields).AsQueryable();

            return PagedList<ShapedEntity>.ToPagedList(shapedRooms, roomParameters.PageNumber, roomParameters.PageSize);
        }

        public ShapedEntity GetRoomById(Guid roomId, string fields)
        {
            var room = FindByCondition(room => room.Id.Equals(roomId)).FirstOrDefault();

            if (room == null)
            {
                room = new Room();
            }

            return _dataShaper.ShapeData(room, fields);
        }

        public Room GetRoomById(Guid roomId)
        {
            return FindByCondition(i => i.Id.Equals(roomId)).FirstOrDefault();
        }

        public void UpdateRoom(Room dbRoom, Room room)
        {
            dbRoom.Map(room);
            Update(dbRoom);
        }

        private void SearchByName(ref IQueryable<Room> rooms, string roomName)
        {
            if (!rooms.Any() || string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            rooms = rooms.Where(i => i.Name.ToLower().Contains(roomName.Trim().ToLower()));
        }
    }
}
