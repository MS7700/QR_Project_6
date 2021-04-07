using QR_Project_6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        //public static MvcHtmlString AutoSizedTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        //{
        //    var attributes = new Dictionary<string, Object>();
        //    var memberAccessExpression = (MemberExpression)expression.Body;
        //    var stringLengthAttribs = memberAccessExpression.Member.GetCustomAttributes(
        //      typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute), true);

        //    if (stringLengthAttribs.Length > 0)
        //    {
        //        var length = ((StringLengthAttribute)stringLengthAttribs[0]).MaximumLength;

        //        if (length > 0)
        //        {
        //            attributes.Add("size", length);
        //            attributes.Add("maxlength", length);
        //        }
        //    }

        //    return helper.TextBoxFor(expression, attributes);
        //}


        public static MvcHtmlString InputRangeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "range");
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("value", metadata.Model as string);
            tb.AddCssClass("form-control-range");
            tb.Attributes.Add("min", "0");
            tb.Attributes.Add("max", "5");
            tb.Attributes.Add("step", "1");
            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString CustomValoracionRange<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, List<Valoracion> valoracions)
        {
            int min = valoracions.Min(v=>v.Valor);
            int max = valoracions.Max(v => v.Valor);
            int? medium = valoracions.GetMedianValoracion();

            

            var name = ExpressionHelper.GetExpressionText(expression);
            var id = name.Replace('.', '_');
            var rangeid = "range" + id;
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            

        

            TagBuilder div = new TagBuilder("div");
            TagBuilder range = new TagBuilder("input");
            TagBuilder selectionText = new TagBuilder("select");

            //Valoracion vModel = (Valoracion) metadata.Model;
            range.Attributes.Add("type", "range");
            //range.Attributes.Add("name", name);
            range.Attributes.Add("id", rangeid);
            //if(vModel != null)
            //{
            //    range.Attributes.Add("value", vModel.Valor.ToString());
            //}
            range.AddCssClass("form-control-range");
            range.Attributes.Add("min", min.ToString());
            range.Attributes.Add("max", max.ToString());
            range.Attributes.Add("onchange", "onChangeValoracion(value)");
            range.Attributes.Add("step", "0.1");

            selectionText.Attributes.Add("name", name);
            selectionText.Attributes.Add("id", id);
            selectionText.Attributes.Add("readonly", "true");
            selectionText.AddCssClass("form-control no-arrow");
            selectionText.Attributes.Add("value", medium.ToString());

            foreach (Valoracion valoracion in valoracions)
            {
                TagBuilder option = new TagBuilder("option");
                option.Attributes.Add("value", valoracion.ValoracionID.ToString());
                option.InnerHtml = valoracion.Descripcion;
                if(valoracion.ValoracionID == medium)
                {
                    option.Attributes.Add("selected", "selected");
                }
                selectionText.InnerHtml += option;
            }

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(range.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(selectionText.ToString(TagRenderMode.Normal));
            div.InnerHtml = htmlBuilder.ToString();
            var html = div.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(html);
        }
        //public static MvcHtmlString InputRangeFor<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        //{
        //    var name = ExpressionHelper.GetExpressionText(expression);
        //    var metadata = ModelMetadata.FromLambdaExpression(expression,null);
        //    TagBuilder tb = new TagBuilder("input");
        //    tb.Attributes.Add("type", "range");
        //    tb.Attributes.Add("name", name);
        //    tb.Attributes.Add("id", name);
        //    tb.Attributes.Add("value", metadata.Model as string);
        //    tb.AddCssClass("form-control-range");
        //    tb.Attributes.Add("min", "0");
        //    tb.Attributes.Add("max", "5");
        //    tb.Attributes.Add("step", "1");
        //    return new MvcHtmlString(tb.ToString());
        //}

        //public static IHtmlString Range<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        //{
        //    var input = new TagBuilder("input");
        //    var htmlText = input.ToString(TagRenderMode.Normal);
        //    return MvcHtmlString.Create(htmlText);
        //}

    }


}