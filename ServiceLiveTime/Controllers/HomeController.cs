using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ServiceLiveTime.Models;
using ServiceLiveTime.Services;

namespace ServiceLiveTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Services

        private readonly ISingleToneService _singleToneService01;
        private readonly ISingleToneService _singleToneService02;
        private readonly ITransientService _transientService01;
        private readonly ITransientService _transientService02;
        private readonly IScopedService _scopedService01;
        private readonly IScopedService _scopedService02;


        public HomeController(ISingleToneService singleToneService01, ISingleToneService singleToneService02,
                              ITransientService transientService01, ITransientService transientService02,
                              IScopedService scopedService01, IScopedService scopedService02)
        {
            _singleToneService01     = singleToneService01;
            _singleToneService02     = singleToneService02; ;
            _transientService01      = transientService01;
            _transientService02      = transientService02;
             _scopedService01        = scopedService01;
             _scopedService02        = scopedService02;
            
            
        }

        public string Index()
        {
            StringBuilder sp = new StringBuilder();
            sp.AppendLine($"SingleTone1 :: {_singleToneService01.GetGuid()}");
            sp.AppendLine($"SingleTone2 :: {_singleToneService02.GetGuid()}");
            sp.AppendLine($"Scoped01 :: {_scopedService01.GetGuid()}");
            sp.AppendLine($"Scoped02 :: {_scopedService02.GetGuid()}");
            sp.AppendLine($"Transient01 :: {_transientService01.GetGuid()}");
            sp.AppendLine($"Transient02 :: {_transientService02.GetGuid()}");
            return sp.ToString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
