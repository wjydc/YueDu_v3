(function ($) {
    //页面后退操作
    $.extend({
        CustomBack: function (pageUrl, pageCookieName) {
            if (pageUrl && (typeof pageUrl == 'string')) {
                var ref = document.referrer.toLowerCase();
                if (!ref) {
                    $('a.back').attr('href', '/');
                    return;
                }

                var currentUrl = document.URL.toLowerCase();
                var referrerLog = { referrer: currentUrl, level: 1 };

                if (!ref || (ref && ref.indexOf(pageUrl)) < 0) {
                    //通过地址栏或从其他页面进入本页面
                    $.cookie(pageCookieName, JSON.stringify(referrerLog), { path: '/', expires: 7 });
                }
                else if (ref && ref.indexOf(pageUrl) >= 0) {
                    //在本页面内进行操作
                    var record = JSON.parse($.cookie(pageCookieName));
                    if (record.referrer != currentUrl) {
                        referrerLog.level = JSON.parse($.cookie(pageCookieName)).level + 1;
                        $.cookie(pageCookieName, JSON.stringify(referrerLog), { path: '/', expires: 7 });
                    }
                }

                var pageLevel = JSON.parse($.cookie(pageCookieName)).level;
                var gobackJs = 'javascript:history.go(-' + pageLevel + ')';
                $('a.back').attr('href', gobackJs);
                return;
            }
            return;
        }
    });
})(jQuery);