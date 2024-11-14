using ClinicaData.Configuracion;
using ClinicaData.Contrato;
using ClinicaEntidades.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaData.Implementacion
{
    public class DashboardRepositorio : IDashboardRepositorio
    {

        private readonly ConnectionStrings _connectionStrings;

        public DashboardRepositorio(IOptions<ConnectionStrings> options)
        {

            _connectionStrings = options.Value; 
        }


        public async Task<DashboardDTO> Obtener()
        {
            DashboardDTO objeto = null!;
            using (var conexion = new SqlConnection(_connectionStrings.CadenaSQL))
            { 
            
            await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerDashboard", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        objeto = new DashboardDTO()
                        {
                            TotalDoctor = Convert.ToInt32(dr["TotalDoctores"]),
                            TotalEspecialidad = Convert.ToInt32(dr["TotalEspecialidades"]),
                            TotalCitaPendiente = Convert.ToInt32(dr["TotalCitaPendientes"]),
                            TotalCitaAtendida = Convert.ToInt32(dr["TotalCitaAtendidas"]),
                             TotalCitaAnulada = Convert.ToInt32(dr["TotalCitaAnuladas"])
                        };
                    }
                }



            }
            return objeto;
        }
    }
}
