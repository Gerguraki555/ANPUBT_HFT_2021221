let restaurants = [];
let connection = null;
getData();
SetUpSignalR();

let restaurantIdToupdate = -1;

async function getData() {
    await fetch('http://localhost:31877/restaurant')
        .then(x => x.json())
        .then(y => {
            restaurants = y;
          
            display();
        });
}

function display() {

    document.getElementById('resultarea').innerHTML = "";

    restaurants.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.restaurant_id + "</td><td>" + t.restaurantName + "</td><td>" + t.rating + " stars<td>" + `<button type="button" onclick="Remove(${t.restaurant_id})">Delete</button>`
            + `<button type="button" onclick="Showupdate(${t.restaurant_id})">Update</button>` + "</td></tr>";
        console.log(t.name);
    });

}

function SetUpSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:31877/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        ("RestaurantCreated", (user, message) => {
            getData();
        });

    connection.on
        ("RestaurantDeleted", (user, message) => {
            getData();
        });

    connection.on
        ("RestaurantUpdated", (user, message) => {
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

    document.getElementById('restaurantnametoupdate').value = restaurants.find(t => t['restaurant_id'] == id)['restaurantName'];
    document.getElementById('ratingtoupdate').value = restaurants.find(t => t['restaurant_id'] == id)['rating'];

    restaurantIdToupdate = id;
}

function Update() {
    
    let Name = document.getElementById('restaurantnametoupdate').value;
    let Rating = document.getElementById('ratingtoupdate').value;
    fetch('http://localhost:31877/restaurant', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                rating: Rating,
                restaurantname: Name,
                restaurant_id: restaurantIdToupdate
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
    fetch('http://localhost:31877/restaurant/' + id, {
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
    let Name = document.getElementById('restaurantname').value;
    let Rating = document.getElementById('rating').value;
    fetch('http://localhost:31877/restaurant', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                rating: Rating,
                restaurantname: Name,
               
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