using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace soportec
{
    public partial class _Default : Page
    {
        // Variables para conectar
        string cadenaConexion;
        SqlConnection conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString;
            this.conexion = new SqlConnection(cadenaConexion);

            if (!IsPostBack)
            {
                CargarEquipos();
            }
        }

        private void CargarEquipos()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarEquipos", conexion)) // SP ya creado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    gridEquipos.DataSource = tabla;
                    gridEquipos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los equipos: " + ex.Message;
            }
        }

        protected void gridEquipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Validar si CommandArgument tiene un valor
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (index >= 0 && index < gridEquipos.Rows.Count) // Validación del índice
                {
                    int idEquipo = Convert.ToInt32(gridEquipos.DataKeys[index].Value);

                    if (e.CommandName == "EditarRegistro")
                    {
                        Response.Redirect($"EditarEquipo.aspx?id={idEquipo}");
                    }
                    else if (e.CommandName == "EliminarRegistro")
                    {
                        EliminarEquipo(idEquipo);
                    }
                }
            }
        }

        private void EliminarEquipo(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarEquipo", conexion)) // SP ya creado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_equipo", id);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
                CargarEquipos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
            }
            finally
            {
                conexion.Close(); // Asegurar cierre de conexión
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroEquipo.aspx");
        }
    }
}
