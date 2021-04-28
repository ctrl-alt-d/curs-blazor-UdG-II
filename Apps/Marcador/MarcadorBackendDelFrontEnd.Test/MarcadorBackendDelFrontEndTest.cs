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
        public void ElsMarcadorsSincrementenCorrectament()
        {
            //Given: un marcador en marxa
            IMarcadorMachine marcadorMachine = new MarcadorMachine();

            _FesPujarElMarcador(marcadorMachine, 10, 20);

            var expectedPuntsLocal = marcadorMachine.PuntsLocal + 1;
            var expectedPuntsVisitant = marcadorMachine.PuntsVisitant + 1;

            //When: incrementem el marcador per a cada equip
            marcadorMachine.IncrementaLocal();
            marcadorMachine.IncrementaVisitant();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedPuntsLocal, marcadorMachine.PuntsLocal);
            Assert.Equal(expectedPuntsVisitant, marcadorMachine.PuntsVisitant);
        }

        [Fact]
        public void SapPosarElMarcadorA0()
        {
            //Given: un marcador en marxa
            IMarcadorMachine marcadorMachine = new MarcadorMachine();

            _FesPujarElMarcador(marcadorMachine, 10, 20);

            var expectedPuntsLocal = 0;
            var expectedPuntsVisitant = 0;

            //When: incrementem el marcador per a cada equip
            marcadorMachine.PosarMarcadorA0();

            //Then: els punts s'han incrementat
            Assert.Equal(expectedPuntsLocal, marcadorMachine.PuntsLocal);
            Assert.Equal(expectedPuntsVisitant, marcadorMachine.PuntsVisitant);
        }

        [Fact]
        public void AvisaCorrectamentDelsCanvisAlMarcador()
        {
            //Given: Una sèrie d'accions que provoquen canvis
            IMarcadorMachine marcadorMachine = new MarcadorMachine();

            var comptador_de_canvis = 0;
            marcadorMachine.CanvisAlMarcador += (marcador, e) => comptador_de_canvis++;

            List<Action> AccionsQueProvoquenCanvis = new()
            {
                () => marcadorMachine.IncrementaLocal(),
                () => marcadorMachine.IncrementaVisitant(),
                () => marcadorMachine.PosarMarcadorA0(),
                () => marcadorMachine.AfegeixExpectador(),
            };

            var expectedNumeroDeCanvis = AccionsQueProvoquenCanvis.Count;

            //When: Invoquem les accions
            AccionsQueProvoquenCanvis.ForEach(accio => accio());

            //Then: Rebem les notificacions de canvis
            Assert.Equal(expectedNumeroDeCanvis, comptador_de_canvis);
        }

        // Helpers ----------------------------
        private static void _FesPujarElMarcador(IMarcadorMachine marcadorMachine, int locals, int visitants)
        {
            Enumerable.Range(0, locals).ToList().ForEach(_ => marcadorMachine.IncrementaLocal());
            Enumerable.Range(0, visitants).ToList().ForEach(_ => marcadorMachine.IncrementaVisitant());
        }
    }
}
