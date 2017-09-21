var _history = "";
$(function () {

    //搜索
    $("#spanSearch").click(function (e) {
        e.preventDefault();
        Search($("#txtSearch").val().trim());
    });

    $("form").submit(function (e) {
        e.preventDefault();
        Search($("#txtSearch").val().trim());
    });

});

//将搜索词添加到搜索记录中
function AddSearchHistory(keyword) {
    var cooieValue = $.cookie("searchHistory");
    if (cooieValue != undefined && cooieValue != null) {
        //判断是否已经存在该历史搜索记录
        var arr = cooieValue.split(',');
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == keyword)
                return;
        }
        _history = cooieValue + keyword + ",";
    }
    else {
        _history = keyword + ",";
    }
    $.cookie("searchHistory", _history, { expires: 7 });
}


function Search(txt) {
    if (txt.trim() == '') {
        $.tips({ message: "搜索内容不能为空" });
        return;
    }
    AddSearchHistory(txt);
    txt = encodeURIComponent(txt);
    window.location.href = targetUrl + '?keyword=' + txt;
}
