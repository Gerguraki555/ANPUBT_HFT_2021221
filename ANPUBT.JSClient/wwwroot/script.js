let employees = [];
let connection = null;
getData();
SetUpSignalR();

let employeeIdToupdate = -1;

async function getData() {
    await fetch('http://localhost:31877/employee')
        .then(x => x.json())
        .then(y => {
            employees = y;
          //  console.log(employees)
            display();
        });
}

function display() {

    document.getElementById('resultarea').innerHTML = "";

    employees.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.employeeId + "</td><td>" + t.name + "</td><td>" + t.salary + " Ft<td>" + `<button type="button" onclick="Remove(${t.employeeId})">Delete</button>`
            + `<button type="button" onclick="Showupdate(${t.employeeId})">Update</button>`+ "</td></tr>";
        console.log(t.name);
    });

}

function SetUpSignalR(){

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

    document.getElementById('employeenametoupdate').value = employees.find(t => t['employeeId'] == id)['name'];
    document.getElementById('restaurantidtoupdate').value = employees.find(t => t['employeeId'] == id)['restaurantId'];
    document.getElementById('salarytoupdate').value = employees.find(t => t['employeeId'] == id)['salary'];

    employeeIdToupdate = id;
}

function Update() {
    let Name = document.getElementById('employeenametoupdate').value;
    let salary = document.getElementById('salarytoupdate').value;
    let restaurantId = document.getElementById('restaurantidtoupdate').value;
    fetch('http://localhost:31877/employee', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: Name,
                employeeId: employeeIdToupdate,
                salary: salary,
                restaurantId: restaurantId
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
    fetch('http://localhost:31877/employee/' + id, {
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
    let Name = document.getElementById('employeename').value;
    let salary = document.getElementById('salary').value;
    let restaurantId = document.getElementById('restaurantid').value;
    fetch('http://localhost:31877/employee', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: Name,
                salary: salary,
                restaurantId: restaurantId
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