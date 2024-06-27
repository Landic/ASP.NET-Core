using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.Services {
    public interface IPerformerService {
        Task<IEnumerable<PerformerDTO>> GetPerformers();
        Task<PerformerDTO> GetPerformerById(int performerId);
        Task<PerformerDTO> GetPerformerByFullName(string fullname);
        Task AddPerformer(PerformerDTO model);
        void UpdatePerformer(PerformerDTO model);
        Task DeletePerformer(int performerId);
        Task Save();
    }
    public class PerformerService : IPerformerService {
        ISaveUnit saveUnit { get; set; }
        public PerformerService(ISaveUnit saveUnit)
        {
            this.saveUnit = saveUnit;
        }
        public async Task<IEnumerable<PerformerDTO>> GetPerformers() 
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Performer, PerformerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Performer>, IEnumerable<PerformerDTO>>(await saveUnit.Performers.GetAll());
        }
        public async Task<PerformerDTO> GetPerformerById(int performerId) 
        {
            var performer = await saveUnit.Performers.GetById(performerId);
            if (performer == null)
            {
                throw new ValidationException("Неверный исполнитель!", "");
            }
            return new PerformerDTO 
            {
                Id = performer.Id,
                FullName = performer.FullName
            };
        }
        public async Task<PerformerDTO> GetPerformerByFullName(string fullname) 
        {
            var performer = await saveUnit.Performers.GetByStr(fullname);
            if (performer == null)
            {
                throw new ValidationException("Неверный исполнитель!", "");
            }
            return new PerformerDTO 
            {
                Id = performer.Id,
                FullName = performer.FullName
            };
        }
        public async Task AddPerformer(PerformerDTO model) 
        {
            await saveUnit.Performers.Add(new Performer 
            {
                Id = model.Id,
                FullName = model.FullName,
            });
        }
        public void UpdatePerformer(PerformerDTO model) 
        {
            saveUnit.Performers.Update(new Performer 
            {
                Id = model.Id,
                FullName = model.FullName
            });
        }
        public async Task DeletePerformer(int performerId)
        {
            await saveUnit.Performers.Delete(performerId);
        }
        public async Task Save()
        {
            await saveUnit.Save();
        }
    }
}