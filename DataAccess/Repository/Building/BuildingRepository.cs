using DataAccess.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repository.Building
{
    public class BuildingRepository
    {
        private readonly string _connectionString = "mongodb://localhost:27017";

        public ReturnViewModel AddBuilding(Entities.Building building)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                if (building.BuildingCost <= 0)
                {
                    returnViewModel.IsSuccess = false;
                    returnViewModel.Message = "Building Cost 0'a eşit veya küçük olamaz.";
                    return returnViewModel;
                }
                if (building.ConstructionTime < 30 || building.ConstructionTime > 1800)
                {
                    returnViewModel.IsSuccess = false;
                    returnViewModel.Message = "Construction time 30dan küçük veya 1800den büyük olamaz.";
                    return returnViewModel;
                }
                var mongoClient = new MongoClient(_connectionString);
                var database = mongoClient.GetDatabase("Building");
                var collection = database.GetCollection<Entities.Building>("building");
                collection.InsertOne(building);
                returnViewModel.IsSuccess = true;
                returnViewModel.Message = "Başarıyla eklendi";
            }
            catch (Exception ex)
            {
                returnViewModel.IsSuccess = false;
                returnViewModel.Message = "Server Error";
                returnViewModel.Data = null;
                return returnViewModel;
            }
            return returnViewModel;

        }

        public ReturnViewModel GetBuilding()
        {
            ReturnViewModel returnViewModel = new ReturnViewModel();
            try
            {
                var mongoClient = new MongoClient(_connectionString);
                var database = mongoClient.GetDatabase("Building");
                var collection = database.GetCollection<Entities.Building>("building");
                returnViewModel.IsSuccess = true;
                returnViewModel.Data = collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception ex)
            {
                returnViewModel.IsSuccess = false;
                returnViewModel.Message = "Server Error";
                returnViewModel.Data = null;
                return returnViewModel;
            }
            return returnViewModel;

        }

    }
}
