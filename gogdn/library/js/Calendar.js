﻿function CalendarCustom() {
  CalendarCustom.prototype.ControlName = "";
  CalendarCustom.prototype.mask = "";
  CalendarCustom.prototype.GetText = getText;
  CalendarCustom.prototype.SetValue = setValue;
  CalendarCustom.prototype.SetText = setText;
  CalendarCustom.prototype.SetEnabled = setEnabled;
  CalendarCustom.prototype.SetMinDate = setMinDate;
  CalendarCustom.prototype.SetMaxDate = setMaxDate;
  CalendarCustom.prototype.SetDisplayedMonth = setDisplayedMonth;
}
function getText() {
  var val = $('#' + this.ControlName).val();
  if (val.indexOf("yyyy") > 0) {
    return "";
  } else {
    return val;
  }
}
function setValue(value) {$('#' + this.ControlName).dpSetSelected((value == null ? "" : value));}
function setText(value) {$('#' + this.ControlName).dpSetSelected((value == null ? "" : value));}
function setEnabled(mode) {$('#' + this.ControlName).dpSetDisabled(!mode);}
function setMinDate(d) {$('#' + this.ControlName).dpSetStartDate(d);}
function setMaxDate(d) {$('#' + this.ControlName).dpSetEndDate(d);}
function setDisplayedMonth(m, y) {$('#' + this.ControlName).dpSetDisplayedMonth(m, y);}
/*date*/
Date.dayNames = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
Date.abbrDayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
Date.monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
Date.abbrMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
Date.firstDayOfWeek = 1;
Date.format = 'dd/mm/yyyy';
Date.fullYearStart = '20';

(function() {

  function add(name, method) {
    if (!Date.prototype[name]) {
      Date.prototype[name] = method;
    }
  };
  add("isLeapYear", function() {
    var y = this.getFullYear();
    return (y % 4 == 0 && y % 100 != 0) || y % 400 == 0;
  });
  add("isWeekend", function() {
    return this.getDay() == 0 || this.getDay() == 6;
  });
  add("isWeekDay", function() {
    return !this.isWeekend();
  });
  add("getDaysInMonth", function() {
    return [31, (this.isLeapYear() ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][this.getMonth()];
  });
  add("getDayName", function(abbreviated) {
    return abbreviated ? Date.abbrDayNames[this.getDay()] : Date.dayNames[this.getDay()];
  });
  add("getMonthName", function(abbreviated) {
    return abbreviated ? Date.abbrMonthNames[this.getMonth()] : Date.monthNames[this.getMonth()];
  });
  add("getDayOfYear", function() {
    var tmpdtm = new Date("1/1/" + this.getFullYear());
    return Math.floor((this.getTime() - tmpdtm.getTime()) / 86400000);
  });
  add("getWeekOfYear", function() {
    return Math.ceil(this.getDayOfYear() / 7);
  });
  add("setDayOfYear", function(day) {
    this.setMonth(0);
    this.setDate(day);
    return this;
  });
  add("addYears", function(num) {
    this.setFullYear(this.getFullYear() + num);
    return this;
  });
  add("addMonths", function(num) {
    var tmpdtm = this.getDate();

    this.setMonth(this.getMonth() + num);

    if (tmpdtm > this.getDate())
      this.addDays(-this.getDate());

    return this;
  });
  add("addDays", function(num) {
    this.setDate(this.getDate() + num);
    return this;
  });
  add("addHours", function(num) {
    this.setHours(this.getHours() + num);
    return this;
  });
  add("addMinutes", function(num) {
    this.setMinutes(this.getMinutes() + num);
    return this;
  });
  add("addSeconds", function(num) {
    this.setSeconds(this.getSeconds() + num);
    return this;
  });
  add("zeroTime", function() {
    this.setMilliseconds(0);
    this.setSeconds(0);
    this.setMinutes(0);
    this.setHours(0);
    return this;
  });
  add("asString", function(format) {
    var r = format || Date.format;
    return r
			.split('yyyy').join(this.getFullYear())
			.split('yy').join((this.getFullYear() + '').substring(2))
			.split('MMMM').join(this.getMonthName(false))
			.split('MMM').join(this.getMonthName(true))
			.split('MM').join(_zeroPad(this.getMonth() + 1))
			.split('dd').join(_zeroPad(this.getDate()));
  });

  Date.fromString = function(s) {
    var f = Date.format;
    var d = new Date('01/01/1977');

    var mLength = 0;

    var iM = f.indexOf('MMMM');
    if (iM > -1) {
      for (var i = 0; i < Date.monthNames.length; i++) {
        var mStr = s.substr(iM, Date.monthNames[i].length);
        if (Date.monthNames[i] == mStr) {
          mLength = Date.monthNames[i].length - 4;
          break;
        }
      }
      d.setMonth(i);
    } else {
      iM = f.indexOf('MMM');
      if (iM > -1) {
        var mStr = s.substr(iM, 3);
        for (var i = 0; i < Date.abbrMonthNames.length; i++) {
          if (Date.abbrMonthNames[i] == mStr) break;
        }
        d.setMonth(i);
      } else {
        d.setMonth(Number(s.substr(f.indexOf('MM'), 2)) - 1);
      }
    }

    var iY = f.indexOf('yyyy');

    if (iY > -1) {
      if (iM < iY) {
        iY += mLength;
      }
      d.setFullYear(Number(s.substr(iY, 4)));
    } else {
      if (iM < iY) {
        iY += mLength;
      }
      d.setFullYear(Number(Date.fullYearStart + s.substr(f.indexOf('yy'), 2)));
    }
    var iD = f.indexOf('dd');
    if (iM < iD) {
      iD += mLength;
    }
    d.setDate(Number(s.substr(iD, 2)));
    if (isNaN(d.getTime())) {
      return false;
    }
    return d;
  };

  var _zeroPad = function(num) {
    var s = '0' + num;
    return s.substring(s.length - 2)
  };

})();
/*datepicker*/
(function($) {
  $.fn.extend({
    renderCalendar: function(s) {
      var dc = function(a) { return document.createElement(a); };
      s = $.extend({}, $.fn.datePicker.defaults, s);

      if (s.showHeader != $.dpConst.SHOW_HEADER_NONE) {
        var headRow = $(dc('tr'));
        for (var i = s.firstday; i < s.firstday + 7; i++) {
          var weekday = i % 7;
          var day = Date.dayNames[weekday];
          day = day.substring(0, 3);
          headRow.append(jQuery(dc('th')).attr({ 'scope': 'col', 'abbr': day, 'title': day, 'class': (weekday == 0 || weekday == 6 ? 'weekend' : 'weekday') }).html(s.showHeader == $.dpConst.SHOW_HEADER_SHORT ? day.substr(0, 1) : day));
        }
      };
      var calendarTable = $(dc('table')).attr({ 'cellspacing': 2, 'align': 'center', 'className': 'jCalendar' }).append((s.showHeader != $.dpConst.SHOW_HEADER_NONE ? $(dc('thead')).append(headRow) : dc('thead')));
      var tbody = $(dc('tbody'));
      var today = (new Date()).zeroTime();
      var month = s.month == undefined ? today.getMonth() : s.month;
      var year = s.year || today.getFullYear();
      var currentDate = new Date(year, month, 1);
      var firstDayOffset = s.firstday - currentDate.getDay() + 1;
      if (firstDayOffset > 1) firstDayOffset -= 7;
      var weeksToDraw = Math.ceil(((-1 * firstDayOffset + 1) + currentDate.getDaysInMonth()) / 7);
      currentDate.addDays(firstDayOffset - 1);

      var doHover = function() { if (s.hoverClass) { $(this).addClass(s.hoverClass); } };
      var unHover = function() { if (s.hoverClass) { $(this).removeClass(s.hoverClass); } };
      var w = 0;
      while (w++ < weeksToDraw) {
        var r = jQuery(dc('tr'));
        for (var i = 0; i < 7; i++) {
          var thisMonth = currentDate.getMonth() == month;
          var thisDisabled = false;
          var d = $(dc('td'))
								.text(currentDate.getDate() + '')
								.attr('className', (thisMonth ? (thisDisabled ? 'other-month disabled-day ' : 'current-month ' + (i == 0 || i == 6 ? 'especial-day ' : '')) : 'other-month ' + (thisDisabled ? 'disabled-day ' : '')) +
													(currentDate.isWeekend() ? 'weekend ' : 'weekday ') + (thisMonth && currentDate.getTime() == today.getTime() ? 'today ' : '')
								).hover(doHover, unHover);
          if (s.renderCallback) { s.renderCallback(d, currentDate, month, year); }
          r.append(d);
          currentDate.addDays(1);
        }
        tbody.append(r);
      }
      calendarTable.append(tbody);
      return this.each(function() { $(this).empty().append(calendarTable); });
    },
    datePicker: function(s) {
      if (!$.event._dpCache) $.event._dpCache = [];
      s = $.extend({}, $.fn.datePicker.defaults, s);

      return this.each(
				function() {
				  var $this = $(this);
				  var alreadyExists = true;

				  if (!this._dpId) {
				    this._dpId = $.event.guid++;
				    $.event._dpCache[this._dpId] = new DatePicker(this);
				    alreadyExists = false;
				  }
				  if (s.inline) {
				    s.createButton = false;
				    s.displayClose = false;
				    s.closeOnSelect = false;
				    $this.empty();
				  }
				  var controller = $.event._dpCache[this._dpId];
				  controller.init(s);
				  if (!alreadyExists && s.createButton) {
				    controller.button = $('<a href="#" class="dp-choose-date" title="' + $.dpText.TEXT_CHOOSE_DATE + '">' + $.dpText.TEXT_CHOOSE_DATE + '</a>').bind('click', function() { $this.dpDisplay(this); this.blur(); return false; });
				    $this.after(controller.button);
				  }
				  if (!alreadyExists && $this.is(':text')) {
				    $this
							.bind('dateSelected', function(e, selectedDate, $td) { this.value = selectedDate.asString(); }
							).bind('change', function() {
							  if (this.value != '') {
							    var d = Date.fromString(this.value);
							    if (d) { controller.setSelected(d, true, true); }
							  }
							}
							);
				    if (s.clickInput) { $this.bind('click', function() { $this.dpDisplay(); }); }
				    var d = Date.fromString(this.value);
				    if (this.value != '' && d) { controller.setSelected(d, true, true); }
				  }
				  $this.addClass('dp-applied');
				}
			)
    },
    dpSetDisabled: function(s) { return _w.call(this, 'setDisabled', s); },
    dpSetStartDate: function(d) { return _w.call(this, 'setStartDate', d); },
    dpSetEndDate: function(d) { return _w.call(this, 'setEndDate', d); },
    dpGetSelected: function() {
      var c = _getController(this[0]);
      if (c) { return c.getSelected(); }
      return null;
    },
    dpSetSelected: function(d, v, m) {
      if (v == undefined) v = true;
      if (m == undefined) m = true;
      return _w.call(this, 'setSelected', Date.fromString(d), v, m, true);
    },
    dpSetDisplayedMonth: function(m, y) {
      return _w.call(this, 'setDisplayedMonth', Number(m), Number(y), true);
    },
    dpDisplay: function(e) { return _w.call(this, 'display', e); },
    dpSetRenderCallback: function(a) { return _w.call(this, 'setRenderCallback', a); },
    dpSetPosition: function(v, h) { return _w.call(this, 'setPosition', v, h); },
    dpSetOffset: function(v, h) { return _w.call(this, 'setOffset', v, h); },
    selectedDateText: null,
    firstday: 0,
    dpClose: function() { return _w.call(this, '_closeCalendar', false, this[0]); },
    _dpDestroy: function() {
    }
  });
  var _w = function(f, a1, a2, a3, a4) {
    return this.each(function() { var c = _getController(this); if (c) { c[f](a1, a2, a3, a4); } });
  };
  function DatePicker(ele) {
    this.ele = ele; this.containerId = null; this.displayedMonth = null; this.displayedYear = null; this.startDate = null; this.endDate = null; this.showYearNavigation = null;
    this.closeOnSelect = null; this.displayClose = null; this.selectMultiple = null; this.verticalPosition = null; this.horizontalPosition = null; this.verticalOffset = null;
    this.horizontalOffset = null; this.button = null; this.renderCallback = []; this.selectedDates = {}; this.inline = null; this.selectedDateText = null;
    this.firstday = 0; this.context = '#dp-popup'; this.classHide = null;
  };
  $.extend(
		DatePicker.prototype,
		{
		  init: function(s) {
		    this.setStartDate(s.startDate); this.setEndDate(s.endDate); this.setDisplayedMonth(Number(s.month), Number(s.year)); this.setRenderCallback(s.renderCallback);
		    this.showYearNavigation = s.showYearNavigation; this.closeOnSelect = s.closeOnSelect; this.displayClose = s.displayClose; this.selectMultiple = s.selectMultiple;
		    this.verticalPosition = s.verticalPosition; this.horizontalPosition = s.horizontalPosition; this.hoverClass = s.hoverClass; this.containerId = s.containerId
		    this.setOffset(s.verticalOffset, s.horizontalOffset); this.firstday = s.firstday; this.inline = s.inline; this.classHide = s.classHide;
		    if (this.inline) { this.context = this.ele; this.display(); }
		    var formatSpanish = Date.format.split("/")[0].toLowerCase() == "dd";
		    var ctrl = this.ele.id;
		    var textbox = $("#" + ctrl);
		    if (!isValidDate(textbox.val(), formatSpanish)) { textbox.addClass("dp-textbox"); }
		    $(textbox)
		            .bind('change', function() {
		              if (textbox.val() == "" || !isValidDate(textbox.val(), formatSpanish)) {
		                ClearControl(textbox);
		                return;
		              }
		              if (isValidDate(textbox.val(), formatSpanish)) {
		                var d = ConvertEnglishFormat(s.startDate, formatSpanish);
		                var dif = DateDiff("d", d, ConvertEnglishFormat(textbox.val(), formatSpanish));
		                if (dif < 0) {
		                  ClearControl(textbox);
		                  return;
		                }
		                d = ConvertEnglishFormat(s.endDate, formatSpanish);
		                dif = DateDiff("d", d, ConvertEnglishFormat(textbox.val(), formatSpanish));
		                if (dif > 0) {
		                  ClearControl(textbox);
		                  return;
		                }
		              } else {
		                ClearControl(textbox);
		                return;
		              }
		              textbox.removeClass("dp-textbox");
		            }
		            )
		            .bind('keydown', function() { textbox.removeClass("dp-textbox"); })
		            .bind('click', function() { if (textbox.hasClass("dp-textbox")) { textbox.val(""); } })
		            .bind('focusout',
		                function() {
		                  if (textbox.val() == "" || !isValidDate(textbox.val(), formatSpanish)) {
		                    ClearControl(textbox);
		                    return;
		                  }
		                  if (isValidDate(textbox.val(), formatSpanish)) {
		                    var d1 = DateAdd("d", -1, ConvertEnglishFormat(s.startDate, formatSpanish));
		                    var d2 = ConvertEnglishFormat(textbox.val(), formatSpanish);
		                    var dif = DateDiff("d", d1, d2);
		                    if (dif < 0) { ClearControl(textbox); }
		                  }
		                }
		            );
		  },
		  setStartDate: function(d) {
		    if (d) { this.startDate = Date.fromString(d); }
		    if (!this.startDate) { this.startDate = (new Date()).zeroTime(); }
		    this.setDisplayedMonth(this.displayedMonth, this.displayedYear);
		  },
		  setEndDate: function(d) {
		    if (d) { this.endDate = Date.fromString(d); }
		    if (!this.endDate) { this.endDate = (new Date('12/31/2999')); }
		    if (this.endDate.getTime() < this.startDate.getTime()) { this.endDate = this.startDate; }
		    this.setDisplayedMonth(this.displayedMonth, this.displayedYear);
		  },
		  setPosition: function(v, h) {
		    this.verticalPosition = v;
		    this.horizontalPosition = h;
		  },
		  setOffset: function(v, h) {
		    this.verticalOffset = parseInt(v) || 0;
		    this.horizontalOffset = parseInt(h) || 0;
		  },
		  setDisabled: function(s) {
		    $e = $(this.ele);
		    $e[s ? 'addClass' : 'removeClass']('dp-disabled');
		    if (this.button) {
		      $but = $(this.button);
		      $but.attr('title', s ? '' : $.dpText.TEXT_CHOOSE_DATE);
		    }
		    if ($e.is(':text')) { $e.attr('disabled', s ? 'disabled' : ''); }
		  },
		  setDisplayedMonth: function(m, y, rerender) {
		    if (this.startDate == undefined || this.endDate == undefined) { return; }
		    var s = new Date(this.startDate.getTime());
		    s.setDate(1);
		    var e = new Date(this.endDate.getTime());
		    e.setDate(1);

		    var t;
		    if ((!m && !y) || (isNaN(m) && isNaN(y))) {
		      t = new Date().zeroTime();
		      t.setDate(1);
		    } else if (isNaN(m)) {
		      t = new Date(y, this.displayedMonth, 1);
		    } else if (isNaN(y)) {
		      t = new Date(this.displayedYear, m, 1);
		    } else {
		      t = new Date(y, m, 1)
		    }
		    if (t.getTime() < s.getTime()) {
		      t = s;
		    } else if (t.getTime() > e.getTime()) {
		      t = e;
		    }
		    var oldMonth = this.displayedMonth;
		    var oldYear = this.displayedYear;
		    this.displayedMonth = t.getMonth();
		    this.displayedYear = t.getFullYear();

		    if (rerender && (this.displayedMonth != oldMonth || this.displayedYear != oldYear)) {
		      this._rerenderCalendar();
		      $(this.ele).trigger('dpMonthChanged', [this.displayedMonth, this.displayedYear]);
		    }
		  },
		  setSelected: function(d, v, moveToMonth, dispatchEvents) {
		    if (v == this.isSelected(d)) { return; }
		    if (this.selectMultiple == false) { this.selectedDates = {}; $('td.selected', this.context).removeClass('selected'); }
		    if (v == "") { return; }
		    if (moveToMonth && this.displayedMonth != d.getMonth()) { this.setDisplayedMonth(d.getMonth(), d.getFullYear(), true); }
		    this.selectedDates[d.toString()] = v;
		    var selectorString = 'td.';
		    selectorString += d.getMonth() == this.displayedMonth ? 'current-month' : 'other-month';
		    selectorString += ':contains("' + d.getDate() + '")';
		    var $td;
		    $(selectorString, this.ele).each(
					function() {
					  if ($(this).text() == d.getDate()) {
					    $td = $(this);
					    $td[v ? 'addClass' : 'removeClass']('selected');
					  }
					}
				);
		    if (dispatchEvents) {
		      var s = this.isSelected(d);
		      $e = $(this.ele);
		      $e.trigger('dateSelected', [d, $td, s]);
		      $e.trigger('change');
		    }
		  },
		  isSelected: function(d) {
		    return this.selectedDates[d.toString()];
		  },
		  getSelected: function() {
		    var r = [];
		    for (s in this.selectedDates) { if (this.selectedDates[s] == true) { r.push(Date.parse(s)); } }
		    return r;
		  },
		  display: function(eleAlignTo) {
		    if ($(this.ele).is('.dp-disabled')) return;
		    if ($.browser.msie) { if ($.browser.version == "6.0") { $("." + this.classHide).css("visibility", "hidden"); } }
		    eleAlignTo = eleAlignTo || this.ele;
		    var c = this;
		    var $ele = $(eleAlignTo);
		    var eleOffset = $ele.offset();
		    var $createIn;
		    var attrs;
		    var attrsCalendarHolder;
		    var cssRules;
		    if (c.inline) {
		      $createIn = $(this.ele);
		      attrs = {
		        'id': 'calendar-' + this.ele._dpId,
		        'className': 'dp-popup dp-popup-inline'
		      };
		      cssRules = {
		    };
		  } else {
		    var xWidth = $('#' + this.ele.id + '').css('width');
		    xWidth = (xWidth == "auto" ? "150" : xWidth);
		    xWidth = xWidth.replace("px", "");
		    var elementId = this.containerId
		    $createIn = $('#' + elementId + '-container');
		    attrs = {
		      'id': 'dp-popup',
		      'className': 'dp-popup'
		    };
		    cssRules = {
		      'top': 20,
		      'left': -(Number(xWidth) + 23),
		      'position': 'absolute'
		    };
		    var _checkMouse = function(e) {
		      var el = e.target;
		      var cal = $('#dp-popup')[0];

		      while (true) {
		        if (el == cal) {
		          return true;
		        } else if (el == document) {
		          c._closeCalendar();
		          return false;
		        } else { el = $(el).parent()[0]; }
		      }
		    };
		    this._checkMouse = _checkMouse;
		    this._closeCalendar(true);
		    if ($.browser.version == "6.0") { try { HideDropDownList(1); } catch (e) { } }
		  }
		  $createIn
					.append(
						$('<span></span>')
							.attr(attrs)
							.css(cssRules)
							.append(
								$('<div class="dp-nav-label"><label style="padding-top:5px"></label></div>'),
								$('<span class="dp-nav-prev"></span>')
									.append(
										$('<a class="dp-nav-prev-year" href="#" title="' + $.dpText.TEXT_PREV_YEAR + '"></a>')
											.bind('click', function() { return c._displayNewMonth.call(c, this, 0, -1); }),
										$('<a class="dp-nav-prev-month" href="#" title="' + $.dpText.TEXT_PREV_MONTH + '"></a>')
											.bind('click', function() { return c._displayNewMonth.call(c, this, -1, 0); })
									),
								$('<span class="dp-nav-next"></span>')
									.append(
										$('<a class="dp-nav-next-year" href="#" title="' + $.dpText.TEXT_NEXT_YEAR + '"></a>')
											.bind('click', function() { return c._displayNewMonth.call(c, this, 0, 1); }),
										$('<a class="dp-nav-next-month" href="#" title="' + $.dpText.TEXT_NEXT_MONTH + '"></a>')
											.bind('click', function() { return c._displayNewMonth.call(c, this, 1, 0); })
									),
								$('<span></span>')
									.attr('className', 'dp-calendar'),

							    $('<div></div>')
									.attr('className', 'dp-line')
							)
							.bgIframe()
						);

		  var $pop = this.inline ? $('.dp-popup', this.context) : $('#dp-popup');
		  if (this.showYearNavigation == false) {
		    $('.dp-nav-prev-year, .dp-nav-next-year', c.context).css('display', 'none');
		  }
		  if (this.displayClose) {
		    $pop.append('<div id="divFooter" class="dp-footer"></div>');
		    var $foot = $("#divFooter");
		    var d = new Date();
		    var formatSpanish = Date.format.split("/")[0].toLowerCase() == "dd";
		    var dif = DateDiff("d", d, c.endDate);

		    if (dif > 0) {
		      $foot.append(
		      	$('<a href="#" id="dp-button" class="dp-button left">' + $.dpText.TEXT_TODAY + '</a>')
							.bind('click', function() {
							  if (dif > 0) {
							    c.setSelected(d, d, true, true);
							    c._closeCalendar();
							    return false;
							  }
							}
							),
						$('<a href="#" id="dp-button" class="dp-button right">' + $.dpText.TEXT_CLEAR + '</a>')
							.bind('click', function() {
							  var ctrlname = c.ele.id;
							  $("#" + ctrlname).addClass("dp-textbox");
							  $("#" + ctrlname).val(Date.format.toLowerCase());
							  c.setSelected("", "", true, true);
							  $("#" + ctrlname).focus();
							  c._closeCalendar();
							  return false;
							}
							)
					);
		    } else {
		      $foot.append(
		      	$('<a href="#" id="dp-button" class="dp-button center">' + $.dpText.TEXT_CLEAR + '</a>')
							.bind('click', function() {
							  var ctrlname = c.ele.id;
							  $("#" + ctrlname).addClass("dp-textbox");
							  $("#" + ctrlname).val(Date.format.toLowerCase());
							  c.setSelected("", "", true, true);
							  $("#" + ctrlname).focus();
							  c._closeCalendar();
							  return false;
							}
							)
					);
		    }
		  }
		  c._renderCalendar();
		  $(this.ele).trigger('dpDisplayed', $pop);
		  if (!c.inline) {
		    if (this.verticalPosition == $.dpConst.POS_BOTTOM) { $pop.css('top', eleOffset.top + $ele.height() - $pop.height() + c.verticalOffset); }
		    if (this.horizontalPosition == $.dpConst.POS_RIGHT) { $pop.css('left', eleOffset.left + $ele.width() - $pop.width() + c.horizontalOffset); }
		    $(document).bind('mousedown', this._checkMouse);
		  }
		},
		setRenderCallback: function(a) {
		  if (a == null) return;
		  if (a && typeof (a) == 'function') { a = [a]; }
		  this.renderCallback = this.renderCallback.concat(a);
		},
		cellRender: function($td, thisDate, month, year) {
		  var c = this.dpController;
		  var d = new Date(thisDate.getTime());
		  var currentDate = c.startDate;

		  var dif = DateDiff("d", currentDate, thisDate);
		  if (dif >= 0) {
		    $td.bind(
					'click',
					function() {
					  var $this = $(this);
					  if (!$this.is('.disabled')) {
					    c.setSelected(d, !$this.is('.selected') || !c.selectMultiple, false, true);
					    if (c.closeOnSelect) { c._closeCalendar(); }
					  }
					}
				);
		  }
		  if (c.isSelected(d)) { $td.addClass('selected'); }
		  for (var i = 0; i < c.renderCallback.length; i++) { c.renderCallback[i].apply(this, arguments); }
		},
		_displayNewMonth: function(ele, m, y) {
		  if (!$(ele).is('.disabled')) {
		    this.setDisplayedMonth(this.displayedMonth + m, this.displayedYear + y, true);
		  }
		  ele.blur();
		  return false;
		},
		_rerenderCalendar: function() {
		  this._clearCalendar();
		  this._renderCalendar();
		},
		_renderCalendar: function() {
		  $('label', this.context).html(Date.monthNames[this.displayedMonth] + ' ' + this.displayedYear);
		  $('.dp-calendar', this.context).renderCalendar({ month: this.displayedMonth, year: this.displayedYear, renderCallback: this.cellRender, dpController: this, hoverClass: this.hoverClass });
		  if (this.displayedYear == this.startDate.getFullYear() && this.displayedMonth == this.startDate.getMonth()) {
		    $('.dp-nav-prev-year', this.context).addClass('disabled');
		    $('.dp-nav-prev-month', this.context).addClass('disabled');
		    $('.dp-calendar td.other-month', this.context).each(
						function() {
						  var $this = $(this);
						  if (Number($this.text()) > 20) { }
						}
					);
		    var d = this.startDate.getDate();
		    $('.dp-calendar td.current-month', this.context).each(
						function() {
						  var $this = $(this);
						  if (Number($this.text()) < d) { $this.addClass('disabled'); }
						}
					);
		  } else {
		    $('.dp-nav-prev-year', this.context).removeClass('disabled');
		    $('.dp-nav-prev-month', this.context).removeClass('disabled');
		    var d = this.startDate.getDate();
		    if (d > 20) {
		      var sd = new Date(this.startDate.getTime());
		      sd.addMonths(1);
		      if (this.displayedYear == sd.getFullYear() && this.displayedMonth == sd.getMonth()) {
		        $('dp-calendar td.other-month', this.context).each(
								function() {
								  var $this = $(this);
								  if (Number($this.text()) < d) { $this.addClass('disabled'); }
								}
							);
		      }
		    }
		  }
		  if (this.displayedYear == this.endDate.getFullYear() && this.displayedMonth == this.endDate.getMonth()) {
		    $('.dp-nav-next-year', this.context).addClass('disabled');
		    $('.dp-nav-next-month', this.context).addClass('disabled');
		    $('.dp-calendar td.other-month', this.context).each(
						function() {
						  var $this = $(this);
						  if (Number($this.text()) < 14) { $this.addClass('disabled'); }
						}
					);
		    var d = this.endDate.getDate();
		    $('.dp-calendar td.current-month', this.context).each(
						function() {
						  var $this = $(this);
						  if (Number($this.text()) > d) { $this.addClass('disabled'); }
						}
					);
		  } else {
		    $('.dp-nav-next-year', this.context).removeClass('disabled');
		    $('.dp-nav-next-month', this.context).removeClass('disabled');
		    var d = this.endDate.getDate();
		    if (d < 13) {
		      var ed = new Date(this.endDate.getTime());
		      ed.addMonths(-1);
		      if (this.displayedYear == ed.getFullYear() && this.displayedMonth == ed.getMonth()) {
		        $('.dp-calendar td.other-month', this.context).each(
								function() {
								  var $this = $(this);
								  if (Number($this.text()) > d) { $this.addClass('disabled'); }
								}
							);
		      }
		    }
		  }
		},
		_closeCalendar: function(programatic, ele) {
		  if (programatic == null) { $("." + this.classHide).css("visibility", "visible"); };
		  if (!ele || ele == this.ele) {
		    $(document).unbind('mousedown', this._checkMouse);
		    this._clearCalendar();
		    $('#dp-popup a').unbind();
		    $('#dp-popup').empty().remove();
		    if (!programatic) {
		      if ($.browser.version == "6.0") { try { HideDropDownList(0); } catch (e) { } }
		      $(this.ele).trigger('dpClosed', [this.getSelected()]);
		    }
		  }
		},
		_clearCalendar: function() { $('.dp-calendar td', this.context).unbind(); $('.dp-calendar', this.context).empty(); }
}
	);
  $.dpConst = { SHOW_HEADER_NONE: 0, SHOW_HEADER_SHORT: 1, SHOW_HEADER_LONG: 2, POS_TOP: 0, POS_BOTTOM: 1, POS_LEFT: 0, POS_RIGHT: 1 };
  $.dpText = { TEXT_PREV_YEAR: 'Previous year', TEXT_PREV_MONTH: 'Previous month', TEXT_NEXT_YEAR: 'Next year', TEXT_NEXT_MONTH: 'Next month', TEXT_CLOSE: 'Close', TEXT_CHOOSE_DATE: 'Choose date', TEXT_TODAY: 'Today', TEXT_CLEAR: 'Clear' };
  $.dpVersion = '$Id: jquery.datePicker.js 15 2008-12-17 04:40:18Z kelvin.luck $';
  $.fn.datePicker.defaults = { month: undefined, year: undefined, showHeader: $.dpConst.SHOW_HEADER_LONG, startDate: undefined, endDate: undefined, inline: false, renderCallback: null, createButton: true, showYearNavigation: true, closeOnSelect: true, displayClose: true, selectMultiple: false, clickInput: false, verticalPosition: $.dpConst.POS_TOP, horizontalPosition: $.dpConst.POS_LEFT, verticalOffset: 0, horizontalOffset: 0, hoverClass: 'dp-hover', firstday: 0 };
  function _getController(ele) { if (ele._dpId) return $.event._dpCache[ele._dpId]; return false; };
  if ($.fn.bgIframe == undefined) { $.fn.bgIframe = function() { return this; }; };
  $(window)
		.bind('unload', function() { var els = $.event._dpCache || []; for (var i in els) { $(els[i].ele)._dpDestroy(); } });
})(jQuery);
function ClearControl(textbox) {
  textbox.addClass("dp-textbox"); textbox.val(Date.format.toLowerCase()); textbox.dpSetSelected("");
}