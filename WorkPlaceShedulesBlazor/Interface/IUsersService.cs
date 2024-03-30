using WorkPlaceShedulesBlazor.DTO;

namespace WorkPlaceShedulesBlazor.Interface
{
    public interface IUsersService
    {
        Task<List<UsersDTO>> GetUsers();
        Task<UsersDTO> FindUsers(int id);
        Task<int> SaveUsers(UsersDTO users);
        Task<int> UpdateUsers(UsersDTO users);
        Task<bool> DeleteUsers(int id);
    }
}
