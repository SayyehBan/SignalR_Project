
//اتصال با هاب کانکشن پشتیبانی
var supportConnection = new signalR.HubConnectionBuilder()
    .withUrl('/supporthub')
    .build();



function Init() {
    supportConnection.start();

};

// بعد از این که صفحه کامل بارگذاری شد  این بخش اجرا می شود
$(document).ready(function () {
    console.log("ready!");
    //متد راه اندازی اجرا می شود
    Init();
});


//دریافت لیست  چت روم ها
supportConnection.on('GetRooms', loadRooms);

function loadRooms(rooms) {
    if (!rooms) return;
    var roomIds = Object.keys(rooms);
    if (!roomIds.length) return;


    removeAllChildren(roomListEl);

    roomIds.forEach(function (id) {
        var roomInfo = rooms[id];
        if (!roomInfo) return;

        //ایجاد دکمه برای لیست
        return $("#roomList").append("<a class='list-group-item list-group-item-action d-flex justify-content-between align-items-center' data-id='" + id + "' href='#'>" + roomInfo + "</a>");

    });

}

var roomListEl = document.getElementById('roomList');


function removeAllChildren(node) {
    if (!node) return;

    while (node.lastChild) {
        node.removeChild(node.lastChild);
    }
}