using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace QR_Project_6.Extensions
{
    public static class HtmlRequestHelper
    {

        public static string Id(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];

            return string.Empty;
        }

        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
    }
    public static class HtmlHelperExtensions
    {
        public static IHtmlString SidebarElement(string controller, string text)
        {
            string link = "/" + controller;
            //<a id="Departamentoes" class="sidebar-button" href="Departamentoes" onclick="select(this)">Services</a>
            var anchor = new TagBuilder("a");
            anchor.AddCssClass("sidebar-button");
            anchor.MergeAttribute("id", controller);
            anchor.MergeAttribute("href", link);
            //anchor.MergeAttribute("onclick", "select(this)");
            anchor.SetInnerText(text);
            var html = anchor.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }

    }


}