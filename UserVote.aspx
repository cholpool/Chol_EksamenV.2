<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserVote.aspx.cs" Inherits="EksamenV._2_1_.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align ="center" >
        <fieldset style ="width:200px;">
           <legend>Stemme</legend>
            <br />
        <asp:DropDownList ID="DropDownListKommu" runat="server">
            <asp:ListItem Selected="True" Value="0">Velg kommune</asp:ListItem>
            </asp:DropDownList>
        <br />
        <br />
             <asp:DropDownList ID="DropDownListParti" runat="server">
            <asp:ListItem Selected="True" Value="0">Velg party</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
             <asp:Button ID="StemButton" runat="server" Text="Stem" OnClick="Button_Click" />
            <br />
           
        </fieldset>
        </div>
    </form>
</body>
</html>
