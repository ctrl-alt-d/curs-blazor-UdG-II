using System;

namespace MarcadorBackendDelFrontEnd.Abstracts
{
    public interface IMarcadorMachine
    {
        int PuntsLocal {get; }
        int PuntsVisitant {get; }
        string NomLocal {get; set;}
        string NomVisitant {get; set;}
        void PosarMarcadorA0();
        event EventHandler CanvisAlMarcador;  
        void IncrementaLocal();
        void IncrementaVisitant();
        int NumeroExpectadors {get;}
        void AfegeixExpectador();
        void EliminaExpectador();
    }
}
