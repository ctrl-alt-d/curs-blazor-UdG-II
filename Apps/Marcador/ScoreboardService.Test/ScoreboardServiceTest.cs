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
        /// <summary>
        /// Els punts s'incrementen correctament
        /// </summary>
        [Fact]
        public void ScoreboardRaiseUpAsExpected()
        {
            //Given: un scoreboard en marxa
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            _RaiseUpScoreboard(scoreboardMachine, 10, 20);

            var expectedHomeScore = scoreboardMachine.HomeScore + 1;
            var expectedAwayScore = scoreboardMachine.AwayScore + 1;

            //When: incrementem el scoreboard per a cada equip
            scoreboardMachine.Add1ToHome();
            scoreboardMachine.Add1ToAway();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedHomeScore, scoreboardMachine.HomeScore);
            Assert.Equal(expectedAwayScore, scoreboardMachine.AwayScore);
        }

        /// <summary>
        /// El servei es posa a 0 quan li demanen
        /// </summary>
        [Fact]
        public void IsAbleToSetScoreboardTo0()
        {
            //Given: un scoreboard en marxa
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            _RaiseUpScoreboard(scoreboardMachine, 10, 20);

            var expectedHomeScore = 0;
            var expectedAwayScore = 0;

            //When: incrementem el scoreboard per a cada equip
            scoreboardMachine.ScoreboardTo0();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedHomeScore, scoreboardMachine.HomeScore);
            Assert.Equal(expectedAwayScore, scoreboardMachine.AwayScore);
        }

        /// <summary>
        /// El servei correctament quan hi ha canvis
        /// </summary>
        [Fact]
        public void NotifiesScoreboardChanges()
        {
            //Given: Una sèrie d'accions que provoquen canvis
            IScoreboardMachine scoreboardMachine = new ScoreboardMachine();

            var changes_counter = 0;
            scoreboardMachine.ScoreboardHasChanged += (scoreboard, e) => changes_counter++;

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
            Assert.Equal(expectedNumeroDeCanvis, changes_counter);
        }

        // Helpers ----------------------------
        /// <summary>
        /// Helper per fer pujar la puntuació de l'scoreboard
        /// </summary>
        private static void _RaiseUpScoreboard(IScoreboardMachine scoreboardMachine, int locals, int visitants)
        {
            ExecuteNtimes(scoreboardMachine.Add1ToHome, locals);
            ExecuteNtimes(scoreboardMachine.Add1ToAway, visitants);
        }

        private static void ExecuteNtimes(Action action, int n)
        {
            Enumerable.Range(0, n).ToList().ForEach(_ => action());
        }
    }
}
