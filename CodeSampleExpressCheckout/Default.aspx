<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CodeSampleExpressCheckout._Default" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <title>Code Sample - Express Checkout</title>
</head>
<body>
    <section>
        <header>
            <h1>Code Sample - Express Checkout</h1>
            <h2>Exemplo de integração Express Checkout com ASP.NET e C#</h2>
        </header>

            <form id="form" runat="server">
                <asp:PlaceHolder ID="products" runat="server"></asp:PlaceHolder>
                <asp:Button id="checkout" runat="server" OnClick="checkoutClickHandler" Text="Checkout"></asp:Button>
            </form>
    </section>
</body>
</html>