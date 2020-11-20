var myCalendar;
function ShowCalender() {

    myCalendar = new dhtmlXCalendarObject(["calendar1", "calendar2", "calendar3"]);
    myCalendar.setSkin('omega');
    myCalendar.setDateFormat("%d-%m-%Y");
    myCalendar.hideTime();


}

function ShowCalender(strFieldName1, strFieldName2) {

    myCalendar = new dhtmlXCalendarObject([strFieldName1, strFieldName2]);
    myCalendar.setSkin('omega');
    myCalendar.setDateFormat("%d-%m-%Y");
    myCalendar.hideTime();


}

//to set date
// myCalendar.setDate('26-11-2011');
//to get date   
//var currentDate = myCalendar.getDate();
//all dates starting from June 08, 2011 will be dimmed. Dates until June 08, 2011 will be active.
// setInsensitiveRange("2011-06-08",null);
//all dates starting from June 08, 2011 will be active. Dates until July 08, 2011 will be dimmed.
// setSensitiveRange("2011-06-08",null);
//June 10, 2011, June 17, 2011, June 18, 2011 will be dimmed. All other dates will be active.
//  setInsensitiveDays((["2011-06-10",new Date(2011,5,17),"2011-06-18"]));
//mondays, tuesdays and thursdays of each week in the calendar will be dimmed. All other dates will be active.
//  disableDays("week", [1,2,4]);
//to show calendar
//  myCalendar.show();
//to hide calendar  
//myCalendar.hide();
//myCalendar.setHolidays('2011-09-25');

//week starts from Sunday
//myCalendar.setWeekStartDay(7);


//myCalendar.attachEvent("onClick", function (d) {
//    writeLog("onClick event called, date " + myCalendar.getFormatedDate(null, d));
//});