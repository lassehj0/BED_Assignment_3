var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

connection.on("update", function () {

    document.window.location.reload();
});

connection.start().then(function () {
}).catch(function (err) {
    console.error(err.toString());
});