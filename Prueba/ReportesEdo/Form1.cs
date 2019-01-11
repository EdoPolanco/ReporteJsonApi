using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using reportes.Negocio;
using System.Windows.Forms;


namespace ReportesEdo
{
    public partial class Form1 : Form
    {
        private Datos datos = new Datos();
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cargarGV2();
            cargarGV();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void cargarGV()
        {
            try
            {
                
                List<Cliente> cli = new List<Cliente>();
                cli = Datos.getCliente();
                Direccion direccion = new Direccion();
                Cliente cliente = new Cliente();
                foreach (var aux in cli)
                {
                    List<Direccion> dir = new List<Direccion>();
                    List<Cliente> cli2 = new List<Cliente>();

                    cliente.id = aux.id;
                    cliente.nombre = aux.nombre;
                    cliente.apellido = aux.apellido;
                    cliente.telefono = aux.telefono;
                    cliente.rut = aux.rut;
                    cliente.fechaNacimiento = aux.fechaNacimiento;
                    direccion.calle = aux.direccion.calle;
                    direccion.numero = aux.direccion.numero;
                    direccion.comuna = aux.direccion.comuna;
                    cliente.activo = aux.activo;
                    cli2.Add(cliente);
                    dataGridView1.ColumnCount = 10;
                    dataGridView1.ColumnHeadersVisible = true;
                    dataGridView1.Columns[0].Name = "id";
                    dataGridView1.Columns[1].Name = "nombre";
                    dataGridView1.Columns[2].Name = "apellido";
                    dataGridView1.Columns[3].Name = "telefono";
                    dataGridView1.Columns[4].Name = "rut";
                    dataGridView1.Columns[5].Name = "fecha nacimineto";
                    dataGridView1.Columns[6].Name = "calle";
                    dataGridView1.Columns[7].Name = "numero";
                    dataGridView1.Columns[8].Name = "comuna";
                    dataGridView1.Columns[9].Name = "activo";

                    string comuna = string.IsNullOrWhiteSpace(direccion.comuna) ? "hola" : direccion.comuna;
                    string[] row1 = new string[] { cliente.id.ToString(), cliente.nombre, cliente.apellido, Convert.ToString(cliente.telefono), cliente.rut, cliente.fechaNacimiento, direccion.calle, Convert.ToString(direccion.numero), comuna, Convert.ToString(cliente.activo) };
                    
                    object[] rows = new object[] { row1 };
                    foreach (string[] rowArray in rows)
                    {
                        dataGridView1.Rows.Add(rowArray);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void cargarGV2()
        {
            try
            {
                // instanciamos una lista de cliente para luego traspasar los datos consumidos de api.
                string dato = textBox1.Text.Trim();
                List<Cliente> cli = new List<Cliente>();
                cli = Datos.getCliente();
                Direccion direccion = new Direccion();
                Cliente cliente = new Cliente();
                // en esta linea hacemos que los datos recividos sean iguales al dato de entrada del textBox1 para filtrar.
                foreach (var aux in cli.Where(x => x.rut.Contains(dato)).ToList())
                {
                    List<Direccion> dir = new List<Direccion>();
                    List<Cliente> cli2 = new List<Cliente>();

                    cliente.id = aux.id;
                    cliente.nombre = aux.nombre;
                    cliente.apellido = aux.apellido;
                    cliente.telefono = aux.telefono;
                    cliente.rut = aux.rut;
                    cliente.fechaNacimiento = aux.fechaNacimiento;
                    direccion.calle = aux.direccion.calle;
                    direccion.numero = aux.direccion.numero;
                    direccion.comuna = aux.direccion.comuna;
                    cliente.activo = aux.activo;
                    cli2.Add(cliente);
                    dataGridView1.ColumnCount = 10;
                    dataGridView1.ColumnHeadersVisible = true;
                    dataGridView1.Columns[0].Name = "id";
                    dataGridView1.Columns[1].Name = "nombre";
                    dataGridView1.Columns[2].Name = "apellido";
                    dataGridView1.Columns[3].Name = "telefono";
                    dataGridView1.Columns[4].Name = "rut";
                    dataGridView1.Columns[5].Name = "fecha nacimineto";
                    dataGridView1.Columns[6].Name = "calle";
                    dataGridView1.Columns[7].Name = "numero";
                    dataGridView1.Columns[8].Name = "comuna";
                    dataGridView1.Columns[9].Name = "activo";

                    string comuna = string.IsNullOrWhiteSpace(direccion.comuna) ? "hola" : direccion.comuna;
                    string[] row1 = new string[] { cliente.id.ToString(), cliente.nombre, cliente.apellido, Convert.ToString(cliente.telefono), cliente.rut, cliente.fechaNacimiento, direccion.calle, Convert.ToString(direccion.numero), comuna, Convert.ToString(cliente.activo) };

                    object[] rows = new object[] { row1 };
                    foreach (string[] rowArray in rows)
                    {
                        dataGridView1.Rows.Add(rowArray);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // en esta parte hacemos que las columnas y las filas tengan igual medida
            dataGridView1.AutoResizeColumnHeadersHeight();
            dataGridView1.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            cargarGV();
            try
            {
                //Definimos como queremos que sea nuestra tabla en el PDF
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                PdfPTable pdftable = new PdfPTable(dataGridView1.ColumnCount);
                pdftable.DefaultCell.Padding = 4;
                pdftable.WidthPercentage = 100;
                pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdftable.DefaultCell.BorderWidth = 1;
                iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                //agregaremos las columnas
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdftable.AddCell(cell);
                }

                //agregaremos las filas
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdftable.AddCell(new Phrase(cell.Value.ToString(), text));

                    }
                }
                var savefiles = new SaveFileDialog();
                savefiles.FileName = "reporte";
                savefiles.DefaultExt = ".pdf";
                if (savefiles.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefiles.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(pdftable);
                        pdfDoc.Close();
                        stream.Close();
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargarGV2();
            try
            {
                //Definimos como queremos que sea nuestra tabla en el PDF
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                PdfPTable pdftable = new PdfPTable(dataGridView1.ColumnCount);
                pdftable.DefaultCell.Padding = 4;
                pdftable.WidthPercentage = 100;
                pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdftable.DefaultCell.BorderWidth = 1;
                iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                //agregaremos las columnas
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdftable.AddCell(cell);
                }

                //agregaremos las filas
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdftable.AddCell(new Phrase(cell.Value.ToString(), text));

                    }
                }
                var savefiles = new SaveFileDialog();
                savefiles.FileName = "reporte";
                savefiles.DefaultExt = ".pdf";
                if (savefiles.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefiles.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(pdftable);
                        pdfDoc.Close();
                        stream.Close();
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
