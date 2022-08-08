using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICustommerService
    {
        Task<IEnumerable<CustommerDTO>> GetCustommersAsync();
        Task<CustommerDTO> GetById(int id);
        Task<bool> InsertAsync(CustommerDTO custommerDTO);
        // Task<int> CompletedAsynce();
    }
}