using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Extensions
{
    public class Notify
    {
        private readonly NotificationService notificationService;

        public Notify(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);

         //   await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
