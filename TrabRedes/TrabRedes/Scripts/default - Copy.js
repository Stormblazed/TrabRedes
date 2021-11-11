
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

function distinctArray(value, index, self) {
    return self.indexOf(value) === index;
}

jQuery.fn.outerHTML = function (s) {
    return s
        ? this.before(s).remove()
        : jQuery("<p>").append(this.eq(0).clone()).html();
};

//Script para ShortCUT
/**
 * http://www.openjs.com/scripts/events/keyboard_shortcuts/
 * Version : 2.01.A
 * By Binny V A
 * License : BSD
 */
shortcut = {
    'all_shortcuts': {},//All the shortcuts are stored in this array
    'add': function (shortcut_combination, callback, opt) {
        //Provide a set of default options
        var default_options = {
            'type': 'keydown',
            'propagate': false,
            'disable_in_input': false,
            'target': document,
            'keycode': false
        }
        if (!opt) opt = default_options;
        else {
            for (var dfo in default_options) {
                if (typeof opt[dfo] == 'undefined') opt[dfo] = default_options[dfo];
            }
        }

        var ele = opt.target
        if (typeof opt.target == 'string') ele = document.getElementById(opt.target);
        var ths = this;
        shortcut_combination = shortcut_combination.toLowerCase();

        //The function to be called at keypress
        var func = function (e) {
            e = e || window.event;

            if (opt['disable_in_input']) { //Don't enable shortcut keys in Input, Textarea fields
                var element;
                if (e.target) element = e.target;
                else if (e.srcElement) element = e.srcElement;
                if (element.nodeType == 3) element = element.parentNode;

                if (element.tagName == 'INPUT' || element.tagName == 'TEXTAREA') return;
            }

            //Find Which key is pressed
            var code = "";
            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;
            var character = String.fromCharCode(code).toLowerCase();

            if (code == 188) character = ","; //If the user presses , when the type is onkeydown
            if (code == 190) character = "."; //If the user presses , when the type is onkeydown

            var keys = shortcut_combination.split("+");
            //Key Pressed - counts the number of valid keypresses - if it is same as the number of keys, the shortcut function is invoked
            var kp = 0;

            //Work around for stupid Shift key bug created by using lowercase - as a result the shift+num combination was broken
            var shift_nums = {
                "`": "~",
                "1": "!",
                "2": "@",
                "3": "#",
                "4": "$",
                "5": "%",
                "6": "^",
                "7": "&",
                "8": "*",
                "9": "(",
                "0": ")",
                "-": "_",
                "=": "+",
                ";": ":",
                "'": "\"",
                ",": "<",
                ".": ">",
                "/": "?",
                "\\": "|"
            }
            //Special Keys - and their codes
            var special_keys = {
                'esc': 27,
                'escape': 27,
                'tab': 9,
                'space': 32,
                'return': 13,
                'enter': 13,
                'backspace': 8,

                'scrolllock': 145,
                'scroll_lock': 145,
                'scroll': 145,
                'capslock': 20,
                'caps_lock': 20,
                'caps': 20,
                'numlock': 144,
                'num_lock': 144,
                'num': 144,

                'pause': 19,
                'break': 19,

                'insert': 45,
                'home': 36,
                'delete': 46,
                'end': 35,

                'pageup': 33,
                'page_up': 33,
                'pu': 33,

                'pagedown': 34,
                'page_down': 34,
                'pd': 34,

                'left': 37,
                'up': 38,
                'right': 39,
                'down': 40,

                'f1': 112,
                'f2': 113,
                'f3': 114,
                'f4': 115,
                'f5': 116,
                'f6': 117,
                'f7': 118,
                'f8': 119,
                'f9': 120,
                'f10': 121,
                'f11': 122,
                'f12': 123
            }

            var modifiers = {
                shift: { wanted: false, pressed: false },
                ctrl: { wanted: false, pressed: false },
                alt: { wanted: false, pressed: false },
                meta: { wanted: false, pressed: false }	//Meta is Mac specific
            };

            if (e.ctrlKey) modifiers.ctrl.pressed = true;
            if (e.shiftKey) modifiers.shift.pressed = true;
            if (e.altKey) modifiers.alt.pressed = true;
            if (e.metaKey) modifiers.meta.pressed = true;

            for (var i = 0; k = keys[i], i < keys.length; i++) {
                //Modifiers
                if (k == 'ctrl' || k == 'control') {
                    kp++;
                    modifiers.ctrl.wanted = true;

                } else if (k == 'shift') {
                    kp++;
                    modifiers.shift.wanted = true;

                } else if (k == 'alt') {
                    kp++;
                    modifiers.alt.wanted = true;
                } else if (k == 'meta') {
                    kp++;
                    modifiers.meta.wanted = true;
                } else if (k.length > 1) { //If it is a special key
                    if (special_keys[k] == code) kp++;

                } else if (opt['keycode']) {
                    if (opt['keycode'] == code) kp++;

                } else { //The special keys did not match
                    if (character == k) kp++;
                    else {
                        if (shift_nums[character] && e.shiftKey) { //Stupid Shift key bug created by using lowercase
                            character = shift_nums[character];
                            if (character == k) kp++;
                        }
                    }
                }
            }

            if (kp == keys.length &&
                modifiers.ctrl.pressed == modifiers.ctrl.wanted &&
                modifiers.shift.pressed == modifiers.shift.wanted &&
                modifiers.alt.pressed == modifiers.alt.wanted &&
                modifiers.meta.pressed == modifiers.meta.wanted) {
                callback(e);

                if (!opt['propagate']) { //Stop the event
                    //e.cancelBubble is supported by IE - this will kill the bubbling process.
                    e.cancelBubble = true;
                    e.returnValue = false;

                    //e.stopPropagation works in Firefox.
                    if (e.stopPropagation) {
                        e.stopPropagation();
                        e.preventDefault();
                    }
                    return false;
                }
            }
        }
        this.all_shortcuts[shortcut_combination] = {
            'callback': func,
            'target': ele,
            'event': opt['type']
        };
        //Attach the function with the event
        if (ele.addEventListener) ele.addEventListener(opt['type'], func, false);
        else if (ele.attachEvent) ele.attachEvent('on' + opt['type'], func);
        else ele['on' + opt['type']] = func;
    },

    //Remove the shortcut - just specify the shortcut and I will remove the binding
    'remove': function (shortcut_combination) {
        shortcut_combination = shortcut_combination.toLowerCase();
        var binding = this.all_shortcuts[shortcut_combination];
        delete (this.all_shortcuts[shortcut_combination])
        if (!binding) return;
        var type = binding['event'];
        var ele = binding['target'];
        var callback = binding['callback'];

        if (ele.detachEvent) ele.detachEvent('on' + type, callback);
        else if (ele.removeEventListener) ele.removeEventListener(type, callback, false);
        else ele['on' + type] = false;
    }
}


//------------------------------------------------------ directAction functions

var directActionField = "__directAction";
shortcut.add("Ctrl+ENTER", function () {
    $(".nav-link[data-widget='control-sidebar']")[0].click();
    setTimeout(function () {
        $("aside.control-sidebar").find("input").focus();
    }, 100);
});
shortcut.add("Ctrl+ALT+P", function () {
    alert('abrir proposta');
});

function directAction(id) {
    var fid = '../Services/AjaxService.asmx';
    var f = $("form:eq(0)").serialize();
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/directAction',
        data: "{'sid': '" + id + "','f': '" + f + "'}",
        dataType: "json",
        success: function (data) {
            if (data.d.Sucess) {
                eval(data.d.Data);
                $("#" + directActionField).val("");
            }
            toggleWait(false);
        },
        error: function (data) {
            toggleWait(false);
        }
    });
}

//------------------------------------------------------ directAction functions

//------------------------------------------------------ doSecureAction()

function doSecureAction(_p, _callback, _error) {
    /*
    _p.fid = ".../Teste.aspx";
    _p.sid = '{"sid":"' + dv + '", "f": ' + f + '}';
    _p.method = '/expressRecordEditor';
    */
    if (_p.fid === undefined) { _p.fid = window.location.pathname; }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: _p.fid + "/" + _p.method,
        data: JSON.stringify(_p.sid),
        dataType: "json",
        success: function (data) {
            if (data.d.Sucess) {
                _callback(data.d);
            } else {
                if (_error !== undefined) {
                    _error(data.d.Message);
                }
            }
        }
    });
}


//------------------------------------------------------ doRequest()

function sendInternalMessage() {
    var _html = '<div id="modal_email" class="modal modal-wide fade porcima" role="dialog" aria-labelledby="modal_email" aria-hidden="true"><div class="modal-dialog"><div class="modal-content modal-md"><div class="modal-header"><span class="pull-left">Enviar Mensagem</span></div><div class="modal-body"><span id="_lblMessage" class="text-danger h4 hide"></span><div class="row"><div class="col-12 p-2"><label>De:</label></div><div class="col-12 p-2"><span id="lblFrom" class="text-danger h3">Robson Ursino Rocha</span></div></div><div class="row"><div class="col-12 p-1"><label>Para:</label></div><div class="col-12 p-1"><input type="text" id="emailto" class="form-control autocomplete" data-value="emailto" /><span class="autocomplete-value hide"></span><div class="autocomplete-multi text-secondary p-3"></div></div></div><div class="row"><div class="col-12 p-1"><label>Mensagem:</label></div><div class="col-12 p-1"><textarea id="messagetext" class="form-control" style="height: 125px;"></textarea></div></div><div class="row"><div class="col-12 p-4"><b class="text-danger">OBS.:</b> Todas as mensagens trocadas pelo sistema são gravadas e podem ser objeto para uso disciplinar.</div><div class="col-12"><span class="text-info"><i>Anexar um arquivo</i></span><input type="file" /><br/></div></div><div class="modal-footer"><div class="col-12 text-center"><div class="row"><div class="col-6 p-1"><div class="btn btn-danger btn-sm pull-left" data-dismiss="modal"><i class="fa fa-arrow-left"></i>&nbsp;Cancelar</div></div><div class="col-6 p-1"><div class="btn btn-success btn-sm pull-right">Enviar&nbsp;<i class="fa fa-envelope-o"></i></div></div></div></div></div></div></div></div></div>';
    var _modal = $(_html).modal({ backdrop: 'static', show: true });
    _modal.on("hidden.bs.modal", function () {
        _modal.empty();
        _modal.remove();
    });
    _modal.on('shown.bs.modal', function () {
        var _tx = $(_modal).find("input:eq(0)");
        var _msg = $(_modal).find("textarea:eq(0)");
        var _toArray = $(_modal).find(".autocomplete-multi");
        _tx.focus();
        listenAutoComplete({ target: _tx }, function (data) {
            _toArray.append("<div class='ml-4'>" + data.split("|")[0] + "<span class='autocomplete-multi-value hide'>" + data.split("|")[1] + "</span>&nbsp;<i class='fa fa-times text-danger' style='cursor:pointer' onclick='$(this).parent().remove();'></i></div>");
            _tx.val("");
            _tx.focus();
        });
        var _send = $(_modal).find(".btn-success:eq(0)");
        _send.on("click", function () {
            var _to = $(_toArray).find(".autocomplete-multi-value").map(function (e) { return $(this).text(); }).get().join();
            if (_to == "") {
                $(_modal).find("#_lblMessage").removeClass("hide");
                $(_modal).find("#_lblMessage").html("<div class='alert alert-danger'>É preciso selecionar ao menos um destinatário para mensagem</div>");
                $(_modal).find("#_lblMessage").fadeIn(3000, function () {
                    $(this).fadeOut(3000);
                })
                return;
            }
            if ($(_msg).val() == "") {
                $(_modal).find("#_lblMessage").removeClass("hide");
                $(_modal).find("#_lblMessage").html("<div class='alert alert-danger'>É preciso digitar o conteúdo da mensagem</div>");
                $(_modal).find("#_lblMessage").fadeIn(3000, function () {
                    $(this).fadeOut(3000);
                })
                return;
            }

            if (!isButtonEnabled($(_send))) { return false; }
            disableButtonAndWait($(_send));

            var sid = '{"to":"' + _to + '", "message": "' + btoa($(_msg).val()) + '"}';
            var fid = '../Services/AjaxService.asmx';
            var f = $("form:eq(0)").serialize();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: fid + '/sendInternalMessage',
                data: "{'sid': '" + sid + "','f': '" + f + "'}",
                dataType: "json",
                success: function (data) {
                    enableButtonAndRemoveWait($(_send));
                    $(_modal).modal("hide");
                    if (data.d.Sucess) {
                        openModalMsg(data.d.Message, false);
                    } else {
                        openModalMsg(data.d.Message, true);
                    }
                },
                error: function (data) {

                }
            });

        });
    });
}

function addUserShortcut(el, type) {
    disableButtonAndWait($(el));

    doSecureAction({
        fid: "../Services/AjaxService.asmx",
        sid: { sid: type, f: $("form:eq(0)").serialize() },
        method: 'addUserShortcut'
    }, function (_data) {
        enableButtonAndRemoveWait($(el));

    });
}

function changeStartupPage(el) {
    var _pin = false;
    var _p = "";
    if ($(el).find("i").hasClass("fa-thumb-tack")) { _pin = true; }
    if (_pin) {
        $(el).find("i").removeClass("text-info").addClass("text-danger");
        $(el).find("i").removeClass("fa-thumb-tack").addClass("fa-circle");
        $(el).find("i").attr("title", "Esta página está selecionada como página inicial");
        _p = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    } else {
        $(el).find("i").removeClass("text-danger").addClass("text-info");
        $(el).find("i").removeClass("fa-circle").addClass("fa-thumb-tack");
        $(el).find("i").attr("title", "Selecionar esta página como página inicial");
    }
    var settings = { "name": "settings.page.startup", "show": false, "value": _p };
    changeSettings(settings);
}

function readedMessage(sid) {
    var fid = '../Services/AjaxService.asmx';
    var f = $("form:eq(0)").serialize();
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/readedMessage',
        data: "{'sid': '" + sid + "','f': '" + f + "'}",
        dataType: "json",
        success: function (data) {
            viewNotification();
        },
        error: function (data) {

        }
    });
}


function viewAllNotification(title) {
    var _bt;
    var _url = "../Processo/ModalMensagemInterna.aspx";
    checkNotification("all", function (_data) {
        var _json = JSON.parse(_data);
        if (title == "notify") {
            _json = atob(_json._notify)
            title = "Notificações";

            _url = "../Processo/ModalNotificaoInterna.aspx";
        } else {
            _json = atob(_json._msg)
            title = "Mensagens";

            _bt = $('<div role="button" class="btn btn-info btn-sm">Nova Mensagem&nbsp;<i class="fa fa-plus"></i></div>');
            _bt.on("click", function () {
                var iframe = $(this).closest("div.modal-content").find("iframe")[0];
                var _urlFrame = iframe.src;
                if (!_urlFrame.includes("?method=newmessage")) {
                    _urlFrame = iframe.src + "?method=newmessage";
                }
                //vamos redimensionar o iframe para 0px, a função resizeIframe() se encarrega do resto
                $(iframe).css("height", "0px");
                iframe.src = _urlFrame;
            })
        }
        createIframeModal(_url, "full", _bt);
    });

}

function viewNotification() {
    checkNotification("check", function (_data) {
        var _html = JSON.parse(_data);

        //notificações alert
        try {
            if (toastr !== undefined) {
                if (_html._ctnotify > 0) {
                    toastr["warning"]("Voc&#234; tem " + _html._ctnotify + " nova(s) notifica&#231;&#227;o(s)");
                }
                if (_html._ctmsg > 0) {
                    toastr["info"]("Voc&#234; tem " + _html._ctmsg + " nova(s) mensagem(s)");
                }
            }
        }
        catch {
            //não vamos fazer nada
        }

        $("#menuNotificacao").empty()
        $("#menuNotificacao").html(atob(encodeURI(_html._notify)));
        $("#menuMensagem").empty()
        $("#menuMensagem").html(atob(encodeURI(_html._msg)));
    });
}
function checkNotification(_type, _callback) {
    var fid = '../Services/AjaxService.asmx';
    var f = $("form:eq(0)").serialize();
    var sid = "check";
    if (_type !== undefined) { sid = _type; }
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/loadUserMessage',
        data: "{'sid': '" + sid + "','f': '" + f + "'}",
        dataType: "json",
        success: function (data) {
            if (data.d.Sucess) {
                if (_callback !== undefined) {
                    eval(_callback(data.d.Data));
                }
            }
        },
        error: function (data) {

        }
    });
}

function initListenNotification() {
    var _time = ((1000 * 60) * 3); //3 minutos
    setInterval(function () {
        checkNotification();
    }, _time);
}

function openModalConfirm(_p, onOkClick, onCancelClick) {
    var _msg = _p;
    var _size = "modal-sm";
    var _btOkLabel = "Sim";
    var _btCancelLabel = "N&atilde;o";
    if (typeof _p !== typeof "") {
        _msg = _p.message;
        if (_p.size !== undefined) { _size = _p.size; }
        if (_p.oklabel !== undefined) { _btOkLabel = _p.oklabel; }
        if (_p.cancellabel !== undefined) { _btCancelLabel = _p.cancellabel; }
    }

    var _html = '<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true"><div class="modal-dialog modal-dialog-centered ' + _size + '" role="document"><div class="modal-content"><div class="modal-body"><span>' + _msg + '</span></div><div class="modal-footer"><div class="w-100"><div class="btn btn-danger btn-sm text-center mr-2 mt-2 float-left" data-dismiss="modal" aria-label="' + _btCancelLabel + '">' + _btCancelLabel + '&nbsp;<i class="fa fa-times"></i></div><div class="btn btn-success float-right btn-sm text-center mr-2 mt-2" aria-label="' + _btOkLabel + '">' + _btOkLabel + '&nbsp;<i class="fa fa-check"></i></div></div></div></div></div>';
    var _modal = $(_html).modal({ backdrop: 'static', show: true });
    _modal.on("hidden.bs.modal", function () {
        _modal.empty();
        _modal.remove();
    });
    _modal.on("shown.bs.modal", function () {
        var _ok = $(_modal).find(".btn-success:eq(0)");
        var _cancel = $(_modal).find(".btn-danger:eq(0)");

        $(_ok).unbind("click");
        $(_ok).bind("click", onOkClick);

        $(_cancel).unbind("click");
        $(_cancel).bind("click", onCancelClick);

    });
    return _modal;
}

function openModalMsg(msg, _p, _callback) {
    var _size = "modal-sm";
    if (typeof _p !== typeof true) {
        _size = _p.size;
    }
    var _html = '<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true"><div class="modal-dialog modal-dialog-centered ' + _size + '" role="document"><div class="modal-content"><div class="modal-body"><span>' + msg + '</span></div><div class="modal-footer"><div><div class="btn btn-danger btn-sm text-center mr-2 mt-2" data-dismiss="modal" aria-label="Fechar">Fechar</div></div></div></div></div>';
    var _modal = $(_html).modal({ backdrop: 'true', show: true });
    _modal.on("hidden.bs.modal", function () {
        if (_callback !== undefined) {
            _callback(_modal);
        }
        _modal.empty();
        _modal.remove();
    });
    return _modal;
}

function openModalProgress(msg, _p, onready) {
    toggleWait(false);
    var _size = "modal-sm";
    if (typeof _p !== typeof true) {
        _size = _p.size;
    }
    var _html = '<div class="modal fade modal-progress" tabindex="-1" role="dialog" aria-hidden="true"><div class="modal-dialog modal-dialog-centered ' + _size + '" role="document"><div class="modal-content"><div class="modal-body"><div class="container"><br><h6 class="text-center">' + msg + '</h6><div class="container"><div class="progress mb-3"><div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"><span class="sr-only"></span></div></div></div></div><span class="small modal-progress-secodary-text text-center"></span></div></div></div>';
    var _modal = $(_html).modal({ backdrop: 'static', keyboard: false, show: true });
    _modal.on("hidden.bs.modal", function () {
        _modal.empty();
        _modal.remove();
    });
    if (onready !== undefined) {
        _modal.on('shown.bs.modal', function () {
            onready(_modal);
        });
    }
}

function createIframeModal(_url, _size, _bt) {
    if (_size === undefined || _size == "full") { _size = "modal-full"; }

    var m = "<iframe scrolling='no' frameborder='0' border='0' class='frame-info w-100 hide' src='" + _url + "' onload='resizeIframe(this)'></iframe>";
    var _html = '<div class="modal fade" tabindex="-1" role="dialog" aria-hidden="true"><div class="modal-dialog modal-dialog-centered ' + _size + '" role="document"><div class="modal-content"><div><div class="float-left ml-2 mt-2 custom-buttons"></div><div class="btn btn-danger btn-sm float-right mr-2 mt-2" data-dismiss="modal" aria-label="Fechar"><span aria-hidden="true">fechar x</span></div></div><div class="modal-body"><div class="text-center w-100 loading"><img src="../images/loading.gif" /><br/><span>Carregando, por favor aguarde ...</span></div>' + m + '</div></div></div></div>';
    var _modal = $(_html).modal({ backdrop: 'true', show: true });
    _modal.on("hidden.bs.modal", function () {
        _modal.empty();
        _modal.remove();
    });
    _modal.on('shown.bs.modal', function () {
        if (_bt !== undefined) {
            $(_modal).find(".custom-buttons").append(_bt);
        }
    });
    return _modal;
}

function resizeIframe(el) {
    $(el).removeClass("hide");
    var h = el.contentWindow.document.body.scrollHeight;
    if (h != 0) {
        el.style.height = h + 'px';
    } else {
        el.style.height = screen.availHeight + 'px';
    }
    $(el).parent().find("div.loading").empty();

}

function iframeform(url) {
    var object = this;
    object.time = new Date().getTime();
    object.form = $('<form action="' + url + '" target="iframe' + object.time + '" method="post" style="display:none;" id="form' + object.time + '" name="form' + object.time + '"></form>');

    object.addParameter = function (parameter, value) {
        $("<input type='hidden' />")
            .attr("name", parameter)
            .attr("value", value)
            .appendTo(object.form);
    }

    object.send = function () {
        var iframe = $('<iframe data-time="' + object.time + '" class="w-100" style="margin:0;padding:0;border:none;" scrolling="no" id="iframe' + object.time + '" name="iframe' + object.time + '"></iframe>');
        $("#FUFoto_DataContainer").append(iframe);
        $("#FUFoto_DataContainer").append(object.form);
        object.form.submit();
        iframe.load(function () { $('#form' + $(this).data('time')).remove(); });
    }
}

function changeSettings(dataSet) {
    var fid = '../Services/AjaxService.asmx';
    var f = $("form:eq(0)").serialize();

    //Verificar se vamos mostrar o retorno ao usuário
    var show = dataSet.show;

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/changeUserSettings',
        data: "{'sid': '" + JSON.stringify(dataSet) + "', 'f': '" + f + "'}",
        dataType: "json",
        success: function (data) {
            if (show) {
                if (data.d.Sucess) {
                    openModalMsg(data.d.Message, false);
                } else {
                    openModalMsg(data.d.Message, true);
                }
            }
        },
        error: function (data) {
            if (show) {
                openModalMsg(data.responseText);
            }
        }
    });
}

function loadSettings(name, callback) {
    var fid = '../Services/AjaxService.asmx';
    var f = $("form:eq(0)").serialize();
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/loadUserSettings',
        data: "{'sid': '" + name + "', 'f': '" + f + "'}",
        dataType: "json",
        success: function (data) {
            if (data.d.Sucess) {
                eval(callback(data.d.Data));
            }
        },
        error: function (data) {
            openModalMsg(data.responseText);
        }
    });
}

function listenAutoComplete(options, _callback) {
    var settings = $.extend({
        fid: '../Services/AjaxService.asmx',
        target: $('input.autocomplete')
    }, options);
    $(settings.target).each(function (i, el) {
        el = $(el);
        var el_value = el.next('input[type="hidden"]');
        var t = $(el).attr("data-value");
        var f = $("form:eq(0)").serialize();
        var _class = el.parent().find("i").attr("class");
        el.autocomplete({
            autoSelectFirst: true,
            showNoSuggestionNotice: true,
            noSuggestionNotice: 'Nenhum resultado encontrado',
            lookup: function (query, done) {
                el_value.val('');
                el.parent().find("i.fa-search").attr("class", "fa fa-spin fa-spinner");
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: settings.fid + '/getSuggestion',
                    data: "{'q': '" + query + "', 't' : '" + t + "', 'f' : '" + f + "'}",
                    dataType: "json",
                    success: function (data) {
                        el.parent().find("i").attr("class", _class);

                        var ret = $.parseJSON(data.d.Data)
                        done(ret);
                    },
                    error: function (data) {
                        var result = {
                            suggestions: [
                                { "value": "Erro ao consultar os dados", "data": "" }
                            ]
                        };
                        done(result);
                    }
                });
            },
            onSelect: function (suggestion) {
                if (suggestion.data == "") {
                    event.preventDefault();
                    return false;
                }
                if (el_value !== undefined) {
                    el_value.val(suggestion.value + '|' + suggestion.data);
                }
                if (_callback !== undefined) {
                    eval(_callback(suggestion.value + '|' + suggestion.data));
                } else {
                    if (t.indexOf("__directAction") != -1) {
                        directAction(suggestion.data);
                    }
                }

            },
            onInvalidateSelection: function () {
                el_value.val('');
            }
        });
    });
}

function openModalConfirm_(dsc, onOkClick, onCancelClick) {

    $('#modal_confirm').on('show.bs.modal', function () {
        //Quando o modal aparecer
    });
    $('#modal_confirm').modal({ backdrop: 'static' });
    $('#modal_confirm_input_value').val('');
    $('#modal_confirm').modal('show');
    $('#modal_confirm_msgText').html(dsc);
    $("#modal_confirm_btnConfirmar").unbind("click");
    $("#modal_confirm_btnFechar").unbind("click");
    $("#modal_confirm_btnConfirmar").bind("click", onOkClick);
    $("#modal_confirm_btnFechar").bind("click", onCancelClick);
    $("#modal_confirm_btnFechar").focus();
};

function makeMenuTree(_level, _callback) {
    doSecureAction({
        fid: "../Services/AjaxService.asmx",
        sid: {
            sid: JSON.stringify({ level: _level }), f: $("form:eq(0)").serialize()
        },
        method: 'getMenuTree'
    }, function (_d) {
        eval(_callback(_d.Data))
    });
}

function copyToClipboard(element) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(element).text().trim()).select();
    document.execCommand("copy");
    $temp.remove();

    if (toastr !== undefined) {
        toastr["success"]("<b style='color: #000000'>" + $(element).text().trim() + "</b> copiado para o clipboard");
    }
}

function toggleWait(param) {
    if (typeof param == typeof true) {
        if (param) {
            if ($(".wait-loading").is(":visible")) { return; }
            $(".wait-loading").fadeIn("fast");
        } else {
            $(".wait-loading").fadeOut("fast");
        }
    }
};

function openModalMsg_(dsc, error, onOkClick) {
    $('#modal_msg').on('show.bs.modal', function () {
        //Quando o modal aparecer
    });
    $('#modal_msg').on("hidden.bs.modal", function () {
        //Quando o modal sumir
    });
    $('#modal_msg').modal({ backdrop: 'static' });
    $('#modal_msg').modal('show');
    $('#modal_msg_msgText').html(dsc);
    $("#modal_msg_btnFechar").text('Ok');
    if (!error) {
        $('#modal_msg_msgText').removeClass("text-danger").addClass("text-success");
        $('#modal_msg_h2Title').removeClass("text-danger").addClass("text-success");
        $('#modal_msg_icon').removeClass("text-danger").addClass("text-success");
        $("#modal_msg_btnFechar").removeClass("btn-danger").addClass("btn-success");

    } else {
        $('#modal_msg_msgText').addClass("text-danger");
        $('#modal_msg_h2Title').addClass("text-danger");
        $('#modal_msg_icon').addClass("text-danger");
    }
    if (typeof onOkClick === 'undefined' || !onOkClick) { $('#modal_msg').attr("tabindex", 1000); }

    $("#modal_msg_btnFechar").unbind("click");
    $("#modal_msg_btnFechar").bind("click", onOkClick)
    $("#modal_msg_btnFechar").focus();
};

function clearDataTable(selector) {
    if ($.fn.DataTable.isDataTable(selector)) {
        try {
            $(selector).DataTable().destroy();
        } catch (ex) {
        }
    }
    $(selector + " tbody").empty();
}
function createDataTable(_p, _ajax) {
    if ($.fn.DataTable.isDataTable(_p.selector)) {
        $(_p.selector).DataTable().destroy();
    }
    if (_p.compact === undefined) { _p.compact = false; }
    if (!_p.compact) {
        if (_p.exportButtons === undefined) { _p.exportButtons = true; }
        if (_p.buttons === undefined) {
            _p.buttons = createDefaultButtons(undefined, _p.exportButtons);
        } else {
            _p.buttons = createDefaultButtons(_p.buttons, _p.exportButtons);
        }
    }

    //load wrap content
    //var settings = { "name": "datatables.content.wrap", "show": false, "value": _wrap };
    //changeSettings(settings);


    var _dom = '<"pull-left custom-tools-datatable"l><"pull-right"f>rtB<"pull-left"i><"pull-right"p>';
    var _select = {
        style: 'multi',
        selector: 'td:not(:has(> .btn)):not(:has(> a)):not(:has(> .dropdown)):not(:has(> i.fa-copy))'
    };
    var _columnDefs = [{
        orderable: false,
        className: 'select-checkbox',
        targets: -1
    }];

    if (_p.compact) { _dom = 'rt<"pull-left"i><"pull-right"p>'; }
    if (_p.compact) { _select = {}; }
    if (_p.compact) { _columnDefs = []; }

    var _dt = $(_p.selector).dataTable({
        dom: _dom,
        select: true,
        autoWidth: false,
        "ordering": false,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        buttons: _p.buttons,
        select: _select,
        columnDefs: _columnDefs,
        ajax: _ajax
    });
    _dt.on("select.dt deselect.dt", function (e, dt, type, indexes) {
        if (e.type === "select") {
            var rows = dt.rows(indexes).nodes().to$();
            $.each(rows, function () {
                if ($(this).hasClass("row-select-none")) {
                    dt.row($(this)).deselect();
                }
            })
        }
    });
    return _dt;
}

function constructDefaultDataTable(options, _callback, _error) {

    //Vamos informar o valor default do fid, caso não tenha sido informado.
    var _p = $.extend({
        fid: window.location.pathname + "/getBaseData",
        selector: ".SMA-table",
        f: $("form:eq(0)").serialize(),
        sid: "constructDefaultDataTable",
        compact: false,
        exportButtons: false,
    }, options);

    $(_p.selector + ' tbody').empty();

    var settings = { selector: _p.selector, buttons: _p.buttons, exportButtons: _p.exportButtons, compact: _p.compact };

    var _ajax = {
        url: _p.fid,
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: function (d) {
            return JSON.stringify({ "sid": _p.sid, "f": _p.f });
        },
        success: function (data) {
            if (data.d.Sucess) {

                if ($.fn.DataTable.isDataTable(_p.selector)) {
                    var table = $(_p.selector).dataTable().api();
                    table.rows.add($(data.d.Data)).draw();
                } else {
                    $(_p.selector + " tbody").append(data.d.Data);
                    createDataTable(settings);
                }

                //var table = $(_p.selector).DataTable();


                //setPagingCount(table);

                //Terminamos de realizar a chamada
                if (_callback !== undefined) {
                    eval(_callback(table));
                }
            } else {
                //Tabela está vazia
                var table = $(_p.selector).DataTable();
                table.clear().draw();

                if (_error !== undefined) {
                    eval(_error(data.d.Message));
                }

            }
        }
    }

    try {
        createDataTable(settings, _ajax);
    } catch (ex) {
        alert(ex);
    }
}


function onCreateDatatable(settings) {
    var api = new $.fn.dataTable.Api(settings);
    var _container = api.table().container();
    if ($(_container).find("div.custom-tools-datatable").find("span.tools").length == 0) {
        var _parent = $(_container).find("div.custom-tools-datatable").find("div:eq(0)");
        var _rowblocked = "";
        /*
        _rowblocked = api.table().rows().nodes().to$().map(function (e) { return $(this).hasClass("row-select-none"); }).get().join();
        if (_rowblocked.replaceAll(",","") != "") { _rowblocked = "<i onclick='dataTableOptionChange(this,\"unlocked\")' class='fa fa-info-circle text-danger c-pointer btn btn-sm' title='Mostrar somente registros selecionaveis'></i>" }
        */
        _parent.prepend("<span class='tools'><i onclick='dataTableOptionChange(this,\"wrap\")' class='fa fa-align-justify text-primary c-pointer btn btn-sm' title='Expandir/quebrar texto da grade'></i><i onclick='dataTableOptionChange(this,\"fontup\")' class='fa fa-a c-pointer btn btn-sm' title='Aumentar texto da grade'></i><i onclick='dataTableOptionChange(this,\"fontdown\")' class='fa fa-a-downcase c-pointer btn btn-sm' title='Diminuir texto da grade'></i>" + _rowblocked + "&nbsp;</div>");
    }
    loadSettings("datatables.paging.current,datatables.content.size,datatables.content.wrap", function (_data) {
        var _ret = JSON.parse(_data);
        $(_ret).each(function () {
            var id = "";
            var value = this.value;
            if (this.setting == "datatables.content.wrap") {
                id = "wrap";
            } else if (this.setting == "datatables.content.size") {
                id = "fontup";
            }
            dataTableOptionChange($(_container).find("div.custom-tools-datatable"), id, value);
            $("i").tooltip();
        });
    })
}

function dataTableOptionChange(el, t, _default) {
    var tb = $(el).closest(".dataTables_wrapper").find("table");
    var tb = $(tb).dataTable().api();
    var _rows = tb.rows().nodes().to$()
    if (t.toLowerCase() == "wrap") {
        var _wrap = (_rows.eq(0).find(".compact-column").length == 0);
        if (_default !== undefined) { _wrap = _default; }
        if (_wrap) {
            _rows.find("td").addClass("compact-column");
        } else {
            _rows.find("td").removeClass("compact-column");
        }
        if (_default === undefined) {
            var settings = { "name": "datatables.content.wrap", "show": false, "value": _wrap };
            changeSettings(settings);
        }
    } else if (t.toLowerCase() == "unlocked") {
        if ($(el).hasClass("fa-circle")) {
            tb.column(1).search("").draw();
            $(el).removeClass("fa-circle").addClass("fa-info-circle");
            $(el).prop("title", "Mostrar somente registros selecionaveis");
        } else {
            tb.column(1).search(t.toLowerCase()).draw();
            $(el).removeClass("fa-info-circle").addClass("fa-circle");
            $(el).prop("title", "Mostrar todos os registros");
        }
    } else if (t.toLowerCase() == "fontup" || t.toLowerCase() == "fontdown") {
        var _size = _rows.find("td:eq(0)").css("font-size");
        if (_default !== undefined) { _size = _default; }
        _size = _size.replaceAll("px", "")
        _size = parseFloat(_size);
        if (t.toLowerCase() == "fontup") {
            _size = _size + (_size * 0.04);
        } else if (t.toLowerCase() == "fontdown") {
            _size = _size - (_size * 0.04);
        }
        _size = _size + "px"
        if (_default === undefined) {
            var settings = { "name": "datatables.content.size", "show": false, "value": _size };
            changeSettings(settings);
        }
        _rows.css("font-size", _size)
    }
}

//Custom Datatables.net Buttons
function createDefaultButtons(_bt, _export) {
    var exportTitle = document.title;
    var filterTitle = undefined;
    var defaultButtons = [];
    if ($(".export-title").length != 0) { exportTitle = $(".export-title").text(); }
    if (_export) {
        defaultButtons.push({
            extend: 'pdf', text: 'PDF <i class="fa fa-file-pdf-o"></i>',
            messageTop: filterTitle,
            title: exportTitle,
            className: 'btn btn-secondary btn-sm datatable-button-space',
            exportOptions: {
                columns: 'th:not(:first-child):not(:last-child)'
            },
            orientation: 'landscape',
            pageSize: 'A3',
            action: function (e, dt, node, config) {
                if (isElementDisable($(node))) { return false; }
                disableButtonAndWait($(node));
                setTimeout(function () {
                    $.fn.dataTable.ext.buttons.pdfHtml5.action.call(dt.button(this), e, dt, node, config);
                    enableButtonAndRemoveWait($(node));
                }, 300);
            }
        });
        defaultButtons.push({
            extend: 'excel', text: 'Excel <i class="fa fa-file-excel-o"</i>',
            messageTop: filterTitle,
            title: exportTitle,
            className: 'btn btn-success btn-sm datatable-button-space',
            exportOptions: {
                columns: 'th:not(:first-child):not(:last-child)'
            },
            action: function (e, dt, node, config) {
                if (isElementDisable($(node))) { return false; }
                disableButtonAndWait($(node));
                setTimeout(function () {
                    $.fn.dataTable.ext.buttons.excelHtml5.action.call(dt.button(this), e, dt, node, config);
                    enableButtonAndRemoveWait($(node));
                }, 300);
            }
        });
    }
    return {
        buttons: [
            { extend: 'selectAll', text: '<i class="fa fa fa-long-arrow-up"></i> Selecionar', className: 'btn btn-info btn-sm' },
            { extend: 'selectNone', text: '<i class="fa fa fa-long-arrow-down"></i> Deselecionar', className: 'btn btn-info btn-sm datatable-button-space' },
            defaultButtons, _bt
        ]
    }
}

//Custom Robson Ursino
function createDefaultButtonRemover() {
    return {
        text: 'Remover <i class="fa fa-eraser"><i>',
        className: 'btn btn-sm btn-danger datatable-button-remove',
        extend: 'selectNone',
        action: function (e, dt, node, config) {
            openDelete(dt);
        }
    };
}
function saveByteArray(reportName, byte) {
    var blob = new Blob([byte], { type: "application/vnd.ms-excel" });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    var fileName = reportName;
    link.download = fileName;
    link.click();
};

function base64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
        var ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
    }
    return bytes;
}


function removeDiacritics(str) {
    var diacriticsMap = {
        A: /[\u0041\u24B6\uFF21\u00C0\u00C1\u00C2\u1EA6\u1EA4\u1EAA\u1EA8\u00C3\u0100\u0102\u1EB0\u1EAE\u1EB4\u1EB2\u0226\u01E0\u00C4\u01DE\u1EA2\u00C5\u01FA\u01CD\u0200\u0202\u1EA0\u1EAC\u1EB6\u1E00\u0104\u023A\u2C6F]/g,
        AA: /[\uA732]/g,
        AE: /[\u00C6\u01FC\u01E2]/g,
        AO: /[\uA734]/g,
        AU: /[\uA736]/g,
        AV: /[\uA738\uA73A]/g,
        AY: /[\uA73C]/g,
        B: /[\u0042\u24B7\uFF22\u1E02\u1E04\u1E06\u0243\u0182\u0181]/g,
        C: /[\u0043\u24B8\uFF23\u0106\u0108\u010A\u010C\u00C7\u1E08\u0187\u023B\uA73E]/g,
        D: /[\u0044\u24B9\uFF24\u1E0A\u010E\u1E0C\u1E10\u1E12\u1E0E\u0110\u018B\u018A\u0189\uA779]/g,
        DZ: /[\u01F1\u01C4]/g,
        Dz: /[\u01F2\u01C5]/g,
        E: /[\u0045\u24BA\uFF25\u00C8\u00C9\u00CA\u1EC0\u1EBE\u1EC4\u1EC2\u1EBC\u0112\u1E14\u1E16\u0114\u0116\u00CB\u1EBA\u011A\u0204\u0206\u1EB8\u1EC6\u0228\u1E1C\u0118\u1E18\u1E1A\u0190\u018E]/g,
        F: /[\u0046\u24BB\uFF26\u1E1E\u0191\uA77B]/g,
        G: /[\u0047\u24BC\uFF27\u01F4\u011C\u1E20\u011E\u0120\u01E6\u0122\u01E4\u0193\uA7A0\uA77D\uA77E]/g,
        H: /[\u0048\u24BD\uFF28\u0124\u1E22\u1E26\u021E\u1E24\u1E28\u1E2A\u0126\u2C67\u2C75\uA78D]/g,
        I: /[\u0049\u24BE\uFF29\u00CC\u00CD\u00CE\u0128\u012A\u012C\u0130\u00CF\u1E2E\u1EC8\u01CF\u0208\u020A\u1ECA\u012E\u1E2C\u0197]/g,
        J: /[\u004A\u24BF\uFF2A\u0134\u0248]/g,
        K: /[\u004B\u24C0\uFF2B\u1E30\u01E8\u1E32\u0136\u1E34\u0198\u2C69\uA740\uA742\uA744\uA7A2]/g,
        L: /[\u004C\u24C1\uFF2C\u013F\u0139\u013D\u1E36\u1E38\u013B\u1E3C\u1E3A\u0141\u023D\u2C62\u2C60\uA748\uA746\uA780]/g,
        LJ: /[\u01C7]/g,
        Lj: /[\u01C8]/g,
        M: /[\u004D\u24C2\uFF2D\u1E3E\u1E40\u1E42\u2C6E\u019C]/g,
        N: /[\u004E\u24C3\uFF2E\u01F8\u0143\u00D1\u1E44\u0147\u1E46\u0145\u1E4A\u1E48\u0220\u019D\uA790\uA7A4]/g,
        NJ: /[\u01CA]/g,
        Nj: /[\u01CB]/g,
        O: /[\u004F\u24C4\uFF2F\u00D2\u00D3\u00D4\u1ED2\u1ED0\u1ED6\u1ED4\u00D5\u1E4C\u022C\u1E4E\u014C\u1E50\u1E52\u014E\u022E\u0230\u00D6\u022A\u1ECE\u0150\u01D1\u020C\u020E\u01A0\u1EDC\u1EDA\u1EE0\u1EDE\u1EE2\u1ECC\u1ED8\u01EA\u01EC\u00D8\u01FE\u0186\u019F\uA74A\uA74C]/g,
        OI: /[\u01A2]/g,
        OO: /[\uA74E]/g,
        OU: /[\u0222]/g,
        P: /[\u0050\u24C5\uFF30\u1E54\u1E56\u01A4\u2C63\uA750\uA752\uA754]/g,
        Q: /[\u0051\u24C6\uFF31\uA756\uA758\u024A]/g,
        R: /[\u0052\u24C7\uFF32\u0154\u1E58\u0158\u0210\u0212\u1E5A\u1E5C\u0156\u1E5E\u024C\u2C64\uA75A\uA7A6\uA782]/g,
        S: /[\u0053\u24C8\uFF33\u1E9E\u015A\u1E64\u015C\u1E60\u0160\u1E66\u1E62\u1E68\u0218\u015E\u2C7E\uA7A8\uA784]/g,
        T: /[\u0054\u24C9\uFF34\u1E6A\u0164\u1E6C\u021A\u0162\u1E70\u1E6E\u0166\u01AC\u01AE\u023E\uA786]/g,
        TZ: /[\uA728]/g,
        U: /[\u0055\u24CA\uFF35\u00D9\u00DA\u00DB\u0168\u1E78\u016A\u1E7A\u016C\u00DC\u01DB\u01D7\u01D5\u01D9\u1EE6\u016E\u0170\u01D3\u0214\u0216\u01AF\u1EEA\u1EE8\u1EEE\u1EEC\u1EF0\u1EE4\u1E72\u0172\u1E76\u1E74\u0244]/g,
        V: /[\u0056\u24CB\uFF36\u1E7C\u1E7E\u01B2\uA75E\u0245]/g,
        VY: /[\uA760]/g,
        W: /[\u0057\u24CC\uFF37\u1E80\u1E82\u0174\u1E86\u1E84\u1E88\u2C72]/g,
        X: /[\u0058\u24CD\uFF38\u1E8A\u1E8C]/g,
        Y: /[\u0059\u24CE\uFF39\u1EF2\u00DD\u0176\u1EF8\u0232\u1E8E\u0178\u1EF6\u1EF4\u01B3\u024E\u1EFE]/g,
        Z: /[\u005A\u24CF\uFF3A\u0179\u1E90\u017B\u017D\u1E92\u1E94\u01B5\u0224\u2C7F\u2C6B\uA762]/g,
        a: /[\u0061\u24D0\uFF41\u1E9A\u00E0\u00E1\u00E2\u1EA7\u1EA5\u1EAB\u1EA9\u00E3\u0101\u0103\u1EB1\u1EAF\u1EB5\u1EB3\u0227\u01E1\u00E4\u01DF\u1EA3\u00E5\u01FB\u01CE\u0201\u0203\u1EA1\u1EAD\u1EB7\u1E01\u0105\u2C65\u0250]/g,
        aa: /[\uA733]/g,
        ae: /[\u00E6\u01FD\u01E3]/g,
        ao: /[\uA735]/g,
        au: /[\uA737]/g,
        av: /[\uA739\uA73B]/g,
        ay: /[\uA73D]/g,
        b: /[\u0062\u24D1\uFF42\u1E03\u1E05\u1E07\u0180\u0183\u0253]/g,
        c: /[\u0063\u24D2\uFF43\u0107\u0109\u010B\u010D\u00E7\u1E09\u0188\u023C\uA73F\u2184]/g,
        d: /[\u0064\u24D3\uFF44\u1E0B\u010F\u1E0D\u1E11\u1E13\u1E0F\u0111\u018C\u0256\u0257\uA77A]/g,
        dz: /[\u01F3\u01C6]/g,
        e: /[\u0065\u24D4\uFF45\u00E8\u00E9\u00EA\u1EC1\u1EBF\u1EC5\u1EC3\u1EBD\u0113\u1E15\u1E17\u0115\u0117\u00EB\u1EBB\u011B\u0205\u0207\u1EB9\u1EC7\u0229\u1E1D\u0119\u1E19\u1E1B\u0247\u025B\u01DD]/g,
        f: /[\u0066\u24D5\uFF46\u1E1F\u0192\uA77C]/g,
        g: /[\u0067\u24D6\uFF47\u01F5\u011D\u1E21\u011F\u0121\u01E7\u0123\u01E5\u0260\uA7A1\u1D79\uA77F]/g,
        h: /[\u0068\u24D7\uFF48\u0125\u1E23\u1E27\u021F\u1E25\u1E29\u1E2B\u1E96\u0127\u2C68\u2C76\u0265]/g,
        hv: /[\u0195]/g,
        i: /[\u0069\u24D8\uFF49\u00EC\u00ED\u00EE\u0129\u012B\u012D\u00EF\u1E2F\u1EC9\u01D0\u0209\u020B\u1ECB\u012F\u1E2D\u0268\u0131]/g,
        j: /[\u006A\u24D9\uFF4A\u0135\u01F0\u0249]/g,
        k: /[\u006B\u24DA\uFF4B\u1E31\u01E9\u1E33\u0137\u1E35\u0199\u2C6A\uA741\uA743\uA745\uA7A3]/g,
        l: /[\u006C\u24DB\uFF4C\u0140\u013A\u013E\u1E37\u1E39\u013C\u1E3D\u1E3B\u017F\u0142\u019A\u026B\u2C61\uA749\uA781\uA747]/g,
        lj: /[\u01C9]/g,
        m: /[\u006D\u24DC\uFF4D\u1E3F\u1E41\u1E43\u0271\u026F]/g,
        n: /[\u006E\u24DD\uFF4E\u01F9\u0144\u00F1\u1E45\u0148\u1E47\u0146\u1E4B\u1E49\u019E\u0272\u0149\uA791\uA7A5]/g,
        nj: /[\u01CC]/g,
        o: /[\u006F\u24DE\uFF4F\u00F2\u00F3\u00F4\u1ED3\u1ED1\u1ED7\u1ED5\u00F5\u1E4D\u022D\u1E4F\u014D\u1E51\u1E53\u014F\u022F\u0231\u00F6\u022B\u1ECF\u0151\u01D2\u020D\u020F\u01A1\u1EDD\u1EDB\u1EE1\u1EDF\u1EE3\u1ECD\u1ED9\u01EB\u01ED\u00F8\u01FF\u0254\uA74B\uA74D\u0275]/g,
        oi: /[\u01A3]/g,
        ou: /[\u0223]/g,
        oo: /[\uA74F]/g,
        p: /[\u0070\u24DF\uFF50\u1E55\u1E57\u01A5\u1D7D\uA751\uA753\uA755]/g,
        q: /[\u0071\u24E0\uFF51\u024B\uA757\uA759]/g,
        r: /[\u0072\u24E1\uFF52\u0155\u1E59\u0159\u0211\u0213\u1E5B\u1E5D\u0157\u1E5F\u024D\u027D\uA75B\uA7A7\uA783]/g,
        s: /[\u0073\u24E2\uFF53\u015B\u1E65\u015D\u1E61\u0161\u1E67\u1E63\u1E69\u0219\u015F\u023F\uA7A9\uA785\u1E9B]/g,
        ss: /[\u00DF]/g,
        t: /[\u0074\u24E3\uFF54\u1E6B\u1E97\u0165\u1E6D\u021B\u0163\u1E71\u1E6F\u0167\u01AD\u0288\u2C66\uA787]/g,
        tz: /[\uA729]/g,
        u: /[\u0075\u24E4\uFF55\u00F9\u00FA\u00FB\u0169\u1E79\u016B\u1E7B\u016D\u00FC\u01DC\u01D8\u01D6\u01DA\u1EE7\u016F\u0171\u01D4\u0215\u0217\u01B0\u1EEB\u1EE9\u1EEF\u1EED\u1EF1\u1EE5\u1E73\u0173\u1E77\u1E75\u0289]/g,
        v: /[\u0076\u24E5\uFF56\u1E7D\u1E7F\u028B\uA75F\u028C]/g,
        vy: /[\uA761]/g,
        w: /[\u0077\u24E6\uFF57\u1E81\u1E83\u0175\u1E87\u1E85\u1E98\u1E89\u2C73]/g,
        x: /[\u0078\u24E7\uFF58\u1E8B\u1E8D]/g,
        y: /[\u0079\u24E8\uFF59\u1EF3\u00FD\u0177\u1EF9\u0233\u1E8F\u00FF\u1EF7\u1E99\u1EF5\u01B4\u024F\u1EFF]/g,
        z: /[\u007A\u24E9\uFF5A\u017A\u1E91\u017C\u017E\u1E93\u1E95\u01B6\u0225\u0240\u2C6C\uA763]/g
    };
    for (var x in diacriticsMap) {     
        str = str.replace(diacriticsMap[x], x);
    }
    return str;
}


/*---------------------------- ENABLE AND DISABLE ELEMENTS ----------------------------*/

function isElementDisable(el) {
    if ($(el).attr('disabled') == "disabled") { return true; }
    if ($(el).attr('disabled') == true) { return true; }
    if ($(el).hasClass('disabled') == true) { return true; }
    if (typeof el === 'undefined' || !el) { return false; }
    if (typeof $(el).attr('disabled') === 'undefined' || !$(el).attr('disabled')) { return false; }
    return false;
}

function disableElement(el, state) {
    if (state == true) {
        $(el).attr('disabled', 'disabled');
    } else {
        $(el).attr('disabled', false);
    }
}

function enableButtonAndRemoveWait(el) {
    enableButton(el);
    if ($(el).find("i").data() === undefined) { return; }
    var _class = $(el).find("i").data().class;
    //var _event = $(el).find("i").data()._event;
    //$(el).on("click", _event);
    $(el).find("i").removeClass().removeClass("fa-spinner fa-spin").addClass(_class);
}
function enableButton(el) {
    $(el).removeAttr("disabled");
    $(el).removeClass("disabled");
}

function isButtonEnabled(el) {
    if ($(el).attr("disabled").toLowerCase() == "disabled") { return false; }
    if ($(el).attr("disabled") == true) { return false; }
    if ($(el).hasClass("disabled") == true) { return false; }
    if (typeof el === 'undefined' || !el) { return true; };
    if (typeof $(el).attr("disabled") === 'undefined' || !$(el).attr("disabled")) { return true; };
    return true;
}
function disableButtonAndWait(el) {
    //var _event = $(el)[0].click;
    //$(el).unbind("click");
    disableButton(el);
    //$(el).find("i").data({ "class": $(el).find("i").attr("class"), "_event": _event });
    $(el).find("i").data({ "class": $(el).find("i").attr("class") });
    $(el).find("i").removeClass().addClass("fa fa-spinner fa-spin");
}
function disableButton(el) {
    $(el).attr("disabled", "true");
    $(el).addClass("disabled");
}

/*---------------------------- ENABLE AND DISABLE ELEMENTS ----------------------------*/

/*---------------------------- IMPRESSÃO RELATORIOS ----------------------------*/

function printEtiquetaRDLC() {
    var protocolo = $("#<%=txtTesteEtiqueta.ClientID%>").val();
    var fid = 'WebService/AjaxWS.asmx';
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: fid + '/printEtiqueta',
        data: "{'protocolo': '" + protocolo + "'}",
        dataType: "json",
        success: function (data) {
            if (data.d.Sucess) {
                if (data.d.Data.split('|')[0] != undefined) {
                    $("#printframe").remove();

                    // create new printframe
                    var iFrame = $('<iframe></iframe>');
                    iFrame
                        .attr("id", "printframe")
                        .attr("name", "printframe")
                        .attr("src", "about:blank")
                        .css("width", "0")
                        .css("height", "0")
                        .css("position", "absolute")
                        .css("display", "none")
                        .appendTo($("body:first"));

                    // load printframe
                    var url = data.d.Data.split('|')[0].replace("\\", "\\\\");
                    iFrame.load(function () {
                        // nasty hack to be able to print the frame
                        var tempFrame = $('#printframe')[0];
                        var tempFrameWindow = tempFrame.contentWindow ? tempFrame.contentWindow : tempFrame.contentDocument.defaultView;
                        tempFrameWindow.focus();
                        tempFrameWindow.print();
                    });
                    iFrame.attr('src', url);
                }
            } else {
                openModal(data.d.Message, true);
            }
        },
        error: function (data) {
            openModal("Erro no envio dos dados.", true);
        }
    });
}

/*---------------------------- IMPRESSÃO RELATORIOS ----------------------------*/


/* PAGINACAO */
function setPagingCount(table) {
    //Load User Settings
    loadSettings("datatables.paging.current", function (param) {
        if (param != "") {
            table.page.len(param).draw();
        }
    });
}

//TRACK USER INFO

$(document).bind("ajaxStart", function (e, xhr, options) {
    //toggleWait(true);
});
$(document).bind("ajaxStop", function () {
    toggleWait(false);
});

$(document).bind("ajaxSend", function (event, jqxhr, settings) {
    if (settings.url.toLocaleLowerCase().indexOf("ajaxservice.asmx/isconnected") == -1) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Services/AjaxService.asmx/isConnected",
            dataType: "json",
            success: function (data) {
                if (data.d == false) {
                    $("input[type='hidden']").val("");
                    if (!$(".modal").is(":visible")) {
                        openModalMsg("<img src='../images/not_found2.png' class='pull-left'><br/>Voc&ecirc; foi desconectado e est&aacute; sendo redirecionado para a tela de login", true, function () {
                            window.location.assign("../Default.aspx");
                        });
                    }
                }
            }
        });

        if (settings.url.toLocaleLowerCase().indexOf("ajaxservice.asmx/changeusersettings") == -1 &&
            settings.url.toLocaleLowerCase().indexOf("ajaxservice.asmx/loadusersettings") == -1 &&
            settings.url.toLocaleLowerCase().indexOf("customwait") == -1 &&
            settings.url.toLocaleLowerCase().indexOf("getsuggestion") == -1) {
            toggleWait(true);
        }
    }
});

//Initialize global functions
$(document).ready(function () {

    //button-box
    $(document).on("click", ".SMA-atachbutton", function (evt) {
        changeStartupPage($(this));
    });

    initListenNotification();
    viewNotification();

    $(document).on("click", ".SMA-addshortcutbutton, .SMA-addtaskbutton", function (evt) {
        var type = ($(this).hasClass("SMA-addshortcutbutton") ? "startup" : "task");
        addUserShortcut($(this), type);

    });

    $(document).on("click", ".SMA-addstartuppage", function (evt) {
        var _page = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
        var settings = { "name": "settings.page.startup", "show": false, "value": _page };
        changeSettings(settings);
    });


    //Hook datatable creation event
    $(document).on('length.dt', function (e, settings, len) {
        var settings = { "name": "datatables.paging.current", "show": false, "value": len };
        changeSettings(settings);
    });
    $(document).on('init.dt', function (e, settings) {
        onCreateDatatable(settings);
    });

    //Refazer todos os tooltip
    if ($.fn.tooltip) {
        $('[data-toggle="tooltip"]').tooltip();
        $("i").tooltip();
    }

    var options = {
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    };

    if ($.fn.datepicker) {
        $("input[type='date']").datepicker(options);
    }

    if ($.fn.SMASelect) {
        $(".multi-search-input").SMASelect();
    }

    if ($.fn.datepicker) {
        $(".form-control-date").datepicker({
            language: "pt-BR"
        });
    }
    if ($.fn.datetimepicker) {
        $(".form-control-time").datetimepicker({
            format: 'LT'
        });
    }
    if ($.fn.inputmask) {
        $(".form-control-time").inputmask();
    }
    /*
    if ($.fn.multiselect) {
        $(".form-control-multi").multiselect({
            includeSelectAllOption: true,
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            numberDisplayed: 1,
            maxHeight: 250
        });
    }
    */

    listenAutoComplete();

    $(document).on("click", "i.fa-copy", function (evt) {
        copyToClipboard($(this).parent());
        evt.stopPropagation();
    });
    $(document).on("click", "td:has(i.fa-copy)", function (evt) {
        copyToClipboard($(this));
        evt.stopPropagation();
    });


    $(document).on("click", '.dropdown-menu a.dropdown-toggle', function (e) {
        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }
        var $subMenu = $(this).next(".dropdown-menu");
        $subMenu.toggleClass('show');

        var menuItem = $(this);
        var _level = $(menuItem).attr("data-level");
        if (_level !== undefined) {
            makeMenuTree(_level, function (_ditem) {
                if (_ditem != "") {
                    $subMenu.html(_ditem);
                } else {
                    $(menuItem).prop("disabled", "disabled");
                }
                $(menuItem).removeAttr("data-level"); //disabilitar uma nova pesquisar de sub-tree
            });
        }

        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });
        return false;
    });


    //fazer o menu dinamico
    makeMenuTree("level-0", function (_d) {
        $(".navbar-menu-base").html(_d);
        $(document).on("click", "a.wb-menu-item[data-level!='']", function (evt) {
            var menuItem = $(this);
            var _level = $(menuItem).attr("data-level");
            if (_level !== undefined) {
                makeMenuTree(_level, function (_ditem) {
                    $("ul[aria-labelledby='" + $(menuItem).attr("id") + "']").html(_ditem);
                    $(menuItem).removeAttr("data-level"); //disabilitar uma nova pesquisar de sub-tree
                });
            }
        });
    });

    $(document).on("click", "a[data-widget='pushmenu'], #sidebar-overlay", function (evt) {
        var collapsed = $("body").hasClass("sidebar-collapse");
        var settings = { "name": "settings.menu.expand", "show": false, "value": (collapsed == false) };
        changeSettings(settings);
    });
    //Collapse-Expand Menu
});

$(window).on("load", function () {
    // Animate loader off screen
    $(".se-pre-con").fadeOut("slow");
})
