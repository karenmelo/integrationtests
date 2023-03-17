using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.DomainObjects;

namespace NerdStore.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : ControllerBase
    {
        //private readonly IProductAppService _productAppService;
        // private readonly IOrderQueries _orderQueries;
        private readonly IMediator _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications,
                              //IProductAppService productAppService,
                              IMediator mediatorHandler,
                              //IOrderQueries orderQueries,
                              IHttpContextAccessor httpContextAccessor) : base(notifications, mediatorHandler, httpContextAccessor)

        {
            _mediatorHandler = mediatorHandler;
            //_productAppService = productAppService;
            //_orderQueries = orderQueries;
        }

        [HttpGet]
        [Route("meu-carrinho")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
