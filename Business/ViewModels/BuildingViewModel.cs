using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels
{
    public class BuildingViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int BuildingType { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int BuildingCost { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        //[MinLength(29, ErrorMessage = "30dan küçük olamaz.")]
        //[MaxLength(1800, ErrorMessage = "1800den büyük olamaz.")]
        public int ConstructionTime { get; set; }
    }
}
