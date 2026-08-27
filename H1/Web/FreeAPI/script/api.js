function testAPI() {
    fetch("https://jsonplaceholder.typicode.com/todos/1")
        .then(response => response.json())
        .then(data => {
            console.log(data);
            // Update the HTML with the data from the API response
            document.getElementById("id").innerHTML = "ID: " + data.id;
            document.getElementById("userId").innerHTML = "User ID: " + data.userId;
            document.getElementById("title").innerHTML = "Title: " + data.title;
            document.getElementById("completed").innerHTML = "Completed: " + data.completed;
        })
        .catch(error => console.error("Error:", error));
}
