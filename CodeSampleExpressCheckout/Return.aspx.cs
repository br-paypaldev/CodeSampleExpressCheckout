using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace CodeSampleExpressCheckout
{
   public partial class Return : System.Web.UI.Page
    {
        /// <summary>
        /// Quando o cliente for redirecionado de volta para a loja pelo PayPal, ele cairá nessa página,
        /// onde chamaremos o método finalize da classe Checkout informando o token e PayerID enviados
        /// pelo PayPal.
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// E.
        /// </param>
        protected virtual void Page_LoadComplete(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string PayerID = Request.QueryString["PayerID"];

            //O método finalize, da classe Checkout, vai retornar um NameValueCollection com os dados
            //obtidos na chamada à operação GetExpressCheckoutDetails. Utilizamos esses dados para
            //popular a base de dados com informações do cliente, etc.
            NameValueCollection nvp = Checkout.finalize(token, PayerID);

            //ACK conterá o status, caso bem sucedido, será Success.
            header.InnerText = nvp["ACK"]; //Coloca o valor do ACK no H1

            createAndPopulateTable(nvp);
        }

        /// <summary>
        /// Aqui uma pequena demonstração de como obter os dados do NVP.
        /// </summary>
        /// <param name='nvp'>
        /// Nvp.
        /// </param>
        private void createAndPopulateTable(NameValueCollection nvp)
        {
            Table table;

            table = createTableWithCaption("Dados do Cliente");

            createTableRowWithNameAndValue(table, "Nome do cliente", nvp["FIRSTNAME"] + " " + nvp["LASTNAME"]);
            createTableRowWithNameAndValue(table, "Email", nvp["EMAIL"]);
            createTableRowWithNameAndValue(table, "ID do cliente no PayPal", nvp["PAYERID"]);

            table = createTableWithCaption("Dados de Entrega");

            createTableRowWithNameAndValue(table, "Entregar para", nvp["PAYMENTREQUEST_0_SHIPTONAME"]);
            createTableRowWithNameAndValue(table, "Endereço", nvp["PAYMENTREQUEST_0_SHIPTOSTREET"]);
            createTableRowWithNameAndValue(table, "Cidade", nvp["PAYMENTREQUEST_0_SHIPTOCITY"]);
            createTableRowWithNameAndValue(table, "Estado", nvp["PAYMENTREQUEST_0_SHIPTOSTATE"]);
            createTableRowWithNameAndValue(table, "CEP", nvp["PAYMENTREQUEST_0_SHIPTOZIP"]);
            createTableRowWithNameAndValue(table, "País", nvp["PAYMENTREQUEST_0_SHIPTOCOUNTRYNAME"]);
        }

        private Table createTableWithCaption(string caption)
        {
            Table table = new Table();

            TableHeaderRow captionRow = new TableHeaderRow();
            TableHeaderCell captionCell = new TableHeaderCell();

            captionCell.Text = caption;
            captionCell.HorizontalAlign = HorizontalAlign.Left;
            captionCell.ColumnSpan = 2;

            captionRow.Cells.Add(captionCell);

            table.Rows.Add(captionRow);

            data.Controls.Add(table);

            return table;
        }

        private void createTableRowWithNameAndValue(Table table, string name, string value)
        {
            TableRow row = new TableRow();
            TableCell nameCell = new TableCell();
            TableCell valueCell = new TableCell();

            nameCell.Text = name;
            nameCell.Width = 300;

            valueCell.Text = value;
            valueCell.Width = 400;

            row.Cells.Add(nameCell);
            row.Cells.Add(valueCell);

            table.Rows.Add(row);
        }
    }
}