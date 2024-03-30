using WorkPlaceShedulesBlazor.DTO;

namespace WorkPlaceShedulesBlazor.Interface
{
    public interface IWorkPlacesService
    {
        Task<List<WorkPlacesDTO>> GetWorkPlaces();
        Task<WorkPlacesDTO> FindWorkPlaces(int id);
        Task<int> SaveWorkPlaces(WorkPlacesDTO workPlaces);
        Task<int> UpdateWorkPlaces(WorkPlacesDTO workPlaces);
        Task<bool> DeleteWorkPlaces(int id);
    }
}
