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
        [Theory]
        [InlineData(100, 101)]
        [InlineData(101, 100)]
        public void ElScoreboardReflexaCorrectamentLaPuntuacio(int homescore, int awayscore)
        {
            // Giving: un scoreboard machine amb puntuació
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);

            scoreboardMock.Setup(m => m.HomeScore).Returns(homescore);
            scoreboardMock.Setup(m => m.AwayScore).Returns(awayscore);
            var expectedLocal = homescore.ToString();
            var expectedVisitant = awayscore.ToString();

            // Act: Renderitzem el component
            var cut = RenderComponent<Scoreboard>();

            // Assert: Ha de contenir les scorens
            cut
                .Find("div.homescore")
                .TextContent
                .Should()
                .Be(expectedLocal);

            cut
                .Find("div.awayscore")
                .TextContent
                .Should()
                .Be(expectedVisitant);
        }

        [Fact]
        public void ElControlEsRepintaQuanRepEventDeCanvi()
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

        [Fact]
        public void ElControlCridaIncrementaScoreboardCorrectament()
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

        [Fact]
        public void ElControlSapPosarseAzero()
        {
            // Giving: Un scoreboard
            var scoreboardMock = new Mock<IScoreboardMachine>();
            this.Services.AddSingleton(_ => scoreboardMock.Object);
            var cut = RenderComponent<Scoreboard>();

            // Act: Enviem un event de canvi
            cut.Find("#posarscoreboarda0").Click();

            // Assert: S'ha d'haver actualitzat
            scoreboardMock.Verify(m => m.ScoreboardTo0(), Times.Once);
        }

    }
}
