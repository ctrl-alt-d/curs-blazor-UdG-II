using System;

namespace ScoreboardService.Abstracts
{
    public interface IScoreboardMachine
    {
        int HomeScore {get; }
        int AwayScore {get; }
        string HomeTeamName {get; set;}
        string AwayTeamName {get; set;}
        void ScoreboardTo0();
        event EventHandler ScoreboardHasChanged;  
        void Add1ToHome();
        void Add1ToAway();
        int TotalAudience {get;}
        void AddViewer();
        void RemoveViewer();
    }
}
