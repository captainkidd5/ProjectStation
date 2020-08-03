using Microsoft.AspNetCore.Mvc;
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

        public IViewComponentResult Invoke()
        {
            var result = clientRepository.ClientCountByType();
            return View(result);
        }
    }
}
