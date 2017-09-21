using Component.Controllers.User;
using Model.Common;
using Service;
using System;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class QrCodeController : UserInfoController
    {
        private IQrCodeService _qrCodeService;

        public QrCodeController(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [ChildActionOnly]
        public ActionResult GetQr(int classId, bool isPromote = false, string replyText = "")
        {
            QrCodeView qrCodeView = null;
            if (string.IsNullOrEmpty(RouteChannelId))
            {
                qrCodeView = _qrCodeService.GetDetail(classId);
            }
            else
            {
                qrCodeView = new QrCodeView();
                qrCodeView.Id = 1;
                qrCodeView.WeChatPubId = SiteSection.Service.WeChatPubId;
                qrCodeView.Path = SiteSection.Html.WeChatQrCode;
                qrCodeView.UpdateTime = DateTime.Now.Date;
            }

            if (qrCodeView != null && qrCodeView.Id > 0)
            {
                qrCodeView.ReplyText = replyText;
            }

            var model = new SimpleResponse<QrCodeView>(qrCodeView != null && qrCodeView.Id > 0, qrCodeView);

            if (isPromote)
                return PartialView("/Views/QrCode/GetPromoteQr.cshtml", model);
            else
                return PartialView(model);
        }
    }
}