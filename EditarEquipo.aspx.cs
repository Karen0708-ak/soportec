using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;

namespace soportec
{
    public partial class EditarEquipo : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString;
        int idEquipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validar que el parámetro "id" venga en la URL y sea un número válido
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idEquipo))
                {
                    CargarSistemasOperativos();
                    CargarDatosEquipo(idEquipo);
                }
                else
                {
                    // Redirigir si no hay id válido
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void CargarSistemasOperativos()
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("obtener_nombres_sistemas_operativos", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlSO.DataSource = reader;
                ddlSO.DataTextField = "nombre";
                ddlSO.DataValueField = "id";
                ddlSO.DataBind();

                ddlSO.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione--", "0"));

                con.Close();
            }
        }

        private void CargarDatosEquipo(int id)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("consultar_equipo_por_id", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_equipo", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtNombre.Text = reader["nombre"].ToString();
                    txtMarca.Text = reader["marca"].ToString();
                    txtModelo.Text = reader["modelo"].ToString();
                    txtProcesador.Text = reader["procesador"].ToString();
                    txtFoto.Text = reader["fotoe"].ToString();
                    txtFicha.Text = reader["fichatec"].ToString();
                    ddlSO.SelectedValue = reader["id_so"].ToString();
                }

                con.Close();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);

                using (SqlConnection con = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("editar_equipo", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_equipo", id);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                    cmd.Parameters.AddWithValue("@modelo", txtModelo.Text);
                    cmd.Parameters.AddWithValue("@procesador", txtProcesador.Text);
                    cmd.Parameters.AddWithValue("@fotoe", txtFoto.Text);
                    cmd.Parameters.AddWithValue("@fichatec", txtFicha.Text);
                    cmd.Parameters.AddWithValue("@id_so", int.Parse(ddlSO.SelectedValue));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
