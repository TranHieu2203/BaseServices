using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetProjectAsync();
        Task<ProjectDTO> GetById(int id);
        Task<bool> InsertAsync(ProjectDTO projectDTO);
        Task<int> CompletedAsync();
    }
}
