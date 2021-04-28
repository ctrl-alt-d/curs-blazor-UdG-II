using MarcadorBackendDelFrontEnd.Abstracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcadorUI
{
    public partial class Marcador
    {
        [Inject] protected IMarcadorMachine MarcadorMachine { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MarcadorMachine.CanvisAlMarcador += MarcadorMachine_CanvisAlMarcador;
        }

        private void MarcadorMachine_CanvisAlMarcador(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
