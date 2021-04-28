using ScoreboardService.Abstracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardUI
{
    public partial class Scoreboard
    {
        [Inject] protected IScoreboardMachine ScoreboardMachine { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ScoreboardMachine.ScoreboardHasChanged += ScoreboardMachine_ScoreboardHasChanged;
        }

        private void ScoreboardMachine_ScoreboardHasChanged(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
