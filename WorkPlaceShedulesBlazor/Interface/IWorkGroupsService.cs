using WorkPlaceShedulesBlazor.DTO;

namespace WorkPlaceShedulesBlazor.Interface
{
    public interface IWorkGroupsService
    {
        Task<List<WorkGroupsDTO>> GetWorkGroups();
        Task<WorkGroupsDTO> FindWorkGroups(int id);
        Task<int> SaveWorkGroups(WorkGroupsDTO workGroups);
        Task<int> UpdateWorkGroups(WorkGroupsDTO workGroups);
        Task<bool> DeleteWorkGroups(int id);
    }
}
