using AutoMapper;
using Core.Entities;
using Infrastructure.Interfaces;
using Core.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Service
{
    public class CustommerService : ICustommerService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CustommerService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustommerDTO>> GetCustommersAsync()
        {
            var custommer = await _unitOfWork.Custommers.GetAll();
            return _mapper.Map<IEnumerable<CustommerDTO>>(custommer);
        }

        public async Task<CustommerDTO> GetById(int id)
        {
            var s = await _unitOfWork.Custommers.GetById(id);
            return _mapper.Map<CustommerDTO>(s);

        }

        public async Task<bool> InsertAsync(CustommerDTO custommerDTO)
        {
            var custommer = _mapper.Map<Custommer>(custommerDTO);
            await _unitOfWork.Custommers.Add(custommer);
            await _unitOfWork.CompletedAsync();
            return true;
        }

        // public async Task<int> CompletedAsync()
        // {
        //     return await _unitOfWork.CompletedAsync();
        // }
    }
}