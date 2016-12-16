<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string xstr = context.Request["x"];
        string ystr = context.Request["y"];
        string wstr = context.Request["w"];
        string hstr = context.Request["h"];
        string sourceFile = context.Server.MapPath("girl.jpg");
        string savePath = "CutImage/" + Guid.NewGuid() + ".jpg";
        int x = 0;
        int y = 0;
        int w = 1;
        int h = 1;
        try
        {
            x = int.Parse(xstr);
            y = int.Parse(ystr);
            w = int.Parse(wstr);
            h = int.Parse(hstr);
        }
        catch { }
        
        
        
        
        ImageCut ic = new ImageCut(x, y, w, h);
        System.Drawing.Bitmap cuted = ic.KiCut(new System.Drawing.Bitmap(sourceFile));
        string cutPath = context.Server.MapPath(savePath);
        cuted.Save(cutPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        context.Response.Write(savePath);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}