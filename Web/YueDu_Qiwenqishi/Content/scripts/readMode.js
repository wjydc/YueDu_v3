//阅读页面用户简单行为相关处理

$(function () {
    // 夜间模式切换
    $('.buy_moon').click(function () {
        var readMode = $.cookie('readMode');//阅读模式 1夜间，0或null为白天模式
        if (readMode == 1) {
            //变白天
            $.cookie('readMode', 0, { path: '/', expires: 31 });
            $(".buying").removeClass("darknight").addClass("daytime");
            $("header").removeClass("headnight").addClass("headday");
        }
        else {
            //变黑夜
            $.cookie('readMode', 1, { path: '/', expires: 31 });
            $(".buying").removeClass("daytime").addClass("darknight");
            $("header").removeClass("headday").addClass("headnight");
        }
    });

    // 文字大小变化
    var basicSize = 7;//初始大小

    var readFontSize = $.cookie("readFontSize");

    if (readFontSize != undefined) {
        var tempSize = parseInt(readFontSize);
        //防止异常点击造成数值不在有效范围内
        if (tempSize < 1 || tempSize > 12) {
            $.removeCookie("readFontSize", { path: '/' });
        }
        else {
            basicSize = tempSize;
        }
    }

    var change = true;//更改标识，防止用户多次快速点击
    $('.textL').click(function () {
        if (change) {
            change = false;
            if (basicSize >= 12) {
                change = true;
                return false;
            }
            var _basic = basicSize;
            basicSize = basicSize + 1;
            $.cookie("readFontSize", basicSize);
            $("#pContainer").removeClass("fontscale7").removeClass("fontscale" + _basic).addClass("fontscale" + basicSize);
            change = true;
        }
    })

    $('.textS').click(function () {
        if (change) {
            change = false;
            if (basicSize <= 1) {
                change = true;
                return false;
            }
            var _basic = basicSize;
            basicSize = basicSize - 1;
            $.cookie("readFontSize", basicSize);
            $("#pContainer").removeClass("fontscale7").removeClass("fontscale" + _basic).addClass("fontscale" + basicSize);
            change = true;
        }
    })

    //加入书架提示显示隐藏控制
    bookCaseShow();

    //下载提示显示隐藏控制
    downloadShow();

    //登录框关闭
    $('.warn_login_close').click(function () {
        $('.mask').hide();
        $(this).parent().hide()
    })
});

function bookCaseShow() {
    var showCase = $.cookie("bookcase");
    if (showCase == 0) {
        $('.buy_dd_bookcase span').hide();
    } else {
        $('.buy_dd_bookcase span').show();
    }
    $('.buy_dd_bookcase').click(function () {
        $(this).find("span").hide();
        $.cookie("bookcase", 0);
    })
}

function downloadShow() {
    var showDownload = $.cookie("download");//判断用户是否点击过关闭
    if (showDownload == undefined) {
        //下载提示 只有当用户看了20章后才显示出来（可以是不同小说，但仅限于小说类别）
        var chapterReadCount = $.cookie("chapterReadCount");
        if (chapterReadCount == undefined) {
            chapterReadCount = 1;
        }
        else {
            chapterReadCount = parseInt(chapterReadCount) + 1;
        }
        $.cookie("chapterReadCount", chapterReadCount);
        if (chapterReadCount >= 20) {
            //用户阅读章节数打到并没有点击过关闭，显示下载
            $('.Download_now').show();
        }
    }

    $('.close').click(function () {
        $(this).parent().parent().hide();
        $.cookie("download", 1);
    })
}

//加入書架
function addMark(isMark, novelId) {
    if (parseInt(isMark) == 1) {
        window.location.href = reloadUrl;
        return;
    }
    if (parseInt(novelId) == 0) {
        return;
    }
    $.ajax({
        url: '/mark/add',
        type: "post",
        data: { novelId: novelId },
        success: function (result) {
            if (result.Success) {
                if (result.ErrorCode == 1) {
                    $('.add_bookcase img').hide();
                    $('.buy_dd_bookcase span').hide();
                    $('#addToMark').text('已在书架');
                    $.tips({ message: "加入书架成功" });
                }
            } else {
                if (result.ErrorCode == 10110) {
                    $('.mask').show();
                    $('.warn_login').show();
                } else if (result.ErrorCode == 10390) {
                    window.location.href = reloadUrl;
                } else {
                    $.tips({ message: result.Message });
                }
            }
        },
        error: function (err) {
            $.tips({ message: "出错了，请重试" });
        }
    });
}