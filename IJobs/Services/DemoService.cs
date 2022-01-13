using IJobs.Models.DTOs;
using IJobs.Models;
using IJobs.Repositories.DatabaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IJobs.Repositories.Services;

namespace IJobs.Repositories.Services
{
    public class DemoService : IDemoService
    {
        public ICompanyRepository _databaseRepository;
        public DemoService(ICompanyRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }
        /*public ModelResultDTO GetDataMappedByTitle(string title)
        {
            Model1 model1 = _databaseRepository.GetByTitleIncludinModel2(title);
            ModelResultDTO result = new()
            {
                Title = model1.Title,
                Order = model1.Order,
                ModelId1 = model1.Id,
                Models2 = (List<Model2>)model1.Models2
            };
            return result;
        }*/
    }
}
