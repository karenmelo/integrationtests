using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Core.DomainObjects;
using Store.Core.Messages.Messages.CommonMessages.Notifications;
using System.Security.Claims;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediator _mediator;
        protected Guid ClientId;
        public ControllerBase(INotificationHandler<DomainNotification> notification,
                              IMediator mediator,
                              IHttpContextAccessor httpContextAccessor)
        {
            _notifications = (DomainNotificationHandler)notification;
            _mediator = mediator;

            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) return;

            var claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            ClientId = Guid.Parse(claim.Value);
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected IEnumerable<string> GetMessagesError()
        {
            return _notifications.GetNotifications().Select(n => n.Value).ToList();
        }


        protected void NotifyError(string code, string message)
        {
            _mediator.Publish(new DomainNotification(code, message));
        }

        protected new IActionResult Response(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                erros = _notifications.GetNotifications().Select(n => n.Value)
            });
        }
    }
}
