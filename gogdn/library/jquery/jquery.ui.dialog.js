/*core*/
jQuery.ui || function(c) {
  c.ui = { version: "1.8.1", plugin: { add: function(a, b, d) { a = c.ui[a].prototype; for (var e in d) { a.plugins[e] = a.plugins[e] || []; a.plugins[e].push([b, d[e]]) } }, call: function(a, b, d) { if ((b = a.plugins[b]) && a.element[0].parentNode) for (var e = 0; e < b.length; e++) a.options[b[e][0]] && b[e][1].apply(a.element, d) } }, contains: function(a, b) { return document.compareDocumentPosition ? a.compareDocumentPosition(b) & 16 : a !== b && a.contains(b) }, hasScroll: function(a, b) {
    if (c(a).css("overflow") == "hidden") return false;
    b = b && b == "left" ? "scrollLeft" : "scrollTop"; var d = false; if (a[b] > 0) return true; a[b] = 1; d = a[b] > 0; a[b] = 0; return d
  }, isOverAxis: function(a, b, d) { return a > b && a < b + d }, isOver: function(a, b, d, e, f, g) { return c.ui.isOverAxis(a, d, f) && c.ui.isOverAxis(b, e, g) }, keyCode: { ALT: 18, BACKSPACE: 8, CAPS_LOCK: 20, COMMA: 188, CONTROL: 17, DELETE: 46, DOWN: 40, END: 35, ENTER: 13, ESCAPE: 27, HOME: 36, INSERT: 45, LEFT: 37, NUMPAD_ADD: 107, NUMPAD_DECIMAL: 110, NUMPAD_DIVIDE: 111, NUMPAD_ENTER: 108, NUMPAD_MULTIPLY: 106, NUMPAD_SUBTRACT: 109, PAGE_DOWN: 34, PAGE_UP: 33,
    PERIOD: 190, RIGHT: 39, SHIFT: 16, SPACE: 32, TAB: 9, UP: 38
  }
  }; c.fn.extend({ _focus: c.fn.focus, focus: function(a, b) { return typeof a === "number" ? this.each(function() { var d = this; setTimeout(function() { c(d).focus(); b && b.call(d) }, a) }) : this._focus.apply(this, arguments) }, enableSelection: function() { return this.attr("unselectable", "off").css("MozUserSelect", "") }, disableSelection: function() { return this.attr("unselectable", "on").css("MozUserSelect", "none") }, scrollParent: function() {
    var a; a = c.browser.msie && /(static|relative)/.test(this.css("position")) ||
/absolute/.test(this.css("position")) ? this.parents().filter(function() { return /(relative|absolute|fixed)/.test(c.curCSS(this, "position", 1)) && /(auto|scroll)/.test(c.curCSS(this, "overflow", 1) + c.curCSS(this, "overflow-y", 1) + c.curCSS(this, "overflow-x", 1)) }).eq(0) : this.parents().filter(function() { return /(auto|scroll)/.test(c.curCSS(this, "overflow", 1) + c.curCSS(this, "overflow-y", 1) + c.curCSS(this, "overflow-x", 1)) }).eq(0); return /fixed/.test(this.css("position")) || !a.length ? c(document) : a
  }, zIndex: function(a) {
    if (a !==
undefined) return this.css("zIndex", a); if (this.length) { a = c(this[0]); for (var b; a.length && a[0] !== document; ) { b = a.css("position"); if (b == "absolute" || b == "relative" || b == "fixed") { b = parseInt(a.css("zIndex")); if (!isNaN(b) && b != 0) return b } a = a.parent() } } return 0
  }
  }); c.extend(c.expr[":"], { data: function(a, b, d) { return !!c.data(a, d[3]) }, focusable: function(a) {
    var b = a.nodeName.toLowerCase(), d = c.attr(a, "tabindex"); return (/input|select|textarea|button|object/.test(b) ? !a.disabled : "a" == b || "area" == b ? a.href || !isNaN(d) : !isNaN(d)) &&
!c(a)["area" == b ? "parents" : "closest"](":hidden").length
  }, tabbable: function(a) { var b = c.attr(a, "tabindex"); return (isNaN(b) || b >= 0) && c(a).is(":focusable") }
  })
} (jQuery);
/*widget*/
(function(b) {
  var j = b.fn.remove; b.fn.remove = function(a, c) { return this.each(function() { if (!c) if (!a || b.filter(a, [this]).length) b("*", this).add(this).each(function() { b(this).triggerHandler("remove") }); return j.call(b(this), a, c) }) }; b.widget = function(a, c, d) {
    var e = a.split(".")[0], f; a = a.split(".")[1]; f = e + "-" + a; if (!d) { d = c; c = b.Widget } b.expr[":"][f] = function(h) { return !!b.data(h, a) }; b[e] = b[e] || {}; b[e][a] = function(h, g) { arguments.length && this._createWidget(h, g) }; c = new c; c.options = b.extend({}, c.options); b[e][a].prototype =
b.extend(true, c, { namespace: e, widgetName: a, widgetEventPrefix: b[e][a].prototype.widgetEventPrefix || a, widgetBaseClass: f }, d); b.widget.bridge(a, b[e][a])
  }; b.widget.bridge = function(a, c) {
    b.fn[a] = function(d) {
      var e = typeof d === "string", f = Array.prototype.slice.call(arguments, 1), h = this; d = !e && f.length ? b.extend.apply(null, [true, d].concat(f)) : d; if (e && d.substring(0, 1) === "_") return h; e ? this.each(function() { var g = b.data(this, a), i = g && b.isFunction(g[d]) ? g[d].apply(g, f) : g; if (i !== g && i !== undefined) { h = i; return false } }) : this.each(function() {
        var g =
b.data(this, a); if (g) { d && g.option(d); g._init() } else b.data(this, a, new c(d, this))
      }); return h
    }
  }; b.Widget = function(a, c) { arguments.length && this._createWidget(a, c) }; b.Widget.prototype = { widgetName: "widget", widgetEventPrefix: "", options: { disabled: false }, _createWidget: function(a, c) {
    this.element = b(c).data(this.widgetName, this); this.options = b.extend(true, {}, this.options, b.metadata && b.metadata.get(c)[this.widgetName], a); var d = this; this.element.bind("remove." + this.widgetName, function() { d.destroy() }); this._create();
    this._init()
  }, _create: function() { }, _init: function() { }, destroy: function() { this.element.unbind("." + this.widgetName).removeData(this.widgetName); this.widget().unbind("." + this.widgetName).removeAttr("aria-disabled").removeClass(this.widgetBaseClass + "-disabled ui-state-disabled") }, widget: function() { return this.element }, option: function(a, c) {
    var d = a, e = this; if (arguments.length === 0) return b.extend({}, e.options); if (typeof a === "string") { if (c === undefined) return this.options[a]; d = {}; d[a] = c } b.each(d, function(f,
h) { e._setOption(f, h) }); return e
  }, _setOption: function(a, c) { this.options[a] = c; if (a === "disabled") this.widget()[c ? "addClass" : "removeClass"](this.widgetBaseClass + "-disabled ui-state-disabled").attr("aria-disabled", c); return this }, enable: function() { return this._setOption("disabled", false) }, disable: function() { return this._setOption("disabled", true) }, _trigger: function(a, c, d) {
    var e = this.options[a]; c = b.Event(c); c.type = (a === this.widgetEventPrefix ? a : this.widgetEventPrefix + a).toLowerCase(); d = d || {}; if (c.originalEvent) {
      a =
b.event.props.length; for (var f; a; ) { f = b.event.props[--a]; c[f] = c.originalEvent[f] }
    } this.element.trigger(c, d); return !(b.isFunction(e) && e.call(this.element[0], c, d) === false || c.isDefaultPrevented())
  }
  }
})(jQuery);
/*dialog*/
(function(c) {
  c.widget("ui.dialog", { options: { autoOpen: true, buttons: {}, closeOnEscape: true, closeText: "close", dialogClass: "", draggable: true, hide: null, height: "auto", maxHeight: false, maxWidth: false, minHeight: 150, minWidth: 150, modal: false, position: "center", resizable: true, show: null, stack: true, title: "", width: 300, zIndex: 1E3 }, _create: function() {
    this.originalTitle = this.element.attr("title"); var a = this, b = a.options, d = b.title || a.originalTitle || "&#160;", e = c.ui.dialog.getTitleId(a.element), g = (a.uiDialog = c("<div></div>")).appendTo(document.body).hide().addClass("ui-dialog ui-widget ui-widget-content ui-corner-all " +
b.dialogClass).css({ zIndex: b.zIndex }).attr("tabIndex", -1).css("outline", 0).keydown(function(i) { if (b.closeOnEscape && i.keyCode && i.keyCode === c.ui.keyCode.ESCAPE) { a.close(i); i.preventDefault() } }).attr({ role: "dialog", "aria-labelledby": e }).mousedown(function(i) { a.moveToTop(false, i) }); a.element.show().removeAttr("title").addClass("ui-dialog-content ui-widget-content").appendTo(g); var f = (a.uiDialogTitlebar = c("<div></div>")).addClass("ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix").prependTo(g),
h = c('<a href="#"></a>').addClass("ui-dialog-titlebar-close ui-corner-all").attr("role", "button").hover(function() { h.addClass("ui-state-hover") }, function() { h.removeClass("ui-state-hover") }).focus(function() { h.addClass("ui-state-focus") }).blur(function() { h.removeClass("ui-state-focus") }).click(function(i) { a.close(i); return false }).appendTo(f); (a.uiDialogTitlebarCloseText = c("<span></span>")).addClass("ui-icon ui-icon-closethick").text(b.closeText).appendTo(h); c("<span></span>").addClass("ui-dialog-title").attr("id",
e).html(d).prependTo(f); if (c.isFunction(b.beforeclose) && !c.isFunction(b.beforeClose)) b.beforeClose = b.beforeclose; f.find("*").add(f).disableSelection(); b.draggable && c.fn.draggable && a._makeDraggable(); b.resizable && c.fn.resizable && a._makeResizable(); a._createButtons(b.buttons); a._isOpen = false; c.fn.bgiframe && g.bgiframe()
  }, _init: function() { this.options.autoOpen && this.open() }, destroy: function() {
    var a = this; a.overlay && a.overlay.destroy(); a.uiDialog.hide(); a.element.unbind(".dialog").removeData("dialog").removeClass("ui-dialog-content ui-widget-content").hide().appendTo("body");
    a.uiDialog.remove(); a.originalTitle && a.element.attr("title", a.originalTitle); return a
  }, widget: function() { return this.uiDialog }, close: function(a) {
    var b = this, d; if (false !== b._trigger("beforeClose", a)) {
      b.overlay && b.overlay.destroy(); b.uiDialog.unbind("keypress.ui-dialog"); b._isOpen = false; if (b.options.hide) b.uiDialog.hide(b.options.hide, function() { b._trigger("close", a) }); else { b.uiDialog.hide(); b._trigger("close", a) } c.ui.dialog.overlay.resize(); if (b.options.modal) {
        d = 0; c(".ui-dialog").each(function() {
          if (this !==
b.uiDialog[0]) d = Math.max(d, c(this).css("z-index"))
        }); c.ui.dialog.maxZ = d
      } return b
    }
  }, isOpen: function() { return this._isOpen }, moveToTop: function(a, b) {
    var d = this, e = d.options; if (e.modal && !a || !e.stack && !e.modal) return d._trigger("focus", b); if (e.zIndex > c.ui.dialog.maxZ) c.ui.dialog.maxZ = e.zIndex; if (d.overlay) { c.ui.dialog.maxZ += 1; d.overlay.$el.css("z-index", c.ui.dialog.overlay.maxZ = c.ui.dialog.maxZ) } a = { scrollTop: d.element.attr("scrollTop"), scrollLeft: d.element.attr("scrollLeft") }; c.ui.dialog.maxZ += 1; d.uiDialog.css("z-index",
c.ui.dialog.maxZ); d.element.attr(a); d._trigger("focus", b); return d
  }, open: function() {
    if (!this._isOpen) {
      var a = this, b = a.options, d = a.uiDialog; a.overlay = b.modal ? new c.ui.dialog.overlay(a) : null; d.next().length && d.appendTo("body"); a._size(); a._position(b.position); d.show(b.show); a.moveToTop(true); b.modal && d.bind("keypress.ui-dialog", function(e) {
        if (e.keyCode === c.ui.keyCode.TAB) {
          var g = c(":tabbable", this), f = g.filter(":first"); g = g.filter(":last"); if (e.target === g[0] && !e.shiftKey) { f.focus(1); return false } else if (e.target ===
f[0] && e.shiftKey) { g.focus(1); return false }
        }
      }); c([]).add(d.find(".ui-dialog-content :tabbable:first")).add(d.find(".ui-dialog-buttonpane :tabbable:first")).add(d).filter(":first").focus(); a._trigger("open"); a._isOpen = true; return a
    }
  }, _createButtons: function(a) {
    var b = this, d = false, e = c("<div></div>").addClass("ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"); b.uiDialog.find(".ui-dialog-buttonpane").remove(); typeof a === "object" && a !== null && c.each(a, function() { return !(d = true) }); if (d) {
      c.each(a,
function(g, f) { g = c('<button type="button"></button>').text(g).click(function() { f.apply(b.element[0], arguments) }).appendTo(e); c.fn.button && g.button() }); e.appendTo(b.uiDialog)
    }
  }, _makeDraggable: function() {
    function a(f) { return { position: f.position, offset: f.offset} } var b = this, d = b.options, e = c(document), g; b.uiDialog.draggable({ cancel: ".ui-dialog-content, .ui-dialog-titlebar-close", handle: ".ui-dialog-titlebar", containment: "document", start: function(f, h) {
      g = d.height === "auto" ? "auto" : c(this).height(); c(this).height(c(this).height()).addClass("ui-dialog-dragging");
      b._trigger("dragStart", f, a(h))
    }, drag: function(f, h) { b._trigger("drag", f, a(h)) }, stop: function(f, h) { d.position = [h.position.left - e.scrollLeft(), h.position.top - e.scrollTop()]; c(this).removeClass("ui-dialog-dragging").height(g); b._trigger("dragStop", f, a(h)); c.ui.dialog.overlay.resize() }
    })
  }, _makeResizable: function(a) {
    function b(f) { return { originalPosition: f.originalPosition, originalSize: f.originalSize, position: f.position, size: f.size} } a = a === undefined ? this.options.resizable : a; var d = this, e = d.options, g = d.uiDialog.css("position");
    a = typeof a === "string" ? a : "n,e,s,w,se,sw,ne,nw"; d.uiDialog.resizable({ cancel: ".ui-dialog-content", containment: "document", alsoResize: d.element, maxWidth: e.maxWidth, maxHeight: e.maxHeight, minWidth: e.minWidth, minHeight: d._minHeight(), handles: a, start: function(f, h) { c(this).addClass("ui-dialog-resizing"); d._trigger("resizeStart", f, b(h)) }, resize: function(f, h) { d._trigger("resize", f, b(h)) }, stop: function(f, h) {
      c(this).removeClass("ui-dialog-resizing"); e.height = c(this).height(); e.width = c(this).width(); d._trigger("resizeStop",
f, b(h)); c.ui.dialog.overlay.resize()
    }
    }).css("position", g).find(".ui-resizable-se").addClass("ui-icon ui-icon-grip-diagonal-se")
  }, _minHeight: function() { var a = this.options; return a.height === "auto" ? a.minHeight : Math.min(a.minHeight, a.height) }, _position: function(a) {
    var b = [], d = [0, 0]; a = a || c.ui.dialog.prototype.options.position; if (typeof a === "string" || typeof a === "object" && "0" in a) {
      b = a.split ? a.split(" ") : [a[0], a[1]]; if (b.length === 1) b[1] = b[0]; c.each(["left", "top"], function(e, g) {
        if (+b[e] === b[e]) {
          d[e] = b[e]; b[e] =
g
        }
      })
    } else if (typeof a === "object") { if ("left" in a) { b[0] = "left"; d[0] = a.left } else if ("right" in a) { b[0] = "right"; d[0] = -a.right } if ("top" in a) { b[1] = "top"; d[1] = a.top } else if ("bottom" in a) { b[1] = "bottom"; d[1] = -a.bottom } } (a = this.uiDialog.is(":visible")) || this.uiDialog.show(); this.uiDialog.css({ top: 0, left: 0 }).position({ my: b.join(" "), at: b.join(" "), offset: d.join(" "), of: window, collision: "fit", using: function(e) { var g = c(this).css(e).offset().top; g < 0 && c(this).css("top", e.top - g) } }); a || this.uiDialog.hide()
  }, _setOption: function(a,
b) {
    var d = this, e = d.uiDialog, g = e.is(":data(resizable)"), f = false; switch (a) {
      case "beforeclose": a = "beforeClose"; break; case "buttons": d._createButtons(b); break; case "closeText": d.uiDialogTitlebarCloseText.text("" + b); break; case "dialogClass": e.removeClass(d.options.dialogClass).addClass("ui-dialog ui-widget ui-widget-content ui-corner-all " + b); break; case "disabled": b ? e.addClass("ui-dialog-disabled") : e.removeClass("ui-dialog-disabled"); break; case "draggable": b ? d._makeDraggable() : e.draggable("destroy"); break;
      case "height": f = true; break; case "maxHeight": g && e.resizable("option", "maxHeight", b); f = true; break; case "maxWidth": g && e.resizable("option", "maxWidth", b); f = true; break; case "minHeight": g && e.resizable("option", "minHeight", b); f = true; break; case "minWidth": g && e.resizable("option", "minWidth", b); f = true; break; case "position": d._position(b); break; case "resizable": g && !b && e.resizable("destroy"); g && typeof b === "string" && e.resizable("option", "handles", b); !g && b !== false && d._makeResizable(b); break; case "title": c(".ui-dialog-title",
d.uiDialogTitlebar).html("" + (b || "&#160;")); break; case "width": f = true; break
    } c.Widget.prototype._setOption.apply(d, arguments); f && d._size()
  }, _size: function() {
    var a = this.options, b; this.element.css({ width: "auto", minHeight: 0, height: 0 }); b = this.uiDialog.css({ height: "auto", width: a.width }).height(); this.element.css(a.height === "auto" ? { minHeight: Math.max(a.minHeight - b, 0), height: "auto"} : { minHeight: 0, height: Math.max(a.height - b, 0) }).show(); this.uiDialog.is(":data(resizable)") && this.uiDialog.resizable("option", "minHeight",
this._minHeight())
  }
  }); c.extend(c.ui.dialog, { version: "1.8.1", uuid: 0, maxZ: 0, getTitleId: function(a) { a = a.attr("id"); if (!a) { this.uuid += 1; a = this.uuid } return "ui-dialog-title-" + a }, overlay: function(a) { this.$el = c.ui.dialog.overlay.create(a) } }); c.extend(c.ui.dialog.overlay, { instances: [], oldInstances: [], maxZ: 0, events: c.map("focus,mousedown,mouseup,keydown,keypress,click".split(","), function(a) { return a + ".dialog-overlay" }).join(" "), create: function(a) {
    if (this.instances.length === 0) {
      setTimeout(function() {
        c.ui.dialog.overlay.instances.length &&
c(document).bind(c.ui.dialog.overlay.events, function(d) { return c(d.target).zIndex() >= c.ui.dialog.overlay.maxZ })
      }, 1); c(document).bind("keydown.dialog-overlay", function(d) { if (a.options.closeOnEscape && d.keyCode && d.keyCode === c.ui.keyCode.ESCAPE) { a.close(d); d.preventDefault() } }); c(window).bind("resize.dialog-overlay", c.ui.dialog.overlay.resize)
    } var b = (this.oldInstances.pop() || c("<div></div>").addClass("ui-widget-overlay")).appendTo(document.body).css({ width: this.width(), height: this.height() }); c.fn.bgiframe &&
b.bgiframe(); this.instances.push(b); return b
  }, destroy: function(a) { this.oldInstances.push(this.instances.splice(c.inArray(a, this.instances), 1)[0]); this.instances.length === 0 && c([document, window]).unbind(".dialog-overlay"); a.remove(); var b = 0; c.each(this.instances, function() { b = Math.max(b, this.css("z-index")) }); this.maxZ = b }, height: function() {
    var a, b; if (c.browser.msie && c.browser.version < 7) {
      a = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight); b = Math.max(document.documentElement.offsetHeight,
document.body.offsetHeight); return a < b ? c(window).height() + "px" : a + "px"
    } else return c(document).height() + "px"
  }, width: function() { var a, b; if (c.browser.msie && c.browser.version < 7) { a = Math.max(document.documentElement.scrollWidth, document.body.scrollWidth); b = Math.max(document.documentElement.offsetWidth, document.body.offsetWidth); return a < b ? c(window).width() + "px" : a + "px" } else return c(document).width() + "px" }, resize: function() {
    var a = c([]); c.each(c.ui.dialog.overlay.instances, function() { a = a.add(this) }); a.css({ width: 0,
      height: 0
    }).css({ width: c.ui.dialog.overlay.width(), height: c.ui.dialog.overlay.height() })
  }
  }); c.extend(c.ui.dialog.overlay.prototype, { destroy: function() { c.ui.dialog.overlay.destroy(this.$el) } })
})(jQuery);
/*position*/
(function(c) {
  c.ui = c.ui || {}; var m = /left|center|right/, n = /top|center|bottom/, p = c.fn.position, q = c.fn.offset; c.fn.position = function(a) {
    if (!a || !a.of) return p.apply(this, arguments); a = c.extend({}, a); var b = c(a.of), d = (a.collision || "flip").split(" "), e = a.offset ? a.offset.split(" ") : [0, 0], g, h, i; if (a.of.nodeType === 9) { g = b.width(); h = b.height(); i = { top: 0, left: 0} } else if (a.of.scrollTo && a.of.document) { g = b.width(); h = b.height(); i = { top: b.scrollTop(), left: b.scrollLeft()} } else if (a.of.preventDefault) {
      a.at = "left top"; g = h =
0; i = { top: a.of.pageY, left: a.of.pageX }
    } else { g = b.outerWidth(); h = b.outerHeight(); i = b.offset() } c.each(["my", "at"], function() { var f = (a[this] || "").split(" "); if (f.length === 1) f = m.test(f[0]) ? f.concat(["center"]) : n.test(f[0]) ? ["center"].concat(f) : ["center", "center"]; f[0] = m.test(f[0]) ? f[0] : "center"; f[1] = n.test(f[1]) ? f[1] : "center"; a[this] = f }); if (d.length === 1) d[1] = d[0]; e[0] = parseInt(e[0], 10) || 0; if (e.length === 1) e[1] = e[0]; e[1] = parseInt(e[1], 10) || 0; if (a.at[0] === "right") i.left += g; else if (a.at[0] === "center") i.left +=
g / 2; if (a.at[1] === "bottom") i.top += h; else if (a.at[1] === "center") i.top += h / 2; i.left += e[0]; i.top += e[1]; return this.each(function() {
  var f = c(this), k = f.outerWidth(), l = f.outerHeight(), j = c.extend({}, i); if (a.my[0] === "right") j.left -= k; else if (a.my[0] === "center") j.left -= k / 2; if (a.my[1] === "bottom") j.top -= l; else if (a.my[1] === "center") j.top -= l / 2; j.left = parseInt(j.left); j.top = parseInt(j.top); c.each(["left", "top"], function(o, r) {
    c.ui.position[d[o]] && c.ui.position[d[o]][r](j, { targetWidth: g, targetHeight: h, elemWidth: k,
      elemHeight: l, offset: e, my: a.my, at: a.at
    })
  }); c.fn.bgiframe && f.bgiframe(); f.offset(c.extend(j, { using: a.using }))
})
  }; c.ui.position = { fit: { left: function(a, b) { var d = c(window); b = a.left + b.elemWidth - d.width() - d.scrollLeft(); a.left = b > 0 ? a.left - b : Math.max(0, a.left) }, top: function(a, b) { var d = c(window); b = a.top + b.elemHeight - d.height() - d.scrollTop(); a.top = b > 0 ? a.top - b : Math.max(0, a.top) } }, flip: { left: function(a, b) {
    if (b.at[0] !== "center") {
      var d = c(window); d = a.left + b.elemWidth - d.width() - d.scrollLeft(); var e = b.my[0] === "left" ?
-b.elemWidth : b.my[0] === "right" ? b.elemWidth : 0, g = -2 * b.offset[0]; a.left += a.left < 0 ? e + b.targetWidth + g : d > 0 ? e - b.targetWidth + g : 0
    }
  }, top: function(a, b) { if (b.at[1] !== "center") { var d = c(window); d = a.top + b.elemHeight - d.height() - d.scrollTop(); var e = b.my[1] === "top" ? -b.elemHeight : b.my[1] === "bottom" ? b.elemHeight : 0, g = b.at[1] === "top" ? b.targetHeight : -b.targetHeight, h = -2 * b.offset[1]; a.top += a.top < 0 ? e + b.targetHeight + h : d > 0 ? e + g + h : 0 } }
  }
  }; if (!c.offset.setOffset) {
    c.offset.setOffset = function(a, b) {
      if (/static/.test(c.curCSS(a, "position"))) a.style.position =
"relative"; var d = c(a), e = d.offset(), g = parseInt(c.curCSS(a, "top", true), 10) || 0, h = parseInt(c.curCSS(a, "left", true), 10) || 0; e = { top: b.top - e.top + g, left: b.left - e.left + h }; "using" in b ? b.using.call(a, e) : d.css(e)
    }; c.fn.offset = function(a) { var b = this[0]; if (!b || !b.ownerDocument) return null; if (a) return this.each(function() { c.offset.setOffset(this, a) }); return q.call(this) }
  }
})(jQuery);
/*Mouse*/
(function(c) {
  c.widget("ui.mouse", { options: { cancel: ":input,option", distance: 1, delay: 0 }, _mouseInit: function() { var a = this; this.element.bind("mousedown." + this.widgetName, function(b) { return a._mouseDown(b) }).bind("click." + this.widgetName, function(b) { if (a._preventClickEvent) { a._preventClickEvent = false; b.stopImmediatePropagation(); return false } }); this.started = false }, _mouseDestroy: function() { this.element.unbind("." + this.widgetName) }, _mouseDown: function(a) {
    a.originalEvent = a.originalEvent || {}; if (!a.originalEvent.mouseHandled) {
      this._mouseStarted &&
this._mouseUp(a); this._mouseDownEvent = a; var b = this, e = a.which == 1, f = typeof this.options.cancel == "string" ? c(a.target).parents().add(a.target).filter(this.options.cancel).length : false; if (!e || f || !this._mouseCapture(a)) return true; this.mouseDelayMet = !this.options.delay; if (!this.mouseDelayMet) this._mouseDelayTimer = setTimeout(function() { b.mouseDelayMet = true }, this.options.delay); if (this._mouseDistanceMet(a) && this._mouseDelayMet(a)) {
        this._mouseStarted = this._mouseStart(a) !== false; if (!this._mouseStarted) {
          a.preventDefault();
          return true
        }
      } this._mouseMoveDelegate = function(d) { return b._mouseMove(d) }; this._mouseUpDelegate = function(d) { return b._mouseUp(d) }; c(document).bind("mousemove." + this.widgetName, this._mouseMoveDelegate).bind("mouseup." + this.widgetName, this._mouseUpDelegate); c.browser.safari || a.preventDefault(); return a.originalEvent.mouseHandled = true
    }
  }, _mouseMove: function(a) {
    if (c.browser.msie && !a.button) return this._mouseUp(a); if (this._mouseStarted) { this._mouseDrag(a); return a.preventDefault() } if (this._mouseDistanceMet(a) &&
this._mouseDelayMet(a)) (this._mouseStarted = this._mouseStart(this._mouseDownEvent, a) !== false) ? this._mouseDrag(a) : this._mouseUp(a); return !this._mouseStarted
  }, _mouseUp: function(a) { c(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate); if (this._mouseStarted) { this._mouseStarted = false; this._preventClickEvent = a.target == this._mouseDownEvent.target; this._mouseStop(a) } return false }, _mouseDistanceMet: function(a) {
    return Math.max(Math.abs(this._mouseDownEvent.pageX -
a.pageX), Math.abs(this._mouseDownEvent.pageY - a.pageY)) >= this.options.distance
  }, _mouseDelayMet: function() { return this.mouseDelayMet }, _mouseStart: function() { }, _mouseDrag: function() { }, _mouseStop: function() { }, _mouseCapture: function() { return true }
  })
})(jQuery);
;
/*Slider*/
(function(d) {
  d.widget("ui.slider", d.ui.mouse, { widgetEventPrefix: "slide", options: { animate: false, distance: 0, max: 100, min: 0, orientation: "horizontal", range: false, step: 1, value: 0, values: null }, _create: function() {
    var a = this, b = this.options; this._mouseSliding = this._keySliding = false; this._animateOff = true; this._handleIndex = null; this._detectOrientation(); this._mouseInit(); this.element.addClass("ui-slider ui-slider-" + this.orientation + " ui-widget ui-widget-content ui-corner-all"); b.disabled && this.element.addClass("ui-slider-disabled ui-disabled");
    this.range = d([]); if (b.range) { if (b.range === true) { this.range = d("<div></div>"); if (!b.values) b.values = [this._valueMin(), this._valueMin()]; if (b.values.length && b.values.length !== 2) b.values = [b.values[0], b.values[0]] } else this.range = d("<div></div>"); this.range.appendTo(this.element).addClass("ui-slider-range"); if (b.range === "min" || b.range === "max") this.range.addClass("ui-slider-range-" + b.range); this.range.addClass("ui-widget-header") } d(".ui-slider-handle", this.element).length === 0 && d("<a href='#'></a>").appendTo(this.element).addClass("ui-slider-handle");
    if (b.values && b.values.length) for (; d(".ui-slider-handle", this.element).length < b.values.length; ) d("<a href='#'></a>").appendTo(this.element).addClass("ui-slider-handle"); this.handles = d(".ui-slider-handle", this.element).addClass("ui-state-default ui-corner-all"); this.handle = this.handles.eq(0); this.handles.add(this.range).filter("a").click(function(c) { c.preventDefault() }).hover(function() { b.disabled || d(this).addClass("ui-state-hover") }, function() { d(this).removeClass("ui-state-hover") }).focus(function() {
      if (b.disabled) d(this).blur();
      else { d(".ui-slider .ui-state-focus").removeClass("ui-state-focus"); d(this).addClass("ui-state-focus") }
    }).blur(function() { d(this).removeClass("ui-state-focus") }); this.handles.each(function(c) { d(this).data("index.ui-slider-handle", c) }); this.handles.keydown(function(c) {
      var e = true, f = d(this).data("index.ui-slider-handle"), h, g, i; if (!a.options.disabled) {
        switch (c.keyCode) {
          case d.ui.keyCode.HOME: case d.ui.keyCode.END: case d.ui.keyCode.PAGE_UP: case d.ui.keyCode.PAGE_DOWN: case d.ui.keyCode.UP: case d.ui.keyCode.RIGHT: case d.ui.keyCode.DOWN: case d.ui.keyCode.LEFT: e =
false; if (!a._keySliding) { a._keySliding = true; d(this).addClass("ui-state-active"); h = a._start(c, f); if (h === false) return } break
        } i = a.options.step; h = a.options.values && a.options.values.length ? (g = a.values(f)) : (g = a.value()); switch (c.keyCode) {
          case d.ui.keyCode.HOME: g = a._valueMin(); break; case d.ui.keyCode.END: g = a._valueMax(); break; case d.ui.keyCode.PAGE_UP: g = a._trimAlignValue(h + (a._valueMax() - a._valueMin()) / 5); break; case d.ui.keyCode.PAGE_DOWN: g = a._trimAlignValue(h - (a._valueMax() - a._valueMin()) / 5); break; case d.ui.keyCode.UP: case d.ui.keyCode.RIGHT: if (h ===
a._valueMax()) return; g = a._trimAlignValue(h + i); break; case d.ui.keyCode.DOWN: case d.ui.keyCode.LEFT: if (h === a._valueMin()) return; g = a._trimAlignValue(h - i); break
        } a._slide(c, f, g); return e
      }
    }).keyup(function(c) { var e = d(this).data("index.ui-slider-handle"); if (a._keySliding) { a._keySliding = false; a._stop(c, e); a._change(c, e); d(this).removeClass("ui-state-active") } }); this._refreshValue(); this._animateOff = false
  }, destroy: function() {
    this.handles.remove(); this.range.remove(); this.element.removeClass("ui-slider ui-slider-horizontal ui-slider-vertical ui-slider-disabled ui-widget ui-widget-content ui-corner-all").removeData("slider").unbind(".slider");
    this._mouseDestroy(); return this
  }, _mouseCapture: function(a) {
    var b = this.options, c, e, f, h, g; if (b.disabled) return false; this.elementSize = { width: this.element.outerWidth(), height: this.element.outerHeight() }; this.elementOffset = this.element.offset(); c = this._normValueFromMouse({ x: a.pageX, y: a.pageY }); e = this._valueMax() - this._valueMin() + 1; h = this; this.handles.each(function(i) { var j = Math.abs(c - h.values(i)); if (e > j) { e = j; f = d(this); g = i } }); if (b.range === true && this.values(1) === b.min) { g += 1; f = d(this.handles[g]) } if (this._start(a,
g) === false) return false; this._mouseSliding = true; h._handleIndex = g; f.addClass("ui-state-active").focus(); b = f.offset(); this._clickOffset = !d(a.target).parents().andSelf().is(".ui-slider-handle") ? { left: 0, top: 0} : { left: a.pageX - b.left - f.width() / 2, top: a.pageY - b.top - f.height() / 2 - (parseInt(f.css("borderTopWidth"), 10) || 0) - (parseInt(f.css("borderBottomWidth"), 10) || 0) + (parseInt(f.css("marginTop"), 10) || 0) }; this._slide(a, g, c); return this._animateOff = true
  }, _mouseStart: function() { return true }, _mouseDrag: function(a) {
    var b =
this._normValueFromMouse({ x: a.pageX, y: a.pageY }); this._slide(a, this._handleIndex, b); return false
  }, _mouseStop: function(a) { this.handles.removeClass("ui-state-active"); this._mouseSliding = false; this._stop(a, this._handleIndex); this._change(a, this._handleIndex); this._clickOffset = this._handleIndex = null; return this._animateOff = false }, _detectOrientation: function() { this.orientation = this.options.orientation === "vertical" ? "vertical" : "horizontal" }, _normValueFromMouse: function(a) {
    var b; if (this.orientation === "horizontal") {
      b =
this.elementSize.width; a = a.x - this.elementOffset.left - (this._clickOffset ? this._clickOffset.left : 0)
    } else { b = this.elementSize.height; a = a.y - this.elementOffset.top - (this._clickOffset ? this._clickOffset.top : 0) } b = a / b; if (b > 1) b = 1; if (b < 0) b = 0; if (this.orientation === "vertical") b = 1 - b; a = this._valueMax() - this._valueMin(); return this._trimAlignValue(this._valueMin() + b * a)
  }, _start: function(a, b) {
    var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) {
      c.value = this.values(b);
      c.values = this.values()
    } return this._trigger("start", a, c)
  }, _slide: function(a, b, c) {
    var e; if (this.options.values && this.options.values.length) { e = this.values(b ? 0 : 1); if (this.options.values.length === 2 && this.options.range === true && (b === 0 && c > e || b === 1 && c < e)) c = e; if (c !== this.values(b)) { e = this.values(); e[b] = c; a = this._trigger("slide", a, { handle: this.handles[b], value: c, values: e }); this.values(b ? 0 : 1); a !== false && this.values(b, c, true) } } else if (c !== this.value()) {
      a = this._trigger("slide", a, { handle: this.handles[b], value: c });
      a !== false && this.value(c)
    }
  }, _stop: function(a, b) { var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) { c.value = this.values(b); c.values = this.values() } this._trigger("stop", a, c) }, _change: function(a, b) { if (!this._keySliding && !this._mouseSliding) { var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) { c.value = this.values(b); c.values = this.values() } this._trigger("change", a, c) } }, value: function(a) {
    if (arguments.length) {
      this.options.value =
this._trimAlignValue(a); this._refreshValue(); this._change(null, 0)
    } return this._value()
  }, values: function(a, b) {
    var c, e, f; if (arguments.length > 1) { this.options.values[a] = this._trimAlignValue(b); this._refreshValue(); this._change(null, a) } if (arguments.length) if (d.isArray(arguments[0])) { c = this.options.values; e = arguments[0]; for (f = 0; f < c.length; f += 1) { c[f] = this._trimAlignValue(e[f]); this._change(null, f) } this._refreshValue() } else return this.options.values && this.options.values.length ? this._values(a) : this.value();
    else return this._values()
  }, _setOption: function(a, b) {
    var c, e = 0; if (d.isArray(this.options.values)) e = this.options.values.length; d.Widget.prototype._setOption.apply(this, arguments); switch (a) {
      case "disabled": if (b) { this.handles.filter(".ui-state-focus").blur(); this.handles.removeClass("ui-state-hover"); this.handles.attr("disabled", "disabled"); this.element.addClass("ui-disabled") } else { this.handles.removeAttr("disabled"); this.element.removeClass("ui-disabled") } break; case "orientation": this._detectOrientation();
        this.element.removeClass("ui-slider-horizontal ui-slider-vertical").addClass("ui-slider-" + this.orientation); this._refreshValue(); break; case "value": this._animateOff = true; this._refreshValue(); this._change(null, 0); this._animateOff = false; break; case "values": this._animateOff = true; this._refreshValue(); for (c = 0; c < e; c += 1) this._change(null, c); this._animateOff = false; break
    }
  }, _value: function() { var a = this.options.value; return a = this._trimAlignValue(a) }, _values: function(a) {
    var b, c; if (arguments.length) {
      b = this.options.values[a];
      return b = this._trimAlignValue(b)
    } else { b = this.options.values.slice(); for (c = 0; c < b.length; c += 1) b[c] = this._trimAlignValue(b[c]); return b }
  }, _trimAlignValue: function(a) { if (a < this._valueMin()) return this._valueMin(); if (a > this._valueMax()) return this._valueMax(); var b = this.options.step > 0 ? this.options.step : 1, c = a % b; a = a - c; if (Math.abs(c) * 2 >= b) a += c > 0 ? b : -b; return parseFloat(a.toFixed(5)) }, _valueMin: function() { return this.options.min }, _valueMax: function() { return this.options.max }, _refreshValue: function() {
    var a =
this.options.range, b = this.options, c = this, e = !this._animateOff ? b.animate : false, f, h = {}, g, i, j, l; if (this.options.values && this.options.values.length) this.handles.each(function(k) {
  f = (c.values(k) - c._valueMin()) / (c._valueMax() - c._valueMin()) * 100; h[c.orientation === "horizontal" ? "left" : "bottom"] = f + "%"; d(this).stop(1, 1)[e ? "animate" : "css"](h, b.animate); if (c.options.range === true) if (c.orientation === "horizontal") {
    if (k === 0) c.range.stop(1, 1)[e ? "animate" : "css"]({ left: f + "%" }, b.animate); if (k === 1) c.range[e ? "animate" : "css"]({ width: f -
g + "%"
    }, { queue: false, duration: b.animate })
  } else { if (k === 0) c.range.stop(1, 1)[e ? "animate" : "css"]({ bottom: f + "%" }, b.animate); if (k === 1) c.range[e ? "animate" : "css"]({ height: f - g + "%" }, { queue: false, duration: b.animate }) } g = f
}); else {
      i = this.value(); j = this._valueMin(); l = this._valueMax(); f = l !== j ? (i - j) / (l - j) * 100 : 0; h[c.orientation === "horizontal" ? "left" : "bottom"] = f + "%"; this.handle.stop(1, 1)[e ? "animate" : "css"](h, b.animate); if (a === "min" && this.orientation === "horizontal") this.range.stop(1, 1)[e ? "animate" : "css"]({ width: f + "%" },
b.animate); if (a === "max" && this.orientation === "horizontal") this.range[e ? "animate" : "css"]({ width: 100 - f + "%" }, { queue: false, duration: b.animate }); if (a === "min" && this.orientation === "vertical") this.range.stop(1, 1)[e ? "animate" : "css"]({ height: f + "%" }, b.animate); if (a === "max" && this.orientation === "vertical") this.range[e ? "animate" : "css"]({ height: 100 - f + "%" }, { queue: false, duration: b.animate })
    }
  }
  }); d.extend(d.ui.slider, { version: "1.8.4" })
})(jQuery);
;