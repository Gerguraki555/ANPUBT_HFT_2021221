let guests = [];
let connection = null;
getData();
SetUpSignalR();

let guestIdToupdate = -1;

async function getData() {
    await fetch('http://localhost:31877/guest')
        .then(x => x.json())
        .then(y => {
            guests = y;
            display();
        });
}

function display() {

    document.getElementById('resultarea').innerHTML = "";

    guests.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.guestId + "</td><td>" + t.name + "</td><td>" + t.email + "</td><td>" + t.number + "<td>" + t.orderId + "<td>" + `<button type="button" onclick="Remove(${t.guestId})">Delete</button>`
            + `<button type="button" onclick="Showupdate(${t.guestId})">Update</button>` + "</td></tr>";
        console.log(t.name);
    });

}

function SetUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:31877/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        ("EmployeeCreated", (user, message) => {
            getData();
        });

    connection.on
        ("EmployeeDeleted", (user, message) => {
            getData();
        });

    connection.on
        ("EmployeeUpdated", (user, message) => {
            getData();
        });

    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function Showupdate(id) {


    document.getElementById('updateformdiv').style.display = 'flex';

    document.getElementById('guestnametoupdate').value = guests.find(t => t['guestId'] == id)['name'];
    document.getElementById('emailtoupdate').value = guests.find(t => t['guestId'] == id)['email'];
    document.getElementById('phonenumbertoupdate').value = guests.find(t => t['guestId'] == id)['number'];
    document.getElementById('orderidtoupdate').value = guests.find(t => t['guestId'] == id)['orderId'];

    guestIdToupdate = id;
}

function Update() {
    let Name = document.getElementById('guestnametoupdate').value;
    let Email = document.getElementById('emailtoupdate').value;
    let Number = document.getElementById('phonenumbertoupdate').value;
    let Orderid = document.getElementById('orderidtoupdate').value;
    fetch('http://localhost:31877/guest', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: Name,
                email: Email,
                number: Number,
                orderId: Orderid,
                guestId: guestIdToupdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

function Remove(id) {
    fetch('http://localhost:31877/guest/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let Name = document.getElementById('guestname').value;
    let Email = document.getElementById('email').value;
    let Number = document.getElementById('phonenumber').value;
    let Orderid = document.getElementById('orderid').value;
    fetch('http://localhost:31877/guest', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: Name,
                email: Email,
                number: Number,
                orderId: Orderid,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}