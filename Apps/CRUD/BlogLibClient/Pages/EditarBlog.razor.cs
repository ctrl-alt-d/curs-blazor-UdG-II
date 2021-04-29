using Blogging.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Client.Components.Pages
{
    public partial class EditarBlog
    {

        private EditarBlogWebView MyEditBlogCanviNomModel = new();

        [Parameter] public int Id { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var result = await _BL.RetrieveBlogById(Id);

                var data = result.Data;
                MyEditBlogCanviNomModel.Nom = data.Nom;
                StateHasChanged();

            }
        }

        private async Task Submit()
        {

            var parms = new BlogCanviaNomParms
            {
                Id = Id,
                NouNom = MyEditBlogCanviNomModel.Nom
            };
            var resultat = await _BL.CanviaNom(parms);

            _NM.NavigateTo("/fetchdata");

        }

    }
}
