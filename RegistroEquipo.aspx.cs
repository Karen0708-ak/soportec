using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace soportec
{
    public partial class RegistroEquipo : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarSistemasOperativos();
        }
        private void CargarSistemasOperativos()
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("obtener_nombres_sistemas_operativos", con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("insertar_equipo", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@marca", txtMarca.Text);
                    cmd.Parameters.AddWithValue("@modelo", txtModelo.Text);
                    cmd.Parameters.AddWithValue("@procesador", txtProcesador.Text);
                    cmd.Parameters.AddWithValue("@fotoe", txtFoto.Text);
                    cmd.Parameters.AddWithValue("@fichatec", txtFicha.Text);
                    cmd.Parameters.AddWithValue("@id_so", ddlSO.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}