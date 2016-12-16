<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Manage.Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>优做后台管理终端</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/Frame/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/Frame/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/Frame/css/matrix-login.css" />
    <link href="/Frame/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="Login.css" rel="stylesheet" />

</head>

<body>
    <div id="loginbox" >

        <div id="div_Login">
        <div class="control-group normal_text">
            <h3>
                <img id="img_logo" src="images/logo1999.png" alt="Logo"   /></h3>
        </div>
        <div class="control-group">
            <div class="controls">
                <div class="main_input_box">
                    <span class="add-on bg_lg " style="height:38px; padding:0;  vertical-align:middle;  margin-bottom:4px; width:40px "><i class="icon-user" style=''>1</i></span>
                    <input type="text" id="txt_inputStr" placeholder="您的登录名或邮箱" style="vertical-align:middle;" />
                </div>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <div class="main_input_box">
                    <span class="add-on bg_ly" style="height:38px; padding:0;  vertical-align:middle;  margin-bottom:4px; width:40px"><i class="icon-lock">2</i></span>
                    <input id="txt_UserPwd" type="password" placeholder="您的安全密码" style="vertical-align:middle;"  />
                  
                </div>
            </div>
        </div>
            <div  style="margin-left:60px; color:#ffffff;" >
                <input id="cb_Remeber" type="checkbox" name="name" value="" checked="checked" />记住密码

            </div>
        <div class="form-actions">
            <span class="pull-left"><a href="#" class="flip-link btn btn-info" id="to-recover">忘记密码?</a></span>
            <span class="pull-right"><a id="a_ckLogin"  class="btn btn-success" onclick="ToLogin()" >登陆</a></span>
        </div>
        </div>
        
        <div id="div_selMer" class="form-actions" style="text-align: center; display: none">
            <dl id="dl_selMer" class="dl_selMer">
                <dt>请选择您要登陆的商家</dt>
                <dd>眺悦眼镜1店</dd>
                <dd>眺悦眼镜2店</dd>

            </dl>
                    <div class="form-actions">
  
            <span class="pull-right"><a  class="btn btn-success" onclick="Exit()" >退 出</a></span>
        </div>
        </div>

    </div>
        <script src="../Script/jquery-1.8.2.js"></script>

    <script src="js/matrix.login.js"></script>

    <script src="Script/ZYUiPub.js"></script>
    <script src="Login.js"></script>
<%--    <!-- Start of KF5 supportbox script -->
<script src="//assets.kf5.com/supportbox/main.js" id="kf5-provide-supportBox" kf5-domain="lituo001.kf5.com"></script>
<!-- End of KF5 supportbox script -->--%>
</body>

</html>
