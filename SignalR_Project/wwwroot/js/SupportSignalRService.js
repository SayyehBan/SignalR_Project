var activeRoomId = ''; // برای نگهداری چت روم فعال برای پشتیبان


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


supportConnection.on('getNewMessage', addMessages);

function addMessages(messages) {
    if (!messages) return;
    messages.forEach(function (m) {
        showMessage(m.sender, m.message, m.dateTime);
    });
}

function showMessage(sender, message, dateTime) {
    $("#chatMessage").append('<li><div><span class="name"> ' + sender + ' </span><span class="time">' + dateTime + '</span></div><div class="message"> ' + message + ' </div></li>');
}




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
        return $("#roomList").append("<a class='list-group-item list-group-item-action d-flex justify-content-between align-items-center' data-id='" + roomInfo + "' href='#'>" + roomInfo + "</a>");

    });

}

var roomListEl = document.getElementById('roomList');
var roomMessagesEl = document.getElementById('chatMessage');


function removeAllChildren(node) {
    if (!node) return;

    while (node.lastChild) {
        node.removeChild(node.lastChild);
    }
}

function setActiveRoomButton(el) {
    var allButtons = roomListEl.querySelectorAll('a.list-group-item');

    allButtons.forEach(function (btn) {
        btn.classList.remove('active');
    });
    el.classList.add('active');
}


function switchActiveRoomTo(id) {
    if (id === activeRoomId) return;

    removeAllChildren(roomMessagesEl);
    activeRoomId = id;

    supportConnection.invoke('LoadMessage', activeRoomId);
}



roomListEl.addEventListener('click', function (e) {
    roomMessagesEl.style.display = 'block';
    setActiveRoomButton(e.target);
    var roomId = e.target.getAttribute('data-id');
    switchActiveRoomTo(roomId);
});