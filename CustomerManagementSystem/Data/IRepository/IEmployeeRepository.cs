using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeListDto>> GetAllAsync();

        Task<EmployeeDetailDto?> GetByIdAsync(int id);

        Task<bool> CreateAsync(EmployeeDto dto);

        Task<bool> UpdateAsync(EmployeeDetailDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
