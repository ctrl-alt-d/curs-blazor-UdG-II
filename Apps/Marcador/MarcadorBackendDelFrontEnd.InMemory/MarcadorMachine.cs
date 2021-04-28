using System;
using MarcadorBackendDelFrontEnd.Abstracts;

namespace MarcadorBackendDelFrontEnd.InMemory
{
    public class MarcadorMachine : IMarcadorMachine
    {
        public virtual int PuntsLocal {get; private set;}

        public virtual int PuntsVisitant {get; private set;}

        protected virtual string _NomLocal {get; set;} = "Local";
        protected virtual string _NomVisitant {get; set;} = "Visitant";
        public virtual string NomLocal {
            get => _NomLocal;
            set
            {
                _NomLocal=value;
                AvisaCanvis();
            }
        }
        public virtual string NomVisitant {
            get => _NomVisitant;
            set
            {
                _NomVisitant=value;
                AvisaCanvis();
            }
        }

        public virtual int NumeroExpectadors {get; protected set;}

        public virtual event EventHandler? CanvisAlMarcador;

        public virtual void IncrementaLocal()
        {
            PuntsLocal++;
            AvisaCanvis();
        }

        public virtual void IncrementaVisitant()
        {
            PuntsVisitant++;
            AvisaCanvis();
        }

        public virtual void PosarMarcadorA0()
        {
            PuntsLocal = 0;
            PuntsVisitant = 0;
            AvisaCanvis();
        }

        protected virtual void AvisaCanvis()
        {
            var e = new EventArgs();
            CanvisAlMarcador?.Invoke(this, e);
        }

        public virtual void AfegeixExpectador()
        {
            NumeroExpectadors++;
            AvisaCanvis();
        }

        public virtual void EliminaExpectador()
        {
            NumeroExpectadors--;
            AvisaCanvis();
        }
    }
}
