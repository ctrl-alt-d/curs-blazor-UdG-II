using System;
using System.Collections.Generic;
using System.Linq;
using ScoreboardService.Abstracts;
using ScoreboardService.InMemory;
using Xunit;

namespace ScoreboardService.Test
{
    public class ScoreboardServiceTest
    {
        [Fact]
        public void ElsScoreboardsSincrementenCorrectament()
        {
            //Given: un scoreboard en marxa
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            _FesPujarElScoreboard(scoreboardMachine, 10, 20);

            var expectedHomeScore = scoreboardMachine.HomeScore + 1;
            var expectedAwayScore = scoreboardMachine.AwayScore + 1;

            //When: incrementem el scoreboard per a cada equip
            scoreboardMachine.Add1ToHome();
            scoreboardMachine.Add1ToAway();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedHomeScore, scoreboardMachine.HomeScore);
            Assert.Equal(expectedAwayScore, scoreboardMachine.AwayScore);
        }

        [Fact]
        public void SapPosarElScoreboardA0()
        {
            //Given: un scoreboard en marxa
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            _FesPujarElScoreboard(scoreboardMachine, 10, 20);

            var expectedHomeScore = 0;
            var expectedAwayScore = 0;

            //When: incrementem el scoreboard per a cada equip
            scoreboardMachine.ScoreboardTo0();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedHomeScore, scoreboardMachine.HomeScore);
            Assert.Equal(expectedAwayScore, scoreboardMachine.AwayScore);
        }

        [Fact]
        public void AvisaCorrectamentDelsScoreboardHasChanged()
        {
            //Given: Una sèrie d'accions que provoquen canvis
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            var comptador_de_canvis = 0;
            scoreboardMachine.ScoreboardHasChanged += (scoreboard, e) => comptador_de_canvis++;

            List<Action> AccionsQueProvoquenCanvis = new()
            {
                () => scoreboardMachine.Add1ToHome(),
                () => scoreboardMachine.Add1ToAway(),
                () => scoreboardMachine.ScoreboardTo0(),
                () => scoreboardMachine.AddViewer(),
            };

            var expectedNumeroDeCanvis = AccionsQueProvoquenCanvis.Count;

            //When: Invoquem les accions
            AccionsQueProvoquenCanvis.ForEach(accio => accio());

            //Then: Rebem les notificacions de canvis
            Assert.Equal(expectedNumeroDeCanvis, comptador_de_canvis);
        }

        // Helpers ----------------------------
        private static void _FesPujarElScoreboard(IScoreboardMachine scoreboardMachine, int locals, int visitants)
        {
            Enumerable.Range(0, locals).ToList().ForEach(_ => scoreboardMachine.Add1ToHome());
            Enumerable.Range(0, visitants).ToList().ForEach(_ => scoreboardMachine.Add1ToAway());
        }
    }
}
