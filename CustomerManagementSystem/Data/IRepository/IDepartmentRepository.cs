using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto> GetByIdAsync(int id);

        Task<bool> CreateAsync(DepartmentDto dto);

        Task<bool> UpdateAsync(DepartmentDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
