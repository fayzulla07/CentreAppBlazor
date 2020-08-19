using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services
{
    public delegate void LoadingHandler();
    public class LoadingService
    {
      public  event LoadingHandler Notify;
        private bool visible;
        public bool Visible 
        {
            get { return visible; }
            set { visible = value; Notify.Invoke(); }
        }
    }
}
