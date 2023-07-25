using Business.UnitOfWork;
using Business.ViewModels;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingApiController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public BuildingApiController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBuilding")]
        public object AddBuilding([FromForm] BuildingViewModel buildingViewModel)
        {
            var building = new Building()
            {
                BuildingCost = buildingViewModel.BuildingCost,
                BuildingType = buildingViewModel.BuildingType,
                ConstructionTime = buildingViewModel.ConstructionTime
            };
            return _unitOfWork.Buildings.AddBuilding(building);


        }


        [HttpGet("GetBuilding")]
        public object GetBuilding()
        {
            return _unitOfWork.Buildings.GetBuilding();
        }
    }
}
