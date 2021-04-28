using System;
using ScoreboardService.Abstracts;

namespace ScoreboardService.InMemory
{
    public class ScoreboardMachine : IScoreboardMachine
    {
        public virtual int HomeScore {get; private set;}

        public virtual int AwayScore {get; private set;}

        protected virtual string _HomeTeamName {get; set;} = "Local";
        protected virtual string _AwayTeamName {get; set;} = "Visitant";
        public virtual string HomeTeamName {
            get => _HomeTeamName;
            set
            {
                _HomeTeamName=value;
                AvisaCanvis();
            }
        }
        public virtual string AwayTeamName {
            get => _AwayTeamName;
            set
            {
                _AwayTeamName=value;
                AvisaCanvis();
            }
        }

        public virtual int TotalAudience {get; protected set;}

        public virtual event EventHandler? ScoreboardHasChanged;

        public virtual void Add1ToHome()
        {
            HomeScore++;
            AvisaCanvis();
        }

        public virtual void Add1ToAway()
        {
            AwayScore++;
            AvisaCanvis();
        }

        public virtual void ScoreboardTo0()
        {
            HomeScore = 0;
            AwayScore = 0;
            AvisaCanvis();
        }

        protected virtual void AvisaCanvis()
        {
            var e = new EventArgs();
            ScoreboardHasChanged?.Invoke(this, e);
        }

        public virtual void AddViewer()
        {
            TotalAudience++;
            AvisaCanvis();
        }

        public virtual void RemoveViewer()
        {
            TotalAudience--;
            AvisaCanvis();
        }
    }
}
