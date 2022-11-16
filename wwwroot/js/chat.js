"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

connection.on("ReceiveMessage", () => {
	if (window.location === "https://localhost:7257/Kitchen")
		window.location.reload();
});

connection.start().then(function () { }).catch(function (err) {
	return console.error(err.toString());
});