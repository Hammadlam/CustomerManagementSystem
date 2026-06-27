using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IDesignationRepository
    {
        Task<List<DesignationDto>> GetAllAsync();

        Task<DesignationDto?> GetByIdAsync(int id);

        Task<bool> CreateAsync(DesignationDto dto);

        Task<bool> UpdateAsync(DesignationDto dto);
    }

}
