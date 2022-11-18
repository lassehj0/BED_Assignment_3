"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();

connection.on("ReceiveMessage", () => {
	if (window.location == "https://localhost:7257/Kitchen")
		window.location.reload();
});

connection.start().then(console.log("Connection started")).catch((err) => {
	return console.error(err.toString());
});

function subb() {
	connection.invoke('Send');
}
