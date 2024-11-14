using ClinicaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaData.Contrato
{
    public interface IRecepcionistaRepositorio
    {
        Task<List<Cita>> ListaCitasPendiente();

    }
}
