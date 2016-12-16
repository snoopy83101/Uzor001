using System.Net;
using System.Text;
using System.Web;
using System.Collections.Generic;
using LitJson;
using BeeCloud.Model;
using System;

namespace BeeCloud
{
    public class BCPay
    {   
        public enum PayChannel
        {
            WX_NATIVE,
            WX_JSAPI,
            ALI_WEB,
            ALI_QRCODE,
            ALI_WAP,
            UN_WEB,
            JD_WAP,
            JD_WEB,
            YEE_WAP,
            YEE_WEB,
            KUAIQIAN_WAP,
            KUAIQIAN_WEB,
            BD_WEB,
            BD_WAP
        };

        public enum InternationalPay
        {
            PAYPAL_PAYPAL,
            PAYPAL_CREDITCARD,
            PAYPAL_SAVED_CREDITCARD
        };

        public enum QueryChannel
        {
            WX,
            ALI,
            UN,
            WX_APP,
            WX_NATIVE,
            WX_JSAPI,
            ALI_APP,
            ALI_WEB,
            ALI_QRCODE,
            ALI_WAP,
            UN_APP,
            UN_WEB,
            JD_WAP,
            JD_WEB,
            YEE_WAP,
            YEE_WEB,
            KUAIQIAN_WAP,
            KUAIQIAN_WEB,
            BD_WEB,
            BD_WAP,
            PAYPAL
        };

        public enum RefundChannel
        {
            WX,
            ALI,
            UN,
            JD,
            YEE,
            KUAIQIAN,
            BD
        };

        public enum RefundStatusChannel
        {
            WX,
            YEE,
            KUAIQIAN,
            BD
        };

        public enum TransferChannel
        {
            ALI,
            WX_REDPACK, 
            WX_TRANSFER, 
            ALI_TRANSFER
        };

        #region 支付
        //准备支付数据
        public static string preparePayParameters(BCBill bill)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            if (!BCCache.Instance.testMode)
            {
                data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            }
            else
            {
                data["app_sign"] = BCPrivateUtil.getAppSignatureByTestSecret(timestamp.ToString());
            }   
            data["timestamp"] = timestamp;
            data["channel"] = bill.channel;
            data["total_fee"] = bill.totalFee;
            data["bill_no"] = bill.billNo;
            data["title"] = bill.title;
            data["return_url"] = bill.returnUrl;

            data["bill_timeout"] = bill.billTimeout;

            data["openid"] = bill.openId;
            data["show_url"] = bill.showURL;
            data["qr_pay_mode"] = bill.qrPayMode;


            if (bill.optional != null && bill.optional.Count > 0)
            {
                data["optional"] = new JsonData();
                foreach (string key in bill.optional.Keys)
                {
                    data["optional"][key] = bill.optional[key];
                }
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //处理支付回调
        public static BCBill handlePayResult(string respString, BCBill bill)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            if (bill.channel == "WX_NATIVE")
            {
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    if (BCCache.Instance.testMode)
                    {
                        bill.codeURL = responseData["url"].ToString();
                    }
                    else
                    {
                        bill.codeURL = responseData["code_url"].ToString();
                    } 
                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
                
            }
            if (bill.channel == "WX_JSAPI")
            {
                if (BCCache.Instance.testMode)
                {
                    throw new BCException("微信公众号内支付不支持测试模式");
                }
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    bill.appId = responseData["app_id"].ToString();
                    bill.package = responseData["package"].ToString();
                    bill.noncestr = responseData["nonce_str"].ToString();
                    bill.timestamp = responseData["timestamp"].ToString();
                    bill.paySign = responseData["pay_sign"].ToString();
                    bill.signType = responseData["sign_type"].ToString();

                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
            }
            if (bill.channel == "ALI_WEB" || bill.channel == "ALI_WAP")
            {
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    if (BCCache.Instance.testMode)
                    {
                        bill.html = string.Format("<html><head></head><body><script>location.href='{0}'</script></body></html>", responseData["url"].ToString());
                    }
                    else
                    {
                        bill.html = responseData["html"].ToString();
                    }
                    bill.url = responseData["url"].ToString();

                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
            }
            if (bill.channel == "ALI_QRCODE")
            {
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    bill.url = responseData["url"].ToString();
                    if (BCCache.Instance.testMode)
                    {
                        bill.html = string.Format("<html><head></head><body><script>location.href='{0}'</script></body></html>", responseData["url"].ToString());
                    }
                    else
                    {
                        bill.html = responseData["html"].ToString();
                    }
                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
            }
            if (bill.channel == "JD_WAP" || bill.channel == "JD_WEB" || bill.channel == "KUAIQIAN_WAP" || bill.channel == "KUAIQIAN_WEB" || bill.channel == "UN_WEB")
            {
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    if (BCCache.Instance.testMode)
                    {
                        bill.html = string.Format("<html><head></head><body><script>location.href='{0}'</script></body></html>", responseData["url"].ToString());
                    }
                    else
                    {
                        bill.html = responseData["html"].ToString();
                    }
                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
            }
            if (bill.channel == "BD_WEB" || bill.channel == "BD_WAP" || bill.channel == "YEE_WEB" || bill.channel == "YEE_WAP")
            {
                if (responseData["result_code"].ToString() == "0")
                {
                    bill.id = responseData["id"].ToString();
                    bill.url = responseData["url"].ToString();
                    return bill;
                }
                else
                {
                    var ex = new BCException(responseData["err_detail"].ToString());
                    throw ex;
                }
            }
            return bill;
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.PayChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     BD_WEB:       百度web支付
        ///     BD_WAP:       百度wap支付
        /// </param>
        /// <param name="totalFee">订单总金额
        ///     只能为整数，单位为分
        ///     必填
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一（即所有渠道所有订单号不同）
        ///     必填
        /// </param>
        /// <param name="title">订单标题
        ///     32个字节内，最长支持16个汉字
        ///     必填
        /// </param>
        /// <param name="optional">附加数据
        ///     用户自定义的参数，将会在webhook通知中原样返回，该字段主要用于商户携带订单的自定义数据
        ///     {"key1":"value1","key2":"value2",...}
        ///     可空
        /// </param>
        /// <param name="returnUrl">同步返回页面
        ///     支付渠道处理完请求后,当前页面自动跳转到商户网站里指定页面的http路径。
        ///     当channel 参数为 ALI_WEB 或 ALI_QRCODE 或 UN_WEB时为必填
        /// </param>
        /// <param name="billTimeout">订单失效时间
        ///     必须为非零正整数，单位为秒，建议最短失效时间间隔必须大于300秒
        ///     可空
        ///     京东系列支付不支持该参数，填空
        /// </param>
        /// <param name="openId">用户相对于微信公众号的唯一id
        ///     例如'0950c062-5e41-44e3-8f52-f89d8cf2b6eb'
        ///     微信公众号支付(WX_JSAPI)的必填参数
        /// </param>
        /// <param name="showURL">商品展示地址
        ///     以http://开头,例如'http://beecloud.cn'
        ///     支付宝网页支付(ALI_WEB)的选填参数
        /// </param>
        /// <param name="qrPayMode">二维码类型
        ///     支付宝内嵌二维码支付(ALI_QRCODE)的选填参数
        ///     二维码类型含义
        ///     0： 订单码-简约前置模式,对应 iframe 宽度不能小于 600px, 高度不能小于 300px
        ///     1： 订单码-前置模式,对应 iframe 宽度不能小于 300px, 高度不能小于 600px
        ///     3： 订单码-迷你前置模式,对应 iframe 宽度不能小于 75px, 高度不能小于 75px
        /// </param>
        /// <returns>
        /// </returns>
        public static BCBill BCPayByChannel(BCBill bill)
        {
            string payUrl = "";
            if (!BCCache.Instance.testMode)
            {
                payUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billURL;
            }
            else
            {
                payUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billTestURL;
            }
            

            string paraString = preparePayParameters(bill);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString, BCCache.Instance.networkTimeout);

                string respString = BCPrivateUtil.GetResponseString(response);

                return handlePayResult(respString, bill);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }
        #endregion

        #region （预）退款
        //准备退款参数
        public static string prepareRefundParameters(BCRefund refund)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = refund.channel;
            data["refund_no"] = refund.refundNo;
            data["bill_no"] = refund.billNo;
            data["refund_fee"] = refund.refundFee;
            if (refund.optional != null && refund.optional.Count > 0)
            {
                data["optional"] = new JsonData();
                foreach (string key in refund.optional.Keys)
                {
                    data["optional"][key] = refund.optional[key];
                }
            }
            data["need_approval"] = refund.needApproval;
            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款回调
        public static BCRefund handleRefundResult(string respString, BCRefund refund)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            if (responseData["result_code"].ToString() == "0")
            {
                refund.id = responseData["id"].ToString();
                try
                {
                    refund.url = responseData["url"].ToString();
                }
                catch
                {
                    //
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
            return refund;
        }

        /// <summary>
        /// (预)退款
        /// </summary>
        /// <param name="channel">渠道类型   
        ///     选填
        ///     可以通过enum BCPay.RefundChannel获取
        ///     ALI:      支付宝
        ///     WX:       微信
        ///     UN:       银联
        ///     JD:       京东
        ///     YEE:      易宝
        ///     KUAIQIAN: 快钱
        ///     BD:       百度
        ///     注意：不传channel也能退款的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="refundNo">商户退款单号
        ///     格式为:退款日期(8位) + 流水号(3~24 位)。不可重复，且退款日期必须是当天日期。流水号可以接受数字或英文字符，建议使用数字，但不可接受“000”。
        ///     必填
        ///     例如：201506101035040000001
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一
        ///     DIRECT_REFUND和PRE_REFUND时必填
        /// </param>
        /// <param name="refundFee">退款金额
        ///     只能为整数，单位为分
        ///     DIRECT_REFUND和PRE_REFUND时必填
        /// </param>
        /// <param name="optional">附加数据
        ///     用户自定义的参数，将会在webhook通知中原样返回，该字段主要用于商户携带订单的自定义数据
        ///     选填
        ///     {"key1":"value1","key2":"value2",...}
        /// </param>
        /// <param name="needApproval">是否为预退款
        ///     预退款needApproval值传true,直接退款传false
        ///     如果needApproval值传true，开发者需要调用审核退款接口或者直接去BeeCloud控制台的预退款界面审核退款方能最终退款
        /// </param>
        /// <returns>
        /// </returns>
        public static BCRefund BCRefundByChannel(BCRefund refund)
        {
            Random random = new Random();
            string refundUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL;
            string paraString = prepareRefundParameters(refund);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(refundUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundResult(respString, refund);
                
            }
            catch(Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }            
        }
        #endregion

        #region 退款审核
        //准备退款审核参数
        public static string prepareApproveRefundParameters(string channel, List<string> ids, bool agree, string denyReason)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["ids"] = JsonMapper.ToObject(JsonMapper.ToJson(ids));
            data["agree"] = agree;
            data["denyReason"] = denyReason;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款审核回调
        public static BCApproveRefundResult handleApproveRefundResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCApproveRefundResult result = new BCApproveRefundResult();
            if (responseData["result_code"].ToString() == "0")
            {
                try
                {
                    result.url = responseData["url"].ToString();
                }
                catch
                {
                    //
                }
                result.status = JsonMapper.ToObject<Dictionary<string, string>>(responseData["result_map"].ToJson().ToString());
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
            return result;
        }

        /// <summary>
        ///  预退款(批量)审核
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同渠道选不同的值
        ///     必填
        ///     可以通过enum BCPay.RefundChannel获取
        ///     ALI:      支付宝
        ///     WX:       微信
        ///     UN:       银联
        ///     JD:       京东
        ///     YEE:      易宝
        ///     KUAIQIAN: 快钱
        ///     BD:       百度
        /// </param>
        /// <param name="ids">退款记录id列表
        ///     批量审核的退款记录的唯一标识符集合
        ///     必填
        /// </param>
        /// <param name="agree">同意或者驳回
        ///     批量驳回传false，批量同意传true
        ///     必填
        /// </param>
        /// <param name="denyReason">驳回理由
        ///     可空
        /// </param>
        /// <returns>
        ///     参考BCApproveRefundResult
        /// </returns>
        public static BCApproveRefundResult BCApproveRefund(string channel, List<string> ids, bool agree, string denyReason)
        {
            Random random = new Random();
            string approveRefundUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL;

            string paraString = prepareApproveRefundParameters(channel, ids, agree, denyReason);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePutHttpResponse(approveRefundUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleApproveRefundResult(respString, channel);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            } 
        }
        #endregion

        #region 查询
        ///准备订单查询参数
        public static string preparePayQueryByConditionParameters(BCQueryBillParameter para)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            if (!BCCache.Instance.testMode)
            {
                data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            }
            else
            {
                data["app_sign"] = BCPrivateUtil.getAppSignatureByTestSecret(timestamp.ToString());
            }
            data["timestamp"] = timestamp;
            data["channel"] = para.channel;
            data["bill_no"] = para.billNo;
            data["start_time"] = para.startTime;
            data["end_time"] = para.endTime;
            data["skip"] = para.skip;
            data["spay_result"] = para.result;
            data["need_detail"] = para.needDetail;
            data["limit"] = para.limit;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理订单条件查询回调
        public static List<BCBill> handlePayQueryByConditionResult(string respString, bool? needDetail)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            List<BCBill> bills = new List<BCBill>();
            if (responseData["result_code"].ToString() == "0")
            {
                if (responseData["bills"].IsArray)
                {
                    foreach (JsonData billData in responseData["bills"])
                    {
                        BCBill bill = new BCBill();
                        bill.id = billData["id"].ToString();
                        bill.title = billData["title"].ToString();
                        bill.totalFee = int.Parse(billData["total_fee"].ToString());
                        bill.createdTime = BCUtil.GetDateTime((long)billData["create_time"]);
                        bill.billNo = billData["bill_no"].ToString();
                        bill.result = (bool)billData["spay_result"];
                        bill.channel = billData["sub_channel"].ToString();
                        bill.tradeNo = billData["trade_no"].ToString();
                        bill.optional = JsonMapper.ToObject<Dictionary<string, string>>(billData["optional"].ToString());
                        if (needDetail == true)
                        {
                            bill.messageDetail = billData["message_detail"].ToString();
                        }
                        bill.revertResult = (bool)billData["revert_result"];
                        bill.refundResult = (bool)billData["refund_result"];
                        bills.Add(bill);
                    }
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }

            return bills;
        }

        //处理订单/退款单数量
        public static int handleQueryCountResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            if (responseData["result_code"].ToString() == "0")
            {
                if (responseData["count"].IsInt)
                {
                    return int.Parse(responseData["count"].ToString());
                }
                else
                {
                    var ex = new BCException("服务出错啦:-(");
                    throw ex;
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
        }

        //处理订单Id查询回调
        public static BCBill handlePayQueryByIdResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCBill bill = new BCBill();
            if (responseData["result_code"].ToString() == "0")
            {
                JsonData billData = responseData["pay"];
                bill.id = billData["id"].ToString();
                bill.title = billData["title"].ToString();
                bill.totalFee = int.Parse(billData["total_fee"].ToString());
                bill.createdTime = BCUtil.GetDateTime((long)billData["create_time"]);
                bill.billNo = billData["bill_no"].ToString();
                bill.result = (bool)billData["spay_result"];
                bill.channel = billData["sub_channel"].ToString();
                bill.tradeNo = billData["trade_no"].ToString();
                bill.optional = JsonMapper.ToObject<Dictionary<string, string>>(billData["optional"].ToString());
                bill.messageDetail = billData["message_detail"].ToString();
                bill.revertResult = (bool)billData["revert_result"];
                bill.refundResult = (bool)billData["refund_result"];
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }

            return bill;
        }

        //准备订单/退款id查询参数
        public static string prepareQueryByIdParameters(string id)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            if (!BCCache.Instance.testMode)
            {
                data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            }
            else
            {
                data["app_sign"] = BCPrivateUtil.getAppSignatureByTestSecret(timestamp.ToString());
            }
            data["timestamp"] = timestamp;

            string paraString = data.ToJson();
            return paraString;
        }

        //准备退款查询参数
        public static string prepareRefundQueryByConditionParameters(BCQueryRefundParameter para)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = para.channel;
            data["bill_no"] = para.billNo;
            data["refund_no"] = para.refundNo;
            data["start_time"] = para.startTime;
            data["end_time"] = para.endTime;
            data["need_approval"] = para.needApproval;
            data["need_detail"] = para.needDetail;
            data["skip"] = para.skip;
            data["limit"] = para.limit;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款条件查询回调
        public static List<BCRefund> handleRefundQueryByConditionResult(string respString, bool? needDetail)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            List<BCRefund> refunds = new List<BCRefund>();
            if (responseData["result_code"].ToString() == "0")
            {
                if (responseData["refunds"].IsArray)
                {
                    foreach (JsonData refundData in responseData["refunds"])
                    {
                        BCRefund refund = new BCRefund();
                        refund.id = refundData["id"].ToString();
                        refund.title = refundData["title"].ToString();
                        refund.billNo = refundData["bill_no"].ToString();
                        refund.refundNo = refundData["refund_no"].ToString();
                        refund.totalFee = int.Parse(refundData["total_fee"].ToString());
                        refund.refundFee = int.Parse(refundData["refund_fee"].ToString());
                        refund.channel = refundData["channel"].ToString();
                        refund.finish = (bool)refundData["finish"];
                        refund.result = (bool)refundData["result"];
                        refund.optional = JsonMapper.ToObject<Dictionary<string, string>>(refundData["optional"].ToString());
                        if (needDetail == true)
                        {
                            refund.messageDetail = refundData["message_detail"].ToString();
                        }
                        refund.createdTime = BCUtil.GetDateTime((long)refundData["create_time"]);
                        refunds.Add(refund);
                    }
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }

            return refunds;
        }

        //处理退款Id查询回调
        public static BCRefund handleRefundQueryByIdResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCRefund refund = new BCRefund();
            if (responseData["result_code"].ToString() == "0")
            {
                JsonData refundData = responseData["refund"];
                refund.id = refundData["id"].ToString();
                refund.title = refundData["title"].ToString();
                refund.billNo = refundData["bill_no"].ToString();
                refund.refundNo = refundData["refund_no"].ToString();
                refund.totalFee = int.Parse(refundData["total_fee"].ToString());
                refund.refundFee = int.Parse(refundData["refund_fee"].ToString());
                refund.channel = refundData["channel"].ToString();
                refund.finish = (bool)refundData["finish"];
                refund.result = (bool)refundData["result"];
                refund.optional = JsonMapper.ToObject<Dictionary<string, string>>(refundData["optional"].ToString());
                refund.messageDetail = refundData["message_detail"].ToString();
                refund.createdTime = BCUtil.GetDateTime((long)refundData["create_time"]);
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }

            return refund;
        }

        //准备退款状态查询参数
        public static string prepareRefundStatusQueryParameters(string channel, string refundNo)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["refund_no"] = refundNo;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款状态查询回调
        public static string handleRefundStatusQueryResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            string refundStatus = "";
            if (responseData["result_code"].ToString() == "0")
            {
                refundStatus = responseData["refund_status"].ToString();
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
            return refundStatus;
        }

        /// <summary>
        /// 支付订单查询
        /// </summary>
        /// <param name="channel">渠道类型
        ///     选填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     注意：不传channel也能查询的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="billNo">商户订单号
        /// </param>
        /// <param name="startTime">起始时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="endTime">结束时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="spayResult">订单状态
        ///     订单是否成功，null为全部返回，true只返回成功订单，false只返回失败订单
        ///     选填
        /// </param>
        /// <param name="needDetail">是否需要返回渠道详细信息
        ///     决定是否需要返回渠道的回调信息，true为需要
        ///     选填
        /// </param>
        /// <param name="skip">查询起始位置
        ///     默认为0。设置为10表示忽略满足条件的前10条数据
        ///     选填
        /// </param>
        /// <param name="limit">查询的条数
        ///     默认为10，最大为50。设置为10表示只返回满足条件的10条数据
        ///     选填
        /// </param>
        /// <returns></returns>
        public static List<BCBill> BCPayQueryByCondition(BCQueryBillParameter para)
        {
            Random random = new Random();
            string payQueryUrl = "";
            if (!BCCache.Instance.testMode)
            {
                payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billsURL;
            }
            else
            {
                payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billsTestURL;
            }
            

            string paraString = preparePayQueryByConditionParameters(para);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handlePayQueryByConditionResult(respString, para.needDetail);
            }
            catch(Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获得订单笔数，配合BCPayQueryByCondition使用，使用查询订单时一样的参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static int BCPayQueryCount(BCQueryBillParameter para)
        {
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billsCountURL;

            string paraString = preparePayQueryByConditionParameters(para);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleQueryCountResult(respString);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 支付订单查询(指定ID)
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns></returns>
        public static BCBill BCPayQueryById(string id)
        {
            Random random = new Random();
            string payQueryUrl = "";
            if (!BCCache.Instance.testMode)
            {
                payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billURL + "/" + id;
            }
            else
            {
                payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billTestURL + "/" + id;
            }
            

            string paraString = prepareQueryByIdParameters(id);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handlePayQueryByIdResult(respString);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 退款订单查询
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     注意：不传channel也能查询的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="billNo">商户订单号
        /// </param>
        /// <param name="refundNo">商户退款单号
        /// </param>
        /// <param name="startTime">起始时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填</param>
        /// <param name="endTime">结束时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="needApproval">需要审核     
        ///     标识退款记录是否为预退款
        ///     选填
        /// </param>
        /// <param name="needDetail">是否需要返回渠道详细信息
        ///     决定是否需要返回渠道的回调信息，true为需要
        ///     选填
        /// </param>
        /// <param name="skip">查询起始位置
        ///     默认为0。设置为10表示忽略满足条件的前10条数据
        ///     选填
        /// </param>
        /// <param name="limit">查询的条数
        ///     默认为10，最大为50。设置为10表示只返回满足条件的10条数据
        ///     选填
        /// </param>
        /// <returns>
        /// </returns>
        public static List<BCRefund> BCRefundQueryByCondition(BCQueryRefundParameter para)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundsURL;

            string paraString = prepareRefundQueryByConditionParameters(para);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundQueryByConditionResult(respString, para.needDetail);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获得退款笔数，配合BCRefundQueryByCondition使用，使用查询退款时一样的参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static int BCRefundQueryCount(BCQueryRefundParameter para)
        {
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundsCountURL;

            string paraString = prepareRefundQueryByConditionParameters(para);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleQueryCountResult(respString);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 退款订单查询(指定ID)
        /// </summary>
        /// <param name="id">退款记录的唯一标识，可用于查询单笔记录</param>
        /// <returns></returns>
        public static BCRefund BCRefundQueryById(string id)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL + "/" + id;

            string paraString = prepareQueryByIdParameters(id);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundQueryByIdResult(respString);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        ///退款状态查询"
        /// </summary>
        /// <param name="channel">渠道类型
        ///     只有WX、YEE、KUAIQIAN、BD需要
        /// </param>
        /// <param name="refundNo">商户退款单号
        /// </param>
        /// <returns>
        /// </returns>
        public static string BCRefundStatusQuery(string channel, string refundNo)
        {
            Random random = new Random();
            string refundStatusUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundStatusURL;

            string paraString = prepareRefundStatusQueryParameters(channel, refundNo);
            
            string url = refundStatusUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            try
            {
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundStatusQueryResult(respString);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }
        #endregion

        #region 打款
        //准备单笔单款参数
        public static string prepareTransferParameters(BCTransferParameter para)
        {
            if (BCCache.Instance.masterSecret == null)
            {
                var ex = new BCException("masterSecret未注册, 请查看registerApp方法");
                throw ex;
            }

            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = para.channel;
            data["transfer_no"] = para.transferNo;
            data["total_fee"] = para.totalFee;
            data["desc"] = para.desc;
            data["channel_user_id"] = para.channelUserId;
            data["channel_user_name"] = para.channelUserName;
            data["account_name"] = para.accountName;
            if (para.info != null)
            {
                data["redpack_info"] = new JsonData();
                data["redpack_info"]["send_name"] = para.info.sendName;
                data["redpack_info"]["wishing"] = para.info.wishing;
                data["redpack_info"]["act_name"] = para.info.actName;
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //准备批量打款参数
        public static string prepareTransfersParameters(BCTransfersParameter para)
        {
            if (BCCache.Instance.masterSecret == null)
            {
                var ex = new BCException("masterSecret未注册, 请查看registerApp方法");
                throw ex;
            }

            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = para.channel;
            data["batch_no"] = para.batchNo;
            data["account_name"] = para.accountName;
            JsonData list = new JsonData();
            foreach (var transfer in para.transfersData)
            {
                JsonData d = new JsonData();
                d["transfer_id"] = transfer.transferId;
                d["receiver_account"] = transfer.receiverAccount;
                d["receiver_name"] = transfer.receiverName;
                d["transfer_fee"] = transfer.transferFee;
                d["transfer_note"] = transfer.transferNote;
                list.Add(d);
            }
            data["transfer_data"] = list;
            string paraString = data.ToJson();
            return paraString;
        }

        //处理(批量)打款回调
        public static string handleTransfersResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            string result = "";
            if (responseData["result_code"].ToString() == "0")
            {
                if (channel.Contains("ALI"))
                {
                    result = responseData["url"].ToString();
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 批量打款
        /// </summary>
        /// <param name="channel">渠道
        ///     必填
        ///     现在只支持支付宝（TransferChannel.ALI_TRANSFER）</param>
        /// <param name="batchNo">批量付款批号
        ///     必填
        ///     此次批量付款的唯一标示，11-32位数字字母组合
        /// </param>
        /// <param name="accountName">付款方的支付宝账户名
        ///     必填
        /// </param>
        /// <param name="transferData">付款的详细数据
        ///     必填
        ///     每一个Map对应一笔付款的详细数据, list size 小于等于 1000。
        ///     具体参BCTransferData类
        /// </param>
        /// <returns>
        ///     如果channel类型是TRANSFER_CHANNEL.ALI_TRANSFER, 返回需要跳转支付的url, 否则返回空字符串
        /// </returns>
        public static string BCTransfers(BCTransfersParameter para)
        {
            string transferUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.transfersURL;

            string paraString = prepareTransfersParameters(para);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(transferUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleTransfersResult(respString, para.channel);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 打款
        /// </summary>
        /// <param name="channel">渠道类型
        ///     WX_REDPACK 微信红包, 
        ///     WX_TRANSFER 微信企业打款, 
        ///     ALI_TRANSFER 支付宝企业打款
        /// </param>
        /// <param name="transferNo">打款单号
        ///     支付宝为11-32位数字字母组合， 微信为10位数字
        /// </param>
        /// <param name="totalFee">打款金额
        ///     此次打款的金额,单位分,正整数(微信红包1.00-200元，微信打款>=1元)
        /// </param>
        /// <param name="desc">打款说明
        ///     此次打款的说明
        /// </param>
        /// <param name="channelUserId">用户id
        ///     支付渠道方内收款人的标示, 微信为openid, 支付宝为支付宝账户
        /// </param>
        /// <param name="channelUserName">用户名
        ///     支付渠道内收款人账户名， 支付宝必填
        /// </param>
        /// <param name="info">红包信息
        ///     查看BCRedPackInfo
        /// </param>
        /// <param name="account_name">打款方账号名称
        ///     打款方账号名全称，支付宝必填
        /// </param>
        /// <returns>
        ///     批量打款跳转支付url
        /// </returns>
        public static string BCTransfer(BCTransferParameter para)
        {
            Random random = new Random();
            string transferUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.transferURL;

            string paraString = prepareTransferParameters(para);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(transferUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleTransfersResult(respString, para.channel);
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }
        #endregion

        #region 境外支付
        //准备境外支付参数
        public static string prepareInternationalPayParameters(BCInternationlBill bill)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = bill.channel;
            data["total_fee"] = bill.totalFee;
            data["bill_no"] = bill.billNo;
            data["title"] = bill.title;
            data["currency"] = bill.currency;
            if (bill.info != null)
            {
                data["credit_card_info"] = JsonMapper.ToObject(JsonMapper.ToJson(bill.info));
            }
            if (bill.creditCardId != null)
            {
                data["credit_card_id"] = bill.creditCardId;
            }
            if (bill.returnUrl != null)
            {
                data["return_url"] = bill.returnUrl;
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //处理境外支付回调
        public static BCInternationlBill handleInternationalPayResult(string respString, BCInternationlBill bill)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            if (responseData["result_code"].ToString() == "0")
            {
                if (bill.channel == "PAYPAL_PAYPAL")
                {
                    bill.url = responseData["url"].ToString();
                }
                if (bill.channel == "PAYPAL_CREDITCARD")
                {
                    bill.creditCardId = responseData["credit_card_id"].ToString();
                }
            }
            else
            {
                var ex = new BCException(responseData["err_detail"].ToString());
                throw ex;
            }
            return bill;
        }

        /// <summary>
        /// 境外支付
        /// </summary>
        /// <param name="channel">渠道类型
        ///     enum InternationalPay提供了三个境外支付渠道类型，分别是：
        ///     PAYPAL_PAYPAL ： 跳转到paypal使用paypal内支付
        ///     PAYPAL_CREDITCARD ： 直接使用信用卡支付（paypal渠道）
        ///     PAYPAL_SAVED_CREDITCARD ： 使用存储的行用卡id支付（信用卡信息存储在PAYPAL)
        /// </param>
        /// <param name="totalFee">订单总金额
        ///     只能为整数，单位为分
        ///     必填
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一（即所有渠道所有订单号不同）
        ///     必填
        /// </param>
        /// <param name="title">订单标题
        ///     32个字节内，最长支持16个汉字
        ///     必填
        /// </param>
        /// <param name="currency">三位货币种类代码
        ///     必填
        ///     类型如下：
        ///         Australian dollar	AUD
        ///         Brazilian real**	BRL
        ///         Canadian dollar	    CAD
        ///         Czech koruna	    CZK
        ///         Danish krone	    DKK
        ///         Euro	            EUR
        ///         Hong Kong dollar	HKD
        ///         Hungarian forint	HUF
        ///         Israeli new shekel	ILS
        ///         Japanese yen	    JPY
        ///         Malaysian ringgit	MYR
        ///         Mexican peso	    MXN
        ///         New Taiwan dollar	TWD
        ///         New Zealand dollar	NZD
        ///         Norwegian krone	    NOK
        ///         Philippine peso	    PHP
        ///         Polish złoty	    PLN
        ///         Pound sterling	    GBP
        ///         Singapore dollar	SGD
        ///         Swedish krona	    SEK
        ///         Swiss franc	        CHF
        ///         Thai baht	        THB
        ///         Turkish lira	    TRY
        ///         United States dollar	USD
        /// </param>
        /// <param name="info">信用卡信息
        ///     具体查看BCCreditCardInfo类
        ///     当channel 为PAYPAL_CREDITCARD必填
        /// </param>
        /// <param name="creditCardId">
        ///     当使用PAYPAL_CREDITCARD支付完成后会返回一个credit_card_id，商家可以存储这个id方便下次通过这个id发起支付无需再输入卡面信息
        /// </param>
        /// <param name="returnUrl">同步返回页面
        ///     支付渠道处理完请求后,当前页面自动跳转到商户网站里指定页面的http路径。
        ///     当channel参数为PAYPAL_PAYPAL时为必填
        /// </param>
        /// <returns></returns>
        public static BCInternationlBill BCInternationalPay(BCInternationlBill bill)
        {
            Random random = new Random();
            string payUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.internationalURL;

            string paraString = prepareInternationalPayParameters(bill);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleInternationalPayResult(respString, bill);                
            }
            catch (Exception e)
            {
                var ex = new BCException(e.Message);
                throw ex;
            }
        }
        #endregion
    }
}
