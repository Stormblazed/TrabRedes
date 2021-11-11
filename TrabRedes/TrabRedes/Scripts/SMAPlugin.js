function isBase64(t) {
    var _vl = t;
    if (_vl == "") { return false; }
    try {
        _vl = atob(t);
        return true;
    } catch (e) {//base64 string invalid
        return false;
    }
}
(function ($) {
    $.fn.SMASelect = function (options) {

        return this.each(function () {

            var _label = 'Pesquisar';
            // Default options
            var settings = $.extend({
                title: 'Selecione os registros',
                html: '<span id="_lblMessage" class="text-danger h4 hide"></span><div class="row"><div class="col-12 p-2"><label>' + _label + '</label><div class="btn btn-sm btn-danger btn-sm pull-right btn-clear"><i class="fa fa-times"></i>&nbsp;limpar conte&uacute;do</div></div></div><div class="row"><div class="col-12 p-1"><textarea name="TxMultiSelectValue" type="text" class="form-control form-control-sm w-100" style="height: 120px"></textarea></div></div><div class="row"><div class="col-12 p-4"><b class="text-danger">OBS.:</b> Utilize uma linha para cada registro.</div></div><div class="modal-footer"><div class="col-12 text-center"><div class="row"><div class="col-6 p-1"><div class="btn btn-sm btn-danger btn-sm pull-left btn-bk"><i class="fa fa-arrow-left"></i>&nbsp;Cancelar</div></div><div class="col-6 p-1"><div class="btn btn-sm btn-success btn-sm pull-right btn-ok">Ok&nbsp;<i class="fa fa-check"></i></div></div></div>'

            }, options);

            $(this).prop('autocomplete', 'off');

            if (isBase64($(this).val())) {
                $(this).prop('readonly', true);
            }

            // Apply options
            var dom = '<div class="align-items-end d-flex">' + $(this)[0].outerHTML + '<div class="btn btn-sm btn-multi-s"><i class="fa fa-1x fa-level-up text-danger"></i></div></div>'
            $(this)[0].outerHTML = dom;
            $(document).on("click", ".btn-multi-s", function () {

                var _pi = $(this).parent().next("input[type='hidden']");
                var _txp = $(this).parent().find("input[type='text']");
                //_pi.prop('readonly', true);

                _txp.prop("disabled", true);
                _txp.addClass("disabled");
                _txp.css({ "border-color": "gray" });
                _txp.prop("placeholder","usando consulta em lote");

                //alert(settings.html);

                $(this).SMAPopOver({
                    title: settings.title,
                    body: settings.html,
                    buttons: false,
                    onBack: function () {
                        if (_pi.val() == "") {
                            $(_pi).css({ color: $(_pi).data().color });
                            _txp.prop("disabled", false);
                            _txp.removeClass("disabled");
                            _txp.css({ "border-color": "" });
                            _txp.prop("placeholder", "");
                        }
                    },
                    onCreate: function (tip) {
                        var _tx = $(tip).find("textarea:eq(0)");
                        _tx.focus();
                        var _vl = _pi.val();
                        if (_vl != "") {
                            if (isBase64(_vl)) {
                                _vl = atob(_vl);
                            }
                        }
                        if (_vl.indexOf("=") > -1) {
                            _vl = _vl.split("=")[1];
                        } else {
                            //_vl = _pi.val();
                            _vl = _txp.val();
                            _txp.val("");
                        }
                        _vl = decodeURIComponent(_vl)
                        _vl = _vl.replace(/,/g, "\n");

                        _tx.val(_vl);
                        var _search = $(tip).find(".btn-success:eq(0)");
                        var _clear = $(tip).find(".btn-clear:eq(0)");
                        _clear.on("click", function () {
                            $(_tx).val("");
                            _tx.focus();
                        });
                        _search.on("click", function () {
                            if ($(_tx).val().trim() == "") {
                                _pi.val("");
                            }
                        });
                    },
                    onWork: function (tip, _data) {
                        if (_data.split("=")[1] != "") {
                            _txp.prop("disabled", true);
                            _txp.addClass("disabled");
                            _txp.css({ "border-color": "gray" });
                            _txp.prop("placeholder", "usando consulta em lote");
                        } else {
                            _txp.prop("disabled", false);
                            _txp.removeClass("disabled");
                            _txp.css({ "border-color": "" });
                            _txp.prop("placeholder", "");
                        }
                        $(_pi).val(btoa(_data));
                    }
                })
            })
            return this;
        });
    };

}(jQuery));


(function ($) {
    $.fn.SMAPopOver = function (options) {

        // Default options
        var settings = $.extend({
            title: "<i class='fa fa-plus text-success'></i>&nbsp;Adicionar novo registro",
            body: "<div style='min-width: 220px;'><div class='row'><div class='col-12 p-3'><span>Descrição:</span><br/><input type='text' class='form-control input-sm'></div></div></div>",
            buttons: true,
            onBack: function () { },
            onWork: function (tip, data) { },
            onCreate: function (tip) { }

        }, options);

        $(".SMA-popoverbasic:visible").each(function () {
            var _id = $(this).attr("id");
            $("div[aria-describedby='" + _id + "']").each(function () {
                $(this).data().option.onBack();
                $(this).popover("dispose");
            })

        });

        var el = this;
        var _template = "<div class='popover SMA-popoverbasic' role='tooltip'><div class='arrow'></div><h3 class='popover-header'></h3><div class='popover-body'></div></div>";
        var _btc = "fa-plus text-success";

        //vamos adicionar o botão de fechar
        settings.title += "<i class='fa fa-times text-secondary pull-right' style='cursor:pointer'>&nbsp;</i>";

        $(el).on('shown.bs.popover', function () {
            var _tooltipnew = $(this).data("bs.popover").tip;

            $(el).data({ option: options });

            var _btTimes = $(_tooltipnew).find(".fa-times:eq(0)");
            var _btBk = $(_tooltipnew).find(".btn-bk:eq(0)");
            var _btOk = $(_tooltipnew).find(".btn-ok:eq(0)");
            var _txInput = $(_tooltipnew).find("input:eq(0)");
            _txInput.focus();
            _txInput.on("keypress", function (e) {
                if (e.which == 13) {
                    $(_btOk).click();
                }
            });
            _btTimes.on("click", function () {
                settings.onBack();
                $(el).popover("dispose");
            });
            _btBk.on("click", function () {
                settings.onBack();
                $(el).popover("dispose");
            });
            _btOk.on("click", function () {
                var formData = $(_tooltipnew).find(":input").serialize();
                settings.onWork(_tooltipnew, formData);
                $(el).popover("dispose");
            });

            settings.onCreate(_tooltipnew);
        });


        if (settings.buttons) {
            //vamos adicionar a footer com os botões padrões
            settings.body += "<div class='row'><div class='col-12 p-3'><div class='btn btn-danger btn-sm pull-left btn-bk'><i class='fa fa-arrow-left'/>&nbsp;Cancelar</div><div class='btn btn-success btn-sm pull-right btn-ok'>Adicionar&nbsp;<i class='fa fa-plus'/></div></div></div>";
        }
        var md = $(el).popover({
            animation: false,
            content: settings.body,
            title: settings.title,
            html: true,
            sanitize: false,
            container: 'body'
        })
        $(el).popover("show");

        return this;
    };

}(jQuery));