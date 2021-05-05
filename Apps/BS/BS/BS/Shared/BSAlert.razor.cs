using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS.Shared
{
    public partial class BSAlert
    {

        protected virtual List<string> LlistaDeMissatges { get; set; } = new();
        public void MostraAixo(string msg)
        {
            LlistaDeMissatges.Add(msg);
            StateHasChanged();

            //
            var aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (_, _) =>
            {
                LlistaDeMissatges.Remove(msg);
                StateHasChanged();
            };
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }
    }
}
