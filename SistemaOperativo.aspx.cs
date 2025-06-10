using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace soportec
{
    public partial class SistemaOperativo : System.Web.UI.Page
    {
        string cadenaConexion;
        SqlConnection conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSsitema"].ConnectionString;
            conexion = new SqlConnection(cadenaConexion);

            if (!IsPostBack)
            {
                consultarSO();
            }
        }

        public void limpiarCampos()
        {
            txt_nombre_so.Text = "";
            txt_version_so.Text = "";
            hf_id_so.Value = "";
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            lbl_mensaje.Text = "Formulario limpiado.";
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hf_id_so.Value))
            {
                insertarSO();
            }
            else
            {
                actualizarSO();
            }
        }

        private void insertarSO()
        {
            SqlCommand comando = new SqlCommand("sp_insertar_so", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@nombre_so", txt_nombre_so.Text);
            comando.Parameters.AddWithValue("@version_so", txt_version_so.Text);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();

                lbl_mensaje.Text = "Sistema Operativo insertado correctamente.";
                limpiarCampos();
                consultarSO();
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error: " + ex.Message;
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void actualizarSO()
        {
            SqlCommand comando = new SqlCommand("sp_actualizar_so", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@id_so", Convert.ToInt32(hf_id_so.Value));
            comando.Parameters.AddWithValue("@nombre_so", txt_nombre_so.Text);
            comando.Parameters.AddWithValue("@version_so", txt_version_so.Text);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();

                lbl_mensaje.Text = "Sistema Operativo actualizado correctamente.";
                limpiarCampos();
                consultarSO();
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error al actualizar: " + ex.Message;
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void consultarSO()
        {
            SqlCommand comando = new SqlCommand("sp_listar_so", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            try
            {
                conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                gv_so.DataSource = dt;
                gv_so.DataBind();
                conexion.Close();
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error al consultar: " + ex.Message;
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        protected void gv_so_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gv_so.DataKeys[e.RowIndex].Value);

            SqlCommand comando = new SqlCommand("sp_eliminar_so", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_so", id);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();

                lbl_mensaje.Text = "Sistema Operativo eliminado correctamente.";
                consultarSO();
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error al eliminar: " + ex.Message;
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        protected void gv_so_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarSO")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv_so.Rows[index];

                hf_id_so.Value = gv_so.DataKeys[index].Value.ToString();
                txt_nombre_so.Text = row.Cells[1].Text;
                txt_version_so.Text = row.Cells[2].Text;

                lbl_mensaje.Text = "Edición en curso...";
            }
        }
    }
}
