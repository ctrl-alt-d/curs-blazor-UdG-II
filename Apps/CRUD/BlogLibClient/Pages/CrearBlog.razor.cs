using Blogging.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Client.Components.Pages
{
    public partial class CrearBlog
    {

        private CrearBlogWebView MyViewModel = new();

        [Parameter] public int Id { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
            }
        }

        private async Task Submit()
        {
            var parms = new BlogCreateParms(
                nomBlog: MyViewModel.Nom,
                titolPrimerPost: MyViewModel.NomPost,
                contingutPerAdults: MyViewModel.ContingutPerAdults
                );
            var resultat = await _BL.CreateBlog(parms);

            // No comprovem si hi ha errors - Taller hands-on:
            //    Comprovar si hi ha error
            //    No sortir de la pàgina
            //    Mostrar-lo

            _NM.NavigateTo("/fetchdata");
            return;
        }

    }
}
