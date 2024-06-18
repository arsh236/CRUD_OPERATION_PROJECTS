using CRUD_OPERATION_PROJECTS.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_OPERATION_PROJECTS
{
    public partial class View_emp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //List<EmployeeMaster> employees = new List<EmployeeMaster>
                //{
                //    new EmployeeMaster { PKN_EMP_ID = 1, C_EMP_NAME = "John Doe", C_EMP_DESCRIPTION = "Manager" },
                //    new EmployeeMaster { PKN_EMP_ID = 2, C_EMP_NAME = "Jane Smith", C_EMP_DESCRIPTION = "Developer" },
                //    new EmployeeMaster { PKN_EMP_ID = 3, C_EMP_NAME = "Sam Brown", C_EMP_DESCRIPTION = "Designer" }
                //};

                //EMP_datatable.Add(Bind_Emp_Master_Data(employees));

                //Bind_Emp_Master_Data_TO_GV();

                Bind_Emp_Master();
            }
        }

        private void Bind_Emp_Master()
        {
            List<EmployeeMaster> employees = GetEmployeeDataFromDatabase();

            EMP_datatable.Add(Bind_Emp_Master_Data(employees));

            Bind_Emp_Master_Data_TO_GV();

            // DataTable employeeTable = Bind_Emp_Master_Data(employees);

            //gv_employees.DataSource = employeeTable;
            //gv_employees.DataKeyNames = new string[] { "PKN_EMP_ID" };
            //gv_employees.DataBind();
        }

        private List<EmployeeMaster> GetEmployeeDataFromDatabase()
        {
            try
            {
                List<EmployeeMaster> employees = new List<EmployeeMaster>();
                string connectionString = ConfigurationManager.ConnectionStrings["OCS"].ConnectionString;

                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    string query = "SELECT PKN_EMP_ID, C_EMP_NAME, C_EMP_DESCRIPTION FROM EmployeeMaster order by PKN_EMP_ID ";
                    OracleCommand cmd = new OracleCommand(query, conn);
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new EmployeeMaster
                            {
                                PKN_EMP_ID = reader["PKN_EMP_ID"] == DBNull.Value ? 0 : Convert.ToInt32( reader["PKN_EMP_ID"].ToString()),
                                C_EMP_NAME = reader["C_EMP_NAME"] == DBNull.Value ? string.Empty : reader["C_EMP_NAME"].ToString(),
                                C_EMP_DESCRIPTION = reader["C_EMP_DESCRIPTION"] == DBNull.Value ? string.Empty : reader["C_EMP_DESCRIPTION"].ToString()
                            });
                        }
                    }
                }

                return employees;
            }
            catch (OracleException)
            {
                throw;
            }
            catch (Exception )
            {
                throw;
            }
            
        }

        protected List<DataTable> EMP_datatable
        {
            get
            {
                if (ViewState["EMP_datatable"] == null)
                {
                    ViewState["EMP_datatable"] = new List<DataTable>();
                }
                return (List<DataTable>)ViewState["EMP_datatable"];
            }
            set
            {
                ViewState["EMP_datatable"] = value;
            }
        }

        private void Bind_Emp_Master_Data_TO_GV()
        {
            DataTable MergedData = new DataTable();

            MergedData.Columns.Add("PKN_EMP_ID");
            MergedData.Columns.Add("C_EMP_NAME");
            MergedData.Columns.Add("C_EMP_DESCRIPTION");
          
            foreach (DataTable table in EMP_datatable)
            {
                MergedData.Merge(table);
            }

            gv_employees.DataSource = MergedData;
            gv_employees.DataBind();

        }

        private DataTable Bind_Emp_Master_Data(List<EmployeeMaster> employees)
        {
            DataTable gridData = new DataTable();
            gridData.Columns.Add("PKN_EMP_ID");
            gridData.Columns.Add("C_EMP_NAME");
            gridData.Columns.Add("C_EMP_DESCRIPTION");
          
            foreach (var IM in employees)
            {
                DataRow dataRow = gridData.NewRow();
                dataRow["PKN_EMP_ID"] = IM.PKN_EMP_ID;
                dataRow["C_EMP_NAME"] = IM.C_EMP_NAME;
                dataRow["C_EMP_DESCRIPTION"] = IM.C_EMP_DESCRIPTION;
               
                gridData.Rows.Add(dataRow);
            }
            return gridData;
        }


        protected void gv_employees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowindex = e.NewEditIndex;
            gv_employees.EditIndex = rowindex;

            // Rebind the GridView
            Bind_Emp_Master_Data_TO_GV();

            // Set focus to the desired control in the edited row
            GridViewRow row = gv_employees.Rows[rowindex];
            TextBox txt_gv_name = (TextBox)row.FindControl("txtGVname");
            SetFocus(txt_gv_name);

        }

        protected void gv_employees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_employees.EditIndex = -1;
            Bind_Emp_Master_Data_TO_GV();
        }

        protected void gv_employees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
          
            GridViewRow row = gv_employees.Rows[e.RowIndex];
            int empID = Convert.ToInt32(gv_employees.DataKeys[e.RowIndex].Values["PKN_EMP_ID"]);
            TextBox txtName = (TextBox)row.FindControl("txtGVname");
            TextBox txtDescription = (TextBox)row.FindControl("txtGVdesignation");
            string name = txtName.Text;
            string description = txtDescription.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showToast('Please enter Name.')", true);
                txtName.CssClass = "border border-danger";
                return;
            }
            else if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showToast('Please enter descripton.')", true);
                txtDescription.CssClass = "border border-danger";
                return;
            }
            else
            {
                txtName.CssClass = "gvtextupdate form-control";
                txtDescription.CssClass = "gvtextupdate form-control";
            }

            UpdateEmployee(empID, name, description);



            gv_employees.EditIndex = -1;
            Bind_Emp_Master_Data_TO_GV();

            
        }

        protected void gv_employees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
          
            int empId = Convert.ToInt32(gv_employees.DataKeys[e.RowIndex].Values["PKN_EMP_ID"]);
            Session["empID"] = empId;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showAlertQuestBox('delete..??')", true);

           
            Bind_Emp_Master_Data_TO_GV();

        }

        protected void btn_Popup_Msg_Click(object sender, EventArgs e)
        {
            int empid = Convert.ToInt32(Session["empID"]);
            DeleteEmployee(empid);
        }

        private void UpdateEmployee(int empId, string name, string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OCS"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE EmployeeMaster SET C_EMP_NAME = :name, C_EMP_DESCRIPTION = :description WHERE PKN_EMP_ID = :empId";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add(":description", OracleDbType.Varchar2).Value = description;
                cmd.Parameters.Add(":empId", OracleDbType.Int32).Value = empId;

                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteEmployee(int empId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OCS"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                string query = "DELETE FROM EmployeeMaster WHERE PKN_EMP_ID = :empId";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add(new OracleParameter("empId", empId));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}