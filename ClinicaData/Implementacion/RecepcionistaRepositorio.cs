using ClinicaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaData.Configuracion;
using Microsoft.Extensions.Options;
using ClinicaData.Contrato;

namespace ClinicaData.Implementacion
{
    public class RecepcionistaRepositorio : IRecepcionistaRepositorio
    {


        private readonly ConnectionStrings con;
        public RecepcionistaRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<List<Cita>> ListaCitasPendiente()
        {
            List<Cita> lista = new List<Cita>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listar_citas", conexion);
                //cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Cita()
                        {
                            IdCita = Convert.ToInt32(dr["IdCita"]),
                            FechaCita = dr["FechaCita"].ToString()!,
                            HoraCita = dr["HoraCita"].ToString()!,
                            Especialidad = new Especialidad()
                            {
                                Nombre = dr["NombreEspecialidad"].ToString()!,
                            },
                            Doctor = new Doctor()
                            {
                                Nombres = dr["Nombres"].ToString()!,
                                Apellidos = dr["Apellidos"].ToString()!,
                            }
                        });
                    }
                }
            }
            return lista;
        }

        //public Task<List<Cita>> ListaCitasPendiente()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
