using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace soportec
{
    public partial class SistemaOperativo : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarSistemasOperativos();
            }
        }

        // Método para listar los sistemas operativos en el GridView
        void ListarSistemasOperativos()
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_ListarSistemasOperativos", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv_sistemas_operativos.DataSource = dt;
            gv_sistemas_operativos.DataBind();
        }

        // Método para agregar un nuevo sistema operativo
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre.Text.Trim();
            string version = txt_version.Text.Trim();
            string documentacion = txt_documentacion.Text.Trim();

            SqlCommand cmd = new SqlCommand("sp_AgregarSistemaOperativo", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@version", version);
            cmd.Parameters.AddWithValue("@documentacion", string.IsNullOrEmpty(documentacion) ? (object)DBNull.Value : documentacion);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            // Limpiar campos después de guardar
            txt_nombre.Text = "";
            txt_version.Text = "";
            txt_documentacion.Text = "";

            // Actualizar el GridView
            ListarSistemasOperativos();

            // Mensaje de éxito
            labl_mensajes.Text = "Sistema Operativo agregado correctamente.";
        }

        // Método para cancelar y limpiar los campos
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            txt_nombre.Text = "";
            txt_version.Text = "";
            txt_documentacion.Text = "";
            labl_mensajes.Text = "";
        }

        // Método para manejar las acciones de editar y eliminar
        protected void gv_sistemas_operativos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv_sistemas_operativos.Rows[index];
            int id = Convert.ToInt32(gv_sistemas_operativos.DataKeys[index].Value);

            if (e.CommandName == "EditarSO")
            {
                // Cargar datos en los controles de texto para editar
                hf_id_sistema_operativo.Value = id.ToString();
                txt_nombre.Text = row.Cells[1].Text;
                txt_version.Text = row.Cells[2].Text;
                txt_documentacion.Text = row.Cells[3].Text;
            }
            else if (e.CommandName == "EliminarSO")
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarSistemaOperativo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                // Actualizar el GridView después de eliminar
                ListarSistemasOperativos();

                // Mensaje de éxito
                labl_mensajes.Text = "Sistema Operativo eliminado correctamente.";
            }
        }

        // Método para actualizar los datos de un sistema operativo
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hf_id_sistema_operativo.Value);
            string nombre = txt_nombre.Text.Trim();
            string version = txt_version.Text.Trim();
            string documentacion = txt_documentacion.Text.Trim();

            SqlCommand cmd = new SqlCommand("sp_ActualizarSistemaOperativo", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@version", version);
            cmd.Parameters.AddWithValue("@documentacion", string.IsNullOrEmpty(documentacion) ? (object)DBNull.Value : documentacion);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            // Limpiar campos después de actualizar
            hf_id_sistema_operativo.Value = "";
            txt_nombre.Text = "";
            txt_version.Text = "";
            txt_documentacion.Text = "";

            // Actualizar el GridView
            ListarSistemasOperativos();

            // Mensaje de éxito
            labl_mensajes.Text = "Sistema Operativo actualizado correctamente.";
        }
        protected void gv_sistemas_operativos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gv_sistemas_operativos.DataKeys[e.RowIndex].Value);

            SqlCommand cmd = new SqlCommand("sp_EliminarSistemaOperativo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            // Actualizar el GridView después de eliminar
            ListarSistemasOperativos();

            // Mensaje de éxito
            labl_mensajes.Text = "Sistema Operativo eliminado correctamente.";
        }

    }
}