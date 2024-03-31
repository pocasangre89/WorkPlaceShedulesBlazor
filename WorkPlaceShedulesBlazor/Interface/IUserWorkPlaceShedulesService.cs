using WorkPlaceShedulesBlazor.DTO;

namespace WorkPlaceShedulesBlazor.Interface
{
    public interface IUserWorkPlaceShedulesService
    {
        Task<List<UserWorkPlaceShedulesDTO>> GetUserWorkPlaceShedules();
        //Task<UserWorkPlaceShedulesDTO> FindUsers(int id);
        Task<int> SaveUserWorkPlaceShedules(UserWorkPlaceShedulesDTO userWorkPlaceShedules);
        //Task<int> UpdateUsers(UserWorkPlaceShedulesDTO users);
        //Task<bool> DeleteUsers(int id);
    }
}
