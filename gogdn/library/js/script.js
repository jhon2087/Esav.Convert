( function( $ ) {
$( document ).ready(function() {
$('#cssmenu > ul > li > a').click(function() {
  $('#cssmenu li').removeClass('active');
  $(this).closest('li').addClass('active');	
  var checkElement = $(this).next();
  if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
    $(this).closest('li').removeClass('active');
    checkElement.slideUp('normal');
  }
  if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
    $('#cssmenu ul ul:visible').slideUp('normal');
    checkElement.slideDown('normal');
  }
  if($(this).closest('li').find('ul').children().length == 0) {
    return true;
  } else {
    return false;	
  }		
});
});
})(jQuery);
$(document).ready(function () {
    var browser = detectBrowser();
    switch (browser) {
        case "IE":
            if (jQuery.browser.version == "6.0") {
                $.each($(".spin_control"), function (i, v) {
                    var theElement = $(v);
                    var xWidth = Number(theElement.css("width").replace("px", ""));
                    xWidth = xWidth + 18;
                    theElement.parent().css("width", xWidth + "px");
                });
            }
            break;
    }
});

function maxLength(control, controlConteo, maxlimit) {
    if (control.value.length > maxlimit)
        control.value = control.value.substring(0, maxlimit);
}

function ValidarCorreo(correo) {
    // var re = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
    //  result = re.exec(correo);
    //  return result != null;
    //var objRegExp = /(^[(a-z|0-9)]([(a-z|0_9)_\-.]*)@([[a-z|0-9]_\-.]*)([.][(a-z|0-9)]*)$)|(^[(a-z|0-9)]([(a-z|0-9)_\-.]*)@([(a-z|0-9)_\-.]*)(\.[(a-z|0-9)]*)(\.[(a-z|0-9)]*)*$)/i;
    //var objRegExp = /(^[(a-z|0-9)]([(a-z|0_9)_\-.]*)@([[a-z|0-9]_\-.]*)([.][(a-z|0-9)]{3})$)|(^[(a-z|0-9)]([(a-z|0-9)_\-.]*)@([(a-z|0-9)_\-.]*)(\.[(a-z|0-9)]{3})(\.[(a-z|0-9)]{2})*$)/i;
    var objRegExp = /^((([A-Za-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([A-Za-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([A-Za-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([A-Za-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/;
    return objRegExp.test(correo);
}

var tabflag = false;
function ClosePopUp2(op) {
    parent.popup.SetContentUrl('');
    necesitaRefrescar = false;
    if (op == 1) {
        necesitaRefrescar = true;
        parent.popup.Hide();
        var sURL = unescape(parent.window.location.pathname);
        parent.window.location.href = sURL;
    }
    else
        parent.popup.Hide();
}
function ClosePopUp(op) {
    parent.popup.SetContentUrl('');
    necesitaRefrescar = false;
    if (op == 1) {
        parent.popup.Hide();
        parent.grid.Refresh();
    }
    else
        parent.popup.Hide();
}
function OpenModal(page, Titulo, popup, width, height) {
    try { parent.ShowPanel(); } catch (e) { }

    popup.SetHeaderText(unescape(Titulo));
    popup.SetContentUrl(page);
    popup.Show();
    popup.SetSize(width, height);
}
function OpenVentana(page, Titulo, width, height) {

    window.open(page, 'mywindow', 'width=' + width + ',height=' + height + ',location=0,status=1,scrollbars=1');
}
function OpenModal2(page, Titulo, popup, width, height) {
    popup.SetHeaderText(unescape(Titulo));
    popup.SetContentUrl(page);
    popup.Show();
    popup.SetSize(width, height);
}

function OpenModal3(popup, width, height) {
    popup.Show();
    popup.SetSize(width, height);
}
function contar(sender, e, len) {
    var descrip = sender.GetText();
    n = descrip.length;
    if (n > len) {
        if (sender.GetText() != null)
            sender.SetText(descrip.substring(0, len));
    }
    else {
        if (Result.GetText() != null)
            Result.SetText(len - n);
    }
}
function cortarCaracteres(sender, e, len, ctrl) {
    //alert(sender.value);
    mensaje = sender.value;
    if (mensaje.length > len) {
        sender.value(mensaje.substring(0, len));
    }
    contarCaracteres(sender, e, len, ctrl);
}
function contarCaracteres(sender, e, len, ctrl) {
    ctrl.value = sender.value.length;
}
function cortarEditor(sender, e, len, ctrl) {
    mensaje = sender.GetHtml();
    if (mensaje.length > len) {
        sender.SetHtml(mensaje.substring(0, len));
    }
    contarEditor(sender, e, len, ctrl);
}
function contarEditor(sender, e, len, ctrl) {
    ctrl.SetText(sender.GetHtml().length);
}
function trim(cadena) {
    for (i = 0; i < cadena.length; ) {
        if (cadena.charAt(i) == " ")
            cadena = cadena.substring(i + 1, cadena.length);
        else
            break;
    }
    for (i = cadena.length - 1; i >= 0; i = cadena.length - 1) {
        if (cadena.charAt(i) == " ")
            cadena = cadena.substring(0, i);
        else
            break;
    }
    return cadena;
}

function onlyNumbersOrLetters(e) {
    var key = ASCIIKeyPressValue(e);
    if ((key > 47 && key < 58) || (key > 64 && key < 91) || (key > 96 && key < 123)) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function NumbersAndLettersAndEspecialCar(e) {
    var key = ASCIIKeyPressValue(e);
    if (key > 47 && key < 58 || key > 31 && key < 47 || key > 64 && key < 91 || key > 96 && key < 123 || key == 64 || key == 95) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function NumbersAndLettersAndEspecialCarShort(e) {
    var key = ASCIIKeyPressValue(e);
    if (key > 47 && key < 58 || key > 64 && key < 91 || key > 96 && key < 123 || key == 64 || key == 95 || key == 46 || key == 45) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function onlyNumbersOrLettersTelefono(e) {
    var key = ASCIIKeyPressValue(e);
    if ((key > 47 && key < 58) || key == 40 || key == 41) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function onlyNumbersAndSlash(e) {
    var key = ASCIIKeyPressValue(e);
    if (key > 46 && key < 58) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function onlyLetters(e) {
    var key = ASCIIKeyPressValue(e);
    if (key > 64 && key < 91 || key > 96 && key < 123) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function onlyLettersAndSpace(e) {
    var key = ASCIIKeyPressValue(e);
    if ((key > 64 && key < 91 || key > 96 && key < 123) || key == 32 || key == 241) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}

function onlyNumbersOrLettersAndSpace(e) {
    var key = ASCIIKeyPressValue(e);
    if (((key > 47 && key < 58) || (key > 64 && key < 91) || (key > 96 && key < 123)) || key == 32) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}
function onlyNumbers(e) {
    var key = ASCIIKeyPressValue(e);
    if (key >= 48 && key <= 57) {
        return true;
    }
    return IsMovementOrEnterKey(key);
}

function onlyDecimal(e) {
    var key = ASCIIKeyPressValue(e);
    if ((key >= 48 && key <= 57) || key == 46) {
        return true;
    }

    return IsMovementOrEnterKey(key);
}

function SoloNumerosDecimales(e) {
    var key = ASCIIKeyPressValue(e);
    if ((key >= 48 && key <= 57) || key == 46) {
        e.returnValue = true;
    } else {
        e.returnValue = false;
    }

    var key = ASCIIKeyPressValue(e);
    var oElement = e.srcElement ? e.srcElement : e.target;
    var valor = new String(oElement.value);
    if (key == 46) {
        if (valor.search(/\./g) > -1) {
            e.returnValue = false;
        }
    }
}

function ASCIIKeyPressValue(e) {
    var key;
    if (e.charCode != null) {
        key = e.charCode;
    } else if (e.keyCode != null) {
        key = e.keyCode;
    } else if (e.which != null) {
        key = e.which;
    }
    return key;
}
function IsMovementOrEnterKey(key) {
    if (key <= 13) {
        return true;
    }
    return false;
}
function TabIndexOpera(e, control) {
    if (control == null) return false;
    var key = ASCIIKeyPressValue(e);
    if (key == 9 && navigator.appName == "Opera") {
        control.Focus();
        return true
    }
    return false;
}
function Collapse(s, e) {
    s.PerformCallback(e.visibleIndex);
}
function Expand(s, e) {
    s.PerformCallback(e.visibleIndex);
}
function detectBrowser() {
    var mReturnValue = null
    var mAgent = new String(navigator.userAgent.toLowerCase())
    if (mAgent.indexOf("msie") !== -1) {
        mReturnValue = "IE"
    } else if (mAgent.indexOf("firefox") !== -1) {
        mReturnValue = "FF"
    } else if (mAgent.indexOf("chrome") !== -1) {
        mReturnValue = "CHR"
    } else if (mAgent.indexOf("safari") !== -1) {
        mReturnValue = "SF"
    } else if (mAgent.indexOf("opera") !== -1) {
        mReturnValue = "OP"
    }
    return mReturnValue
}
function CaracterSpecial(letter) {
    var cards = new Array();
    cards[0] = { value: "ñ", convert: "1" };
    cards[1] = { value: "Ñ", convert: "2" };
    cards[2] = { value: "á", convert: "3" };
    cards[3] = { value: "é", convert: "4" };
    cards[4] = { value: "í", convert: "5" };
    cards[5] = { value: "ó", convert: "6" };
    cards[6] = { value: "ú", convert: "7" };
    cards[7] = { value: "Á", convert: "8" };
    cards[8] = { value: "É", convert: "9" };
    cards[9] = { value: "Í", convert: "<" };
    cards[10] = { value: "Ó", convert: ">" };
    cards[11] = { value: "Ú", convert: "=" };

    for (var i = 0; i < cards.length; i++) {
        var letra = letter.replace(cards[i].value, cards[i].convert);
    }
    return letra;
}
function validarEnteroSpin(valor) {
    valor = parseInt(valor);
    if (isNaN(valor))
        return "";
    else
        return valor;
}
function validarRangoSpin(e, hashTable) {
    var oElement = e.srcElement ? e.srcElement : e.target;
    if (hashTable.containsKey(oElement.id)) {
        var arrayData = hashTable.get(oElement.id).split(";")
        enteroValidado = validarEnteroSpin(oElement.value);
        var _minValue = arrayData[1];
        var _maxValue = arrayData[2];
        if (enteroValidado == "") {
            oElement.value = _minValue;
        } else {
            if (enteroValidado < _minValue)
                oElement.value = _minValue;
            else if (enteroValidado > _maxValue)
                oElement.value = _maxValue;
            else
                oElement.value = enteroValidado;
        }
    }
}
function incrementsWithKeys(e, hashTable) {
    var key = e.keyCode ? e.keyCode : e.charCode;
    var oElement = e.srcElement ? e.srcElement : e.target;
    var data;
    if (hashTable.containsKey(oElement.id)) {
        var arrayData = hashTable.get(oElement.id).split(";")
        enteroValidado = parseInt(oElement.value);
        var _increment = parseInt(arrayData[0]);
        var _minValue = parseInt(arrayData[1]);
        var _maxValue = parseInt(arrayData[2]);
        if (key == 38 || key == 33) {
            var _val = enteroValidado + _increment;
            if (_val > _maxValue)
                _val = _maxValue;
            oElement.value = _val;
            return;
        }

        if (key == 40 || key == 34) {
            var _val = enteroValidado - _increment;
            if (_val < _minValue)
                _val = _minValue;
            oElement.value = _val;
            return;
        }
    }
}
function validarEntero(valor) {
    var numberfilter = /^\d+?$/;
    if (!numberfilter.test(valor))
        return "";
    else
        return valor;
}
function compruebaValidoEntero(oElement) {
    enteroValidado = validarEntero(oElement.value)
    oElement.value = enteroValidado
}
function panelMessage(msg) {
    //$("#miclase").removeClass("blockUI2 blockMsg blockPage");
    //$("#miclase").addClass("blockUI blockMsg blockPage");

    $.blockUI(
        {
            message: '<div class="panelImgAllMexicoPass" style="text-align:center"></div> <div class="panelMsg" ><div class="panelImgLoading" style="text-align:center"></div>' + msg + '</div>'
        });
}

function panelMessagePopup(msg, ancho, alto) {
    //$("#miclase").removeClass("blockUI2 blockMsg blockPage");
    //$("#miclase").addClass("blockUI blockMsg blockPage");

    $.blockUI(
        {
            message: '<div class="panelImgPopup" style="text-align:center"></div> <div class="panelMsgPopup" ><div class="panelImgLoadingPopup" style="text-align:center"></div>' + msg + '</div>',
            css: {
                top: ($(window).height() - 180) / 2 + 'px',
                left: ($(window).width() - 250) / 2 + 'px',
                width: 290 + 'px',
                height: 152 + 'px'
            }
        });
}

function panelMessagePopupAllMexicoPass(msg, ancho, alto) {
    //$("#miclase").removeClass("blockUI2 blockMsg blockPage");
    //$("#miclase").addClass("blockUI blockMsg blockPage");

    $.blockUI(
        {
            message: '<div class="panelImgPopupAllMexicoPass" style="text-align:center"></div> <div class="panelMsgPopup" ><div class="panelImgLoadingPopup" style="text-align:center"></div>' + msg + '</div>',
            css: {
                top: ($(window).height() - 180) / 2 + 'px',
                left: ($(window).width() - 250) / 2 + 'px',
                width: 290 + 'px',
                height: 162 + 'px'
            }
        });
}

function panelMessageAllMexicoPass_(msg, url) {
    var nn = url.split('::');
    var url_ = "";
    var image = "";
    var imagetop = "";
    var navegador = navigator.appName;

    url_ = nn[0] + nn[1] + nn[2];
    image = nn[0] + "/gogdn/Themes/Default/Imagenes/loading-thickbox.gif";
    imagetop = nn[0] + "/gogdn/Themes/Default/Imagenes/LogoAllMexicoPass.png";

    //alert(url_);
    $("#miclase").removeClass("blockUI blockMsg blockPage");
    $("#miclase").attr('class', '');
    $.blockUI.defaults.css = {};

    callback_panel();
    //alert(image);
    if (nn[0] != "" && url_ != "" && image != "" && imagetop != "") {
        var mostrar = "";
        mostrar += '<div class="panelImgAllMexicoPass" style="text-align:left;">';
        mostrar += '</div>';
        mostrar += '<div class="panelMsg">';
        mostrar += '<div style="text-align:center;width:316px;height:224px;">';
        mostrar += '<img src="' + url_.toString() + '" width="316px" height="224px" />';
        mostrar += '</div><div><br /></div>';

        mostrar += '<div style="height:19px;width:208px;margin-bottom:15px;">'
        mostrar += '<img src="' + image.toString() + '" style="margin-left:55px;" />';

        mostrar += '</div><div style="text-align:center;">' + msg + '</div></div>';

        //setTimeout($.blockUI({ message: mostrar }), 3000);
        $.blockUI({ message: mostrar });
    } else {
        alert("Problemas al cargar la imagen");
    }

    callback_panel();
}

function panelMessage_(msg, url) {
    var nn = url.split('::');
    var url_ = "";
    var image = "";
    var imagetop = "";
    var navegador = navigator.appName;

    url_ = nn[0] + nn[1] + nn[2];
    image = nn[0] + "/gogdn/Themes/Default/Imagenes/loading-thickbox.gif";
    imagetop = nn[0] + "/gogdn/Themes/Default/Imagenes/LogoGoGDNW.gif";

    //alert(url_);
    $("#miclase").removeClass("blockUI blockMsg blockPage");
    $("#miclase").attr('class', '');
    $.blockUI.defaults.css = {};

    callback_panel();
    //alert(image);
    if (nn[0] != "" && url_ != "" && image != "" && imagetop != "") {
        var mostrar = "";
        mostrar += '<div class="panelImg" style="text-align:left;">';
        mostrar += '</div>';
        mostrar += '<div class="panelMsg">';
        mostrar += '<div style="text-align:center;width:316px;height:224px;">';
        mostrar += '<img src="' + url_.toString() + '" width="316px" height="224px" />';
        mostrar += '</div><div><br /></div>';
        mostrar += '<div style="height:19px;width:208px;margin-bottom:15px;">'
        mostrar += '<img src="' + image.toString() + '" style="margin-left:55px;" />';

        mostrar += '</div><div style="text-align:center;">' + msg + '</div></div>';
        $.blockUI({ message: mostrar });
        //setTimeout($.blockUI({ message: mostrar }), 3000);

    } else {
        alert("Problemas al cargar la imagen");
    }

    callback_panel();
}

function callback_panel() {
    $("#miclase").addClass("blockUI2 blockMsg blockPage");
    $("#miclase").attr('class', 'blockUI2 blockMsg blockPage');
}
function panelHidden() {
    try { $.unblockUI(); } catch (e) { }
}
function timeValidate(e) {
    var srcElement = e.srcElement ? e.srcElement : e.target;
    var value = srcElement.value;
    if (value.indexOf("_") == -1) {
        var hora = value.split(":")[0];
        if (parseInt(hora) > 23) {
            srcElement.value = "";
        }
    }
}
function showTabs() {
    $("ul.tabs li").click(function () {
        if (this.className.indexOf("tab-active") != -1)
            return false;

        $("ul.tabs li").removeClass("tab-active");
        $(this).addClass("tab-active");
        $(".tab_content").hide();

        var activeTab = $(this).find("a").attr("href");
        $(activeTab).fadeIn();
        return false;
    });
}
function getPositionTop(ctrlname) {
    var el = document.getElementById(ctrlname);
    var pos = 0;
    while (el) {
        pos += el.offsetTop;
        el = el.offsetParent;
    }
    return pos
}
function getPositionLeft(ctrlname) {
    var el = document.getElementById(ctrlname);
    var pos = 0;
    while (el) {
        pos += el.offsetLeft;
        el = el.offsetParent;
    }
    return pos;
}
function SetBodyDialog(controlName, body) {
    document.getElementById(controlName + "-container").innerHTML = body;
}
function CloseDialog() {
    $('.ui-dialog-titlebar-close').trigger('click');
}
function openGUIDialogIframe(control, url, titulo, ancho, alto, scroll, onCloseDialog) {
    alert(control);
    alert(url);
    var _scroll = 'scrolling="no"';
    if (scroll != null) {
        if (scroll) {
            ancho -= 20;
            _scroll = 'scrolling="auto"';
        }
    }
    $('<iframe id=' + control + '-container" class="dialog-test" frameborder="0" ' + _scroll + ' src="' + url + '" />').dialog({ closeOnEscape: onCloseDialog == null,
        close: function (event, ui) {
            if (onCloseDialog != null) {
                window.setTimeout(onCloseDialog, 100)//window.setTimeout(onCloseDialog + "()", 100)
            }
        }, title: titulo, autoOpen: true, width: ancho, height: alto, modal: true, resizable: false, autoResize: true, overlay: { opacity: 0.5, background: 'black' }
    }).width(ancho - 20).height(alto - 15);

    //window.scrollTo(0, 0);

}

function openGUIDialogIframeWithOutClose(control, url, titulo, ancho, alto, scroll, onCloseDialog) {
    var _scroll = 'scrolling="no"';
    if (scroll != null) {
        if (scroll) {
            ancho -= 20;
            _scroll = 'scrolling="auto"';
        }
    }
    $('<iframe id=' + control + '-container" class="dialog-test" frameborder="0" ' + _scroll + ' src="' + url + '" />').dialog({ closeOnEscape: onCloseDialog == null,
        close: function (event, ui) {
            if (onCloseDialog != null) {
                window.setTimeout(onCloseDialog, 100)//window.setTimeout(onCloseDialog + "()", 100)
            }
        }, title: titulo, autoOpen: true, closeOnEscape: false, open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }, width: ancho, height: alto, modal: true, resizable: false, autoResize: true, overlay: { opacity: 0.5, background: 'black' }
    }).width(ancho - 20).height(alto - 15);

    //window.scrollTo(0, 0);

}
function openGUIDialog(control, titulo, ancho, alto, body, modal) {
    var x = getPositionLeft(control);
    var y = getPositionTop(control) + 15;
    if (alto == 0) {
        $('#' + control + '-container').dialog({ autoOpen: false, show: 'blind', hide: 'blind', position: [x, y], resizable: false, title: titulo, width: 250, minHeight: 0 });
    } else {
        $('#' + control + '-container').dialog({ autoOpen: false, show: 'blind', hide: 'blind', position: [x, y], resizable: false, title: titulo, width: 250, height: alto });
    }
    if (modal) {
        $('#' + control + '-container').dialog({ modal: true });
    }
    $('#' + control + '-container').dialog('open');
}
/*ajax*/
function DoCallBack(functionName, functionArguments, endFunction, context) {
    var vstateID = document.getElementById("__VIEWSTATEID");
    var vstateData;
    if (vstateID != null) {
        vstateID = vstateID.value;
    }
    if (vstateID != null && vstateID.length > 0) {
        vstateData = document.getElementById(vstateID);
    }
    if (vstateData != null) {
        vstateData = vstateData.value;
    }
    if (vstateData == null) {
        vstateData = "";
    }
    __theFormPostData = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=&" + vstateID + "=" + vstateData + "&";

    WebForm_DoCallback('__Page', functionName + '|' + functionArguments, endFunction, context, callback_Error, false)
}
function callback_Error(message, context) {
    alert(message)
}
var mFormData = null

function DoFormCallBackUserControl(objectControl, functionName, functionArguments, endFunction) {
    mFormData = new String()
    InitFormCallback()

    __theFormPostData = "__FORMCALLBACK=1&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=&" + mFormData
    WebForm_DoCallback(objectControl, functionName + "|" + functionArguments, endFunction, null, null, false)
}

function DoFormCallBack(functionName, functionArguments, endFunction) {
    mFormData = new String()
    InitFormCallback()

    __theFormPostData = "__FORMCALLBACK=1&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=&" + mFormData
    WebForm_DoCallback('__Page', functionName + "|" + functionArguments, endFunction, null, null, false)
}
function InitFormCallback() {
    var count = theForm.elements.length;
    var element;
    for (var i = 0; i < count; i++) {
        element = theForm.elements[i];
        var tagName = element.tagName.toLowerCase();
        if (tagName == "input") {
            var type = element.type;

            if ((type == "text" || type == "hidden" || type == "password" || ((type == "checkbox" || type == "radio") && element.checked)) && (element.id != "__EVENTVALIDATION" && element.id != "__VIEWSTATE" && element.id != "__EVENTTARGET" && element.id != "__EVENTARGUMENT")) {
                InitCallbackAddField(element.name, element.value);
            }
        }
        else if (tagName == "select") {
            var selectCount = element.options.length;
            for (var j = 0; j < selectCount; j++) {
                var selectChild = element.options[j];
                if (selectChild.selected == true) {
                    InitCallbackAddField(element.name, element.value);
                }
            }
        }
        else if (tagName == "textarea") {
            InitCallbackAddField(element.name, element.value);
        }
    }
}
function InitCallbackAddField(name, value) {
    mFormData += name + "=" + WebForm_EncodeCallback(value) + "&";
}
/*hastable*/
function Hashtable() {
    this.clear = hashtable_clear;
    this.containsKey = hashtable_containsKey;
    this.containsValue = hashtable_containsValue;
    this.get = hashtable_get;
    this.isEmpty = hashtable_isEmpty;
    this.keys = hashtable_keys;
    this.put = hashtable_put;
    this.remove = hashtable_remove;
    this.size = hashtable_size;
    this.toString = hashtable_toString;
    this.values = hashtable_values;
    this.hashtable = new Array();
}
function hashtable_clear() {
    this.hashtable = new Array();
}
function hashtable_containsKey(key) {
    var exists = false;
    for (var i in this.hashtable) {
        if (i == key && this.hashtable[i] != null) {
            exists = true;
            break;
        }
    }
    return exists;
}
function hashtable_containsValue(value) {
    var contains = false;
    if (value != null) {
        for (var i in this.hashtable) {
            if (this.hashtable[i] == value) {
                contains = true;
                break;
            }
        }
    }
    return contains;
}
function hashtable_get(key) {
    return this.hashtable[key];
}
function hashtable_isEmpty() {
    return (parseInt(this.size()) == 0) ? true : false;
}
function hashtable_keys() {
    var keys = new Array();
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null)
            keys.push(i);
    }
    return keys;
}
function hashtable_put(key, value) {
    if (key == null || value == null) {
        throw "NullPointerException {" + key + "},{" + value + "}";
    } else {
        this.hashtable[key] = value;
    }
}
function hashtable_remove(key) {
    var rtn = this.hashtable[key];
    this.hashtable[key] = null;
    return rtn;
}
function hashtable_size() {
    var size = 0;
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null)
            size++;
    }
    return size;
}
function hashtable_toString() {
    var result = "";
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null)
            result += "{" + i + "},{" + this.hashtable[i] + "}\n";
    }
    return result;
}
function hashtable_values() {
    var values = new Array();
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null)
            values.push(this.hashtable[i]);
    }
    return values;
}
var spinCheckHashTable = new Hashtable();
/*date functions*/
function isDate(p_Expression) {
    return !isNaN(new Date(p_Expression));
}

function dateAdd(p_Interval, p_Number, p_Date) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }
    if (isNaN(p_Number)) { return "invalid number: '" + p_Number + "'"; }

    p_Number = new Number(p_Number);
    var dt = new Date(p_Date);
    switch (p_Interval.toLowerCase()) {
        case "yyyy":
            {
                dt.setFullYear(dt.getFullYear() + p_Number);
                break;
            }
        case "q":
            {
                dt.setMonth(dt.getMonth() + (p_Number * 3));
                break;
            }
        case "m":
            {
                dt.setMonth(dt.getMonth() + p_Number);
                break;
            }
        case "y":
        case "d":
        case "w":
            {
                dt.setDate(dt.getDate() + p_Number);
                break;
            }
        case "ww":
            {
                dt.setDate(dt.getDate() + (p_Number * 7));
                break;
            }
        case "h":
            {
                dt.setHours(dt.getHours() + p_Number);
                break;
            }
        case "n":
            {
                dt.setMinutes(dt.getMinutes() + p_Number);
                break;
            }
        case "s":
            {
                dt.setSeconds(dt.getSeconds() + p_Number);
                break;
            }
        case "ms":
            {
                dt.setMilliseconds(dt.getMilliseconds() + p_Number);
                break;
            }
        default:
            {
                return "invalid interval: '" + p_Interval + "'";
            }
    }
    return dt;
}
function dateDiff(p_Interval, p_Date1, p_Date2, p_firstdayofweek, p_firstweekofyear) {
    if (!isDate(p_Date1)) { return "invalid date: '" + p_Date1 + "'"; }
    if (!isDate(p_Date2)) { return "invalid date: '" + p_Date2 + "'"; }
    var dt1 = new Date(p_Date1);
    var dt2 = new Date(p_Date2);
    var iDiffMS = dt2.valueOf() - dt1.valueOf();
    var dtDiff = new Date(iDiffMS);
    var nYears = dtDiff.getUTCFullYear() - 1970;
    var nMonths = dtDiff.getUTCMonth() + (nYears != 0 ? nYears * 12 : 0);
    var nQuarters = parseInt(nMonths / 3);
    var nWeeks = parseInt(iDiffMS / 1000 / 60 / 60 / 24 / 7);
    var nDays = parseInt(iDiffMS / 1000 / 60 / 60 / 24);
    var nHours = parseInt(iDiffMS / 1000 / 60 / 60);
    var nMinutes = parseInt(iDiffMS / 1000 / 60);
    var nSeconds = parseInt(iDiffMS / 1000);
    var nMilliseconds = iDiffMS;
    var iDiff = 0;
    switch (p_Interval.toLowerCase()) {
        case "yyyy": return nYears;
        case "q": return nQuarters;
        case "m": return nMonths;
        case "y":
        case "d": return nDays;
        case "w": return nDays;
        case "ww": return nWeeks;
        case "h": return nHours;
        case "n": return nMinutes;
        case "s": return nSeconds;
        case "ms": return nMilliseconds;
        default: return "invalid interval: '" + p_Interval + "'";
    }
}

function datePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }

    var dtPart = new Date(p_Date);
    switch (p_Interval.toLowerCase()) {
        case "yyyy": return dtPart.getFullYear();
        case "q": return parseInt(dtPart.getMonth() / 3) + 1;
        case "m": return dtPart.getMonth() + 1;
        case "y": return dateDiff("y", "1/1/" + dtPart.getFullYear(), dtPart); 		// day of year
        case "d": return dtPart.getDate();
        case "dd": return String(dtPart.getDate()).length == 1 ? "0" + dtPart.getDate() : dtPart.getDate();
        case "w": return dtPart.getDay(); // weekday
        case "ww": return dateDiff("ww", "1/1/" + dtPart.getFullYear(), dtPart); 	// week of year
        case "h": return dtPart.getHours();
        case "hh": return String(dtPart.getHours()).length == 1 ? "0" + dtPart.getHours() : dtPart.getHours();
        case "n": return dtPart.getMinutes();
        case "nn": return String(dtPart.getMinutes()).length == 1 ? "0" + dtPart.getMinutes() : dtPart.getMinutes();
        case "s": return dtPart.getSeconds();
        case "ms": return dtPart.getMilliseconds(); // millisecond	// <-- extension for JS, NOT available in VBScript
        default: return "invalid interval: '" + p_Interval + "'";
    }
}
function weekdayName(p_Date, p_abbreviate) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }
    var dt = new Date(p_Date);
    var retVal = dt.toString().split(' ')[0];
    var retVal = Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thrusday', 'Friday', 'Saturday')[dt.getDay()];
    if (p_abbreviate == true) { retVal = retVal.substring(0, 3) } // abbr to 1st 3 chars
    return retVal;
}
function monthName(p_Date, p_abbreviate) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }
    var dt = new Date(p_Date);
    var retVal = Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December')[dt.getMonth()];
    if (p_abbreviate == true) { retVal = retVal.substring(0, 3) } // abbr to 1st 3 chars
    return retVal;
}
function dateName(p_Date, p_Type) {
    var dt = new Date(p_Date);

    switch (p_Type) {
        case "1": return dt.getDate() + " of " + monthName(p_Date, false) + " of " + dt.getFullYear();
        case "2": return monthName(p_Date, true) + " " + DatePart("dd", dt) + ", " + dt.getFullYear() + " - " + DatePart("hh", dt) + ":" + DatePart("nn", dt);
        default: return "invalid type: '" + p_Type + "'";
    }
}
function ConvertEnglishFormat(dateStr, isFormatSpanish) {
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = dateStr.match(datePat); // is the format ok?

    if (isFormatSpanish) {
        day = matchArray[1];
        month = matchArray[3];
    } else {
        month = matchArray[1];
        day = matchArray[3];
    }
    year = matchArray[5];

    return month + "/" + day + "/" + year;
}
function isValidDate(dateStr, esFormatoSpanish) {
    var isFormatSpanish = esFormatoSpanish == 1;
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = dateStr.match(datePat);

    if (matchArray == null) {
        return false;
    }

    if (isFormatSpanish) {
        day = matchArray[1];
        month = matchArray[3];
    } else {
        month = matchArray[1];
        day = matchArray[3];
    }
    year = matchArray[5];
    if (month < 1 || month > 12) {
        return false;
    }

    if (day < 1 || day > 31) {
        return false;
    }

    if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        return false;
    }

    if (month == 2) {
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day == 29 && !isleap)) {
            return false;
        }
    }
    return true;
}
function DateName(p_Date, p_Type) {
    return dateName(p_Date, p_Type);
}
function IsValidDate(p_Expression) {
    return isValidDate(p_Expression);
}

function IsDate(p_Expression) {
    return isDate(p_Expression);
}
function DateAdd(p_Interval, p_Number, p_Date) {
    return dateAdd(p_Interval, p_Number, p_Date);
}
function DateDiff(p_interval, p_date1, p_date2, p_firstdayofweek, p_firstweekofyear) {
    return dateDiff(p_interval, p_date1, p_date2, p_firstdayofweek, p_firstweekofyear);
}
function DatePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear) {
    return datePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear);
}
function WeekdayName(p_Date) {
    return weekdayName(p_Date);
}
function MonthName(p_Date) {
    return monthName(p_Date);
}
/*spin*/
(function ($) {
    var calcFloat = {
        get: function (num) {
            var num = num.toString();
            if (num.indexOf('.') == -1) return [0, eval(num)];
            var nn = num.split('.'); var po = nn[1].length; var st = nn.join(''); var sign = '';
            if (st.charAt(0) == '-') { st = st.substr(1); sign = '-'; }
            for (var i = 0; i < st.length; ++i) if (st.charAt(0) == '0') st = st.substr(1, st.length);
            st = sign + st;
            return [po, eval(st)];
        },
        getInt: function (num, figure) {
            var d = Math.pow(10, figure); var n = this.get(num); var v1 = eval('num * d'); var v2 = eval('n[1] * d');
            if (this.get(v1)[1] == v2) return v1;
            return (n[0] == 0 ? v1 : eval(v2 + '/Math.pow(10, n[0])'));
        },
        sum: function (v1, v2) {
            var n1 = this.get(v1); var n2 = this.get(v2); var figure = (n1[0] > n2[0] ? n1[0] : n2[0]);
            v1 = this.getInt(v1, figure);
            v2 = this.getInt(v2, figure);
            return eval('v1 + v2') / Math.pow(10, figure);
        }
    };
    $.extend({
        spin: { imageBasePath: './library/img/', spinBtnImage: 'spin-button.png', spinUpImage: 'spin-button.png', spinDownImage: 'spin-button.png', interval: 1, max: null, min: null, timeInterval: 500,
            timeBlink: 200, btnClass: null, btnCss: { cursor: 'pointer', padding: 0, margin: 0, verticalAlign: 'top' }, txtCss: { marginRight: 0, paddingRight: 0 }, lock: false, decimal: null, beforeChange: null,
            changed: null, buttonUp: null, buttonDown: null
        }
    });
    $.fn.extend({
        spin: function (o) {
            return this.each(function () {
                o = o || {};
                var opt = {};
                $.each($.spin, function (k, v) { opt[k] = (typeof o[k] != 'undefined' ? o[k] : v); });

                var txt = $(this);
                var spinBtnImage = opt.imageBasePath + opt.spinBtnImage;
                var btnSpin = new Image();
                btnSpin.src = spinBtnImage;
                var spinUpImage = opt.imageBasePath + opt.spinUpImage;
                var btnSpinUp = new Image();
                btnSpinUp.src = spinUpImage;
                var spinDownImage = opt.imageBasePath + opt.spinDownImage;
                var btnSpinDown = new Image();
                btnSpinDown.src = spinDownImage;

                var btn = $(document.createElement('img'));
                btn.attr('src', spinBtnImage);
                if (opt.btnClass) btn.addClass(opt.btnClass);
                if (opt.btnCss) btn.css(opt.btnCss);
                if (opt.txtCss) txt.css(opt.txtCss);

                btn.css({ marginRight: 1 });
                btn.css({ marginTop: 1 })
                btn.css({ marginBottom: 1 });
                $('#' + txt[0].id).after('<div id="divImage' + txt[0].id + '" style="display:none"></div>');
                $('#divImage' + txt[0].id).after(btn);

                if (opt.lock) { txt.focus(function () { txt.blur(); }); }
                $('#' + txt[0].id).bind({ keypress: onlyNumbers });
                function spin(vector) {
                    var val = txt.val();
                    var org_val = val;
                    if (opt.decimal) val = val.replace(opt.decimal, '.');
                    if (!isNaN(val)) {
                        val = calcFloat.sum(val, vector * opt.interval);
                        if (opt.min !== null && val < opt.min) val = opt.min;
                        if (opt.max !== null && val > opt.max) val = opt.max;
                        if (val != txt.val()) {
                            if (opt.decimal) val = val.toString().replace('.', opt.decimal);
                            var ret = ($.isFunction(opt.beforeChange) ? opt.beforeChange.apply(txt, [val, org_val]) : true);
                            if (ret !== false) {
                                txt.val(val);
                                if ($.isFunction(opt.changed)) opt.changed.apply(txt, [val]);
                                txt.change();
                                src = (vector > 0 ? spinUpImage : spinDownImage);
                                btn.attr('src', src);
                                if (opt.timeBlink < opt.timeInterval) setTimeout(function () { btn.attr('src', spinBtnImage); }, opt.timeBlink);
                            }
                        }
                    }
                    if (vector > 0) { if ($.isFunction(opt.buttonUp)) opt.buttonUp.apply(txt, [val]); } else { if ($.isFunction(opt.buttonDown)) opt.buttonDown.apply(txt, [val]); }
                }

                btn.mousedown(function (e) {
                    var pos = e.pageY - btn.offset().top;
                    var vector = (btn.height() / 2 > pos ? 1 : -1);
                    (function () {
                        spin(vector);
                        var tk = setTimeout(arguments.callee, opt.timeInterval);
                        $(document).one('mouseup', function () { clearTimeout(tk); btn.attr('src', spinBtnImage); });
                    })();
                    return false;
                });
            });
        }
    });
})(jQuery);
/*Autocomplete*/
; (function ($) {
    $.fn.extend({
        autocomplete: function (urlOrData, options) {
            var isUrl = typeof urlOrData == "string";
            options = $.extend({}, $.Autocompleter.defaults, {
                url: isUrl ? urlOrData : null,
                data: isUrl ? null : urlOrData,
                delay: isUrl ? $.Autocompleter.defaults.delay : 10,
                max: options && !options.scroll ? 10 : 150
            }, options);
            options.highlight = options.highlight || function (value) { return value; };
            options.formatMatch = options.formatMatch || options.formatItem;
            return this.each(function () { new $.Autocompleter(this, options); });
        },
        result: function (handler) { return this.bind("result", handler); },
        search: function (handler) { return this.trigger("search", [handler]); },
        flushCache: function () { return this.trigger("flushCache"); },
        setOptions: function (options) { return this.trigger("setOptions", [options]); },
        unautocomplete: function () { return this.trigger("unautocomplete"); }
    });

    $.Autocompleter = function (input, options) {
        var KEY = { UP: 38, DOWN: 40, DEL: 46, TAB: 9, RETURN: 13, ESC: 27, COMMA: 188, PAGEUP: 33, PAGEDOWN: 34, BACKSPACE: 8 };
        var $input = $(input).attr("autocomplete", "off").addClass(options.inputClass);
        var timeout;
        var previousValue = "";
        var cache = $.Autocompleter.Cache(options);
        var hasFocus = 0;
        var lastKeyPressCode;
        var config = { mouseDownOnSelect: false };
        var select = $.Autocompleter.Select(options, input, selectCurrent, config);
        var blockSubmit;
        $.browser.opera && $(input.form).bind("submit.autocomplete", function () { if (blockSubmit) { blockSubmit = false; return false; } });
        $input.bind(($.browser.opera ? "keypress" : "keydown") + ".autocomplete", function (event) {
            lastKeyPressCode = event.keyCode;
            switch (event.keyCode) {
                case KEY.UP:
                    event.preventDefault();
                    if (select.visible()) { select.prev(); } else { onChange(0, true); }
                    break;
                case KEY.DOWN:
                    event.preventDefault();
                    if (select.visible()) { select.next(); } else { onChange(0, true); }
                    break;
                case KEY.PAGEUP:
                    event.preventDefault();
                    if (select.visible()) { select.pageUp(); } else { onChange(0, true); }
                    break;
                case KEY.PAGEDOWN:
                    event.preventDefault();
                    if (select.visible()) { select.pageDown(); } else { onChange(0, true); }
                    break;
                case options.multiple && $.trim(options.multipleSeparator) == "," && KEY.COMMA:
                case KEY.TAB:
                case KEY.RETURN:
                    if (selectCurrent()) { event.preventDefault(); blockSubmit = true; return false; }
                    break;
                case KEY.ESC:
                    select.hide();
                    break;
                default:
                    clearTimeout(timeout);
                    timeout = setTimeout(onChange, options.delay);
                    break;
            }
        }).focus(function () { hasFocus++; }).blur(function () {
            hasFocus = 0; if (!config.mouseDownOnSelect) { hideResults(); }
        }).click(function () {
            if (hasFocus++ > 1 && !select.visible()) { onChange(0, true); }
        }).bind("search", function () {
            var fn = (arguments.length > 1) ? arguments[1] : null;
            function findValueCallback(q, data) {
                var result;
                if (data && data.length) { for (var i = 0; i < data.length; i++) { if (data[i].result.toLowerCase() == q.toLowerCase()) { result = data[i]; break; } } }
                if (typeof fn == "function") fn(result);
                else $input.trigger("result", result && [result.data, result.value]);
            }
            $.each(trimWords($input.val()), function (i, value) {
                request(value, findValueCallback, findValueCallback);
            });
        }).bind("flushCache", function () {
            cache.flush();
        }).bind("setOptions", function () {
            $.extend(options, arguments[1]);
            if ("data" in arguments[1])
                cache.populate();
        }).bind("unautocomplete", function () {
            select.unbind();
            $input.unbind();
            $(input.form).unbind(".autocomplete");
        });

        function selectCurrent() {
            var selected = select.selected();
            if (!selected) return false;

            var v = selected.result;
            previousValue = v;

            if (options.multiple) {
                var words = trimWords($input.val());
                if (words.length > 1)
                    v = words.slice(0, words.length - 1).join(options.multipleSeparator) + options.multipleSeparator + v;

                v += options.multipleSeparator;
            }
            if (v != " ")
                $input.val(v);
            $("#__datavaluelookup").val(selected.value)

            hideResultsNow();
            $input.trigger("result", [selected.data, selected.value]);
            return true;
        }

        function onChange(crap, skipPrevCheck) {
            if (lastKeyPressCode == KEY.DEL) { select.hide(); return; }
            var currentValue = $input.val();
            if (!skipPrevCheck && currentValue == previousValue)
                return;

            previousValue = currentValue;
            currentValue = lastWord(currentValue);
            if (currentValue.length >= options.minChars) {
                $input.addClass(options.loadingClass);
                if (!options.matchCase)
                    currentValue = currentValue.toLowerCase();
                request(currentValue, receiveData, hideResultsNow);
            } else {
                stopLoading();
                select.hide();
            }
        };

        function trimWords(value) {
            if (!value) return [""];
            var words = value.split(options.multipleSeparator);
            var result = [];
            $.each(words, function (i, value) { if ($.trim(value)) result[i] = $.trim(value); });
            return result;
        }

        function lastWord(value) {
            if (!options.multiple)
                return value;
            var words = trimWords(value);
            return words[words.length - 1];
        }

        function autoFill(q, sValue) {
            if (options.autoFill && (lastWord($input.val()).toLowerCase() == q.toLowerCase()) && lastKeyPressCode != KEY.BACKSPACE) {
                $input.val($input.val() + sValue.substring(lastWord(previousValue).length));
                $.Autocompleter.Selection(input, previousValue.length, previousValue.length + sValue.length);
            }
        };

        function hideResults() { clearTimeout(timeout); timeout = setTimeout(hideResultsNow, 200); };
        function hideResultsNow() {
            var wasVisible = select.visible();
            select.hide();
            clearTimeout(timeout);
            stopLoading();
            if (options.mustMatch) {
                $input.search(
				function (result) {
				    if (!result) {
				        if (options.multiple) {
				            var words = trimWords($input.val()).slice(0, -1);
				            $input.val(words.join(options.multipleSeparator) + (words.length ? options.multipleSeparator : ""));
				        }
				        else
				            $input.val("");
				    }
				}
			);
            }
            if (wasVisible) $.Autocompleter.Selection(input, input.value.length, input.value.length);
        };

        function receiveData(q, data) {
            if (data && data.length && hasFocus) {
                stopLoading();
                select.display(data, q);
                autoFill(q, data[0].value);
                select.show();
            } else
                hideResultsNow();
        };

        function request(term, success, failure) {
            if (!options.matchCase) term = term.toLowerCase();
            var data = cache.load(term);
            if (data && data.length) {
                success(term, data);
            } else if ((typeof options.url == "string") && (options.url.length > 0)) {
                var extraParams = { timestamp: +new Date() };
                $.each(options.extraParams, function (key, param) { extraParams[key] = typeof param == "function" ? param() : param; });
                var mName = $(input).attr("id") + '_condition'
                var mWhere = $("#" + mName).val();
                $.ajax({
                    mode: "abort",
                    port: "autocomplete" + input.name,
                    dataType: options.dataType,
                    url: options.url + '&w=' + mWhere,
                    data: $.extend({
                        q: lastWord(term),
                        limit: options.max
                    }, extraParams),
                    success: function (data) {
                        var parsed = options.parse && options.parse(data) || parse(data);
                        cache.add(term, parsed);
                        success(term, parsed);
                    }
                });
            } else {
                select.emptyList();
                failure(term);
            }
        };

        function parse(data) {
            var parsed = [];
            var rows = data.split("\n");
            for (var i = 0; i < rows.length; i++) {
                var row = $.trim(rows[i]);
                if (row) {
                    row = row.split("|");
                    parsed[parsed.length] = { data: row, value: row[0], result: options.formatResult && options.formatResult(row, row[0]) || row[0] };
                }
            }
            return parsed;
        };
        function stopLoading() { $input.removeClass(options.loadingClass); };
    };

    $.Autocompleter.defaults = {
        inputClass: "ac_input", resultsClass: "ac_results", loadingClass: "ac_loading", minChars: 1, delay: 400, matchCase: false, matchSubset: true, matchContains: false,
        cacheLength: 10, max: 100, mustMatch: false, extraParams: {}, selectFirst: true, formatItem: function (row) { return row[0]; }, formatMatch: null,
        autoFill: false, width: 0, multiple: false, multipleSeparator: ", ", highlight: function (value, term) {
            return value.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1") + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>");
        }, scroll: true, scrollHeight: 180
    };

    $.Autocompleter.Cache = function (options) {
        var data = {};
        var length = 0;
        function matchSubset(s, sub) {
            if (!options.matchCase) s = s.toLowerCase();
            var i = s.indexOf(sub);
            if (i == -1) return false;
            return i == 0 || options.matchContains;
        };

        function add(q, value) {
            if (length > options.cacheLength) flush();
            if (!data[q]) length++;
            data[q] = value;
        }
        function populate() {
            if (!options.data) return false;
            var stMatchSets = {}, nullData = 0;

            if (!options.url) options.cacheLength = 1;
            stMatchSets[""] = [];
            for (var i = 0, ol = options.data.length; i < ol; i++) {
                var rawValue = options.data[i];
                rawValue = (typeof rawValue == "string") ? [rawValue] : rawValue;
                var value = options.formatMatch(rawValue, i + 1, options.data.length);
                if (value === false) continue;
                var firstChar = value.charAt(0).toLowerCase();
                if (!stMatchSets[firstChar]) stMatchSets[firstChar] = [];
                var row = { value: value, data: rawValue, result: options.formatResult && options.formatResult(rawValue) || value };
                stMatchSets[firstChar].push(row);
                if (nullData++ < options.max) stMatchSets[""].push(row);
            };
            $.each(stMatchSets, function (i, value) { options.cacheLength++; add(i, value); });
        }

        setTimeout(populate, 25);
        function flush() { data = {}; length = 0; }
        return {
            flush: flush,
            add: add,
            populate: populate,
            load: function (q) {
                if (!options.cacheLength || !length)
                    return null;
                if (!options.url && options.matchContains) {
                    var csub = [];
                    for (var k in data) {
                        if (k.length > 0) {
                            var c = data[k];
                            $.each(c, function (i, x) { if (matchSubset(x.value, q)) { csub.push(x); } });
                        }
                    }
                    return csub;
                } else
                    if (data[q]) {
                        return data[q];
                    } else
                        if (options.matchSubset) {
                            for (var i = q.length - 1; i >= options.minChars; i--) {
                                var c = data[q.substr(0, i)];
                                if (c) {
                                    var csub = [];
                                    $.each(c, function (i, x) { if (matchSubset(x.value, q)) { csub[csub.length] = x; } });
                                    return csub;
                                }
                            }
                        }
                return null;
            }
        };
    };

    $.Autocompleter.Select = function (options, input, select, config) {
        var CLASSES = { ACTIVE: "ac_over" };

        var listItems, active = -1, data, term = "", needsInit = true, element, list;
        function init() {
            if (!needsInit)
                return;
            element = $("<div/>").hide().addClass(options.resultsClass).css("position", "absolute").appendTo(document.body);
            list = $("<ul/>").appendTo(element).mouseover(function (event) {
                if (target(event).nodeName && target(event).nodeName.toUpperCase() == 'LI') {
                    active = $("li", list).removeClass(CLASSES.ACTIVE).index(target(event));
                    $(target(event)).addClass(CLASSES.ACTIVE);
                }
            }).click(function (event) {
                $(target(event)).addClass(CLASSES.ACTIVE);
                select();
                input.focus();
                return false;
            }).mousedown(function () {
                config.mouseDownOnSelect = true;
            }).mouseup(function () {
                config.mouseDownOnSelect = false;
            });

            if (options.width > 0) element.css("width", options.width);
            needsInit = false;
        }

        function target(event) {
            var element = event.target;
            while (element && element.tagName != "LI")
                element = element.parentNode;
            if (!element)
                return [];
            return element;
        }

        function moveSelect(step) {
            listItems.slice(active, active + 1).removeClass(CLASSES.ACTIVE);
            movePosition(step);
            var activeItem = listItems.slice(active, active + 1).addClass(CLASSES.ACTIVE);
            if (options.scroll) {
                var offset = 0;
                listItems.slice(0, active).each(function () {
                    offset += this.offsetHeight;
                });
                if ((offset + activeItem[0].offsetHeight - list.scrollTop()) > list[0].clientHeight) {
                    list.scrollTop(offset + activeItem[0].offsetHeight - list.innerHeight());
                } else if (offset < list.scrollTop()) {
                    list.scrollTop(offset);
                }
            }
        };

        function movePosition(step) {
            active += step;
            if (active < 0) {
                active = listItems.size() - 1;
            } else if (active >= listItems.size()) {
                active = 0;
            }
        }

        function limitNumberOfItems(available) { return options.max && options.max < available ? options.max : available; }

        function fillList() {
            list.empty();
            var max = limitNumberOfItems(data.length);
            for (var i = 0; i < max; i++) {
                if (!data[i])
                    continue;
                var formatted = options.formatItem(data[i].data, i + 1, max, data[i].value, term);
                if (formatted === false)
                    continue;
                var li = $("<li/>").html(options.highlight(formatted, term)).addClass(i % 2 == 0 ? "ac_even" : "ac_odd").appendTo(list)[0];
                $.data(li, "ac_data", data[i]);
            }
            listItems = list.find("li");
            if (options.selectFirst) {
                listItems.slice(0, 1).addClass(CLASSES.ACTIVE);
                active = 0;
            }
            if ($.fn.bgiframe) list.bgiframe();
        }

        return {
            display: function (d, q) { init(); data = d; term = q; fillList(); },
            next: function () { moveSelect(1); },
            prev: function () { moveSelect(-1); },
            pageUp: function () { if (active != 0 && active - 8 < 0) { moveSelect(-active); } else { moveSelect(-8); } },
            pageDown: function () { if (active != listItems.size() - 1 && active + 8 > listItems.size()) { moveSelect(listItems.size() - 1 - active); } else { moveSelect(8); } },
            hide: function () { element && element.hide(); listItems && listItems.removeClass(CLASSES.ACTIVE); active = -1; },
            visible: function () { return element && element.is(":visible"); },
            current: function () { return this.visible() && (listItems.filter("." + CLASSES.ACTIVE)[0] || options.selectFirst && listItems[0]); },
            show: function () {
                var offset = $(input).offset(); element.css({ width: typeof options.width == "string" || options.width > 0 ? options.width : $(input).width(), top: offset.top + input.offsetHeight, left: offset.left }).show(); if (options.scroll) {
                    list.scrollTop(0); list.css({ maxHeight: options.scrollHeight, overflow: 'auto' });
                    if ($.browser.msie && typeof document.body.style.maxHeight === "undefined") {
                        var listHeight = 0;
                        listItems.each(function () { listHeight += this.offsetHeight; });
                        var scrollbarsVisible = listHeight > options.scrollHeight; list.css('height', scrollbarsVisible ? options.scrollHeight : listHeight);
                        if (!scrollbarsVisible) listItems.width(list.width() - parseInt(listItems.css("padding-left")) - parseInt(listItems.css("padding-right")));
                    }
                }
            },
            selected: function () { var selected = listItems && listItems.filter("." + CLASSES.ACTIVE).removeClass(CLASSES.ACTIVE); return selected && selected.length && $.data(selected[0], "ac_data"); },
            emptyList: function () { list && list.empty(); },
            unbind: function () { element && element.remove(); }
        };
    };

    $.Autocompleter.Selection = function (field, start, end) {
        if (field.createTextRange) {
            var selRange = field.createTextRange();
            selRange.collapse(true);
            selRange.moveStart("character", start);
            selRange.moveEnd("character", end);
            selRange.select();
        } else if (field.setSelectionRange) {
            field.setSelectionRange(start, end);
        } else {
            if (field.selectionStart) {
                field.selectionStart = start;
                field.selectionEnd = end;
            }
        }
        field.focus();
    };
})(jQuery);
/*Hotel*/
function getVisiblebyid(i) {
    var n = document.getElementById(i);
    if (n.style.display == 'none')
        return false;
    else
        return true;
}
function setVisiblebyid(i, tf) {
    var n = document.getElementById(i);
    if (tf == true)
        n.style.display = '';
    else
        n.style.display = 'none';
}
function calcula_dias_maximos_mes(mes, anyo) {
    if ((anyo % 100) == 0) return 29;
    return dias_Mes[mes];
}
function incrementa_fecha(fecha_inicio, incremento) {
    dia_inicio = fecha_inicio.getDate();
    mes_inicio = fecha_inicio.getMonth();
    ano_inicio = fecha_inicio.getFullYear();
    inc = parseInt(incremento);
    while (incremento > calcula_dias_maximos_mes(mes_inicio, ano_inicio)) {
        incremento -= calcula_dias_maximos_mes(mes_inicio, ano_inicio);
        mes_inicio++;
        if (mes_inicio == 12) { mes_inicio = 0; ano_inicio++; }
    }
    dia_inicio += incremento;
    if (dia_inicio > calcula_dias_maximos_mes(mes_inicio, ano_inicio)) {
        dia_inicio -= calcula_dias_maximos_mes(mes_inicio, ano_inicio);
        mes_inicio++;
        if (mes_inicio == 12) { mes_inicio = 0; ano_inicio++; }
    }
    dia_entrega = new Date(ano_inicio, mes_inicio, dia_inicio);
    return (dia_entrega);
}
function onComplete(arg) {
    //alert (arg);
}
/*menu*/
var arrowimages = { down: ['downarrowclass', 'down.gif', 23], right: ['rightarrowclass', 'right.gif'] }
var jqueryslidemenu = { animateduration: { over: 200, out: 100 },
    buildmenu: function (menuid, arrowsvar) {
        jQuery(document).ready(function ($) {
            var $mainmenutop = $("#" + menuid + ">ul li")
            $mainmenutop.click(function () {
                $(".active").removeClass("active");
                $(this).addClass("active");
            });
            var $mainmenu = $("#" + menuid + ">ul")
            var $headers = $mainmenu.find("ul").parent()
            $headers.each(function (i) {
                var $curobj = $(this)
                var $subul = $(this).find('ul:eq(0)')
                this._dimensions = { w: this.offsetWidth, h: this.offsetHeight, subulw: $subul.outerWidth(), subulh: $subul.outerHeight() }
                this.istopheader = $curobj.parents("ul").length == 1 ? true : false
                $subul.css({ top: this.istopheader ? this._dimensions.h + "px" : 0 })
                $curobj.hover(
				function (e) {
				    var $targetul = $(this).children("ul:eq(0)")
				    this._offsets = { left: $(this).offset().left, top: $(this).offset().top }
				    var menuleft = this.istopheader ? 0 : this._dimensions.w
				    menuleft = (this._offsets.left + menuleft + this._dimensions.subulw > $(window).width()) ? (this.istopheader ? -this._dimensions.subulw + this._dimensions.w : -this._dimensions.w) : menuleft
				    if ($targetul.queue().length <= 1)
				        $targetul.css({ left: menuleft + "px", width: this._dimensions.subulw + 'px' }).slideDown(jqueryslidemenu.animateduration.over)
				},
				function (e) {
				    var $targetul = $(this).children("ul:eq(0)")
				    $targetul.slideUp(jqueryslidemenu.animateduration.out)
				}
			)
                $curobj.click(function () {
                    $(this).children("ul:eq(0)").hide()
                })
            })
            $mainmenu.find("ul").css({ display: 'none', visibility: 'visible' })
        })
    }
}
/*************/
function roundToDecimalPlaces(X, N) {
    if (String(X).indexOf(".") != -1) {
        return X;
    } else {
        var T = Number('1e' + N)
        var mValue = Math.round(X * T) / T
        var mStringValue = new String(mValue)
        var mStartPosition = 0
        var mDecimalPlaces = 0
        mStartPosition = mStringValue.indexOf(".")

        mDecimalPlaces = ((mStringValue.length - mStartPosition) - 1)
        if (mStringValue.indexOf(".") == -1 || mDecimalPlaces !== N) {
            if (mStringValue.indexOf(".") == -1) {
                mStringValue += "."
                mDecimalPlaces = 0
            }

            for (idx = mDecimalPlaces; idx < N; idx++) {
                mStringValue += "0"
            }
        }

        return mStringValue
    }
}
function cleanData(value) {
    var newValue = value.replace(/,/g, "")
    return newValue;
}
function toLocaleString(value) {
    var mStringValue = new String(roundToDecimalPlaces(value, 2))
    var mNewStringValue = new String()
    var mPosLeadingDigit = 0
    var mApplyLeadingDigit = false

    if ((mStringValue.indexOf(".")) == "-1") {
        mStringValue += ".00"
    }

    for (idx = mStringValue.length; idx >= 0; idx--) {

        if (mApplyLeadingDigit) {
            if (mPosLeadingDigit == 3) {
                mNewStringValue = "," + mNewStringValue
                mPosLeadingDigit = 0
            }
            mPosLeadingDigit++
        }

        mNewStringValue = mStringValue.substr(idx, 1) + mNewStringValue

        if (mStringValue.substr(idx, 1) == "." && !mApplyLeadingDigit) {
            mApplyLeadingDigit = true
        }
    }


    return mNewStringValue
}


function mostrar(mensaje) {

    $("#pop").css('display', '');
    $("#pop1").css('display', '');
    $("#msg").html(mensaje);

    var img_w = $("#pop1").width() + 10;
    var img_h = $("#pop1").height() + 200;

    //height="507" width="600"
    //Darle el alto y ancho
    //$("#pop1").css('width', img_w + 'px') ; 
    //$("#pop1").css('height', img_h + 'px');
    //Esconder el popup
    //$("#pop").hide();

    //Consigue valores de la ventana del navegador
    var w = $(this).width(); var h = $(this).height();

    //Centra el popup
    w = (w / 2) - (img_w / 2); h = (h / 2) - (img_h / 2);
    $("#pop1").css("left", w + "px"); $("#pop1").css("top", h + "px");

    //temporizador, para que no aparezca de golpe
    //setTimeout("mostrar()", 1500);
    //$("#pop1").fadeIn('slow');
    $("#pop1").fadeIn('slow');

    //Función para cerrar el popup    
    $("#pop1").click(function () { $(this).fadeOut('slow'); });

}

function showLoader() {
    try { $("#loader").show(); } catch (e) { }
}
function hideLoader() {
    try { $("#loader").hide(); } catch (e) { }
}


function showToolTip(_this, e, text) {
    if (text != "") {
        eleOffset = $(_this).offset();

        $("#ToolTip").html(text);
        ToolTip.style.pixelLeft = (eleOffset.left + $(_this).outerWidth());
        ToolTip.style.pixelTop = (eleOffset.top + 15);
        $("#ToolTip").show();
    }
}
function hideToolTip() {
    $("#ToolTip").hide();
}

function abrirVentana(opc) {
    if (opc == 1)
        window.open("http://www.visanet.com.pe", "tarjeta", "");

    if (opc == 2)
        window.open("http://www.mastercard.com/index.html", "tarjeta", "");

    if (opc == 3)
        window.open("https://www.americanexpress.com/", "tarjeta", "");

    if (opc == 0)
        window.open("http://www.visanet.com.pe/visa.htm", "verified", "menubar=1,resizable=1,width=606,height=402");

    if (opc == -1)
        window.open("http://www.visanet.com.pe/promovbv/bancos.html", "tarjeta", "");
}

