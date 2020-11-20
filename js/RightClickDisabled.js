//var message = "";

//function clickIE4() {
//    if (event.button == 2) {
//        alert(message);
//        return false;
//    }
//}

//function clickNS4(e) {
//    if (document.layers || document.getElementById && !document.all) {
//        if (e.which == 2 || e.which == 3) {
//            alert(message);
//            return false;
//        }
//    }
//}

//if (document.layers) 
//{
//    document.captureEvents(Event.MOUSEDOWN);
//    document.onmousedown = clickNS4;
//}
//else if (document.all && !document.getElementById) 
//{
//    document.onmousedown = clickIE4;
//}

//document.oncontextmenu = new Function("return false");

//document.onkeypress = function (event) {
//    event = (event || window.event);
//    if (event.keyCode == 123) {
//        //alert('No F-12');
//        return false;
//    }
//}
//document.onmousedown = function (event) {
//    event = (event || window.event);
//    if (event.keyCode == 123) {
//        //alert('No F-keys');
//        return false;
//    }
//}
//document.onkeydown = function (event) {
//    event = (event || window.event);
//    if (event.keyCode == 123) {
//        //alert('No F-keys');
//        return false;
//    }
//}