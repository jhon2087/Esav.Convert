function PaxGridCustom() {
  PaxGridCustom.prototype.ControlName = "";
  PaxGridCustom.prototype.ControlClientID = "";
  PaxGridCustom.prototype.ParentControlID = "";
  PaxGridCustom.prototype.refresh = Begin_RefreshGrid;
  PaxGridCustom.prototype.search = SearchData;
  PaxGridCustom.prototype.SelectedRecords = new Array();
  PaxGridCustom.prototype.PageIndex = 1;
  PaxGridCustom.prototype.initGrid = initGrid;
  PaxGridCustom.prototype.postInitGrid = null;
  PaxGridCustom.prototype.onDblClickGrid = null;
  PaxGridCustom.prototype.onClickGrid = null;
  PaxGridCustom.prototype.onSelection = null;
  PaxGridCustom.prototype.onMainSelection = null;
  PaxGridCustom.prototype.GetRecord = getRecord;
  PaxGridCustom.prototype.GetRecordForKey = getRecordForKey;
  PaxGridCustom.prototype.TotalRows = totalRows;
  PaxGridCustom.prototype.selectRecord = SelectRecord;
  PaxGridCustom.prototype.clearSelectedRecord = clearSelectedRecord
  PaxGridCustom.prototype.MultipleSelection = true;
  PaxGridCustom.prototype.AutoResize = null;
}
function SelectRecord(idx) {
  try {
    if (totalRows() > 0) {
      var objTr = $(".bodyPaxGrid table tbody tr")[idx]
      var classTR = objTr.className.replace(" selected", "")
      $("." + classTR).addClass("selected");
    }
  } catch (ex) { }
}
function getRecord(element) {
  var id = element.attr("class").replace(" selected", "")
  if (id.indexOf("item-") == -1) { id = element.parent().parent().attr("class").replace(" selected", ""); }
  var data = $("." + id + " .data").text()
  var thisRecord = new this.record(data)
  return thisRecord
}
function getRecordForKey(id) {
  var data = $(".item-" + id + " .data").text()
  var thisRecord = new this.record(data)
  return thisRecord;
}
function totalRows() { return $(".bodyPaxGrid table tr").length; }

function clearSelectedRecord() {
  this.SelectedRecords = new Array();
  $("#" + this.ControlClientID + "_content :checked").attr("checked", false)
  $("#" + this.ControlClientID + "_content tr").removeClass("selected", "")
}
function clearAllSelection(gridObj, gridClient, notThisItemId) {
  eval(gridObj + ".SelectedRecords = new Array()");
  var oChecked = $("#" + gridClient + "_content .item-" + notThisItemId + " .selection").attr("checked")
  $("#" + gridClient + "_content :checked").attr("checked", false)
  $("#" + gridClient + "_content tr").removeClass("selected", "")
  if (oChecked) {
    $("#" + gridClient + "_content .item-" + notThisItemId + " .selection").attr("checked", true)
    $("#" + gridClient + "_content .item-" + notThisItemId).addClass("selected", "")
  }
}
function initGrid() {
  $("#" + this.ControlClientID + "_content :checkbox").attr("checked", false)
  this.SelectedRecords = new Array();
  if (this.AutoResize) { refreshLayout() }; //window.setTimeout("refreshLayout()", 50); }
  var arg = this.ControlName + ":::" + this.MultipleSelection + ":::" + this.ControlClientID
  $("#" + this.ControlClientID + "_content .dbBCont :checkbox").bind("click", arg, function(event) {
    var id = $(this).val()
    var data = ($(".item-" + id + " .data").text())
    data = data.replace(/'/g, '%::%:')
    var dataArg = event.data.split(":::")
    var controlName = dataArg[0]
    var controlClientID = dataArg[2]
    if (dataArg[1] == "false") { eval("clearAllSelection('" + controlName + "','" + controlClientID + "','" + id + "')"); }
    if ($(this).attr("checked")) { eval(dataArg[0] + ".SelectedRecords.push(new " + dataArg[0] + ".record('" + data + "'))") } else { eval("removeSelectedRecord(" + controlName + ", '" + id + "')"); }
  })
  if (this.onSelection) { $("#" + this.ControlClientID + "_content .dbBCont :checkbox").click(this.onSelection); }
  var elements = $("#" + this.ControlClientID + "_content .dbBCont :checked")  
  for (a = 0; a < elements.length; a++) {
    var id = elements[a].value
    var data = $("#" + this.ControlClientID + "_content .item-" + id + " .data").text()
    this.SelectedRecords.push(new this.record(data))
  }
  if (this.postInitGrid) { this.postInitGrid(); }
  if (this.onDblClickGrid) { $("#" + controlClientID + "_content .dbBCont tr").dblclick(this.onDblClickGrid); }
  if (this.onClickGrid) { $("#" + controlClientID + "_content .dbBCont tr").click(this.onClickGrid); }
  var checkAllArg = this.ControlClientID
  $("#" + this.ControlClientID + "_content .main-selection").bind("click", checkAllArg, function() {
    var controlName = arg.split(":::")[0];
    var controlClientID = arg.split(":::")[2];
    if ($(this).attr("checked")) {
      eval(controlName + ".SelectedRecords = new Array()");
      $("#" + controlClientID + "_content .dbBCont :checkbox").attr("checked", "checked");
      var elements = $("#" + controlClientID + "_content .dbBCont :checked");
      for (a = 0; a < elements.length; a++) {
        var id = elements[a].value
        var data = $("#" + controlClientID + "_content .item-" + id + " .data").text()
        data = data.replace(/'/g, '%::%:')
        eval(controlName + ".SelectedRecords.push(new " + controlName + ".record('" + data + "'))")
      }
      //$("#" + controlClientID + "_content .dbBCont tr").addClass("selected");
      $("#" + controlClientID + "_content .dbBCont :checkbox").parent().parent().addClass("selected");
    } else {
      eval(controlName + ".SelectedRecords = new Array()");
      $(".x-grid .dbBCont :checkbox").attr("checked", "");
      $("#" + controlClientID + "_content .dbBCont tr").removeClass("selected");
    }
  })
  if (this.onMainSelection) { $("#" + this.ControlClientID + "_content .main-selection").click(this.onMainSelection); }
  SearchWithEnter();
}
function removeSelectedRecord(gridObj, id) {
  for (a = 0; a < gridObj.SelectedRecords.length; a++) {
    if (gridObj.SelectedRecords[a].key == id) {
      gridObj.SelectedRecords.splice(a, 1);
      return;
    }
  }
}
function Begin_RefreshGrid() {
  if (this.ControlClientID !== "") { DoFormCallBack("RefreshGridControl", this.ControlName + ":" + this.ControlClientID + ":" + this.ParentControlID, End_RefreshGrid) }
  $("#" + this.ControlClientID + "_content .bodyPaxGrid").html("<div class='loading'>" + document.getElementById(this.ControlName + "_loading").value + "</div>")
}
function End_RefreshGrid(arg) {
  var mData = arg.split(":::");
  var objGrid = document.getElementById(mData[1] + "_content");
  objGrid.innerHTML = mData[2];
  eval("if (" + mData[0] + ".AutoSize){window.setTimeout('refreshLayout()', 0);}")
  attachFunctions(mData[1]);
  eval(mData[0] + ".initGrid()")
}
function doSort(grid, sortExpression, sortDirection) {
  document.getElementById(grid + "_pageindex").value = 1;
  document.getElementById(grid + "_sort").value = sortExpression;
  document.getElementById(grid + "_direction").value = sortDirection;
  window.setTimeout(grid + ".refresh()");
}
function doPaging(grid, pageIndex) {    
    document.getElementById(grid + "_pageindex").value = pageIndex;
    window.setTimeout(grid + ".refresh()");
}
function doChangePageSize(grid, pageSize) {
  document.getElementById(grid + "_pagesize").value = pageSize;
  document.getElementById(grid + "_pageindex").value = 1;
  window.setTimeout(grid + ".refresh()");
}
function SearchWithEnter(e) {
  try {
    $("#quick-search-box :text").bind("keydown", function(event) {
      if (event.keyCode == 13) {
        $("#quick-search-box .btn_search").click();
        stopEvent(event);
      }
    })
  }catch (e) { }
}
function SearchData() {
  document.getElementById(this.ControlName + "_pageindex").value = 1;
  if (this.ControlName !== "") { DoFormCallBack("RefreshGridControl", this.ControlName + ":" + this.ControlClientID + ":" + this.ParentControlID, End_RefreshGrid) }
  $("#" + this.ControlClientID + "_content .bodyPaxGrid").html("<div class='loading'>" + document.getElementById(this.ControlName + "_loading").value + "</div>")
}
/*Resize*/
var outerLayout, middleLayout, innerLayout;
var openmenu = false;
$(function() {
  if ($('div.outer-center').length !== 0) {
    innerLayout = $('div.outer-center').layout({
      north__paneSelector: ".inner-north",
      center__paneSelector: ".inner-center",
      south__paneSelector: ".inner-south",
      north__spacing_open: 0,
      south__spacing_open: 0,
      center__onresize: "gridLayout.resizeAll"
    });
  }
  if ($('.x-unselectable').disableTextSelect) { $('.x-unselectable').disableTextSelect(); }
  if (jQuery.browser.msie) {if (parseInt(jQuery.browser.version) == 6) { $(window).resize(); }}
});
/*layout*/
var gridLayout;
function refreshLayout2() {
  if (gridLayout) { gridLayout.resizeAll(); }
}
function refreshLayout() {
  gridLayout = $('div.x-grid').layout({
    south__paneSelector: ".footerPaxGrid"
		, north__paneSelector: ".headerPaxGrid"
		, center__paneSelector: ".bodyPaxGrid"
		, north__spacing_open: 0
		, south__spacing_open: 0
  });
}
/*functions*/
$(function() {
  var x = $(".headerPaxGrid:parent").attr("id");
  $('.bodyPaxGrid').scroll(function() {
    var grid = $(this).parent().attr("id");
    var leftX = $(this).scrollLeft();
    $('#' + grid + ' .dbHCont').css('left', '-' + leftX + 'px');
  });
  $('.dbBCont input[type=checkbox]').click(function() {
    if ($(this).attr('checked')) {
      $(this).parent().parent().addClass('selected');
    } else {
      $(this).parent().parent().removeClass('selected');
    }
  });
});
function attachFunctions(grid) {
  $('.bodyPaxGrid').scroll(function() {
    var leftX = $(this).scrollLeft();
    $('#' + grid + '_content .dbHCont').css('left', '-' + leftX + 'px');
  });
  $('.dbBCont input[type=checkbox]').click(function() {
    if ($(this).attr('checked')) {
      $(this).parent().parent().addClass('selected');
    } else {
      $(this).parent().parent().removeClass('selected');
    }
  });
}