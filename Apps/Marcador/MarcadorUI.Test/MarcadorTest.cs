using System;
using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MarcadorBackendDelFrontEnd.Abstracts;
using Moq;
using FluentAssertions;

namespace MarcadorUI.Test
{
    public class MarcadorTest: TestContext
    {
        [Theory]
        [InlineData(100, 101)]
        [InlineData(101, 100)]
        public void ElMarcadorReflexaCorrectamentLaPuntuacio(int puntslocal, int puntsvisitant)
        {
            // Giving: un marcador machine amb puntuació
            var marcadorMock = new Mock<IMarcadorMachine>();
            this.Services.AddSingleton(_ => marcadorMock.Object);

            marcadorMock.Setup(m => m.PuntsLocal).Returns(puntslocal);
            marcadorMock.Setup(m => m.PuntsVisitant).Returns(puntsvisitant);
            var expectedLocal = puntslocal.ToString();
            var expectedVisitant = puntsvisitant.ToString();

            // Act: Renderitzem el component
            var cut = RenderComponent<Marcador>();

            // Assert: Ha de contenir les puntuacions
            cut
                .Find("div.puntslocal")
                .TextContent
                .Should()
                .Be(expectedLocal);

            cut
                .Find("div.puntsvisitant")
                .TextContent
                .Should()
                .Be(expectedVisitant);
        }

        [Fact]
        public void ElControlEsRepintaQuanRepEventDeCanvi()
        {
            // Giving: Un marcador machine
            var marcadorMock = new Mock<IMarcadorMachine>();
            this.Services.AddSingleton(_ => marcadorMock.Object);
            var cut = RenderComponent<Marcador>();

            // Act: El marcador machine notifica un event de canvi
            marcadorMock.Raise(m => m.CanvisAlMarcador += null, new EventArgs());

            // Assert: S'ha de renderitzar de nou el component
            cut.RenderCount.Should().Be(2);
        }

        [Fact]
        public void ElControlCridaIncrementaMarcadorCorrectament()
        {
            // Giving: Un marcador
            var marcadorMock = new Mock<IMarcadorMachine>();
            this.Services.AddSingleton(_ => marcadorMock.Object);
            var cut = RenderComponent<Marcador>();

            // Act: Enviem un event de canvi
            cut.Find("#incrementalocal").Click();
            cut.Find("#incrementavisitant").Click();

            // Assert: S'ha d'haver actualitzat
            marcadorMock.Verify(m => m.IncrementaLocal(), Times.Once);
            marcadorMock.Verify(m => m.IncrementaVisitant(), Times.Once);
        }

        [Fact]
        public void ElControlSapPosarseAzero()
        {
            // Giving: Un marcador
            var marcadorMock = new Mock<IMarcadorMachine>();
            this.Services.AddSingleton(_ => marcadorMock.Object);
            var cut = RenderComponent<Marcador>();

            // Act: Enviem un event de canvi
            cut.Find("#posarmarcadora0").Click();

            // Assert: S'ha d'haver actualitzat
            marcadorMock.Verify(m => m.PosarMarcadorA0(), Times.Once);
        }

    }
}
