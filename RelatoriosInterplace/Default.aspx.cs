using DevExpress.Web.Bootstrap;
using System;
using RelatoriosInterplace;
using System.IO;
using DevExpress.DashboardWeb;

public partial class Default : System.Web.UI.Page
{

    protected void CardViewControl_CustomCallback(object sender, DevExpress.Web.ASPxCardViewCustomCallbackEventArgs e)
    {
        int newPageSize = Int32.Parse(e.Parameters);
    }

    protected void BootstrapScheduler1_Init(object sender, EventArgs e)
    {
        var scheduler = (BootstrapScheduler)sender;
        scheduler.Storage.Appointments.Labels.Clear();
        foreach (SchedulerLabel label in SchedulerLabelsHelper.GetItems())
            scheduler.Storage.Appointments.Labels.Add(label.Id, label.Name, label.BackgroundCssClass, label.TextCssClass);
    }

    protected void dashGeral_Load(object sender, EventArgs e)
    {


    }

    protected void dashGeral_DashboardLoading(object sender, DevExpress.DashboardWeb.DashboardLoadingWebEventArgs e)
    {
        string sDash = Request.QueryString["dash"];

        if (sDash == null)
            return;

        string sArquivo = "";
        if (sDash == "Custo")
        {
            sArquivo = "~/App_Data/CustoProduto.xml";
        }


        string definitionPath = Server.MapPath(sArquivo);
        string dashboardDefinition = File.ReadAllText(definitionPath);
        dashGeral.OpenDashboard(dashboardDefinition);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DashboardConfigurator.PassCredentials = true;
        Page.Title = "Relatórios InterplaceLog";
    }
}