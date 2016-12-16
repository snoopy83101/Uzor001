using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;


#region 某些可以让ashx路由的东东

namespace System.Web.Routing
{
    public class HttpHandlerRoute<T> : IRouteHandler where T : IHttpHandler
    {
        private String _virtualPath = null;
        public HttpHandlerRoute(String virtualPath)
        {
            _virtualPath = virtualPath;
        }
        public HttpHandlerRoute() { }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return Activator.CreateInstance<T>();
        }
    }
    public class HttpHandlerRoute : IRouteHandler
    {
        private String _virtualPath = null;
        public HttpHandlerRoute(String virtualPath)
        {
            _virtualPath = virtualPath;
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (!string.IsNullOrEmpty(_virtualPath))
            {
                return (IHttpHandler)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(_virtualPath, typeof(IHttpHandler));
            }
            else
            {
                throw new InvalidOperationException("HttpHandlerRoute threw an error because the virtual path to the HttpHandler is null or empty.");
            }
        }
    }
    public static class RoutingExtension
    {
        public static void MapHttpHandlerRoute(this RouteCollection routes, string routeName, string routeUrl, string physicalFile, RouteValueDictionary defaults = null, RouteValueDictionary constraints = null)
        {
            var route = new Route(routeUrl, defaults, constraints, new HttpHandlerRoute(physicalFile));
            routes.Add(routeName, route);
        }
        public static void MapHttpHandlerRoute<T>(this RouteCollection routes, string routeName, string routeUrl, RouteValueDictionary defaults = null, RouteValueDictionary constraints = null) where T : IHttpHandler
        {
            var route = new Route(routeUrl, defaults, constraints, new HttpHandlerRoute<T>());
            routes.Add(routeName, route);
        }
    }
}
#endregion




namespace Common
{


    public class RoutesSetting
    {



        public static void RegisterRoutes(RouteCollection routes)
        {




            #region Ajax
            routes.MapHttpHandlerRoute("ajax统计查询", "as/", "~/AjaxServer/AjaxSearch.ashx");
           // routes.MapPageRoute("ajax客户用户", "amb/", "~/AjaxServer/AjaxMember.aspx");
            routes.MapHttpHandlerRoute("ajax客户用户", "amb/", "~/AjaxServer/AjaxMember.ashx");
            routes.MapHttpHandlerRoute("ajax公交车", "abus/", "~/AjaxServer/AjaxBus.ashx");
            routes.MapHttpHandlerRoute("ajax社群", "ab/", "~/AjaxServer/AjaxBBS.ashx");


            routes.MapPageRoute("ajax微信", "awx/", "~/AjaxServer/AjaxWx.aspx");
            routes.MapHttpHandlerRoute("ajax后台工作", "aw/", "~/AjaxServer/AjaxWork.ashx");
            routes.MapHttpHandlerRoute("ajax商家", "am/", "~/AjaxServer/AjaxMerchant.ashx");
            routes.MapHttpHandlerRoute("ajax消息", "amsg/", "~/AjaxServer/AjaxMsg.ashx");



            routes.MapPageRoute("ajax招聘", "aj/", "~/AjaxServer/JobAjax.aspx");
            routes.MapPageRoute("ajax房产", "ah/", "~/AjaxServer/HouseAjax.aspx");
            routes.MapHttpHandlerRoute("ajax公共", "ac/", "~/AjaxServer/AjaxComm.ashx");
            routes.MapHttpHandlerRoute("ajax地图", "amap/", "~/AjaxServer/AjaxMap.aspx");
            routes.MapHttpHandlerRoute("ajax关于", "aa/", "~/AjaxServer/AjaxAbout.ashx");
            routes.MapHttpHandlerRoute("ajax用户", "au/", "~/AjaxServer/AjaxUser.ashx");
            routes.MapHttpHandlerRoute("ajax外发订单", "ao/", "~/AjaxServer/AjaxOrder.ashx");
            routes.MapHttpHandlerRoute("ajax新闻", "aar/", "~/AjaxServer/AjaxArticle.ashx");
         //   routes.MapPageRoute("ajax乡镇", "at/", "~/AjaxServer/TownAjax.aspx");

          //  routes.Add("sdfsd", new Route("data", new PageRoute("~/handler.ashx")));
            #endregion

            #region 公共部分

            routes.MapPageRoute("验证码", "yzm/", "~/Comm/yzm.aspx");



            #endregion

            #region 南麻集
            routes.MapPageRoute("信息列表", "InformationList/", "~/Information/InformationList.aspx");
            routes.MapPageRoute("信息主体", "InformationInfo/", "~/Information/InformationInfo.aspx");
            routes.MapPageRoute("发布信息", "SaveInformation/", "~/Information/SaveInformation.aspx");

            #endregion

            #region 房产
            routes.MapPageRoute("求租求购主体", "House/Demand/", "~/House/HouseDemandInfo.aspx");
            routes.MapPageRoute("求租求购", "House/qzqg/", "~/House/FindHouseDemand.aspx");
            routes.MapPageRoute("显示房源", "House/fangyuan/", "~/House/HouseInfo.aspx");
            routes.MapPageRoute("查找房源", "House/chaxun/", "~/House/FindHouse.aspx");
            routes.MapPageRoute("发布房源", "House/fabu/", "~/House/SaveHouse.aspx");
            routes.MapPageRoute("发布求组求购", "House/SaveHouseDemand/", "~/House/SaveHouseDemand.aspx");
            #endregion

            #region 用户

            #region 用户公共
            routes.MapPageRoute("SaveComment", "SaveComment/", "~/UI/SaveComment.aspx");  //用户页面
            routes.MapPageRoute("UserInfo", "UserInfo/{*UserId}", "~/User/UserInfo.aspx");  //用户页面
   
            routes.MapPageRoute("用户注册", "zhuce/", "~/User/Registration.aspx");
            #endregion


            #region 用户前台

            routes.MapPageRoute("用户首页", "u/", "~/User/UserInfo.aspx");



            #endregion

            #region 用户后台
            routes.MapHttpHandlerRoute("用户登录", "Login", "~/Login.aspx");
            routes.MapHttpHandlerRoute("后台Main", "Main", "~/Main.aspx");
            #endregion



            #endregion

            #region 招聘
            routes.MapPageRoute("简历列表", "Job/zhaorencai/", "~/Job/FindResume.aspx");
            routes.MapPageRoute("发布职位", "Job/fabuzhiwei/", "~/Job/SaveJob.aspx");
            routes.MapPageRoute("看简历", "Job/jianli/", "~/Job/ResumeInfo.aspx");  //职位信息页面
            routes.MapPageRoute("发布简历(我的简历)", "Job/wodejianli/", "~/Job/SaveResume.aspx");  //职位信息页面
            routes.MapPageRoute("qiuzhi", "Job/qiuzhi/", "~/job/FindJob.aspx");
            routes.MapPageRoute("JobInfo", "Job/JobInfo/", "~/Job/JobInfo.aspx");  //职位信息页面
            #endregion

            #region 商家

            #region 商家和产品展示(用户看到的商家)
            routes.MapPageRoute("产品展示", "Pro/", "~/Merchant/ProInfo2.aspx");
            routes.MapPageRoute("商家新闻主体", "MerArticle/", "~/Merchant/MerArticleInfo.aspx");
            routes.MapPageRoute("商家首页", "Mer/{*MerchantId}", "~/Merchant/MerchantInfo2.aspx");
            #endregion

            #region 店长看到的商家
            #region 通用
            routes.MapPageRoute("我的商家招聘", "MyMerJob/", "~/Merchant/MyMerJob.aspx");
            routes.MapPageRoute("我的商家资料维护", "MyMerSet/", "~/Merchant/MyMerSet.aspx");
            routes.MapPageRoute("我的商家动态", "MyMer/", "~/Merchant/MyMer.aspx");
            routes.MapPageRoute("商家设置", "MyMerSet/{*MerchantId}", "~/MerMer/MyMerSet.aspx");

            #endregion



            #region 我的商家产品

            routes.MapPageRoute("我的商家产品类别维护-弹窗", "MyMerProClassInfoPop/", "~/Merchant/PopWindow/ProClassInfo.aspx");
            routes.MapPageRoute("我的商家产品类别维护", "MyMerProClass/", "~/Merchant/MyMerProClass.aspx");
            routes.MapPageRoute("我的商家产品列表", "MyMerProList/", "~/Merchant/MyMerProList.aspx");
            routes.MapPageRoute("我的商家产品", "MyMerProInfo/", "~/Merchant/MyMerProInfo.aspx");

            #endregion

            #region 我的商家新闻
            routes.MapPageRoute("商家新闻列表", "MyMerArticleList/", "~/Merchant/MyMerArticleList.aspx");
            routes.MapPageRoute("商家新闻类别", "MyMerArticleClass/", "~/Merchant/MyMerArticleClassList.aspx");
            routes.MapPageRoute("保存新闻", "MyMerArticleInfo/", "~/Merchant/MyMerArticleInfo.aspx");
            #endregion
            #endregion
            #endregion

            #region 社群

            routes.MapPageRoute("帖子内容", "t/", "~/BBS/tieziInfo.aspx");
            routes.MapPageRoute("保存帖子", "st/", "~/BbS/SaveTieZi.aspx");
            routes.MapPageRoute("帖子图片列表", "tImgList/", "~/BBS/TieZiImgList.aspx");

            #endregion

            #region 其他乱起八糟

            routes.MapPageRoute(


          "Map", //路由名
          "Map/{*test}", //路由URL
          "~/MapDefault.aspx" //处理路由的网页
            );
            routes.MapPageRoute(


"About", //路由名
"About/{*test}", //路由URL
"~/About/Default.aspx" //处理路由的网页
 );
            routes.MapPageRoute(
        "Post", //路由名
        "Post/{*PostId}", //路由URL
        "~/sns/PostList.aspx" //处理路由的网页
          );
            routes.MapPageRoute(
"PostImg", //路由名
"PostImg/{*PostId}", //路由URL
"~/sns/PostImgList.aspx" //处理路由的网页
);
            routes.MapPageRoute(
"Index", //路由名
"default.html", //路由URL
"~/default.aspx" //处理路由的网页
  );

            routes.MapPageRoute(
"Index2", //路由名
"index.html", //路由URL
"~/default.aspx" //处理路由的网页
);

            routes.MapPageRoute(
"Index3", //路由名
"index.aspx", //路由URL
"~/default.aspx" //处理路由的网页
);
            //处理Categories/{CategoryName}的路由
            //更多信息，请参考http://forums.asp.net/p/1417546/3131024.aspx
            routes.MapPageRoute(
          "View Category", //路由名
          "Categories/{*CategoryName}", //路由URL
          "~/CategoryProducts.aspx" //处理路由的网页
            );
            // Register a route for Products/{ProductName}
            #endregion
        }
    }

}
