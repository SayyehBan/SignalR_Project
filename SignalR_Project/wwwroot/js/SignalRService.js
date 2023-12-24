var chatBox = $("#ChatBox");
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();

// نمایش چت باکس برای کاربر
function showChatDialog() {
    chatBox.css("display", "block");
}

function Init() {
    setTimeout(showChatDialog, 1000);
}
///ارسال پیام به سرور
function sendMessage(text) {
    connection.invoke('SendNewMessage', "بازدید کننده", text);
}
///دریافت پیام از سرور
connection.on("getNewMessage", getMessage);
function getMessage(sender, message, time) {

    $("#Messages").append("<li><div><span class='name'>" + sender + "</span><span class='time'>" + time +"</span></div><div class='message'>" + message +"</div></li>");
}
$(document).ready(function () {
    Init();
    // ایجاد وقفه برای ارسال پیام و جلوگیری از رفرش صفحه
    $("#NewMessageForm").submit(function (e) {
        e.preventDefault();  // جلوگیری از رفرش فرم
        var message = $("#MessageInput").val();
        sendMessage(message);
        $("#MessageInput").val('');  // پاک کردن محتوای ورودی
    });
});
