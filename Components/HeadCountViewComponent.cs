using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectStation.Components
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IClientRepository clientRepository;

        public HeadCountViewComponent(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public IViewComponentResult Invoke(ClientType? clientTypeName = null)
        {
            var result = clientRepository.ClientCountByType(clientTypeName);
            return View(result);
        }
    }
}
