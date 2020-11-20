// Author : Arvind Warade
// Desc   : to get Questring in .html page.......... 
// Return : object = {'param1':'value','param2':'value'};    
//  var query = getQueryParams(document.location.search);
// alert(query.foo);

var message = "";

var query = document.location.href;
var queryArr = query.split('/');
var len = queryArr.length;
var path = getUrlPath(objRootURLPath); // for online 3
function getUrlPath(start) { var path = ''; if (len == start) { path = ''; } else if (len == start + 1) { path = '../'; } else if (len == start + 2) { path = '../../'; } else if (len == start + 3) { path = '../../../'; } else if (len == start + 4) { path = '../../../../'; } else if (len == start + 5) { path = '../../../../../'; } return path; }

$(document).ready(function () {
    HideAJAXLoader();
    // right click disabled 
    //if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS4; } else if (document.all && !document.getElementById) { document.onmousedown = clickIE4; }
    //document.oncontextmenu = new Function("return false");
    hideStatusBar();
    //adjustframeheight();
});

function adjustframeheight() {
    //    if (parent.document.getElementById('dvhide') != null) {
    //        parent.$("#dvhide").height('10px');
    //        if (parseInt($(document).height()) < 500) {
    //            parent.$("#dvhide").height('500px');
    //        } else {
    //            parent.$("#dvhide").height(($(document).height() - 550) + 'px');
    //        }
    //    }
}



function AlertMessages(MessageId, isAutoClose, isParent, OpenWith, param, param1, param2) {
    var OpenTimeing = 1000;


    var url = path + '../AlertBox.htm?MessageId=' + MessageId + '&param=' + param;
    //var content = '<div style="width: 300px;color:red;font-weight: bold;font-size: 25px;margin: 0px auto; padding:10px 30px;" onload="javascript:fanclyload();" >' + CommonMessageInWebsite(MessageId, param) + '</div>';

    if (isParent == true) {
     
       
       

        parent.$.fancybox.open({ href: url,
           // type: 'iframe',
            padding: 5,
            openEffect: 'elastic',
            openSpeed: 250,
            closeEffect: 'elastic',
            closeSpeed: 250,
            autoSize: true,
            width: '500px',
            height: 'auto',
            centerOnScroll: true,
            autoDimensions: true,
            transitionIn: 'fade',
            transitionOut: 'fade'

        });
    } else {
        $.fancybox.open({ href: url,
          //  type: 'iframe',
            padding: 5,
            openEffect: 'elastic',
            openSpeed: 250,
            closeEffect: 'elastic',
            closeSpeed: 250,
            autoSize: true,
            width: '500px',
            height: 'auto',
            centerOnScroll: true,
            autoDimensions: true,
            transitionIn: 'fade',
            transitionOut: 'fade'

        });
    }

}

function fanclyload() {
    alert('');
}


function CommonMessageInWebsite(MessageId, param) {
    var Message = "";
    if (MessageId == 0) { Message = 'Save Successfully'; }
    else if (MessageId == 0) { Message = 'Delete Successfully'; }
    else if (MessageId == 1) { Message = 'Update Successfully'; }
    else if (MessageId == 2) { Message = 'Employee Registered Successfully'; }
    else if (MessageId == 3) { Message = 'Wait for a minute. Loading Search...........'; }
    else if (MessageId == 4) { Message = 'Select row for edit'; }
    else if (MessageId == 5) { Message = param; }
    else if (MessageId == 6) { Message = 'Select row'; }
    else if (MessageId == 7) { Message = 'Select Mandatory Field'; }
    else if (MessageId == 8) { Message = 'Failure! Check internet connection and try again'; }
    else if (MessageId == 9) { Message = 'Email Sent Successfully'; }



    return Message;
}



function HideAJAXLoader() { $(".lodingParentElement").hide(); }
function ShowAJAXLoader() { $(".lodingParentElement").show(); }
function clickIE4() { if (event.button == 2) { alert(message); return false; } }
function clickNS4(e) { if (document.layers || document.getElementById && !document.all) { if (e.which == 2 || e.which == 3) { alert(message); return false; } } }

function hideStatusBar() {

    setTimeout(function () {
        $("#divStatusBar").hide()
    }, 2000);
}



function getQueryParams(qs) {
    qs = qs.split("+").join(" ");

    var params = {}, tokens, re = /[?&]?([^=]+)=([^&]*)/g;

    while (tokens = re.exec(qs)) {
        params[decodeURIComponent(tokens[1])] = decodeURIComponent(tokens[2]);
    }

    return params;
}

function getUrlParameters(parameter, staticURL, decode) {

    var currLocation = (staticURL.length) ? staticURL : window.location.search, parArr = currLocation.split("?")[1].split("&"), returnBool = true;

    for (var i = 0; i < parArr.length; i++) {
        parr = parArr[i].split("=");
        if (parr[0] == parameter) {
            return (decode) ? decodeURIComponent(parr[1]) : parr[1];
            returnBool = true;
        } else {
            returnBool = false;
        }
    }

    if (!returnBool) return false;
}



function fillFancyBox(url, scrwidth, scrheight, type, isParent, _topwidth) {
    if (type == 0) {
        if (scrheight == 'auto') {
            scrheight = ($(window).height() - 100) + 'px';
        }

        if (isParent == true) {
            parent.$.fancybox.open({ href: url,
              //  type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: true,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                autoDimensions: true,
                transitionIn: 'fade',
                transitionOut: 'fade',

                afterLoad: function () { },
                onClosed: function () { $("#btnSearch").click(); ctlDetails.selectRow = ''; },
                onComplete: function () { },
                scrolling: 'no',
                iframe: { 'scrolling': 'no' }


            });

        }
        else {
            $.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: true,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                topwidth: _topwidth,
                transitionIn: 'fade',
                transitionOut: 'fade',

                afterLoad: function () { },
                onClosed: function () { $("#btnSearch").click(); ctlDetails.selectRow = ''; },
                scrolling: 'no',
                iframe: { 'scrolling': 'no' }
            });
        }
    }
    else if (type == 1) {
        if (scrheight == 'auto') {
            scrheight = ($(window).height() - 100) + 'px';
        }
        if (isParent == true) {
            parent.$.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',

                afterLoad: function () { },
                onClosed: function () { }
            });
        }
        else {
            $.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',
                topwidth: _topwidth,
                afterLoad: function () { },
                onClosed: function () { }


            });
        }
    }
    else if (type == 2) {
        if (isParent == true) {
            parent.$.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',
                topwidth: _topwidth,
                afterLoad: function () { },
                afterClose: function () {
                    if (typeof oCell != 'undefined') { fillSubTable(oCell); }
                },
                onClosed: function () { }
            });
        }
        else {
            $.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',
                topwidth: _topwidth,
                afterLoad: function () { },
                afterClose: function () {
                    if (typeof oCell != 'undefined') { fillSubTable(oCell); }
                },
                onClosed: function () { }


            });
        }

    }
    else if (type == 4) {
        if (scrheight == 'auto') {
            scrheight = ($(window).height() - 100) + 'px';
        }
        if (isParent == true) {
            parent.$.fancybox.open({ href: url,
              //  type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',

                afterLoad: function () { },
                onClosed: function () { $("#btnSearch").click(); }
            });
        }
        else {
            $.fancybox.open({ href: url,
               // type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',
                topwidth: _topwidth,
                afterLoad: function () { },
                afterClose: function () {
                                      $("#btnSearch").click();
                }


            });
        }
    }
    else if (type == 3) {
        if (isParent == true) {
            parent.$.fancybox.open({ href: url,
             //   type: 'iframe',
                padding: 5,
                openEffect: 'elastic',
                openSpeed: 250,
                closeEffect: 'elastic',
                closeSpeed: 250,
                autoSize: false,
                width: scrwidth,
                height: scrheight,
                centerOnScroll: true,
                transitionIn: 'fade',
                transitionOut: 'fade',
                topwidth: _topwidth,
                afterLoad: function () { },
                afterClose: function () {
                    _saveCat(0, CategoryId.a, CategoryId.b, '');
                },
                onClosed: function () { }
            });
        }

    }

}


function closeFancyBox(type) {
    if (typeof parent.$.fancybox == 'undefined') { return false; }
    if (type == 1) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#btnSearch');
        $(iframeContent).click();
    } else if (type == 0) {
        parent.$.fancybox.close();
    }
    else if (type == 2) {
        parent.$("#frm2").attr('src', '../Post_setup/AssignMemberToPost.aspx?CategoryId=136');
        parent.$.fancybox.close();
    }
    else if (type == 3) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#btnGo');
        $(iframeContent).click();
    }
    else if (type == 4) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#btnRefresh');
        $(iframeContent).click();
    }
    else if (type == 5) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#demorow_' + ctlDetails.objRow.title);
        $(iframeContent).click();
        //$(iframeContent).click();
    }
    else if (type == 6) {
        parent.$("#frm2").attr('src', '../../Admin/StaffMenu/HolidaySetup/Calender.aspx');
        parent.$.fancybox.close();
    }
    else if (type == 7) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#ancRefresh');
        $(iframeContent).click();
    }
    else if (type == 8) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#btnref');
        $(iframeContent).click();
    }
    else if (type == 9) {
        parent.$("#frm2").attr('src', '../../Admin/ContactPerson.aspx');
        parent.$.fancybox.close();
    }
    else if (type == 10) {
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#imgTransfer');
        $(iframeContent).click();
    }
    else if (type == 11) {
        parent.parent.parent.$("#frm2").attr('src', '../HRModule/MainConfig.aspx');
        parent.$.fancybox.close();
    }

    else if (type == 12) {
//        parent.$.fancybox.close();
//        var iframeInner = parent.$("#frm2").contents();
//        var iframeContent = $(iframeInner).contents().find('#btnSearch1');
//        $(iframeContent).click();
        parent.$.fancybox.close();
        var iframeInner = parent.$("#frm2").contents();
        var iframeContent = $(iframeInner).contents().find('#btnSearch1');
        $(iframeContent).click();
    }
    else if (type == 13) {
        parent.parent.parent.$("#frm2").attr('src', '../../Admin/OutDoorSearch.aspx');
        parent.$.fancybox.close();
    }

    else if (type == 14) {
        parent.parent.parent.$("#frm2").attr('src', '../../Admin/BankAccountDetails.aspx');
        parent.$.fancybox.close();
    }
    else if (type == 15) {
        parent.parent.parent.$("#frm2").attr('src', '../../appointment/demos/StaffLeave.aspx');
        parent.$.fancybox.close();
    }
    

}


// parent.$.fancybox.update();



function fillBranchValidation(ctlControlName, selectedid, path) {
    var usercategory = $.dough("_CategoryId");
    var userid = $.dough("_UserId");

    var strUrl = path + 'XML/BranchList.json';
    $.ajax({ url: strUrl, type: "GET", data: "json",
        success: function (data) {
            document.getElementById(ctlControlName).length = 0;
            if (data == '') return false;
            eval(data);

            if (_objBranchJSON) {
                var arr = _objBranchJSON[0].split(',');
                var $ddlBranch = $("#" + ctlControlName);

                if (usercategory == 6 && typeof isBranchFirstColumnBlank == 'undefined') $('<option></option').attr('value', 0).text('--select Branch--').appendTo($ddlBranch);
                for (var i = 0; i < arr.length; i++) {
                    var arr1 = arr[i].split('|');
                    if (usercategory == '6') {
                        $('<option></option').attr('value', arr1[0]).text(arr1[2]).appendTo($ddlBranch);
                    } else if (usercategory == '2') {
                        if (userid == arr1[0]) {
                            $('<option></option').attr('value', arr1[0]).text(arr1[2]).appendTo($ddlBranch);

                        }
                    }
                }

            }

            if (selectedid != 0) $ddlBranch.val(selectedid);
            if (typeof isMultySelect != 'undefined') {
                $("#" + ctlControlName).multipleSelect();
                $("#" + ctlControlName).multipleSelect("uncheckAll");
            }

        }
    });

}
