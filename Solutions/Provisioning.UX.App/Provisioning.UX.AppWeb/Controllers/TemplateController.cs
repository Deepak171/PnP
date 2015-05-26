using OfficeDevPnP.Core.WebAPI;
using Provisioning.Common.Data.SiteRequests;
using Provisioning.Common.Data.Templates;
using Provisioning.UX.AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Provisioning.UX.AppWeb.Controllers
{
    /// <summary>
    /// Web API Class to work with Site Templates
    /// </summary>
    public class TemplateController : ApiController
    {
        [HttpPut]
        public void Register(WebAPIContext sharePointServiceContext)
        {
            WebAPIHelper.AddToCache(sharePointServiceContext);
        }
      
        /// <summary>
        /// Returns a list of available site templates to create
        /// </summary>
        /// <returns></returns>
        [Route("api/provisioning/templates/getAvailableTemplates")]
        [WebAPIContextFilter]
        [HttpGet]
        public List<SiteTemplateResults> GetSiteTemplates()
        {
            var _returnResults = new List<SiteTemplateResults>();
            var _siteFactory = SiteTemplateFactory.GetInstance();
            var _tm = _siteFactory.GetManager();
            var _templates = _tm.GetAvailableTemplates();

            foreach (var _t in _templates)
            {
                var _st = new SiteTemplateResults();
                _st.Title = _t.Title;
                _st.Description = _t.Description;
                _st.ImageUrl = _t.ImageUrl;
                _st.HostPath = _t.HostPath;
                _st.SharePointOnPremises = _t.SharePointOnPremises;
                _st.TenantAdminUrl = _t.TenantAdminUrl;
                _returnResults.Add(_st);
            }
            return _returnResults;
        }

    
    }
}
