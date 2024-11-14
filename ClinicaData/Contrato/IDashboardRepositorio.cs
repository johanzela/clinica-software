using ClinicaEntidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaData.Contrato
{
    public interface IDashboardRepositorio
    {

        Task<DashboardDTO> Obtener();

    }
}
