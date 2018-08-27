using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Activities;
using System.Data;
using System.Drawing;
using System.ComponentModel;

namespace Ciphix.DataVisualization
{
    public class PlotTable : CodeActivity
    {
        private DataGridView myDataGridView = new DataGridView();
        private CodeActivityContext contextGlobal;

        [Category("Input"), Description("The datatable to display in the form"), RequiredArgument]
        public InArgument<DataTable> InputTable { get; set; }

        [Category("Input"), Description("The name of the datatable")]
        public InArgument<String> Suffix { get; set; }

        [Category("Misc"), Description("Stop or continue execution when displaying the window"), DefaultValue(true)]
        public InArgument<bool> ShowAsDialog { get; set; }

        [Category("Output"), Description("The datatable when the window is closed")]
        public OutArgument<DataTable> OutputTable { get; set; }

        [Category("Output"), Description("Properties of the last selected cell")]
        public OutArgument<Dictionary<string, string>> SelectedCell { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable inputTable = InputTable.Get(context);
            String suffix = Suffix.Get(context);
            this.contextGlobal = context;
            //Create the empty form

            Form form = new Form();
            form.FormClosing += new FormClosingEventHandler(Form_Closing);

            //Check if suffix is set
            if (suffix == null || suffix == "")
            {
                form.Text = "Data viewer";                    
            }
            else
            {
                form.Text = "Data viewer: " + suffix;
            }
                
            form.Controls.Add(myDataGridView);
            myDataGridView.Dock = DockStyle.Fill;
            myDataGridView.DataSource = inputTable;

            //We make every column NotSortable
            foreach (DataGridViewColumn column in myDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            
            form.WindowState = FormWindowState.Maximized;
            if (ShowAsDialog.Expression == null || ShowAsDialog.Get(context))
            {
                form.ShowDialog();
            }
            else
            {
                form.Show();
            }

            form.WindowState = FormWindowState.Maximized;
            form.BringToFront();
            form.Focus();
        }


        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            DataTable outputTable = myDataGridView.DataSource as DataTable;
            OutputTable.Set(this.contextGlobal, outputTable);
            //Get info of selected Cell
            if (this.myDataGridView.SelectedCells.Count > 0)
            {
                Dictionary<string, string> props = new Dictionary<string, string>();
                int rowIndex = myDataGridView.CurrentCell.RowIndex;
                int columnIndex = myDataGridView.CurrentCell.ColumnIndex;
                String columnName = myDataGridView.Columns[columnIndex].Name;
                String value = myDataGridView.CurrentCell.Value.ToString();

                props.Add("rowIndex", rowIndex.ToString());
                props.Add("columnIndex", columnIndex.ToString());
                props.Add("columnName", columnName);
                props.Add("value", value);

                SelectedCell.Set(this.contextGlobal, props);
            }
            e.Cancel = false;
            
        }

    }
}