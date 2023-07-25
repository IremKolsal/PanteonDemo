using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities
{
    public class Building
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int BuildingType { get; set; }
        public int BuildingCost { get; set; }
        public int ConstructionTime { get; set; }

    }
}
