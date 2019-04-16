<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Score.ComponentManager.Shell.Applications.ComponentManager.Dashboard" %>
<%@ Import Namespace="Score.ComponentManager.Shell.Applications.ComponentManager" %>
<%@ Import Namespace="Score.ComponentManager.Parts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Areas/Score/js/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="../../Areas/SageDotCom/js/SiteCluster/cluster.js?v=#BUILD-VERSION#" type="text/javascript"></script>
    
<link rel="stylesheet" type="text/css" href="/sitecore/shell/themes/standard/default/WebFramework.css" />
<link rel="stylesheet" type="text/css" href="/sitecore/shell/themes/standard/default/calendar.css" />
<link rel="stylesheet" type="text/css" href="/sitecore/admin/default.css" />
    <link href="/sitecore/shell/Applications/Security/ItemSecurityEditor/ItemSecurityEditor.css" rel="stylesheet" />


<style type="text/css">
body{text-align:left;}
    .scEditorHeader {
        padding: 30px 15px;
        height: 35px;
        background-color: #fff;
    }

    .scEditorHeaderIcon, a.scEditorHeaderIcon, a.scEditorHeaderIcon:link, a.scEditorHeaderIcon:hover, a.scEditorHeaderIcon:active, a.scEditorHeaderIcon:visited {
        float: left;
    }

    .ff a {
        outline: none;
    }

    a, a:active, a:visited {
        text-decoration: none;
        color: #474747;
        outline: none;
    }

    .scEditorHeaderTitlePanel {
        float: left;
    }

    .scEditorHeaderTitle:hover {
        background-color: #E3E3E3;
        text-decoration: none;
    }

    .scEditorHeaderTitle, .scEditorHeaderTitle:link, .scEditorHeaderTitle:hover, .scEditorHeaderTitle:active, .scEditorHeaderTitle:visited {
        display: inline;
        font-size: 18px;
        white-space: nowrap;
        padding: 5px 15px;
        line-height: 32px;
    }

    .body-content {
        text-align: left;
    }

    .top-container {
        margin-top: 30px;
        margin-bottom: 30px;
    }

    body {
        background-color: white;
        background-image: none;
		font-family: "Open Sans", Arial, sans-serif;
    }

    h4 {
        float: left;
    }
    .market-name {
        padding-left:0px;
		position:relative;
		top:0px;
		
    }
    h2 {
        margin-bottom: 20px;
		font-weight:normal;
		text-align:left;
		padding-left:40px;
		position:relative;
		top:0px;
    }
    .form-tab {
        width: 600px;
    }
    .top-container {
        margin-left: 45px;
    }

    .master-tab-toggle {
        cursor: pointer;
    }
	
	
	
    td span {
        
        padding-left: 1px;
    }
	
	
	table td{
		text-align:left;
		padding-right:25px;
	}
	
	table td.action:hover{

	}
	
	.path{
	position:relative;
	top:-20px;
	left:40px;
	color:#999;
	}
	
	.flag{
		
	}
	
	
	.typeicon{
		position:relative;
		top:3px;
	}
	
	.active{
		color:black;
	}
	
	td.title{
		font-size:1.2em;
		font-weight:bold;
		color:#999;
	}
	
	td.location{
		
	}
	td.action{

	}
	hr{
		color:#eaeded ;
		
	}
    
    a.pathLink:link
    {
        text-decoration:none;
    } 
    a.pathLink:hover
    {
        text-decoration: unset;
    } 
	
	.scEditorHeaderQuickInfoInputID{
		border:none;
		width:100%;
		font-size:0.8em;
	}
</style>
    </head>
<body>
    <form id="form1" runat="server" class="">
        <div class="top-container">
            <div>
                <a href="#" class="scEditorHeaderIcon">
                    <img src="/~/icon/Apps/32x32/Codes.png" class="scEditorHeaderIcon" alt="" border="0" /></a>
                <h2><%=ContextItem.Name%></h2>
				<span class=path><%=ContextItem.Paths.FullPath%></span>
            </div>
			
			<br><br>
            <div class="container">
			
			                
			<div>
				<table style="width:100%;">
                    <asp:Repeater runat="server" ID="rptParts" OnItemDataBound="BindRepeater" >
                        <ItemTemplate>
					        <tr>
						        <td class=title style="width:70%;">
							        <%#((PartBase)Container.DataItem).Title%>
						        </td>
						        <td class=title>
							        Status
						        </td>
						        <td class=title>
							        Actions
						        </td>
					        </tr>
					        <tr>
						        <td class="location">
							        <%#((PartBase)Container.DataItem).Path%>
                                    <br />
									<input class="scEditorHeaderQuickInfoInputID" readonly="readonly" onclick="javascript:this.select();return false" value="<%#((PartBase)Container.DataItem).ExtraInfo%>" runat=server>
                                    
						        </td>
						        <td class=location>
							        <%#((PartBase)Container.DataItem).Status.ToString()%>
						        </td>
						        <td class=action>
                                    <asp:Button ID="generate" runat="server" Text="GENERATE" OnClick="GenerateClick" CommandArgument="<%#((PartBase)Container.DataItem).GetType().FullName %>" />
						        </td>
					        </tr>
                            <tr><td colspan=3>&nbsp;</td></tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
				</table>
			</div>
            </div>

			<br><br><br>
			
			<img src="/~/icon/Applications/16x16/about.png.aspx" class="scContentTreeNodeIcon" style="float: left;" alt="" width="16" height="16" border="0">
			<span style="float: left; color:#000;">&nbsp;To create the artifact again, delete the current and click in generate.</span>
			
			<br><br><br>
			
        </div>
    </form>
</body>
</html>
