using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeSampleExpressCheckout.Model.CodeSampleExpressCheckout;


namespace CodeSampleExpressCheckout
{
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Cria e popula a tabela do carrinho com alguns itens pré-configurados.
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// E.
        /// </param>
        protected virtual void Page_LoadComplete(object sender, EventArgs e)
        {
            products.Controls.Add(createTable());
        }

        /// <summary>
        /// Manipulador do clique no botão Checkout, na página do carrinho. Esse botão iniciará
        /// o processo de checkout, chamando o método start da classe Checkout, que faz a chamada
        /// à operação SetExpressCheckout.
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// E.
        /// </param>
        protected virtual void checkoutClickHandler(object sender, EventArgs e)
        {
            Response.Redirect(Checkout.start(
            "http://127.0.0.1:49682/Return.aspx", "http://127.0.0.1:49682/Cancel.aspx"
            ));
        }

        private Table createTable()
        {
            Table table = new Table();

            createTableHeader(table);
            populateTable(table);

            return table;
        }

        private void createTableHeader(Table table)
        {
            TableRow row = new TableHeaderRow();
            TableCell productCell = new TableHeaderCell();
            TableCell descriptionCell = new TableHeaderCell();
            TableCell quantityCell = new TableHeaderCell();
            TableCell priceCell = new TableHeaderCell();
            TableCell totalCell = new TableHeaderCell();

            productCell.Text = "Produto";
            descriptionCell.Text = "Descrição";
            quantityCell.Text = "Quantitade";
            priceCell.Text = "Preço";
            totalCell.Text = "Total";

            row.Cells.Add(productCell);
            row.Cells.Add(descriptionCell);
            row.Cells.Add(quantityCell);
            row.Cells.Add(priceCell);
            row.Cells.Add(totalCell);

            table.Rows.Add(row);
        }

        private void populateTable(Table table)
        {
            Dictionary<Product, int> items = Cart.Instance().Items;

            foreach (Product product in items.Keys)
            {
                TableRow row = new TableRow();
                TableCell productCell = new TableCell();
                TableCell descriptionCell = new TableCell();
                TableCell quantityCell = new TableCell();
                TableCell priceCell = new TableCell();
                TableCell totalCell = new TableCell();

                int quantity = items[product];

                productCell.Text = product.Name;
                descriptionCell.Text = product.Description;
                quantityCell.HorizontalAlign = HorizontalAlign.Center;
                quantityCell.Text = quantity.ToString();

                priceCell.Text = String.Format("R$ {0:0.00}", product.Price);
                totalCell.Text = String.Format("R$ {0:0.00}", quantity * product.Price);

                row.Cells.Add(productCell);
                row.Cells.Add(descriptionCell);
                row.Cells.Add(quantityCell);
                row.Cells.Add(priceCell);
                row.Cells.Add(totalCell);

                table.Rows.Add(row);
            }
        }
    }
}

