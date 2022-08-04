using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProjectService: IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectAsync()
        {
            var projects = await _unitOfWork.Projects.GetAll();

        return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetById(int  id)
        {
            var s = await _unitOfWork.Projects.GetById(id);
            return _mapper.Map<ProjectDTO>(s);

        }

        public async Task<bool> InsertAsync(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
             await _unitOfWork.Projects.Add(project);
            await _unitOfWork.CompletedAsync();
            return true;
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }

}
