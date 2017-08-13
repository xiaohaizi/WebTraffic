var travelShadeHtml = "<div class='travel_shade'></div>";
var travelBaseShadeHtml = "<div id='travel_Base_Shade'></div>";
var travelBase = {

    travelOpup: function (_content, _option) {
        var _dialogdefaults = {
            width: 400,
            height: 400,
        }
        var dwidth = $(window).width();
        var dheight = $(document).height();
        var dheight1 = $(window).height();
        var opts = _dialogdefaults;

        if (_option)
            opts = jQuery.extend(_dialogdefaults, _option);

        var contentHtml = $("<div class='travelShadeboder' style='width:" + opts.width + "px;height:" + opts.height + "px;background-color: #fff;z-index: 2000;position: fixed;top: " + ((dheight1 - opts.height) / 2) + "px;left:" + ((dwidth - opts.width) / 2) + "px'><i class='travel_shade_close'>X</i></div>");
        contentHtml.append(_content);

        var travelShadeHtmlShow = $(travelBaseShadeHtml).append($(travelShadeHtml).css({ "height": dheight }));


        $("body").append(travelShadeHtmlShow);
        travelShadeHtmlShow.append(contentHtml);
        $(".travel_shade_close", travelShadeHtmlShow).on("click", function () {
            // $("#travel_Base_Shade")
            $("#travel_Base_Shade").remove();
        });

    }
}

//Post提交
function AjaxPost(_url, _data, _fun) {

    $.post(_url, _data, _fun, "json");
}
//跳转
function redirectUrl(url, time) {
    time = !time ? 1000 : time;
    setTimeout(function () {
        location.href = url;
    }, time);
}

