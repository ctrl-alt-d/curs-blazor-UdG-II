using System;
using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using ScoreboardService.Abstracts;
using Moq;
using FluentAssertions;
using ScoreboardUI;

namespace ScoreboardUI.Test
{
    public class ScoreboardTest: TestContext
    {

        /// <summary>
        /// El scoreboard mostra les puntuacions
        /// </summary>
        [Theory]
        [InlineData(100, 101)]
        [InlineData(101, 100)]
        public void ScoreboardShowTheScores(int homescore, int awayscore)
        {
            // Giving: un scoreboard machine amb puntuació
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);

            scoreboardMock.Setup(m => m.HomeScore).Returns(homescore);
            scoreboardMock.Setup(m => m.AwayScore).Returns(awayscore);
            var expectedHome = homescore.ToString();
            var expectedAway = awayscore.ToString();

            // Act: Renderitzem el component
            var cut = RenderComponent<Scoreboard>();

            // Assert: Ha de contenir les scorens
            cut
                .Find(".score.homescore")
                .TextContent
                .Should()
                .Be(expectedHome);

            cut
                .Find(".score.awayscore")
                .TextContent
                .Should()
                .Be(expectedAway);
        }

        /// <summary>
        /// Quan el servei notifica un canvi el marcador es refresca
        /// </summary>
        [Fact]
        public void ScoreboardRefreshOnChanges()
        {
            // Giving: Un scoreboard machine
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);
            var cut = RenderComponent<Scoreboard>();

            // Act: El scoreboard machine notifica un event de canvi
            scoreboardMock.Raise(m => m.ScoreboardHasChanged += null, new EventArgs());

            // Assert: S'ha de renderitzar de nou el component
            cut.RenderCount.Should().Be(2);
        }

        /// <summary>
        ///  Quan premem botó d'afegir punt a un equip es fa la crida al servei
        /// </summary>
        [Fact]
        public void ScoreboardBlazorControlCallsServiceAdd1()
        {
            // Giving: Un scoreboard
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);
            var cut = RenderComponent<Scoreboard>();

            // Act: Enviem un event de canvi
            cut.Find("#add1tohome").Click();
            cut.Find("#add1toaway").Click();

            // Assert: S'ha d'haver actualitzat
            scoreboardMock.Verify(m => m.Add1ToHome(), Times.Once);
            scoreboardMock.Verify(m => m.Add1ToAway(), Times.Once);
        }

        /// <summary>
        /// Quan premem el botó de posar-se a zero es fa la crida al servei
        /// </summary>
        [Fact]
        public void ScoreboardBlazorControlCallsServiceScoreboardTo0()
        {
            // Giving: Un scoreboard
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);
            var cut = RenderComponent<Scoreboard>();

            // Act: Enviem un event de canvi
            cut.Find("#scoreboardto0").Click();

            // Assert: S'ha d'haver actualitzat
            scoreboardMock.Verify(m => m.ScoreboardTo0(), Times.Once);
        }

    }
}
