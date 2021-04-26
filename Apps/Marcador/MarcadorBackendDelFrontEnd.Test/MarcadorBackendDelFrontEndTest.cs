using System;
using System.Collections.Generic;
using System.Linq;
using MarcadorBackendDelFrontEnd.Abstracts;
using MarcadorBackendDelFrontEnd.InMemory;
using Xunit;

namespace MarcadorBackendDelFrontEnd.Test
{
    public class MarcadorBackendDelFrontEndTest
    {
        [Fact]
        public void IncrementaBeElsMarcadorsTest()
        {
            //Given: un marcador en marxa
            IMarcadorMachine marcador = new MarcadorMachine();

            IncrementaLocalIvisitant(marcador);

            var expectedPuntsLocal = marcador.PuntsLocal + 1;
            var expectedPuntsVisitant = marcador.PuntsVisitant + 1;

            //When: incrementem el marcador per a cada equip
            marcador.IncrementaLocal();
            marcador.IncrementaVisitant();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedPuntsLocal, marcador.PuntsLocal);
            Assert.Equal(expectedPuntsVisitant, marcador.PuntsVisitant);
        }

        [Fact]
        public void SapPosarElMarcadorA0Test()
        {
            //Given: un marcador en marxa
            IMarcadorMachine marcador = new MarcadorMachine();

            IncrementaLocalIvisitant(marcador);

            var expectedPuntsLocal = 0;
            var expectedPuntsVisitant = 0;

            //When: incrementem el marcador per a cada equip
            marcador.PosarMarcadorA0();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedPuntsLocal, marcador.PuntsLocal);
            Assert.Equal(expectedPuntsVisitant, marcador.PuntsVisitant);
        }

        [Fact]
        public void AvisaCorrectamentDelsCanvisTest()
        {
            //Given: Una sèrie d'accions que provoquen canvis
            IMarcadorMachine marcador = new MarcadorMachine();

            var comptador_de_canvis = 0;
            marcador.CanvisAlMarcador += (marcador, e) => comptador_de_canvis++;

            List<Action> AccionsQueProvoquenCanvis = new()
            {
                () => marcador.IncrementaLocal(),
                () => marcador.IncrementaVisitant(),
                () => marcador.PosarMarcadorA0(),
                () => marcador.AfegeixExpectador(),
            };

            //When: Invoquem les accions
            AccionsQueProvoquenCanvis.ForEach(accio => accio());

            //Then: Rebem les notificacions de canvis
            Assert.Equal(AccionsQueProvoquenCanvis.Count(), comptador_de_canvis);
        }

        // Helpers ----------------------------
        private static void IncrementaLocalIvisitant(IMarcadorMachine marcador)
        {
            Enumerable.Range(1, 5).ToList().ForEach(_ => marcador.IncrementaLocal());
            Enumerable.Range(1, 7).ToList().ForEach(_ => marcador.IncrementaVisitant());
        }
    }
}
