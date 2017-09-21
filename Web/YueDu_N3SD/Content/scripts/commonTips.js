(function ($) {
    var defaults = {
        message: '',
        showTime: 0,
        hideTime: 0,
        timeOut: 1000
    };
    $.extend({
        tips: function (options) {
            var options = $.extend(defaults, options);
            var $div = $("<div class=\"small_tip\" style=\"z-index:999;\">" + options.message + "</div>")
            $("body").append($div);
            $div.show(options.showTime);            setTimeout(function () {
                $div.remove();
            }, options.timeOut);
        }
    });
})(jQuery);

(function ($) {
    $.confirm = function (msg, callback) {
        GenerateHtml(msg);
        btnOk(callback);
        btnNo();
    }

    //生成Html
    var GenerateHtml = function (msg) {
        var _html = '<div class="mask" style="display:block;"></div>';
        _html += '<div class="dele_sure box" style="display:block;"><p>' + msg + '</p>';
        _html += '<span class="dele_sure_yes">确认</span>';
        _html += '<span class="dele_sure_no">取消</span>';
        _html += '</div>';
        $("body").append(_html);
    }

    //确定按钮事件
    var btnOk = function (callback) {
        $(".dele_sure_yes").click(function () {
            $(".mask,.dele_sure").remove();
            if (typeof (callback) == 'function') {
                callback();
            }
        });
    }
    //取消按钮事件
    var btnNo = function () {
        $(".dele_sure_no").click(function () {
            $(".mask,.dele_sure").remove();
        });
    }
})(jQuery);