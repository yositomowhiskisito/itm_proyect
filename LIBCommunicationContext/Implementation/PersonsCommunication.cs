using LIBCommunicationContext.Core;
using System;

namespace LIBCommunicationContext.Implementations
{
    public class PersonsCommunication : Communication
    {
        protected override void LoadSettings()
        {
            base.LoadSettings();
            Service = "WSITM_Proyect";
            Name = "Persons";
        }
    }
}