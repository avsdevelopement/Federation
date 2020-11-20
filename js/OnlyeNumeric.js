

$(document).ready(function () {



    $(".number").bind("keypress", function (e) {
        var specialKeys1 = new Array();
        specialKeys1.push(8); //Backspace
       // specialKeys1.push(46); //dot

        var keyCode = e.which ? e.which : e.keyCode
        //alert(keyCode);
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys1.indexOf(keyCode) != -1);
        return ret;
    });

    $(".number").bind("blur", function (e) { if ($(this).val() == '') { $(this).val(''); return false; } });

    $(".number").bind("paste", function (e) { return false; });

    $(".number").bind("drop", function (e) { return false; });

    $('input').bind('keypress', function (e) {
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        specialKeys.push(46); //dot
        //specialKeys.push(32); //dot

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        //alert(specialKeys.indexOf(e.keyCode) + ' - ' + e.charCode + ' - ' + e.keyCode);
        var ret = ( (keyCode==32) ||  (keyCode >= 40 && keyCode <= 47) || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 60 && keyCode <= 95) || (keyCode >= 97 && keyCode <= 125) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
        return ret;
    });

    $('.email').blur(function () {
        emailValidation();

    });
});

function specialchar() {
    $('input').bind('keypress', function (e) {
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        specialKeys.push(46); //dot

        var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
        //alert(specialKeys.indexOf(e.keyCode) + ' - ' + e.charCode + ' - ' + e.keyCode);
        var ret = ((keyCode >= 40 && keyCode <= 46) || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 60 && keyCode <= 95) || (keyCode >= 97 && keyCode <= 125) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
        return ret;
    });
}

function number() {

    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    specialKeys.push(46); //dot
    $(".number").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        //alert(keyCode);
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        //$(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".number").bind("paste", function (e) {
        return false;
    });
    $(".number").bind("blur", function (e) {
        if ($(this).val() == '') { $(this).val('0'); return false; }
    });
    $(".number").bind("drop", function (e) {
        return false;
    });

    $('.email').blur(function () {
        emailValidation();

    });
}

function _emailValidation() {
    $(".email").bind("blur", function (e) {
        emailValidation();
    });

}

function emailValidation() {
    var sEmail = $('.email').val();
    var Email = $('.email');
    // Checking Empty Fields
    Email.css('border', '1px solid #cccccc');
    if (validateEmail(sEmail) == false) {
        //alert('Invalid Email Address');
        Email.css('border', '1px solid red');
        Email.focus();
    }
    else {
        Email.css('border', '1px solid #cccccc');
    }
}


// Function that validates email address through a regular expression.
function validateEmail(sEmail) {
    var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}



function IsAlphaNumeric(e) {
    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    specialKeys.push(9); //Tab
    specialKeys.push(46); //Delete
    specialKeys.push(36); //Home
    specialKeys.push(35); //End
    specialKeys.push(37); //Left
    specialKeys.push(39); //Right
    specialKeys.push(46); //dot


    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 40 && keyCode <= 46) || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 60 && keyCode <= 95) || (keyCode >= 97 && keyCode <= 125) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    return ret;
}
function dateLen(d) {
    var d1 = d +'';
    d1 = (d1.length == 1) ? '0' + d : d;
    return d1;
}

