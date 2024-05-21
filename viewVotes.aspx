<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewVotes.aspx.cs" Inherits="EksamenV._2_1_.viewVotes" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   
    <form id="form1" runat="server">
        <div align ="center">
            
        <asp:Label ID="Label1" runat="server" Text="Landoversikt av stemmene"></asp:Label>
            <br />
           <asp:Chart ID="Chart1" runat="server" Width="488px">  
        <series>  
            <asp:Series Name="Series1" XValueMember="0" YValueMembers="2">  
            </asp:Series>  
        </series>  
        <chartareas>  
            <asp:ChartArea Name="ChartArea1">  
            </asp:ChartArea>  
        </chartareas>  
</asp:Chart>
        </div>

        <br /><br />

        <div align ="center">
            <asp:Label ID="Label2" runat="server" Text="Landoversikt av stemmene i prosent"></asp:Label>
            <br />
            <asp:Chart ID="Chart2" runat="server">
                <Series>
                    <asp:Series Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>

        <br /><br />

        <div align ="center">
            <asp:DropDownList ID="kommuDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="kommuDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <br />
            <asp:Chart ID="Chart3" runat="server">
                <Series>
                    <asp:Series Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>

        <div align ="center">
            <asp:Label ID="Totaloversikt" runat="server" Text="Totaloversikt"></asp:Label>
            <br />
            <asp:GridView ID="totaltOversiktGV" runat="server">
               <%-- <Columns>
                    <asp:BoundField DataField="Kommune" HeaderText="Kommune" />
                    <asp:BoundField DataField="PartiNavn" HeaderText="Parti" />
                    <asp:BoundField DataField="stemmer" HeaderText="Stemmer" />
                </Columns>--%>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
