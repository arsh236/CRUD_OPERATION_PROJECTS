using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_OPERATION_PROJECTS
{
    public partial class Add_emp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetFocus(txt_emp_name);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
           
            string empname = txt_emp_name.Text;
            string empdsc = txt_emp_desc.Text;

            bool isSucces = Insert_EMP_DETAILS(empname, empdsc);
            if (isSucces)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showAlert('Data saved successfully!')", true);

                txt_emp_desc.Text = txt_emp_name.Text = string.Empty;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showAlert('Failed to save .')", true);
            }

        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            txt_emp_name.Text = txt_emp_desc.Text = string.Empty;
        }


        public bool Insert_EMP_DETAILS(string empname, string empdesc)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OCS"].ConnectionString;

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    int empid;
                    using (OracleCommand idcmt = new OracleCommand("select nvl(max(PKN_EMP_ID)+1,1) IDNext from EmployeeMaster", connection))
                    {
                        empid = Convert.ToInt32(idcmt.ExecuteScalar().ToString());
                    }


                    using (OracleCommand cmd = connection.CreateCommand())
                    {

                        cmd.CommandText = "INSERT INTO EMPLOYEEMASTER (PKN_EMP_ID,C_EMP_NAME, C_EMP_DESCRIPTION) VALUES (:PKN_EMP_ID,:C_EMP_NAME,:C_EMP_DESCRIPTION)";
                        cmd.Parameters.Add(":PKN_EMP_ID", OracleDbType.Int32).Value = empid;
                        cmd.Parameters.Add(":C_EMP_NAME", OracleDbType.Varchar2).Value = empname;
                        cmd.Parameters.Add(":C_EMP_DESCRIPTION", OracleDbType.Varchar2).Value = empdesc;


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;

                    }
                }


            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}