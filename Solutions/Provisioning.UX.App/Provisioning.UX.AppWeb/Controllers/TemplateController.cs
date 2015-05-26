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
        #region Public Members
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

            foreach (var _template in _templates)
            {
                var _st = new SiteTemplateResults();
                _st.Title = _template.Title;
                _st.Description = _template.Description;
                _st.ImageUrl = _template.ImageUrl;
                _st.HostPath = _template.HostPath;
                _st.SharePointOnPremises = _template.SharePointOnPremises;
                _st.TenantAdminUrl = _template.TenantAdminUrl;
                _returnResults.Add(_st);
            }
            return _returnResults;
        }
        #endregion
    }
}
