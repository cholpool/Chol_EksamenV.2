<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="EksamenV._2_1_.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script type="text/javascript">
        function validateFødselNr()
        {
            var fødselNr = document.getElementById('<%= FødselNrTextBox.ClientID %>').value;
            if (fødselNr.length !== 11) {
                alert('Please enter exactly 11 digits.');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align ="center">
            <fieldset style ="width: 300px">
                <legend>Sett inn fødsølnumeret ditt</legend>
                <br />
                <asp:TextBox ID="FødselNrTextBox" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Button ID="UserLoginBT" runat="server" Text="Button" OnClick="UserLoginBT_Click" style="height: 26px" />
            </fieldset>
        </div>
    </form>
</body>
</html>
