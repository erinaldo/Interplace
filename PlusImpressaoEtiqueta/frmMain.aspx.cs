﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session.Count == 0)
                Response.Redirect("frmLogin.aspx");



            if (Session["login"].ToString() == "")
            {

                Response.Redirect("frmLogin.aspx");

            }






        }
    }
}