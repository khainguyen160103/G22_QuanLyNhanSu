using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhanSu
{
    public partial class qlPhongBan : Page
    {
        protected List<Department> Departments { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Departments = new List<Department>();
                Session["Departments"] = Departments;
            }
            else
            {
                Departments = (List<Department>)Session["Departments"];
            }

            BindRepeater();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();

            bool hasError = false;
            // validate
            if (string.IsNullOrEmpty(name))
            {
                lblErrorName.Text = "Tên phòng ban không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorName.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(description))
            {
                lblErrorDescription.Text = "Mô tả không được để trống!";
                hasError = true;
            }
            else
            {
                lblErrorDescription.Text = string.Empty;
            }

            if (hasError)
            {
                return;
            }

            Departments.Add(new Department { Name = name, Description = description });

            Session["Departments"] = Departments;

            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            lblErrorName.Text = string.Empty;
            lblErrorDescription.Text = string.Empty;

            BindRepeater();
        }




        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;

            int index = Convert.ToInt32(btnEdit.CommandArgument);

            txtName.Text = Departments[index].Name;
            txtDescription.Text = Departments[index].Description;

            hfEditIndex.Value = index.ToString();

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(hfEditIndex.Value);

            Departments[index].Name = txtName.Text;
            Departments[index].Description = txtDescription.Text;

            Session["Departments"] = Departments;

            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            btnAdd.Visible = true;
            btnUpdate.Visible = false;

            BindRepeater();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;

            int index = Convert.ToInt32(btnDelete.CommandArgument);

            Departments.RemoveAt(index);

            Session["Departments"] = Departments;

            BindRepeater();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filteredDepartments = Departments
                .Where(d => d.Name.ToLower().Contains(keyword) || d.Description.ToLower().Contains(keyword))
                .ToList();

            departmentRepeater.DataSource = filteredDepartments;
            departmentRepeater.DataBind();
        }

        private void BindRepeater()
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filteredDepartments = string.IsNullOrEmpty(keyword)
                ? Departments
                : Departments
                    .Where(d => d.Name.ToLower().Contains(keyword) || d.Description.ToLower().Contains(keyword))
                    .ToList();

            departmentRepeater.DataSource = filteredDepartments;
            departmentRepeater.DataBind();
        }

        public class Department
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
