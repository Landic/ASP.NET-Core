$(document).ready(function () {
    const $chatSection = $('#chatSection');
    const $loginSection = $('#loginSection');
    const $usernameInput = $('#usernameInput');
    const $loginButton = $('#loginButton');
    const $messageContainer = $('#messageContainer');
    const $userList = $('#userList');
    const $messageInput = $('#messageInput');
    const $sendMessageButton = $('#sendMessageButton');
    const $userIdHidden = $('#userIdHidden');
    const $usernameHidden = $('#usernameHidden');
    const $chatHeader = $('#chatHeader');

    const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

    hubConnection.on("AddMessage", function (name, message) {
        $messageContainer.append(`<p><b>${htmlEncode(name)}</b>: <i>${htmlEncode(message)}</i></p>`);
    });

    hubConnection.on("Connected", function (id, userName, allUsers, messagesData) {
        $loginSection.hide();
        $chatSection.show().removeClass('d-none');
        $userIdHidden.val(id);
        $usernameHidden.val(userName);
        $chatHeader.html(`<h3>Добро пожаловать, ${userName}</h3>`);

        allUsers.forEach(user => addUser(user.connectionId, user.name));
        messagesData.forEach(msg => $messageContainer.append(`<p><b>${htmlEncode(msg.UserName)}</b>: <i>${htmlEncode(msg.Content)}</i></p>`));
    });

    hubConnection.on("NewUserConnected", addUser);

    hubConnection.on("UserDisconnected", function (id) {
        $(`#${id}`).remove();
    });

    hubConnection.start().then(function () {
        $sendMessageButton.click(function () {
            hubConnection.invoke("Send", $usernameHidden.val(), $messageInput.val()).catch(function (err) {
                return console.error(err.toString());
            });
            $messageInput.val('');
        });

        $loginButton.click(function () {
            const name = $usernameInput.val();
            if (name.length > 0) {
                hubConnection.invoke("Connect", name).catch(function (err) {
                    return console.error(err.toString());
                });
            } else {
                alert("Введите имя!");
            }
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    function htmlEncode(value) {
        return $('<div />').text(value).html();
    }

    function addUser(id, name) {
        $userList.append(`<li id="${id}"><b>${name}</b></li>`);
    }
});
